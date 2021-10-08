using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using CrudService.EntityClasses;
using System;
namespace CrudService
{
    public class DbService
    {
        public static string connectionString;
        private static SqlConnection CreateSqlConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
        public static bool AddProduct(Product product)
        {
            bool isProductAdded = false;
            using (SqlConnection connection = CreateSqlConnection())
            {
                connection.Open();
                var sqlQuery = $"INSERT INTO ProductTable (ProductName,UnitPrice,ProductModel,ProductInStock) Values ('{product.ProductName}',{product.UnitPrice},'{product.ProductModel}',{Convert.ToInt16(product.Discontinued)})";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                sqlCommand.CommandType = CommandType.Text;
                int rows = sqlCommand.ExecuteNonQuery();
                if (rows > 0)
                {
                    isProductAdded = true;
                }

                connection.Close();
                connection.Dispose();

            }
            return isProductAdded;
        }
        public static List<Product> GetAllProduct()
        {
            List<Product> list = new List<Product>();
            DataSet ds = new DataSet();
            using (SqlConnection connection = CreateSqlConnection())
            {
                connection.Open();
                var sqlQuery = "Select ProductID,ProductName,UnitPrice,ProductModel,ProductInStock FROM ProductTable";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                sqlCommand.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlCommand;

                da.Fill(ds, "ProductTable");
                connection.Close();
                connection.Dispose();

            }
            if (ds.Tables.Contains("ProductTable"))
            {
                DataTable ProductTable = ds.Tables["ProductTable"];
                if (ProductTable.Rows.Count > 0)
                {
                    foreach (DataRow row in ProductTable.Rows)
                    {
                        Product obj = new Product();
                        obj.ProductID = Convert.ToInt32(row["ProductID"]);
                        obj.ProductName = Convert.ToString(row["ProductName"]);
                        obj.ProductModel = row["ProductModel"].ToString();
                        obj.UnitPrice = Convert.ToDecimal(row["UnitPrice"]);
                        obj.Discontinued = Convert.ToBoolean(row["ProductInStock"]);
                        list.Add(obj);
                    }
                }
            }
            return list;
        }

         public static bool DeleteProduct(int productId)
        {
            bool isProductDeleted=false;
             using (SqlConnection connection = CreateSqlConnection())
            {
                connection.Open();
                var sqlQuery = $"DELETE ProductTable WHERE ProductID={productId}";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                sqlCommand.CommandType = CommandType.Text;
               int rows=sqlCommand.ExecuteNonQuery();
               if(rows>0)
               {
                   isProductDeleted= true;
               }
               
                connection.Close();
                connection.Dispose();

            }
            return isProductDeleted;
        }

         public static bool UpdateProduct(Product product)
        {
            bool isProductUpdated=false;
             using (SqlConnection connection = CreateSqlConnection())
            {
                connection.Open();
                var sqlQuery = $"UPDATE ProductTable SET ProductName='{product.ProductName}' WHERE ProductID={product.ProductID}";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                sqlCommand.CommandType = CommandType.Text;
               int rows=sqlCommand.ExecuteNonQuery();
               if(rows>0)
               {
                   isProductUpdated= true;
               }
               
                connection.Close();
                connection.Dispose();

            }
            return isProductUpdated;
        }


    }
}