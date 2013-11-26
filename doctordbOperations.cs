using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for doctordbOperations
/// </summary>
public class doctordbOperations
{
	public doctordbOperations()
	{
    }
		
    public static  String sqlConnstr()
    {
    String connStr = ConfigurationManager.ConnectionStrings["mydatabaseConnectionString"].ToString();
    return connStr;
    }

    public bool doctorLogin(string did,string dpss) {

        string sqlQuery = "select dpassword from doctors  where did = '" + did + "'";
        bool flag = false;
        using (SqlConnection sqlCon = new SqlConnection(sqlConnstr()))
        {
            sqlCon.Open();
            using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon))
            {
                using (SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                       String dpassword = reader.GetValue(0).ToString().Trim();
                       if (dpassword == dpss)
                       {
                           flag = true;
                       }
                    }
                }
            }
        }
        return flag;
    }

    public String selectDoctorcaseJson(string did)
    {
        SqlConnection sqlCon = new SqlConnection(sqlConnstr());
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        Dictionary<String, String> dic = new Dictionary<String, String>();
        String json = "";
        try
        {
            sqlCon.Open();
            string sql = "select * from doctors where did = '" + did + "'";
            cmd = new SqlCommand(sql, sqlCon);
            //reader = cmd.ExecuteReader();
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dic.Add(reader.GetName(i), reader.GetValue(i).ToString().Trim());
                }
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            sqlCon.Dispose();
            cmd.Dispose();
            reader.Close();
            reader.Dispose();
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        json = js.Serialize(dic);
        return json;
    }
        
    

}
