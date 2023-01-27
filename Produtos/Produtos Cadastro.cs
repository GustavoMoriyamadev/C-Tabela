using System;
using System.Configuration;
using System.Data.SqlServerCe;
using System.Data;
namespace BancodeDadosLocal
{
    public class DAL
    {   
        private static SqlCeConnection objSqlCeConnection = null; 
        private static DAL objSqlServerCeDAL     = null; 
        private static  string connString = null;
        public DAL()
        {
            string connString = ConfigurationManager.ConnectionStrings["ProdutosSDF"].ToString().Trim();  
            objSqlCeConnection = new SqlCeConnection(connString); 
        }

        public static DAL GetInstance(string connString)  
        {  
            if (objSqlServerCeDAL == null)  
            {  
                objSqlServerCeDAL = new DAL();  
            }  
            return objSqlServerCeDAL;  
        }  
        public void Open()  
        {  
            try  
            {  
                if (objSqlCeConnection.State == ConnectionState.Closed)
                {  
                    objSqlCeConnection.Open();  
                }  
            }  
            catch (Exception e)  
            {  
                throw e;  
            }  
        }  

        public void Dispose()  
        {  
            try  
            {  
                if (objSqlCeConnection.State != ConnectionState.Closed)  
                {  
                    objSqlCeConnection.Close();  
                    objSqlCeConnection.Dispose();  
                }  
            }  
            catch (Exception e)  
            {  
                throw e;  
            }  
        }
        public int Insert(Produto _produto, string sql)
        {
         DAL objSqlCeServerDAL = DAL.GetInstance(connString); 
            objSqlCeServerDAL.Open();
            SqlCeCommand dCmd = new SqlCeCommand(sql,objSqlCeConnection);
            dCmd.CommandType = CommandType.Text;
            try
            {
                dCmd.Parameters.AddWithValue("@nome", _produto.nome);
                dCmd.Parameters.AddWithValue("@estq", _produto.estoque);
                dCmd.Parameters.AddWithValue("@cust", _produto.custo);
                dCmd.Parameters.AddWithValue("@desc", _produto.descricao);
                return dCmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                dCmd.Dispose();
                objSqlCeServerDAL.Dispose();
            }
        }
        public int Update(Produto _produto, string sql )
        {
          DAL objSqlCeServerDAL = DAL.GetInstance(connString); 
            objSqlCeServerDAL.Open();
            SqlCeCommand dCmd = new SqlCeCommand(sql, objSqlCeConnection);
            dCmd.CommandType = CommandType.Text;
            try
            {
                dCmd.Parameters.AddWithValue("@nome", _produto.nome);
                dCmd.Parameters.AddWithValue("@estq", _produto.estoque);
                dCmd.Parameters.AddWithValue("@cust", _produto.custo);
                dCmd.Parameters.AddWithValue("@desc", _produto.descricao);
                return dCmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                dCmd.Dispose();
                objSqlCeServerDAL.Dispose();
            }
        }
        public DataTable Load(string sql)
        {
          DAL objSqlCeServerDAL = DAL.GetInstance(connString); 
            objSqlCeServerDAL.Open();
            SqlCeDataAdapter dAd = new SqlCeDataAdapter(sql, objSqlCeConnection);
            dAd.SelectCommand.CommandType = CommandType.Text;
            DataSet dSet = new DataSet();
            try
            {
                dAd.Fill(dSet, "Produtos");
                return dSet.Tables["Produtos"];
            }
            catch
            {
                throw;
            }
            finally
            {
                dSet.Dispose();
                dAd.Dispose();
                objSqlCeServerDAL.Dispose();
            }
        }
        public int Delete(int codigo,string sql)
        {
         DAL objSqlCeServerDAL = DAL.GetInstance(connString); 
            objSqlCeServerDAL.Open();
            SqlCeCommand dCmd = new SqlCeCommand(sql, objSqlCeConnection);
            dCmd.CommandType = CommandType.Text;
            try
            {
                dCmd.Parameters.AddWithValue("@codigo", codigo);
                return dCmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                dCmd.Dispose();
                objSqlCeServerDAL.Dispose();
            }
        }
    }
}