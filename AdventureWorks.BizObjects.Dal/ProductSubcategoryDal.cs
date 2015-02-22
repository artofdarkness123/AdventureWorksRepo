using AdventureWorks.BizObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using System.Data.SqlClient;
using System.Data;
using Csla.Data;
using System.Configuration;

namespace AdventureWorks.BizObjects.Dal
{
    public class ProductSubcategoryDal : IProductSubcategory
    {
        public int Id
        {
            get;
            private set;
        }

        public int CategoryId
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }
        
        public Guid RowGuidId
        {
            get;
            private set;
        }

        public SmartDate ModifiedDate
        {
            get;
            private set;
        }

        public int Create(int categoryId, string name)
        {
            string stmt = @"insert into Production.ProductSubcategory(ProductCategoryID, Name)
values(@CategoryId, @Name)

select SCOPE_IDENTITY() as Id";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(stmt, connection))
                {
                    cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public int Read(int id)
        {
            string query = @"
select ProductSubcategoryID, ProductCategoryID, Name, rowguid, ModifiedDate
from Production.ProductSubcategory
where ProductCategoryID = @Id
";
            int rowCount = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    using (SafeDataReader sdr = new SafeDataReader(cmd.ExecuteReader()))
                    {
                        while (sdr.Read())
                        {
                            this.Id = sdr.GetInt32("ProductSubcategoryID");
                            this.CategoryId = sdr.GetInt32("ProductCategoryID");
                            this.Name = sdr.GetString("Name");
                            this.RowGuidId = sdr.GetGuid("rowguidid");
                            this.ModifiedDate = sdr.GetSmartDate("ModifiedDate");
                        }
                    }
                }
            }

            return rowCount;
        }

        public int Update(int id, int categoryId, string name, Guid rowGuidId, SmartDate modifiedDate)
        {
            string stmt = @"
UPDATE Production.ProductSubcategory
SET ProductCategoryID = @ProductCategoryID,
Name = @Name,
rowguid = @rowguid,
ModifiedDate = GETDATE()
WHERE ProductCategoryID = @Id and ModifiedDate = @ModDate
";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(stmt, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@ProductCategoryID", SqlDbType.Int).Value = categoryId;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                    cmd.Parameters.Add("@rowguidid", SqlDbType.UniqueIdentifier).Value = rowGuidId;
                    cmd.Parameters.Add("@ModDate", SqlDbType.DateTime).Value = modifiedDate;

                    return (int)cmd.ExecuteNonQuery();
                }
            }
        }

        public int Delete(int id, SmartDate modifiedDate)
        {
            string stmt = @"
DELETE FROM Production.ProductSubcategory WHERE ProductSubcategoryID = @Id and ModifiedDate = @ModDate";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(stmt, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@ModDate", SqlDbType.DateTime).Value = modifiedDate;
                    return (int)cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
