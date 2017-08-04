using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
namespace xDock_scanners_app
{
    public static class incarcare_detalii
    {
        private static SqlConnection conn;

        static incarcare_detalii()
        {
            incarcare_detalii.conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Xdock"].ConnectionString);
        }

        public static List<string> comezenzi_aferente_cursei(string id, string status)
        {
            List<string> detalii = new List<string>();
            string[] strArrays = new string[] { "select orderId from orders where tripId = '", id, "' and (orderStatus ='", status, "') order by plannedFromInstant asc,  deliverFromInstant asc" };
            SqlCommand comm = new SqlCommand(string.Concat(strArrays), incarcare_detalii.conn);
            try
            {
                try
                {
                    incarcare_detalii.conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        detalii.Add(reader[0] as string);
                    }
                }
                catch (Exception exception)
                {
                }
            }
            finally
            {
                incarcare_detalii.conn.Close();
            }
            return detalii;
        }

        public static Dictionary<string, string> comezenzi_aferente_cursei_dictionar(string id)
        {
            Dictionary<string, string> detalii = new Dictionary<string, string>();
            string query = string.Concat("select orderId, orderStatus from orders where tripId = '", id, "' and orderStatus != 'finished'");
            SqlCommand comm = new SqlCommand(query, incarcare_detalii.conn);
            try
            {
                try
                {
                    incarcare_detalii.conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        detalii.Add(reader[0] as string, reader[1] as string);
                    }
                }
                catch (Exception exception)
                {
                }
            }
            finally
            {
                incarcare_detalii.conn.Close();
            }
            return detalii;
        }

        public static Tuple<List<string>, List<string>> detalii_comenzi(string id)
        {
            List<string> detalii = new List<string>();
            List<string> locatii = new List<string>();
            string query = string.Concat("select palletsId, pickupAddressId, deliverAddressId from orders where orderId ='", id, "'");
            SqlCommand comm = new SqlCommand(query, incarcare_detalii.conn);
            string _temp = string.Empty;
            try
            {
                try
                {
                    incarcare_detalii.conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        _temp = reader[0] as string;
                        locatii.Add(reader[1] as string);
                        locatii.Add(reader[2] as string);
                    }
                }
                catch (Exception exception)
                {
                }
            }
            finally
            {
                incarcare_detalii.conn.Close();
            }
            string[] strArrays = _temp.Trim().Split(new char[0]);
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                detalii.Add(strArrays[i]);
            }
            return new Tuple<List<string>, List<string>>(detalii, locatii);
        }

        public static List<string> detalii_curse(string id)
        {
            List<string> detalii = new List<string>();
            string query = string.Concat("select truckSealNumber , quantity, rampNumberForLoad, rampNumberForUnload, numberLoadPallets, numberUnloadPallets from trips where tripId = '", id, "'");
            try
            {
                try
                {
                    SqlCommand comm = new SqlCommand(query, incarcare_detalii.conn);
                    incarcare_detalii.conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        detalii.Add(reader[0] as string);
                        List<string> strs = detalii;
                        int? item = (int?)(reader[1] as int?);
                        strs.Add(((item.HasValue ? item.GetValueOrDefault() : 0)).ToString());
                        List<string> strs1 = detalii;
                        item = (int?)(reader[2] as int?);
                        strs1.Add(((item.HasValue ? item.GetValueOrDefault() : 0)).ToString());
                        List<string> strs2 = detalii;
                        item = (int?)(reader[3] as int?);
                        strs2.Add(((item.HasValue ? item.GetValueOrDefault() : 0)).ToString());
                        List<string> strs3 = detalii;
                        item = (int?)(reader[4] as int?);
                        strs3.Add(((item.HasValue ? item.GetValueOrDefault() : 0)).ToString());
                        List<string> strs4 = detalii;
                        item = (int?)(reader[5] as int?);
                        strs4.Add(((item.HasValue ? item.GetValueOrDefault() : 0)).ToString());
                    }
                }
                catch (Exception exception)
                {
                }
            }
            finally
            {
                incarcare_detalii.conn.Close();
            }
            return detalii;
        }

        public static void incrementare_paleti(string camp, string id)
        {
            string[] strArrays = new string[] { "update trips set ", camp, " = ", camp, " + 1 where tripId = '", id, "'" };
            SqlCommand comm = new SqlCommand(string.Concat(strArrays), incarcare_detalii.conn);
            try
            {
                try
                {
                    incarcare_detalii.conn.Open();
                    comm.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                }
            }
            finally
            {
                incarcare_detalii.conn.Close();
            }
        }
    }
}