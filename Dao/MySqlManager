using System.Data;
using System;
/// <summary>
/// mysql管理器
/// by：何天宇
/// 2016.2.18
/// </summary>
public class MySqlManager {
    /// <summary>
    /// 获得数据
    /// </summary>
    public static string[] GetDataBaseValue(string tableName,string valueName,string value)
    {
        string[] values = null;
        try
        {

            SqlAccess sql = new SqlAccess();

            //sql.CreateTableAutoID("momo", new string[] { "id", "name", "qq", "email", "blog" }, new string[] { "int", "text", "text", "text", "text" });
            //sql.CreateTable("momo",new string[]{"name","qq","email","blog"}, new string[]{"text","text","text","text"});
            //sql.InsertInto("momo", new string[] { "name", "qq", "email", "blog" }, new string[] { "xuanyusong", "289187120", "xuanyusong@gmail.com", "xuanyusong.com" });
            //sql.UpdateInto("momo", new string[] { "name", "qq" }, new string[] { "'ruoruo'", "'11111111'" }, "email", "'xuanyusong@gmail.com'");
            //sql.Delete("momo", new string[] { "id", "email" }, new string[] { "1", "'000@gmail.com'" });

            //危险源信息
            DataSet ds = sql.SelectWhere(tableName, new string[] { "*" }, new string[] { valueName }, new string[] { "=" }, new string[] { value });

            if (ds != null)
            {
                DataTable table = ds.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    int num = 0;
                    values  = new string[table.Columns.Count];
                    foreach (DataColumn column in table.Columns)
                    {
                        values[num] = row[column].ToString();
                        num++;
                    }
                }
            }

            sql.Close();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return values;
    }

    public static string[] ladder = new string[] { "name", "question_5" , "helmet", "helmet_tape", "belt", "insulantshoe", "question_6", "question_7", "onwLadderTwoMan", "noHelper", "question_8" };
    public static string[] pole = new string[] { "name", "question_5", "helmet", "helmet_tape", "belt", "insulantshoe", "question_0", "question_1", "question_2", "onehanduppole", "guardianshipclose_up", "question_3", "onepoletwoman", "quertion_4", "guardianshipclose_down" };

    /// <summary>
    /// 生成新行
    /// </summary>
    public static void InsertInto(string name,string[] valuename,string[] values)
    {
        try
        {
            SqlAccess sql = new SqlAccess();
            sql.InsertInto(name, valuename, values);
            sql.Close();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <summary>
    /// 删除表
    /// </summary>
    public static void DeleteTable(string tableName,string nameValue)
    {
        try
        {
            SqlAccess sql = new SqlAccess();
            sql.Delete(tableName, new string[] { "name" }, new string[] { nameValue });
            sql.Close();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
        
}
