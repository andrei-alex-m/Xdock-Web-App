using System;
using System.Data.SqlClient;

namespace X_Doc_v0._8
{
    class scrie_modificari_utilizator
    {
        public static void scrie_modificarea(string e, string id)
        {
            e =  e + " " + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + " || ";
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Xdock"].ConnectionString);
            SqlCommand comm = new SqlCommand("update trips set dateTimeModifications = concat(dateTimeModifications, ' ', @d) where tripId = @i", conn);
            comm.Parameters.AddWithValue("@d", e);
            comm.Parameters.AddWithValue("@i", id);
            try
            {
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                conn.Close();
            }

        }

        public static void scrie_modificari_comenzi(string e, string id)
        {
            e =  e + " " + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + " || ";
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Xdock"].ConnectionString);
            SqlCommand comm = new SqlCommand("update orders set dateTimeModifications = concat(dateTimeModifications, ' ', @d) where orderId = @i", conn);
            comm.Parameters.AddWithValue("@d", e);
            comm.Parameters.AddWithValue("@i", id);
            try
            {
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                conn.Close();
            }
        }
    }
}
