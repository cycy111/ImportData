using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WmsReport.Model;

namespace WmsReport.Infrastructure
{
    /// <summary>
    /// 调用SqlServer数据方法
    /// </summary>
    public class SqlServerPackage
    {
        //public void GetDataTable(PackageModel packageModel, string connStr, out DataSet dt1)
        //{
        //    //默认定义参数组
        //    SqlParameter[] param = new SqlParameter[2];
        //    //根据参数传递个数生成数组
        //    Array.Resize(ref param, packageModel.ParameterCount + packageModel.ParameterRCount);
        //    for (int i = 0; i < packageModel.ParameterCount; i++)
        //    {
        //        param[i] = new SqlParameter(packageModel.Parameter[i, 0].ToString(), GetTypeOra(packageModel.Parameter[i, 2].ToString()), 1);
        //        param[i].Value = packageModel.Parameter[i, 1].ToString();
        //    }

        //    for (int n = packageModel.ParameterCount; n < param.Length; n++)
        //    {
        //        param[n] = new SqlParameter(packageModel.ParameterR[n - packageModel.ParameterCount, 0], GetTypeOra(packageModel.ParameterR[n - packageModel.ParameterCount, 1]), 2);
        //    }


        //    ExecuteReader(connStr, packageModel, param, out dt1);

        //}

        public void ExecuteReader(string connectstring, PackageModel olap, SqlParameter[] parameter, out DataSet dt1)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connectstring);
            // define the command for the stored procedure
            SqlCommand cmd = new SqlCommand(olap.Package + "." + olap.Procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = string.Format("UP_HandleKpiInfo");//Sql语句为数据库的存储过程
            //for (int m = 0; m < parameter.Length; m++)
            //{
            //    cmd.Parameters.Add(parameter[m]);
            //}
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dt1 = ds;
            conn.Close();

        }

        //public DataSet ExecuteReader(string connectstring)
        //{
        //    try
        //    {
        //        //****进行数据连接****//
        //        string conString = "server=localhost;database=stuDB;uid=sa";//连接串
        //        SqlConnection sqlConnection = new SqlConnection(conString);//创建连接对象
        //        sqlConnection.Open();//打开连接
        //        SqlCommand sqlCommand = new SqlCommand();//创建SqlCommand命令对象
        //        sqlCommand.Connection = sqlConnection;//SqlCommand命令对象的连接属性赋值
        //        sqlCommand.CommandType = CommandType.StoredProcedure;//**************命令对象的类型为执行数据库的存储过程***********
        //        string sql = string.Format("proc_insert_stuClass");//Sql语句为数据库的存储过程
        //        sqlCommand.CommandText = sql;//命令文本

        //        //****设置存储过程的参数****//
        //        SqlParameter sp1 = new SqlParameter("@outcome", SqlDbType.Bit);//创建参数对象，并设置@outcome参数的类型为Bit类型
        //        sp1.Direction = System.Data.ParameterDirection.Output;//设置此项参数的类型为输出参数
        //        sqlCommand.Parameters.Add(sp1);//将此项参数添加到命令参数集

        //        //****执行存储过程****//
        //        sqlCommand.ExecuteNonQuery();//执行存储过程
        //        string outcome = sp1.Value.ToString();//将输出参数的值取出
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //}

        //private SqlDbType GetTypeOra(string ty)
        //{
        //    switch (ty)
        //    {
        //        case "Varchar2":
        //            return SqlDbType.NVarChar;
        //        case "number":
        //            return SqlDbType.Decimal;
        //        case "float":
        //            return SqlDbType.Float;
        //        default:
        //            return SqlDbType.Char;
        //    }

        //}
    }
}
