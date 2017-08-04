using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Data;
using System.Text;

namespace booking_mockUp
{
    public partial class _Default : Page
    {
        List<string> detalii = new List<string>();

        List<string> rezervare = new List<string>();

        public static SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Xdock"].ConnectionString);

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!verificare_zi_libera())
                {
                    SqlCommand comm = null;
                    switch (DropDownList1.SelectedIndex)
                    {
                        case 0:
                            {
                                comm = new SqlCommand("insert_date", conn);
                                break;
                            }
                        case 1:
                            {
                                comm = new SqlCommand("insert_data_tureni", conn);
                                break;
                            }
                        case 2:
                            {
                                comm = new SqlCommand("insert_date_aricesti", conn);
                                break;
                            }
                        case 3:
                            {
                                comm = new SqlCommand("insert_date_nestle_ul", conn);
                                break;
                            }
                    }
                    comm.CommandType = CommandType.StoredProcedure;
                    SqlParameterCollection parameters = comm.Parameters;
                    DateTime selectedDate = Calendar1.SelectedDate;
                    parameters.AddWithValue("@date", selectedDate.ToString("yyyy-MM-dd"));
                    try
                    {
                        try
                        {
                            conn_open();
                            comm.ExecuteNonQuery();
                        }
                        catch (Exception exception)
                        {
                            Exception ex = exception;
                            Show(string.Concat("A aparut o eroare: ", ex.Message), this);
                            lbl_info.Text = "An error occurred!";
                            lbl_info.ForeColor = Color.Red;
                        }
                    }
                    finally
                    {
                        conn_close();
                    }
                }
                insereaza();
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (verifica_daca_exista_id(txt_id_stergere.Text))
            {
                sterge_rezervarea(txt_id_stergere.Text);
                int a = 0;
                SqlCommand comm = new SqlCommand("delete from trips where tripid = @txt", conn);
                comm.Parameters.AddWithValue("@txt", txt_id_stergere.Text);
                try
                {
                    try
                    {
                        conn_open();
                        a = comm.ExecuteNonQuery();
                        delete_from_orders(txt_id_stergere.Text);
                        txt_id_stergere.Text = string.Empty;
                    }
                    catch (Exception exception)
                    {
                        lbl_info.Text = exception.Message;
                        lbl_info.ForeColor = Color.Red;
                    }
                }
                finally
                {
                    conn_close();
                }
                switch (a)
                {
                    case 0:
                        {
                            lbl_stergere_cursa.Text = "The trip was not deleted, please check the id!";
                            break;
                        }
                    case 1:
                        {
                            Label lblStergereCursa = lbl_stergere_cursa;
                            string str = "The trip was deleted!";
                            string str1 = str;
                            lbl_info.Text = str;
                            lblStergereCursa.Text = str1;
                            break;
                        }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (div_curse_client.Visible)
            {
                div_curse_client.Visible = false;
                tabel_date.Visible = true;
            }
            else
            {
                div_curse_client.Visible = true;
                tabel_date.Visible = false;
                string query = string.Concat("select * from trips where specialObservations like '%", Session["utilizator"].ToString(), "%' and unloadDate >= @data1 and unloadDate <= @data2");
                SqlCommand comm = new SqlCommand(query, conn);
                SqlParameterCollection parameters = comm.Parameters;
                DateTime selectedDate = Calendar2.SelectedDate;
                parameters.AddWithValue("@data1", selectedDate.ToString("yyyy-MM-dd"));
                SqlParameterCollection sqlParameterCollection = comm.Parameters;
                selectedDate = Calendar3.SelectedDate;
                sqlParameterCollection.AddWithValue("@data2", selectedDate.ToString("yyyy-MM-dd"));
                try
                {
                    try
                    {
                        conn_open();
                        SqlDataReader rdr = comm.ExecuteReader();
                        grid_toate_cursele.DataSource = rdr;
                        grid_toate_cursele.DataBind();
                    }
                    catch (Exception exception)
                    {
                        lbl_info.Text = exception.Message;
                    }
                }
                finally
                {
                    conn_close();
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (dtl_contact.Visible)
            {
                dtl_contact.Visible = false;
            }
            else
            {
                dtl_contact.Visible = true;
                incarca_dtl_contact();
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(base.Request.RawUrl);
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            if (Calendar1.SelectedDate <= DateTime.Today)
            {
                lbl_info.Text = "You can't select a date earlier than today, or today!";
                lbl_info.ForeColor = Color.Red;
                verifica_zi();
            }
            else if (Calendar1.SelectedDate.DayOfWeek == DayOfWeek.Saturday)
            {
                lbl_info.Text = "You can't select a weekend day!";
                lbl_info.ForeColor = Color.Red;
                verifica_zi();
            }
            else if (Calendar1.SelectedDate.DayOfWeek != DayOfWeek.Sunday)
            {
                lbl_info.Text = "The trips must be saved!";
                lbl_info.ForeColor = Color.White;
                update_table();
            }
            else
            {
                lbl_info.Text = "You can't select a weekend day!";
                lbl_info.ForeColor = Color.Red;
                verifica_zi();
            }
        }

        private void ClearAllControlsRecursive(Control container)
        {
            foreach (object control in Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }
            }
        }

        protected string compune_ore(string aa, int cc)
        {
            aa = string.Concat("[", aa, "],");
            int bb = Convert.ToInt32(aa.Substring(1, 2));
            int i = 1;
            while (i < cc)
            {
                if (bb != 23)
                {
                    bb++;
                    aa = string.Concat(aa, "[", bb.ToString("00"), ":00],");
                    i++;
                }
                else
                {
                    break;
                }
            }
            aa = aa.Remove(aa.Length - 1);
            return aa;
        }

        private void conn_close()
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        private void conn_open()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        public bool creare_comanda(ref Dictionary<string, string> lst)
        {
            bool creat = false;
            SqlCommand comm = new SqlCommand("insert into orders(orderId, palletsId, deliverAddressID, tripId, orderStatus) values (@id, @idpaleti, @locD, @id, @sp)", conn);
            comm.Parameters.AddWithValue("@id", lst["id"]);
            comm.Parameters.AddWithValue("@locD", lst["locatie_oprire"]);
            comm.Parameters.AddWithValue("@idpaleti", string.Empty);
            comm.Parameters.AddWithValue("@sp", DropDownList5.SelectedValue.ToString());
            try
            {
                try
                {
                    conn_open();
                    if (comm.ExecuteNonQuery() > 0)
                    {
                        creat = true;
                    }
                }
                catch (Exception exception)
                {
                    lbl_info.Text = exception.Message;
                    lbl_info.ForeColor = Color.Red;
                }
            }
            finally
            {
                conn_close();
            }
            if (creat)
            {
                salveaza_id_paleti(lst["id"], Convert.ToInt32(lst["numar_paleti"]));
            }
            return creat;
        }

        private void curata()
        {
            foreach (Control c in Page.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = string.Empty;
                }
                else if (c is ListBox)
                {
                    ((ListBox)c).SelectedIndex = 0;
                }
            }
        }

        private void delete_from_orders(string p)
        {
            SqlCommand comm = new SqlCommand("delete from orders where tripid = @txt", conn);
            comm.Parameters.AddWithValue("@txt", p);
            try
            {
                try
                {
                    conn_open();
                    comm.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            finally
            {
                conn_close();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                DropDownList3.Items.Clear();
                DropDownList3.Items.Add(new ListItem("rampa 2", "2"));
                DropDownList3.Items.Add(new ListItem("rampa 3", "3"));
                DropDownList3.Items.Add(new ListItem("rampa 4", "4"));
                DropDownList3.Items.Add(new ListItem("rampa 5", "5"));
                DropDownList3.Items.Add(new ListItem("rampa 6", "6"));
                DropDownList3.Items.Add(new ListItem("rampa 7", "7"));
                DropDownList3.Items.Add(new ListItem("rampa 9", "9"));
                DropDownList3.Items.Add(new ListItem("rampa 10", "10"));
                DropDownList3.Items.Add(new ListItem("rampa 11", "11"));
                DropDownList3.Items.Add(new ListItem("rampa 15", "15"));
                update_table();
                divlegena.Visible = true;
            }
            else if (DropDownList1.SelectedIndex == 1)
            {
                DropDownList3.Items.Clear();
                DropDownList3.Items.Add(new ListItem("rampa 1", "1"));
                DropDownList3.Items.Add(new ListItem("rampa 2", "2"));
                DropDownList3.Items.Add(new ListItem("rampa 3", "3"));
                update_table();
                divlegena.Visible = false;
            }
            else if (DropDownList1.SelectedIndex == 2)
            {
                DropDownList3.Items.Clear();
                DropDownList3.Items.Add(new ListItem("rampa 1", "1"));
                DropDownList3.Items.Add(new ListItem("rampa 2", "2"));
                DropDownList3.Items.Add(new ListItem("rampa 3", "3"));
                DropDownList3.Items.Add(new ListItem("rampa 4", "4"));
                DropDownList3.Items.Add(new ListItem("rampa 5", "5"));
                DropDownList3.Items.Add(new ListItem("rampa 6", "6"));
                DropDownList3.Items.Add(new ListItem("rampa 7", "7"));
                DropDownList3.Items.Add(new ListItem("rampa 8", "8"));
                DropDownList3.Items.Add(new ListItem("rampa 9", "9"));
                DropDownList3.Items.Add(new ListItem("rampa 10", "10"));
                DropDownList3.Items.Add(new ListItem("rampa 11", "11"));
                DropDownList3.Items.Add(new ListItem("rampa 12", "12"));
                DropDownList3.Items.Add(new ListItem("rampa 13", "13"));
                DropDownList3.Items.Add(new ListItem("rampa 14", "14"));
                DropDownList3.Items.Add(new ListItem("rampa 15", "15"));
                DropDownList3.Items.Add(new ListItem("rampa 16", "16"));
                DropDownList3.Items.Add(new ListItem("rampa 17", "17"));
                DropDownList3.Items.Add(new ListItem("rampa 18", "18"));
                DropDownList3.Items.Add(new ListItem("rampa 19", "19"));
                DropDownList3.Items.Add(new ListItem("rampa 20", "20"));
                DropDownList3.Items.Add(new ListItem("rampa 21", "21"));
                DropDownList3.Items.Add(new ListItem("rampa 22", "22"));
                DropDownList3.Items.Add(new ListItem("rampa 23", "23"));
                DropDownList3.Items.Add(new ListItem("rampa 24", "24"));
                DropDownList3.Items.Add(new ListItem("rampa 25", "25"));
                DropDownList3.Items.Add(new ListItem("rampa 26", "26"));
                DropDownList3.Items.Add(new ListItem("rampa 27", "27"));
                DropDownList3.Items.Add(new ListItem("rampa 28", "28"));
                DropDownList3.Items.Add(new ListItem("rampa 29", "29"));
                DropDownList3.Items.Add(new ListItem("rampa 30", "30"));
                DropDownList3.Items.Add(new ListItem("rampa 31", "31"));
                DropDownList3.Items.Add(new ListItem("rampa 32", "32"));
                DropDownList3.Items.Add(new ListItem("rampa 33", "33"));
                DropDownList3.Items.Add(new ListItem("rampa 34", "34"));
                DropDownList3.Items.Add(new ListItem("rampa 35", "35"));
                update_table();
                divlegena.Visible = true;
            }
            else if (DropDownList1.SelectedIndex != 3)
            {
                DropDownList3.Items.Clear();
            }
            else
            {
                DropDownList3.Items.Clear();
                DropDownList3.Items.Add(new ListItem("rampa 1", "1"));
                DropDownList3.Items.Add(new ListItem("rampa 2", "2"));
                DropDownList3.Items.Add(new ListItem("rampa 3", "3"));
                DropDownList3.Items.Add(new ListItem("rampa 4", "4"));
                DropDownList3.Items.Add(new ListItem("rampa 5", "5"));
                DropDownList3.Items.Add(new ListItem("rampa 6", "6"));
                DropDownList3.Items.Add(new ListItem("rampa 7", "7"));
                update_table();
                divlegena.Visible = false;
            }
        }

        private string gaseste_id_identity(string id)
        {
            string ret = string.Empty;
            SqlCommand comm = new SqlCommand("select id from orders where orderId = @id", conn);
            comm.Parameters.AddWithValue("@id", id);
            try
            {
                try
                {
                    conn.Open();
                    ret = comm.ExecuteScalar().ToString();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    ret = id;
                }
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

        private string gaseste_informatii_rezervare(string p)
        {
            string str;
            SqlCommand comm = new SqlCommand("select rezervationsDetails from trips where tripId = @id", conn);
            comm.Parameters.AddWithValue("@id", p);
            try
            {
                try
                {
                    conn_open();
                    str = comm.ExecuteScalar().ToString();
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    lbl_info.Text = string.Concat(ex.Message, " st rez");
                    lbl_info.ForeColor = Color.Red;
                    str = null;
                }
            }
            finally
            {
                conn_close();
            }
            return str;
        }

        public string generare_id_paleti(string id, int numar_paleti)
        {
            StringBuilder str = new StringBuilder();
            string idd = gaseste_id_identity(id);
            for (int i = 0; i < numar_paleti; i++)
            {
                str.Append(string.Concat(idd, i.ToString("00")));
                str.Append(" ");
            }
            return str.ToString();
        }

        private static string hourseBook(string aa, int cc, string detalii)
        {
            string[] str = new string[] { "[", aa, "]='", detalii, "'," };
            aa = string.Concat(str);
            int bb = Convert.ToInt32(aa.Substring(1, 2));
            int i = 1;
            while (i < cc)
            {
                if (bb != 23)
                {
                    bb++;
                    string str1 = aa;
                    str = new string[] { str1, "[", bb.ToString("00"), ":00] = '", detalii, "'," };
                    aa = string.Concat(str);
                    i++;
                }
                else
                {
                    break;
                }
            }
            aa = aa.Remove(aa.Length - 1);
            return aa;
        }

        private void incarca_detalii_rezervare()
        {
            rezervare.Clear();
            rezervare.Add(ret_dep(DropDownList1.SelectedIndex));
            rezervare.Add(DropDownList5.SelectedValue.ToString());
            rezervare.Add(Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
            rezervare.Add(DropDownList2.SelectedValue);
            rezervare.Add(DropDownList3.SelectedValue);
        }

        protected void incarca_detaliile()
        {
            detalii.Clear();
            detalii.Add(txt_numar_camion.Text);
            detalii.Add(txt_numar_remorca.Text);
            detalii.Add(txt_nume_sofer.Text);
            detalii.Add(txt_numar_telefon.Text);
            detalii.Add(txt_numar_paleti_total.Text);
            detalii.Add(drop_tip_paleti.SelectedValue);
            detalii.Add(txt_greutate_totala.Text);
            detalii.Add(txt_numar_sigiliu.Text);
            detalii.Add(txt_numerele_avizelor.Text);
            detalii.Add(txt_id_identificare.Text);
        }

        private void incarca_dtl_contact()
        {
            try
            {
                try
                {
                    SqlCommand comm = new SqlCommand("select * from detalii_contact", conn);
                    conn_open();
                    SqlDataReader rdr = comm.ExecuteReader();
                    GridView2.DataSource = rdr;
                    GridView2.DataBind();
                    rdr.Close();
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    Show(string.Concat("A aparut o eroare: ", ex.Message), this);
                    lbl_info.Text = "An error occurred!";
                    lbl_info.ForeColor = Color.Red;
                }
            }
            finally
            {
                conn_close();
            }
        }

        public Dictionary<string, string> info_comanda()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                { "id", txt_id_identificare.Text },
                { "locatie_oprire", DropDownList1.SelectedValue.ToString() },
                { "numar_paleti", return_number(txt_numar_paleti_total.Text) }
            };
            return dic;
        }

        private bool inregistreaza_cursa()
        {
            bool b = false;
            string obs_speciale = "Client: {2}, Companie: {3}, Numar de telefon : {0}, numerele avizelor si facturilor: {1}";
            string[] str = new string[] { DropDownList1.SelectedValue.ToString(), " ", null, null, null, null, null, null, null };
            DateTime selectedDate = Calendar1.SelectedDate;
            str[2] = selectedDate.ToString("yyyy-MM-dd");
            str[3] = " ";
            str[4] = DropDownList3.SelectedItem.ToString();
            str[5] = " ";
            str[6] = DropDownList2.SelectedValue.ToString();
            str[7] = " ";
            str[8] = DropDownList4.SelectedValue.ToString();
            string info_rez = string.Concat(str);
            object[] item = new object[] { detalii[3], detalii[8], Session["utilizator"] as string, Session["companie"] as string };
            obs_speciale = string.Format(obs_speciale, item);
            SqlCommand comm = new SqlCommand("inregistreaza_curse_inbound", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            comm.Parameters.AddWithValue("@id", detalii[9]);
            comm.Parameters.AddWithValue("@status", rezervare[1]);
            comm.Parameters.AddWithValue("@truck_number", detalii[0]);
            comm.Parameters.AddWithValue("@truck_seal_number", detalii[7]);
            comm.Parameters.AddWithValue("@trailer_number", detalii[1]);
            comm.Parameters.AddWithValue("@truck_driver", detalii[2]);
            comm.Parameters.AddWithValue("@special_observations", obs_speciale);
            comm.Parameters.AddWithValue("@crossdoc", rezervare[0]);
            comm.Parameters.AddWithValue("@greutate", Convert.ToInt32(return_number(detalii[6])));
            comm.Parameters.AddWithValue("@load_date", Convert.ToDateTime(string.Concat(rezervare[2], " ", rezervare[3])));
            comm.Parameters.AddWithValue("@unload_date", Convert.ToDateTime(string.Concat(rezervare[2], " ", rezervare[3])));
            comm.Parameters.AddWithValue("@ramp_number_load", 0);
            comm.Parameters.AddWithValue("@ramp_number_unload", Convert.ToInt32(rezervare[4]));
            comm.Parameters.AddWithValue("@type_of_pallets", detalii[5]);
            comm.Parameters.AddWithValue("@quantity", Convert.ToInt32(return_number(detalii[4])));
            comm.Parameters.AddWithValue("@detaliirez", info_rez);
            try
            {
                try
                {
                    conn_open();
                    comm.ExecuteNonQuery();
                    lbl_info.Text = string.Concat("Trip with ID: ", txt_id_identificare.Text, " has been saved!");
                    lbl_info.ForeColor = Color.White;
                    b = true;
                }
                catch (Exception exception)
                {
                    lbl_info.Text = exception.Message;
                }
            }
            finally
            {
                conn_close();
            }
            return b;
        }

        protected bool inregistreaza_cursa_evt()
        {
            incarca_detalii_rezervare();
            incarca_detaliile();
            bool b = inregistreaza_cursa();
            bool c = false;
            if (b)
            {
                Dictionary<string, string> dic = info_comanda();
                c = creare_comanda(ref dic);
            }
            return ((!b ? true : !c) ? false : true);
        }

        private void insereaza()
        {
            if (!verificare_id_cursa())
            {
                lbl_info.Text = "This id allready exists in the database! ";
                lbl_info.ForeColor = Color.Red;
            }
            else if (!verificare())
            {
                Show("It looks like that hours are allready occupied!", this);
                lbl_info.Text = "It looks like that hours are allready occupied!!";
                lbl_info.ForeColor = Color.Red;
            }
            else
            {
                int a = 0;
                string detalii = string.Concat("* ", txt_numar_camion.Text, "  ", txt_numar_remorca.Text);
                string[] str = new string[] { "update ", DropDownList1.SelectedValue.ToString(), " set ", hourseBook(DropDownList2.SelectedItem.ToString(), Convert.ToInt32(DropDownList4.SelectedValue), detalii), " where data = '", null, null, null, null };
                DateTime selectedDate = Calendar1.SelectedDate;
                str[5] = selectedDate.ToString("yyyy-MM-dd");
                str[6] = "' and rampa = '";
                str[7] = DropDownList3.SelectedItem.ToString();
                str[8] = "'";
                SqlCommand comand = new SqlCommand(string.Concat(str), conn);
                try
                {
                    try
                    {
                        conn_open();
                        a = comand.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        Exception ex = exception;
                        Show(string.Concat("A aparut o eroare: ", ex.Message), this);
                        lbl_info.Text = ex.Message;
                        lbl_info.ForeColor = Color.Red;
                    }
                }
                finally
                {
                    conn.Close();
                }
                if (a != 0)
                {
                    if (inregistreaza_cursa_evt())
                    {
                        update_table();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dtl_contact.Visible = false;
            if (!base.IsPostBack)
            {
                verifica_zi();
                Calendar2.SelectedDate = DateTime.Today;
                Calendar3.SelectedDate = DateTime.Today;
                drop_tip_paleti.SelectedIndex = 0;
                DropDownList1.SelectedIndex = 0;
                DropDownList2.SelectedIndex = 0;
                DropDownList5.SelectedIndex = 0;
                txt_id_identificare.Text = DateTime.Now.ToString("yyMMddHHmmss");
                lbl_info.Text = "The trip must be saved!";
                lbl_info.ForeColor = Color.Orange;
                lbl_utilizator.Text = Session["utilizator"].ToString();
                lbl_utilizator.ForeColor = Color.OrangeRed;
                div_curse_client.Visible = false;
                update_table();
            }
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            detalii.Clear();
            rezervare.Clear();
            txt_id_identificare.Text = DateTime.Now.ToString("yyMMddHHmmss");
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            detalii.Clear();
            rezervare.Clear();
            txt_id_identificare.Text = DateTime.Now.ToString("yyMMddHHmmss");
        }

        private string ret_dep(int a)
        {
            string str;
            switch (a)
            {
                case 0:
                    {
                        str = "DC-1";
                        break;
                    }
                case 1:
                    {
                        str = "DC-2";
                        break;
                    }
                case 2:
                    {
                        str = "DC-3";
                        break;
                    }
                case 3:
                    {
                        str = "Nestle&Ul";
                        break;
                    }
                default:
                    {
                        str = null;
                        break;
                    }
            }
            return str;
        }

        private string return_number(string ss)
        {
            string ret = "";
            for (int i = 0; i < ss.Length; i++)
            {
                if (char.IsDigit(ss[i]))
                {
                    ret = string.Concat(ret, ss[i]);
                }
            }
            return (ret == string.Empty ? "0" : ret);
        }

        private void salveaza_id_paleti(string id, int numar_paleti)
        {
            SqlCommand comm = new SqlCommand("update orders set palletsId = @iduri where orderId = @id", conn);
            try
            {
                try
                {
                    comm.Parameters.AddWithValue("@iduri", generare_id_paleti(id, numar_paleti));
                    comm.Parameters.AddWithValue("@id", id);
                    conn_open();
                    comm.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            finally
            {
                conn_close();
            }
            Salveaza_order_amount(id, numar_paleti);
        }

        private void Salveaza_order_amount(string id, int numar_paleti)
        {
            SqlCommand comm = new SqlCommand("insert into orderAmount(orderId, plts) values( @id, @nr);", conn);
            try
            {
                try
                {
                    comm.Parameters.AddWithValue("@id", id);
                    comm.Parameters.AddWithValue("@nr", numar_paleti.ToString());
                    conn_open();
                    comm.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            finally
            {
                conn_close();
            }
        }

        private void scrie_booked()
        {
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                TableCell item = GridView1.Rows[j].Cells[1];
                DateTime dateTime = Convert.ToDateTime(GridView1.Rows[j].Cells[1].Text);
                item.Text = dateTime.ToString("dd-MM-yyyy");
                for (int i = 2; i < GridView1.Rows[j].Cells.Count; i++)
                {
                    if (GridView1.Rows[j].Cells[i].Text.Trim() != "&nbsp;")
                    {
                        GridView1.Rows[j].Cells[i].ForeColor = Color.Red;
                        GridView1.Rows[j].Cells[i].Text = "booked";
                    }
                }
            }
        }

        private void scrie_legenda()
        {
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                switch (DropDownList1.SelectedIndex)
                {
                    case 0:
                        {
                            if ((j <= 7 ? false : j < 11))
                            {
                                GridView1.Rows[j].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                GridView1.Rows[j].BackColor = Color.LightCyan;
                            }
                            break;
                        }
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            if (j <= 4)
                            {
                                GridView1.Rows[j].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                GridView1.Rows[j].BackColor = Color.LightCyan;
                            }
                            break;
                        }
                    default:
                        {
                            goto case 1;
                        }
                }
            }
        }

        public void Show(string message, Control owner)
        {
            Page page = owner as Page ?? owner.Page;
            if (page != null)
            {
                page.ClientScript.RegisterStartupScript(owner.GetType(), "ShowMessage", string.Format("<script type='text/javascript'>alert('{0}')</script>", message));
            }
        }

        private void sterge_rezervarea(string p)
        {
            string info = gaseste_informatii_rezervare(p);
            string[] temp = info.Split(new char[0]);
            string[] strArrays = new string[] { "update ", temp[0], " set ", hourseBook(temp[4], Convert.ToInt32(temp[5]), string.Empty), " where rampa = '", temp[2], " ", temp[3], "' and data = '", temp[1], "'" };
            SqlCommand comm = new SqlCommand(string.Concat(strArrays), conn);
            try
            {
                try
                {
                    conn_open();
                    comm.ExecuteNonQuery();
                    update_table();
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    lbl_info.Text = string.Concat(ex.Message, " !rez");
                }
            }
            finally
            {
                conn_close();
            }
        }

        private void update_table()
        {
            string ss = "rampa as 'ramp number', data as 'date', [08:00] as [08:00->09:00],[09:00] as [09:00->10:00],[10:00] as [10:00->11:00], [11:00] as [11:00->12:00],[12:00] as [12:00->13:00],[13:00] as [13:00->14:00],[14:00] as [14:00->15:00],[15:00] as [15:00->16:00],[16:00] as [16:00->17:00],[17:00] as [17:00->18:00],[18:00] as [18:00->19:00],[19:00] as [19:00->20:00],[20:00] as [20:00->21:00],[21:00] as [21:00->22:00],[22:00] as [22:00->23:00],[23:00] as [23:00->00:00],[00:00] as [00:00->01:00],[01:00] as [01:00->02:00],[02:00] as [02:00->03:00],[03:00] as [03:00->04:00],[04:00] as [04:00->05:00],[05:00] as [05:00->06:00],[06:00] as [06:00->07:00],[07:00] as [07:00->08:00] ";
            try
            {
                try
                {
                    string[] str = new string[] { "select ", ss, " from ", DropDownList1.SelectedValue.ToString(), " where data = '", null, null };
                    DateTime selectedDate = Calendar1.SelectedDate;
                    str[5] = selectedDate.ToString("yyyy-MM-dd");
                    str[6] = "' order by cast(RIGHT(rampa, len(rampa)-6 ) as int)";
                    SqlCommand comm = new SqlCommand(string.Concat(str), conn);
                    conn_open();
                    SqlDataReader rdr = comm.ExecuteReader();
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                    rdr.Close();
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    Show(string.Concat("A aparut o eroare: ", ex.Message), this);
                    lbl_info.Text = string.Concat("An error occurred: ", ex.Message);
                    lbl_info.ForeColor = Color.Red;
                }
            }
            finally
            {
                conn_close();
            }
            if (GridView1.Rows.Count != 0)
            {
                lbl_tabel.Visible = false;
                GridView1.CellPadding = 8;
                scrie_booked();
                scrie_legenda();
            }
            else
            {
                lbl_tabel.Visible = true;
            }
        }

        private bool verifica_daca_exista_id(string p)
        {
            bool flag;
            SqlCommand comm = new SqlCommand("select count(tripId) from trips where tripId = @id", conn);
            comm.Parameters.AddWithValue("@id", p);
            try
            {
                try
                {
                    conn_open();
                    int? nullable = (int?)(comm.ExecuteScalar() as int?);
                    if ((nullable.HasValue ? nullable.GetValueOrDefault() : 0) != 1)
                    {
                        Label lblInfo = lbl_info;
                        string str = "There is no trip with this id!";
                        string str1 = str;
                        lbl_stergere_cursa.Text = str;
                        lblInfo.Text = str1;
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                catch (Exception exception)
                {
                    lbl_info.Text = exception.Message;
                    lbl_info.ForeColor = Color.Red;
                    flag = false;
                }
            }
            finally
            {
                conn_close();
            }
            return flag;
        }

        private void verifica_zi()
        {
            DateTime today;
            DayOfWeek dayOfWeek = DateTime.Today.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Sunday)
            {
                Calendar calendar1 = Calendar1;
                today = DateTime.Today;
                calendar1.SelectedDate = today.AddDays(1);
            }
            else
            {
                switch (dayOfWeek)
                {
                    case DayOfWeek.Friday:
                        {
                            Calendar calendar = Calendar1;
                            today = DateTime.Today;
                            calendar.SelectedDate = today.AddDays(3);
                            break;
                        }
                    case DayOfWeek.Saturday:
                        {
                            Calendar calendar11 = Calendar1;
                            today = DateTime.Today;
                            calendar11.SelectedDate = today.AddDays(2);
                            break;
                        }
                    default:
                        {
                            Calendar calendar12 = Calendar1;
                            today = DateTime.Today;
                            calendar12.SelectedDate = today.AddDays(1);
                            break;
                        }
                }
            }
        }

        private bool verificare()
        {
            bool result = true;
            string[] str = new string[] { "Select ", compune_ore(DropDownList2.SelectedItem.ToString(), Convert.ToInt32(DropDownList4.SelectedValue.ToString())), " from ", DropDownList1.SelectedValue.ToString(), " where data = '", null, null, null, null };
            DateTime selectedDate = Calendar1.SelectedDate;
            str[5] = selectedDate.ToString("yyyy-MM-dd");
            str[6] = "' and rampa = '";
            str[7] = DropDownList3.SelectedItem.ToString();
            str[8] = "'";
            SqlCommand comm = new SqlCommand(string.Concat(str), conn);
            try
            {
                try
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (!string.IsNullOrEmpty(reader[i] as string))
                            {
                                result = false;
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    Show(string.Concat("A aparut o eroare: ", ex.Message), this);
                    lbl_info.Text = "An error occurred!";
                    lbl_info.ForeColor = Color.Red;
                }
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        private bool verificare_id_cursa()
        {
            int contor = 0;
            SqlCommand comm = new SqlCommand("select count(tripId) from trips where tripId = @id", conn);
            comm.Parameters.AddWithValue("@id", txt_id_identificare.Text);
            try
            {
                try
                {
                    conn.Open();
                    contor = (int)comm.ExecuteScalar();
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    Show(string.Concat("A aparut o eroare : ", ex.Message), this);
                    lbl_info.Text = "An error occurred!";
                    lbl_info.ForeColor = Color.Red;
                }
            }
            finally
            {
                conn.Close();
            }
            return contor == 0;
        }

        private bool verificare_zi_libera()
        {
            bool liber = true;
            string[] str = new string[] { "select count(data) from ", DropDownList1.SelectedValue.ToString(), " where data = '", null, null };
            DateTime selectedDate = Calendar1.SelectedDate;
            str[3] = selectedDate.ToString("yyyy-MM-dd");
            str[4] = "'";
            string query = string.Concat(str);
            try
            {
                try
                {
                    SqlCommand comm = new SqlCommand(query, conn);
                    conn.Open();
                    if ((int)comm.ExecuteScalar() == 0)
                    {
                        liber = false;
                    }
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    Show(string.Concat("A aparut o eroare: ", ex.Message), this);
                    lbl_info.Text = "An error occurred!";
                    lbl_info.ForeColor = Color.Red;
                }
            }
            finally
            {
                conn.Close();
            }
            return liber;
        }
    }
}