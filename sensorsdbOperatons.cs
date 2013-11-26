using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

/// <summary>
/// Summary description for sensorsdbOperatons
/// </summary>
public class sensorsdbOperatons
{
    public static String sqlConnstr()
    {
        String connStr = ConfigurationManager.ConnectionStrings["mydatabaseConnectionString"].ToString();
        return connStr;
    }

	public sensorsdbOperatons()
	{
	}

  

    public void insertSensor1dataJson(string dataList)
    {
        JArray mydataList = (JArray)JsonConvert.DeserializeObject(dataList);
        JObject oneData = new JObject();
       
        SqlConnection sqlCon = new SqlConnection(sqlConnstr());
        SqlCommand cmd = null;
        String sqlQuery = "";
        sqlCon.Open();
        System.Diagnostics.Debug.Write("count:" + mydataList.Count.ToString());
        for (int i = 0; i < mydataList.Count; i++)
        {
            oneData = (JObject)mydataList[i];

            sqlQuery = "INSERT INTO sensors (pid, time, sensor, data) VALUES('" + oneData["pid"].ToString() + "','" + oneData["time"].ToString() + "','1','" + oneData["data1"].ToString() + "')";
            try
            {
                cmd = new SqlCommand(sqlQuery, sqlCon);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

            sqlQuery = "INSERT INTO sensors (pid, time, sensor, data)VALUES('" + oneData["pid"].ToString() + "','" + oneData["time"].ToString() + "','2','" + oneData["data2"].ToString() + "')";
            try
            {
                cmd = new SqlCommand(sqlQuery, sqlCon);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

            sqlQuery = "INSERT INTO sensors (pid, time, sensor, data)VALUES('" + oneData["pid"].ToString() + "','" + oneData["time"].ToString() + "','3','" + oneData["data3"].ToString() + "')";
            try
            {
                cmd = new SqlCommand(sqlQuery, sqlCon);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }  
        }  
        sqlCon.Dispose();
        cmd.Dispose();  
        //System.Diagnostics.Debug.Write("dara:!" + oneData["data"].ToString());
    }
}
