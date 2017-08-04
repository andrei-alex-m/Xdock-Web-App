using booking_mockUp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using X_Doc_v0._8;
namespace xDock_scanners_app
{
    public partial class _Default : System.Web.UI.Page
    {
        private static int numar_paleti_scanati_pe_comanda = 0;
        private static List<string> lista_id_paleti = new List<string>();

        public static DateTime actiune_start = default(DateTime);

        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Xdock"].ConnectionString);

        private void adaugaPaleti()
        {
            int num;
            this.adaugare_paleti(this.lst_actiune_paleti.SelectedIndex, this.txt_id_cursa.Text, this.lst_paleti_scanati.Items.Count);
            if (this.lst_actiune_paleti.SelectedIndex != 0)
            {
                TextBox txtNumarPaletiDescarcati = this.txt_numar_paleti_descarcati;
                num = Convert.ToInt32(this.txt_numar_paleti_descarcati.Text) + this.lst_paleti_scanati.Items.Count;
                txtNumarPaletiDescarcati.Text = num.ToString();
            }
            else
            {
                TextBox txtNumarPaletiIncarcati = this.txt_numar_paleti_incarcati;
                num = Convert.ToInt32(this.txt_numar_paleti_incarcati.Text) + this.lst_paleti_scanati.Items.Count;
                txtNumarPaletiIncarcati.Text = num.ToString();
            }
        }

        private void adaugare_paleti(int actiune, string id, int numar_paleti)
        {
            string camp = (this.lst_actiune_paleti.SelectedIndex == 0 ? "numberLoadPallets" : "numberUnloadPallets");
            object[] numarPaleti = new object[] { "update trips set ", camp, " = ", camp, " + ", numar_paleti, " where tripId = '", id, "'" };
            SqlCommand comm = new SqlCommand(string.Concat(numarPaleti), this.conn);
            try
            {
                try
                {
                    this.conn.Open();
                    comm.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    this.lbl_status.Text = exception.Message;
                }
            }
            finally
            {
                this.conn.Close();
            }
        }

        private void adaugaStatusuriSpeciale()
        {
            if (this.lst_actiune_paleti.SelectedIndex == 0)
            {
                this.lst_status_special_comanada.Items.Add(new ListItem("In regula"));
                this.lst_status_special_comanada.Items.Add(new ListItem("In plus incarcare"));
                this.lst_status_special_comanada.Items.Add(new ListItem("In minus incarcare"));
                this.lst_status_special_comanada.Items.Add(new ListItem("Deteriorate incarcare"));
            }
            else if (this.lst_actiune_paleti.SelectedIndex == 1)
            {
                this.lst_status_special_comanada.Items.Add(new ListItem("In regula"));
                this.lst_status_special_comanada.Items.Add(new ListItem("In plus descarcare"));
                this.lst_status_special_comanada.Items.Add(new ListItem("In minus descarcare"));
                this.lst_status_special_comanada.Items.Add(new ListItem("Deteriorate descarcare"));
            }
            this.lst_status_special_comanada.SelectedIndex = 0;
        }

        private void adaugaStatusuriSpecialeDinOrd(string tip)
        {
            this.lst_status_special_comanada.Items.Clear();
            try
            {
                try
                {
                    SqlCommand comm = new SqlCommand()
                    {
                        Connection = this.conn,
                        CommandText = string.Concat("use COMTECdefault select realisationKindName, realisationKindCode from realisationKind  where realisationKindCode like '%", tip, "%'")
                    };
                    this.conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        string realisationKindName = reader[0] as string;
                        string realisationKindCode = reader[1] as string;
                        this.lst_status_special_comanada.Items.Add(new ListItem(realisationKindName, realisationKindCode));
                    }
                }
                catch (Exception exception)
                {
                    this.lst_status_special_comanada.Items.Clear();
                    this.lst_status_special_comanada.Items.Add(new ListItem("In regula"));
                    this.lst_status_special_comanada.Items.Add(new ListItem("In plus incarcare"));
                    this.lst_status_special_comanada.Items.Add(new ListItem("In minus incarcare"));
                    this.lst_status_special_comanada.Items.Add(new ListItem("Deteriorate incarcare"));
                    this.lst_status_special_comanada.Items.Add(new ListItem("In regula"));
                    this.lst_status_special_comanada.Items.Add(new ListItem("In plus descarcare"));
                    this.lst_status_special_comanada.Items.Add(new ListItem("In minus descarcare"));
                    this.lst_status_special_comanada.Items.Add(new ListItem("Deteriorate descarcare"));
                }
            }
            finally
            {
                this.conn.Close();
            }
            this.lst_status_special_comanada.SelectedIndex = 0;
        }

