using UnityEngine;
using System;
using System.Data;
using MySql.Data.MySqlClient;

public class SqlAccess
{

    public static MySqlConnection dbConnection;
    //如果只是在本地的话，写localhost就可以。
    // static string host = "localhost";  
    //如果是局域网，那么写上本机的局域网IP
    static string host = "192.168.1.249";
    static string id = "root";
    static string pwd = "root";
    static string database = "ZJDX_GC_Hazard";

    public SqlAccess()
    {
        OpenSql();
    }

    /// <summary>
    /// 打开数据库
    /// </summary>
    public static void OpenSql()
    {

        try
        {
            string connectionString = string.Format("Server = {0};port={4};Database = {1}; User ID = {2}; Password = {3};", host, database, id, pwd, "3306");
            dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();
        }
        catch (Exception e)
        {
            throw new Exception("服务器连接失败，请重新检查是否打开MySql服务。" + e.Message.ToString());

        }

    }

    /// <summary>
    /// 创建表
    /// </summary>
    /// <param name="name">表名</param>
    /// <param name="col">值</param>
    /// <param name="colType">值类型</param>
    /// <returns></returns>
    public DataSet CreateTable(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {

            throw new Exception("columns.Length != colType.Length");

        }

        string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];

        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i] + " " + colType[i];
        }

        query += ")";

        return ExecuteQuery(query);
    }

    /// <summary>
    /// 创建带id的表
    /// </summary>
    /// <param name="name">表名</param>
    /// <param name="col">值</param>
    /// <param name="colType">值类型</param>
    /// <returns></returns>
    public DataSet CreateTableAutoID(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {

            throw new Exception("columns.Length != colType.Length");

        }

        string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0] + " NOT NULL AUTO_INCREMENT";

        for (int i = 1; i < col.Length; ++i)
        {

            query += ", " + col[i] + " " + colType[i];

        }

        query += ", PRIMARY KEY (" + col[0] + ")" + ")";

        return ExecuteQuery(query);
    }

    /// <summary>
    /// 增
    /// 插入一条数据，包括所有，不适用自动累加ID。
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="values">值</param>
    /// <returns></returns>
    public DataSet InsertInto(string tableName, string[] values)
    {

        string query = "INSERT INTO " + tableName + " VALUES (" + "'" + values[0] + "'";

        for (int i = 1; i < values.Length; ++i)
        {

            query += ", " + "'" + values[i] + "'";

        }

        query += ")";

        Debug.Log(query);
        return ExecuteQuery(query);

    }

    /// <summary>
    /// 增
    /// 插入部分ID
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="col">列名</param>
    /// <param name="values">值</param>
    /// <returns></returns>
    public DataSet InsertInto(string tableName, string[] col, string[] values)
    {

        if (col.Length != values.Length)
        {

            throw new Exception("columns.Length != colType.Length");

        }

        string query = "INSERT INTO " + tableName + " (" + col[0];
        for (int i = 1; i < col.Length; ++i)
        {

            query += ", " + col[i];

        }

        query += ") VALUES (" + "'" + values[0] + "'";
        for (int i = 1; i < values.Length; ++i)
        {

            query += ", " + "'" + values[i] + "'";

        }

        query += ")";

        Debug.Log(query);
        return ExecuteQuery(query);

    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="items">要搜到的数据</param>
    /// <param name="col">列名</param>
    /// <param name="operation">判断符</param>
    /// <param name="values">值</param>
    /// <returns></returns>
    public DataSet SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
    {

        if (col.Length != operation.Length || operation.Length != values.Length)
        {

            throw new Exception("col.Length != operation.Length != values.Length");

        }

        string query = "SELECT " + items[0];

        for (int i = 1; i < items.Length; ++i)
        {

            query += ", " + items[i];

        }

        query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";

        for (int i = 1; i < col.Length; ++i)
        {

            query += " AND " + col[i] + operation[i] + "'" + values[0] + "' ";

        }

        return ExecuteQuery(query);

    }

    /// <summary>
    /// 改
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="cols">列名</param>
    /// <param name="colsvalues">值</param>
    /// <param name="selectkey">选择的列</param>
    /// <param name="selectvalue">选择的值</param>
    /// <returns></returns>
    public DataSet UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {

        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {

            query += ", " + cols[i] + " =" + colsvalues[i];
        }

        query += " WHERE " + selectkey + " = " + selectvalue + " ";

        return ExecuteQuery(query);
    }

    /// <summary>
    /// 删
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="cols">列名</param>
    /// <param name="colsvalues">值</param>
    /// <returns></returns>
    public DataSet Delete(string tableName, string[] cols, string[] colsvalues)
    {
        string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {

            query += " or " + cols[i] + " = " + colsvalues[i];
        }
        Debug.Log(query);
        return ExecuteQuery(query);
    }

    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void Close()
    {

        if (dbConnection != null)
        {
            dbConnection.Close();
            dbConnection.Dispose();
            dbConnection = null;
        }

    }

    public static DataSet ExecuteQuery(string sqlString)
    {
        if (dbConnection.State == ConnectionState.Open)
        {
            DataSet ds = new DataSet();
            try
            {

                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, dbConnection);
                da.Fill(ds);

            }
            catch (Exception ee)
            {
                throw new Exception("SQL:" + sqlString + "/n" + ee.Message.ToString());
            }
            finally
            {
            }
            return ds;
        }
        return null;
    }

}
