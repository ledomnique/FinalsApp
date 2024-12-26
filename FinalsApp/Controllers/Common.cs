using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Script.Serialization;

public class Common 
{
    public static string ConvertToJSON(DataTable dataTable)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;

        List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();
        Dictionary<String, Object> row;

        foreach (DataRow dr in dataTable.Rows)
        {
            row = new Dictionary<String, Object>();
            foreach (DataColumn col in dataTable.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            tableRows.Add(row);
        }

        return serializer.Serialize(tableRows);
    }

   
}

public class CustomDataSetConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(DataSet));
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        DataSet x = (DataSet)value;
        JObject jObject = new JObject();

        foreach (DataTable table in x.Tables)
        {
            if (table.Rows.Count == 1)
            {
                JArray jArray = new JArray();
                foreach (DataRow row in table.Rows)
                {
                    JObject jo = new JObject();
                    foreach (DataColumn col in table.Columns)
                    {
                        if (col.DataType.Equals(System.Type.GetType("System.Int32")) || col.DataType.Equals(System.Type.GetType("System.Int64")))
                            jo.Add(col.Caption, int.Parse(row[col].ToString()));
                        else if (col.DataType.Equals(System.Type.GetType("System.Boolean")))
                            jo.Add(col.Caption, Boolean.Parse(row[col].ToString()));
                        else if (col.DataType.Equals(System.Type.GetType("System.DateTime")))
                            jo.Add(col.Caption, DateTime.Parse(row[col].ToString()));
                        else if (col.DataType.Equals(System.Type.GetType("System.Decimal")))
                            jo.Add(col.Caption, Decimal.Parse(row[col].ToString()));
                        else
                            jo.Add(col.Caption, row[col].ToString());
                    }
                    jArray.Add(jo);
                }

                jObject.Add(table.TableName, jArray);
            }
            else
            {
                JArray jArray = new JArray();
                foreach (DataRow row in table.Rows)
                {
                    JObject jo = new JObject();
                    foreach (DataColumn col in table.Columns)
                    {
                        if (col.DataType.Equals(System.Type.GetType("System.Int32")) || col.DataType.Equals(System.Type.GetType("System.Int64")))
                            jo.Add(col.Caption, int.Parse(row[col].ToString()));
                        else if (col.DataType.Equals(System.Type.GetType("System.Boolean")))
                            jo.Add(col.Caption, Boolean.Parse(row[col].ToString()));
                        else if (col.DataType.Equals(System.Type.GetType("System.DateTime")))
                            jo.Add(col.Caption, DateTime.Parse(row[col].ToString()));
                        else if (col.DataType.Equals(System.Type.GetType("System.Decimal")))
                        {
                            string _value;
                            decimal number;
                            _value = row[col].ToString();

                            if (Decimal.TryParse(_value, out number))
                                //Console.WriteLine(number);
                                jo.Add(col.Caption, Decimal.Parse(row[col].ToString()));
                            else
                                jo.Add(col.Caption, 0.ToString());
                        }
                        else
                            jo.Add(col.Caption, row[col].ToString());
                    }
                    jArray.Add(jo);
                }

                jObject.Add(table.TableName, jArray);
            }
        }

        jObject.WriteTo(writer);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}