        protected void adiacent(object sender, EventArgs e)
        {
            this.curata_tot();
        }

        protected void btn_palet_Click(object sender, EventArgs e)
        {
            if ((this.lst_lista_comenzi.SelectedIndex != -1 ? false : !this.Page.IsValid))
            {
                this.lbl_status.Text = "O comanda trebuie selectata!";
            }
            else if (!_Default.lista_id_paleti.Contains(this.return_fara_plus(this.txt_id_palet.Text)))
            {
                this.lbl_paleti_info.Text = "Acest palet nu apartine comenzii!";
                this.txt_id_palet.Text = string.Empty;
                this.txt_id_palet.Focus();
            }
            else if (this.lst_paleti_scanati.Items.Contains(new ListItem(this.return_fara_plus(this.txt_id_palet.Text))))
            {
                this.lbl_paleti_info.Text = "Acest palet a fost deja scanat!";
                this.txt_id_palet.Text = string.Empty;
                this.txt_id_palet.Focus();
            }
            else
            {
                this.lst_paleti_scanati.Items.Add(this.return_fara_plus(this.txt_id_palet.Text));
                this.lst_lista_paleti.Items.Remove(new ListItem(this.return_fara_plus(this.txt_id_palet.Text)));
                this.txt_id_palet.Text = string.Empty;
                this.lbl_paleti_info.Text = string.Concat(this.txt_id_palet.Text, " adaugat!");
                this.txt_id_palet.Focus();
            }
            if (_Default.numar_paleti_scanati_pe_comanda == this.lst_paleti_scanati.Items.Count)
            {
                this.btn_salveaaza_status_comanda.Focus();
            }
        }

        protected void btn_refresh_Click(object sender, EventArgs e)
        {
            this.incarca_detalii_cursa(this, EventArgs.Empty);
        }

        protected void btn_salveaaza_status_comanda_Click(object sender, EventArgs e)
        {
            if (this.lst_paleti_scanati.Items.Count != _Default.numar_paleti_scanati_pe_comanda)
            {
                this.lbl_status.Text = "Numarul paletilor scanati nu coincide cu numarul paletilor aferenti comenzii";
            }
            else if (!this.schimba_status_comanda(false))
            {
                this.lbl_status.Text = "Eroare schimbare status!";
            }
            else
            {
                this.genereazaXML();
                this.adaugaPaleti();
                this.incarca_lista_comenzi(this, EventArgs.Empty);
            }
            if (this.lst_lista_comenzi.Items.Count != 0)
            {
                this.lst_lista_comenzi.Focus();
            }
            else
            {
                this.cursaIncheiata();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.incarca_detalii_cursa(this, EventArgs.Empty);
            this.adiacent(this, EventArgs.Empty);
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TextBox3.Text))
            {
                this.lbl_sigiliu.Text = "Camp gol!";
            }
            else
            {
                SqlCommand comm = new SqlCommand("update trips set truckSealNumber = @sig where tripId = @tripid", this.conn);
                comm.Parameters.AddWithValue("@sig", this.TextBox3.Text);
                comm.Parameters.AddWithValue("@tripid", this.txt_id_cursa.Text);
                try
                {
                    try
                    {
                        this.conn.Open();
                        if (comm.ExecuteNonQuery() <= 0)
                        {
                            this.lbl_sigiliu.Text = "Eroare adaugare sigiliu!";
                        }
                        else
                        {
                            this.lbl_sigiliu.Text = "Cod sigiliu salvat!";
                        }
                        this.txt_sigiliu_camion.Text = string.Empty;
                    }
                    catch (Exception exception)
                    {
                        Exception ex = exception;
                        this.lbl_sigiliu.Text = string.Concat("Eroare: ", ex.Message);
                    }
                }
                finally
                {
                    this.conn.Close();
                }
                string str = string.Concat("Asignare sigiliu camion din Xdock scann ", this.Session["utilizator"].ToString());
                (new Task(() => scrie_modificari_utilizator.scrie_modificarea(str, this.txt_id_cursa.Text))).Start();
                this.incarca_detalii_cursa(this, EventArgs.Empty);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int rampaI;
            int rampaD;
            try
            {
                try
                {
                    if (int.TryParse(this.TextBox1.Text, out rampaI))
                    {
                        this.updateRampi(rampaI, "rampNumberForLoad");
                    }
                    if (int.TryParse(this.TextBox2.Text, out rampaD))
                    {
                        this.updateRampi(rampaD, "rampNumberForUnload");
                    }
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    this.lbl_rampi_schimbate.Text = string.Concat("A aparut o problema! Not a number", ex.Message);
                }
            }
            finally
            {
                this.conn.Close();
            }
            string str = string.Concat("Asignare rampi din Xdock scann ", this.Session["utilizator"].ToString());
            (new Task(() => scrie_modificari_utilizator.scrie_modificarea(str, this.txt_id_cursa.Text))).Start();
        }

