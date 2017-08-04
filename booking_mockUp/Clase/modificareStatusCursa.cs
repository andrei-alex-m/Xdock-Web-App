using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;


namespace booking_mockUp
{
    public class modificareStatusCursa
    {
        private static void inchidere(bool loaded, string id, string utilizator)
        {
            string w = loaded ? "Inc" : "Desc";

            string inchis = w + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " /// " + utilizator + ": " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            SqlCommand comm = new SqlCommand("adaugaTimeStamp");
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@id", id);
            comm.Parameters.AddWithValue("@observatii", string.Empty);
            comm.Parameters.AddWithValue("@date", inchis);
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Xdock"].ConnectionString))
            {
                conn.Open();
                comm.Connection = conn;
                comm.ExecuteNonQuery();
            }
        }

        private static void modificaCursa(string p, string id)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Xdock"].ConnectionString))
            {
                string query = "update trips set tripStatus = @s where tripId = @id";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@id", id);
                comm.Parameters.AddWithValue("@s", p);
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }

        public static void verifica(string id, string utilizator)
        {
            List<comanda> lst = new List<comanda>();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Xdock"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand("select orderStatus from orders where tripId = @id", conn);
                comm.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    lst.Add(new comanda() { status = reader[0] as string });
                }
            }

            if (lst.Count(x => x.status == "unloading") > 0 || (lst.Count(x => x.status == "unloaded") > 0 && lst.Count(x => x.status == "unloaded") < lst.Count()))
            {
                modificaCursa("unloading", id);
            }
            else if (lst.Count(x => x.status == "loading") > 0 || (lst.Count(x => x.status == "loaded") > 0 && lst.Count(x => x.status == "loaded") < lst.Count()))
            {
                modificaCursa("loading", id);
            }
            else if (lst.Count(x => x.status == "unloaded") == lst.Count())
            {
                modificaCursa("unloaded", id);
                inchidere(false, id, utilizator);
            }
            else if (lst.Count(X => X.status == "loaded") == lst.Count())
            {
                modificaCursa("loaded", id);
                inchidere(true, id, utilizator);
            }
            lst.Clear();
        }

        private struct comanda
        {
            public string status;
        }
    }
}
