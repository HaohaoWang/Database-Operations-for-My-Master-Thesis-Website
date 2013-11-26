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
/// Summary description for diagnosisdbOperations
/// </summary>
public class diagnosisdbOperations
{
    public diagnosisdbOperations()
    {
    }
    public static String sqlConnstr()
    {
        String connStr = ConfigurationManager.ConnectionStrings["mydatabaseConnectionString"].ToString();
        return connStr;
    }
    public String selectDiagnosesJson(string pid)
    {
        SqlConnection sqlCon = new SqlConnection(sqlConnstr());
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        List<Dictionary<String, String>> diagnosisList = new List<Dictionary<String, String>>();
        String json = "";
        try
        {
            sqlCon.Open();
            string sql = "select date, diagnosis from diagnosis where pid = '" + pid + "'";
            cmd = new SqlCommand(sql, sqlCon);
            //reader = cmd.ExecuteReader();
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
        
                for (int i = 0; i < reader.FieldCount; i = i + 2)
                {
                    Dictionary<String, String> dic = new Dictionary<String, String>();
                    dic.Add(reader.GetName(i), reader.GetValue(i).ToString().Trim());
                    dic.Add(reader.GetName(i + 1), reader.GetValue(i + 1).ToString().Trim());
                    diagnosisList.Add(dic);
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
        json = js.Serialize(diagnosisList);
        return json;
    }
    public bool insertdiagnosis(string pid, string diagnosis)
    {
        //doctor leave a message to patient:messagedirection =1 ;
        String sqlQuery = "INSERT INTO diagnosis (pid, date, diagnosis) VALUES('" + pid + "','" + System.DateTime.Now.ToShortDateString() + "','" + diagnosis + "')";

        using (SqlConnection sqlCon = new SqlConnection(sqlConnstr()))
        {
            sqlCon.Open();
            using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon))
            {
                try
                {
                    sqlCmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

    }
}