        private void curata_tot()
        {
            this.txt_observatii_speciale.Text = string.Empty;
            this.lst_status_special_comanada.SelectedIndex = 0;
            this.lbl_rampi_schimbate.Text = string.Empty;
            this.TextBox3.Text = string.Empty;
            this.lbl_sigiliu.Text = string.Empty;
            this.txt_id_palet.Text = string.Empty;
            this.lbl_status.Text = "informatii";
            this.lbl_status.Text = string.Empty;
            this.lst_actiune_paleti.SelectedIndex = -1;
            this.lst_lista_comenzi.Items.Clear();
            this.lst_paleti_scanati.Items.Clear();
        }

        private void cursaIncheiata()
        {
            if (this.lst_actiune_paleti.SelectedIndex != 0)
            {
                this.lbl_status.Text = "Cursa a fost descarcata cu succes!";
            }
            else
            {
                this.lbl_status.Text = "Cursa a fost incarcata cu succes!";
            }
            this.txt_id_cursa.Text = string.Empty;
            this.lst_paleti_scanati.Items.Clear();
            this.lst_actiune_paleti.Items.Clear();
        }

        private Tuple<string, string> gaseste_actionID(string id, bool retur)
        {
            string query = string.Empty;
            string stopId = string.Empty;
            string actionId = string.Empty;
            query = (retur ? "use Xdock\r\n                        declare @actionID nvarchar(20)\r\n                        select @actionID = deliverActionId from orders where orderID = @id\r\n                        use COMTECdefault\r\n                        Select id_externalAction as stopid, action_externalId as actionid from  externalAction where id_action = @actionID" : "use Xdock\r\n                            declare @actionID nvarchar(20)  \r\n                            select @actionID = actionID from orders where orderID = @id\r\n                            use COMTECdefault\r\n                            Select id_externalAction as stopid, action_externalId as actionid from  externalAction where id_action = @actionID ");
            SqlCommand comm = new SqlCommand(query, this.conn);
            comm.Parameters.AddWithValue("@id", id);
            try
            {
                try
                {
                    this.conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        int? item = (int?)(reader[0] as int?);
                        stopId = ((item.HasValue ? item.GetValueOrDefault() : 0)).ToString();
                        actionId = reader[1] as string;
                    }
                }
                catch (Exception exception)
                {
                    Tuple<string, string> tuple = new Tuple<string, string>(null, null);
                }
            }
            finally
            {
                this.conn.Close();
            }
            return new Tuple<string, string>(stopId, actionId);
        }

        private void genereazaXML()
        {
            Tuple<string, string> tupl;
            string dateTimeGood = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss+02:00");
            switch (this.lst_actiune_paleti.SelectedIndex)
            {
                case 0:
                    {
                        tupl = this.gaseste_actionID(this.lst_lista_comenzi.SelectedItem.Text, false);
                        List<string> strs = new List<string>()
                    {
                        string.Concat("exI_", this.txt_id_cursa.Text, "_stop_finish"),
                        tupl.Item1,
                        "finished",
                        dateTimeGood,
                        tupl.Item2,
                        this.lst_status_special_comanada.SelectedValue.ToString()
                    };
                        export_xml.scris_status_finish(strs, "finishtime");
                        break;
                    }
                case 1:
                    {
                        tupl = this.gaseste_actionID(this.lst_lista_comenzi.SelectedItem.Text, true);
                        List<string> strs1 = new List<string>()
                    {
                        string.Concat("exD_", this.txt_id_cursa.Text, "_stop_finish"),
                        tupl.Item1,
                        "finished",
                        dateTimeGood,
                        tupl.Item2,
                        this.lst_status_special_comanada.SelectedValue.ToString()
                    };
                        export_xml.scris_status_finish(strs1, "finishtime");
                        break;
                    }
            }
            string str = string.Concat("Schimbare status comanda din xdock scann ", this.Session["utilizator"].ToString());
            (new Task(() => scrie_modificari_utilizator.scrie_modificari_comenzi(str, this.lst_lista_comenzi.SelectedItem.ToString()))).Start();
        }

        private void genereazaXMLStart()
        {
            if (this.lst_lista_comenzi.SelectedIndex != -1)
            {
                string dateTimeGood = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss+02:00");
                Tuple<string, string> tupl = null;
                switch (this.lst_actiune_paleti.SelectedIndex)
                {
                    case 0:
                        {
                            tupl = this.gaseste_actionID(this.lst_lista_comenzi.SelectedItem.Text, false);
                            List<string> strs = new List<string>()
                        {
                            string.Concat("exI_", this.txt_id_cursa.Text, "_start_finish"),
                            tupl.Item1,
                            "finished",
                            dateTimeGood,
                            tupl.Item2,
                            this.lst_status_special_comanada.SelectedValue.ToString()
                        };
                            export_xml.scris_status_finish(strs, "starttime");
                            break;
                        }
                    case 1:
                        {
                            tupl = this.gaseste_actionID(this.lst_lista_comenzi.SelectedItem.Text, true);
                            List<string> strs1 = new List<string>()
                        {
                            string.Concat("exD_", this.txt_id_cursa.Text, "_start_finish"),
                            tupl.Item1,
                            "finished",
                            dateTimeGood,
                            tupl.Item2,
                            this.lst_status_special_comanada.SelectedValue.ToString()
                        };
                            export_xml.scris_status_finish(strs1, "starttime");
                            break;
                        }
                }
            }
        }

        public void incarca_detalii_cursa(object sender, EventArgs e)
        {
            this.lst_lista_paleti.Items.Clear();
            this.lst_paleti_scanati.Items.Clear();
            List<string> detalii = incarcare_detalii.detalii_curse(this.return_fara_plus(this.txt_id_cursa.Text));
            this.txt_sigiliu_camion.Text = detalii[0];
            this.TextBox1.Text = (detalii[2] == "0" ? string.Empty : detalii[2]);
            this.TextBox2.Text = (detalii[3] == "0" ? string.Empty : detalii[3]);
            this.txt_numar_paleti_total.Text = detalii[1];
            this.txt_numar_paleti_incarcati.Text = detalii[4];
            this.txt_numar_paleti_descarcati.Text = detalii[5];
            this.txt_id_cursa.Text = this.return_fara_plus(this.txt_id_cursa.Text);
            this.lst_actiune_paleti.Items.Add(new ListItem("incarcare", "numberLoadPallets"));
            this.lst_actiune_paleti.Items.Add(new ListItem("descarcare", "numberUnloadPallets"));
        }

        public void incarca_lista_comenzi(object sender, EventArgs e)
        {
            string status = string.Empty;
            status = (this.lst_actiune_paleti.SelectedIndex != 0 ? "unloading' or orderStatus = 'loaded" : "loading' or orderStatus = 'planned");
            this.lst_lista_comenzi.Items.Clear();
            this.lst_paleti_scanati.Items.Clear();
            this.txt_id_palet.Text = string.Empty;
            foreach (string s in incarcare_detalii.comezenzi_aferente_cursei(this.return_fara_plus(this.txt_id_cursa.Text), status))
            {
                this.lst_lista_comenzi.Items.Add(s);
            }
        }

        protected void lst_actiune_paleti_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lst_lista_paleti.Items.Clear();
            this.lst_paleti_scanati.Items.Clear();
            this.lbl_paleti_info.Text = string.Empty;
            this.lbl_status.Text = string.Empty;
            this.txt_id_palet.Text = string.Empty;
            this.txt_observatii_speciale.Text = string.Empty;
            this.lst_lista_comenzi.SelectedIndex = -1;
            if (this.lst_actiune_paleti.SelectedIndex == 0)
            {
                this.adaugaStatusuriSpecialeDinOrd("load");
            }
            else if (this.lst_actiune_paleti.SelectedIndex == 1)
            {
                this.adaugaStatusuriSpecialeDinOrd("deliver");
            }
            this.incarca_lista_comenzi(this, EventArgs.Empty);
        }

        protected void lst_lista_comenzi_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Default.lista_id_paleti.Clear();
            _Default.actiune_start = DateTime.Now;
            this.lst_lista_paleti.Items.Clear();
            this.lbl_status.Text = string.Empty;
            this.lbl_paleti_info.Text = string.Empty;
            this.lst_paleti_scanati.Items.Clear();
            this.lst_status_special_comanada.SelectedIndex = 0;
            this.txt_observatii_speciale.Text = string.Empty;
            this.genereazaXMLStart();
            Tuple<List<string>, List<string>> ab = incarcare_detalii.detalii_comenzi(this.lst_lista_comenzi.SelectedValue.ToString());
            foreach (string s in ab.Item1)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    this.lst_lista_paleti.Items.Add(s.Trim());
                    _Default.lista_id_paleti.Add(s);
                }
            }
            this.txt_locInc.Text = ab.Item2[0];
            this.txt_locDesc.Text = ab.Item2[1];
            _Default.numar_paleti_scanati_pe_comanda = this.lst_lista_paleti.Items.Count;
            this.schimba_status_comanda(true);
            this.txt_id_palet.Focus();
        }

        protected void lst_lista_paleti_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void lst_paleti_scanati_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void lst_status_comanda_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lst_status_special_comanada.SelectedIndex = 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.lst_status_special_comanada.SelectedIndex = 0;
                this.txt_id_cursa.Focus();
            }
        }

        public string return_fara_plus(string s)
        {
            s = s.Replace("+", string.Empty);
            return s.ToLower();
        }

        private bool schimba_status_comanda(bool actiuneSecundara)
        {
            bool succesSchimbatStatus = false;
            string actiune = string.Empty;
            SqlCommand comm = new SqlCommand("status_comanda", this.conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (actiuneSecundara)
            {
                actiune = (this.lst_actiune_paleti.SelectedIndex != 0 ? "unloading" : "loading");
                comm.Parameters.AddWithValue("@id", this.lst_lista_comenzi.SelectedValue);
                comm.Parameters.AddWithValue("@status", actiune);
                comm.Parameters.AddWithValue("@status_special", string.Empty);
                comm.Parameters.AddWithValue("@observatii_speciale", string.Empty);
            }
            else
            {
                comm.Parameters.AddWithValue("@id", this.lst_lista_comenzi.SelectedValue);
                comm.Parameters.AddWithValue("@status", this.status_eng());
                comm.Parameters.AddWithValue("@status_special", this.lst_status_special_comanada.SelectedItem.Text);
                comm.Parameters.AddWithValue("@observatii_speciale", this.txt_observatii_speciale.Text);
            }
            succesSchimbatStatus = this.Scrie_Modificari_Comanda(actiuneSecundara, succesSchimbatStatus, actiune, comm);
            return succesSchimbatStatus;
        }

        private bool schimbaStatusComanda(bool c, bool b, string actiune, SqlCommand comm)
        {
            try
            {
                try
                {
                    this.conn.Open();
                    if (comm.ExecuteNonQuery() > 0)
                    {
                        b = true;
                    }
                    if (c)
                    {
                        this.lbl_status.Text = string.Concat("Salvat statusul de ", actiune, " pentru ", this.lst_lista_comenzi.SelectedValue);
                    }
                    else
                    {
                        this.lbl_status.Text = string.Concat("Salvat statusul de ", this.status_eng(), " pentru ", this.lst_lista_comenzi.SelectedValue);
                    }
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    this.lbl_status.Text = string.Concat("A aparut o eroare: ", ex.Message);
                }
            }
            finally
            {
                this.conn.Close();
            }
            return b;
        }

        private bool Scrie_Modificari_Comanda(bool actiuneSecundara, bool succesSchimbatStatus, string actiune, SqlCommand comm)
        {
            bool flag;
            if (!actiuneSecundara)
            {
                if (!actiuneSecundara)
                {
                    succesSchimbatStatus = this.schimbaStatusComanda(actiuneSecundara, succesSchimbatStatus, actiune, comm);
                }
                if ((!succesSchimbatStatus ? false : !actiuneSecundara))
                {
                    (new Task(() => modificareStatusCursa.verifica(this.txt_id_cursa.Text, this.Session["utilizator"].ToString()))).Start();
                }
                flag = succesSchimbatStatus;
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        private string status(int p)
        {
            string str;
            switch (p)
            {
                case 0:
                    {
                        str = "started";
                        break;
                    }
                case 1:
                    {
                        str = "finished";
                        break;
                    }
                default:
                    {
                        str = "error";
                        break;
                    }
            }
            return str;
        }

        private string status_eng()
        {
            string sts = "";
            switch (this.lst_actiune_paleti.SelectedIndex)
            {
                case 0:
                    {
                        sts = "loaded";
                        break;
                    }
                case 1:
                    {
                        sts = "unloaded";
                        break;
                    }
            }
            return sts;
        }

        private void update_nr_paleti()
        {
            SqlCommand comm = new SqlCommand("select numberLoadPallets,  numberUnloadPallets from trips where tripId = @i", this.conn);
            comm.Parameters.AddWithValue("@i", this.txt_id_cursa.Text);
            try
            {
                try
                {
                    this.conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        TextBox txtNumarPaletiIncarcati = this.txt_numar_paleti_incarcati;
                        int? item = (int?)(reader[0] as int?);
                        txtNumarPaletiIncarcati.Text = ((item.HasValue ? item.GetValueOrDefault() : 0)).ToString();
                        TextBox txtNumarPaletiDescarcati = this.txt_numar_paleti_descarcati;
                        item = (int?)(reader[1] as int?);
                        txtNumarPaletiDescarcati.Text = ((item.HasValue ? item.GetValueOrDefault() : 0)).ToString();
                    }
                }
                catch (Exception exception)
                {
                }
            }
            finally
            {
                this.conn.Close();
            }
        }

        private void updateRampi(int rampa1, string rampa)
        {
            SqlCommand comm = new SqlCommand(string.Concat("update trips set ", rampa, " = @rampa  where tripId =   @id"), this.conn);
            comm.Parameters.AddWithValue("@rampa", rampa1);
            comm.Parameters.AddWithValue("@id", this.txt_id_cursa.Text);
            this.conn.Open();
            if (comm.ExecuteNonQuery() <= 0)
            {
                this.lbl_rampi_schimbate.Text = "Eroare la salvare rampi!";
            }
            else
            {
                this.lbl_rampi_schimbate.Text = "Salvat!";
            }
        }

        private bool verifare_rampi(out int irampa1, out int irampa2)
        {
            bool flag;
            int num = 0;
            int num1 = num;
            irampa2 = num;
            irampa1 = num1;
            string rampa1 = this.TextBox1.Text;
            string rampa2 = this.TextBox2.Text;
            if (!(string.IsNullOrEmpty(rampa1) ? false : !string.IsNullOrEmpty(rampa2)))
            {
                flag = false;
            }
            else if (int.TryParse(rampa1, out irampa1))
            {
                flag = (int.TryParse(rampa2, out irampa2) ? true : false);
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private void verifica_daca_toate_comenzile_terminate()
        {
            bool toate = true;
            StringBuilder str = new StringBuilder();
            str.Append("select orderStatus from orders where orderId in (");
            foreach (ListItem lst in this.lst_lista_comenzi.Items)
            {
                str.Append(string.Concat(lst.Text, ", "));
            }
            str = str.Remove(str.Length - 2, 2);
            str.Append(")");
            SqlCommand comm = new SqlCommand(str.ToString(), this.conn);
            try
            {
                try
                {
                    this.conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader[0].ToString() != "unloaded")
                        {
                            toate = false;
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.lbl_status.Text = exception.Message;
                }
            }
            finally
            {
                this.conn.Close();
            }
            if (toate)
            {
                this.lbl_status.Text = string.Concat("Cursa ", this.txt_id_cursa.Text, " a fost incheiata cu success, inserati o noua cursa!");
                this.txt_id_cursa.Text = string.Empty;
            }
        }
    }
}