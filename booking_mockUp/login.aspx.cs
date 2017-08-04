using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace booking_mockUp
{
    public partial class login : System.Web.UI.Page
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Xdock"].ConnectionString);
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (rb_client.Checked)
                {
                    if (!utilizator_valid())
                    {
                        lbl_message.Text = "It appears that you are not registered or you have not yet been validated !";
                        hr_hr.Visible = true;
                    }
                    else
                    {
                        Tuple<string, string> tu = gaseste_nume_client();
                        Session["utilizator"] = tu.Item1;
                        Session["companie"] = tu.Item2;

                        FormsAuthentication.RedirectFromLoginPage(TextBox1.Text, false);
                    }
                }
                else if (!verifica_utilizator())
                {
                    lbl_message.Text = "Name or password incorrect!";
                    hr_hr.Visible = true;
                }
                else
                {
                    Session["utilizator"] = TextBox1.Text;
                    FormsAuthentication.SetAuthCookie(TextBox1.Text, false);
                    base.Response.Redirect("~/Scanning.aspx");
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (register_div.Visible)
            {
                register_div.Visible = false;
            }
            else
            {
                register_div.Visible = true;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!liber())
                {
                    Label8.Text = "It seems that this name or email has been already registered!";
                }
                else
                {
                    creaza_intrarea();
                }
            }
        }

        private void creaza_intrarea()
        {
            string food_type = string.Empty;
            foreach (ListItem it in CheckBoxList1.Items)
            {
                if (it.Selected)
                {
                    food_type = string.Concat(food_type, it.Value.ToString());
                    food_type = string.Concat(food_type, " ");
                }
            }
            SqlCommand comm = new SqlCommand("introducere_client", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            comm.Parameters.AddWithValue("@nume", TextBox3.Text);
            comm.Parameters.AddWithValue("@companie", TextBox5.Text);
            comm.Parameters.AddWithValue("@parola", TextBox4.Text);
            comm.Parameters.AddWithValue("@email", TextBox7.Text);
            comm.Parameters.AddWithValue("@tipmarfa", food_type);
            try
            {
                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                    Label8.Text = "Registration successful , you will be validated as soon as possible!";
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    Label8.Text = string.Concat("An error occurred : ", ex.Message);
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private Tuple<string, string> gaseste_nume_client()
        {
            Tuple<string, string> tuple;
            string nume = "";
            string companie = "";
            SqlCommand comm = new SqlCommand("select nume, companie from clienti where (nume = @nume or email= @nume)", conn);
            comm.Parameters.AddWithValue("@nume", TextBox1.Text);
            try
            {
                try
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        nume = reader[0] as string;
                        companie = reader[1] as string;
                    }
                    tuple = new Tuple<string, string>(nume, companie);
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    Show(string.Concat("An error occurred:", ex.Message), this);
                    tuple = null;
                }
            }
            finally
            {
                conn.Close();
            }
            return tuple;
        }

        private bool liber()
        {
            bool _liber = false;
            int contor = 0;
            SqlCommand comm = new SqlCommand("select count(*) from clienti where nume = @nume or email = @email ", conn);
            comm.Parameters.AddWithValue("@nume", TextBox3.Text);
            comm.Parameters.AddWithValue("@email", TextBox7.Text);
            try
            {
                try
                {
                    conn.Open();
                    int? nullable = (int?)(comm.ExecuteScalar() as int?);
                    contor = (nullable.HasValue ? nullable.GetValueOrDefault() : 0);
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    Label8.Text = string.Concat("An error occurred : ", ex.Message);
                }
            }
            finally
            {
                conn.Close();
            }
            if (contor == 0)
            {
                _liber = true;
            }
            return _liber;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                register_div.Visible = false;
                rb_client.Checked = true;
                hr_hr.Visible = false;
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

        private bool utilizator_valid()
        {
            bool valid = false;
            int _valid = 0;
            SqlCommand comm = new SqlCommand("select valid from clienti where parola = @parola and (nume = @nume or email= @nume)", conn);
            comm.Parameters.AddWithValue("@nume", TextBox1.Text);
            comm.Parameters.AddWithValue("@parola", TextBox2.Text);
            try
            {
                try
                {
                    conn.Open();
                    int? nullable = (int?)(comm.ExecuteScalar() as int?);
                    _valid = (nullable.HasValue ? nullable.GetValueOrDefault() : 0);
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    Show(string.Concat("An error occurred!: ", ex.Message), this);
                }
            }
            finally
            {
                conn.Close();
            }
            if (_valid != 0)
            {
                valid = true;
            }
            return valid;
        }

        private bool verifica_utilizator()
        {
            bool valid = false;
            SqlCommand comm = new SqlCommand(" select count(*) from ussers where name = @name and password = @password", conn);
            comm.Parameters.AddWithValue("@name", TextBox1.Text);
            comm.Parameters.AddWithValue("@password", TextBox2.Text);
            try
            {
                try
                {
                    conn.Open();
                    if ((int)comm.ExecuteScalar() == 1)
                    {
                        valid = true;
                    }
                }
                catch (Exception exception)
                {
                    lbl_message.Text = "An error occurred!";
                }
            }
            finally
            {
                conn.Close();
            }
            return valid;
        }
    }
}