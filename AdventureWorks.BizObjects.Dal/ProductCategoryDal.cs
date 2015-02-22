using AdventureWorks.BizObjects.Interfaces;
using Csla.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using System.Configuration;

namespace AdventureWorks.BizObjects.Dal
{
    public class ProductCategoryDal : IProductCategory
    {
        public int Id
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

        public int Create(string name)
        {
            string stmt = @"
insert into Production.ProductCategory(Name)
values (@Name)

select SCOPE_IDENTITY() as Id
";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(stmt, connection))
                {
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                    return (int)cmd.ExecuteScalar();
                }
            }

        }

        public int Read(int id)
        {
            string query = @"
SELECT ProductCategoryID,
Name,
rowguid,
ModifiedDate
FROM Production.ProductCategory
where ProductCategoryID = @Id";

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
                            this.Id = sdr.GetInt32("Id");
                            this.Name = sdr.GetString("Name");
                            this.RowGuidId = sdr.GetGuid("rowguidid");
                            this.ModifiedDate = sdr.GetSmartDate("ModifiedDate");
                            rowCount++;
                        }
                    }
                }
            }

            return rowCount;
        }

        public int Update(int id, string name, Guid rowGuidId, SmartDate modifiedDate)
        {
            string stmt = @"
UPDATE Production.ProductCategory
SET Name = @Name,
rowguid = @RowGuidId,
ModifiedDate = GETDATE()
WHERE ProductCategoryID = @Id and ModifiedDate = @ModDate
";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(stmt, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                    cmd.Parameters.Add("@RowGuidId", SqlDbType.UniqueIdentifier).Value = rowGuidId;
                    cmd.Parameters.Add("@ModDate", SqlDbType.DateTime).Value = modifiedDate.Date;

                    return (int)cmd.ExecuteNonQuery();
                }
            }
        }

        public int Delete(int id, SmartDate modifiedDate)
        {
            string stmt = @"
delete from Production.ProductCategory where ProductCategoryID = @Id and ModifiedDate = @ModDate ";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(stmt, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@ModDate", SqlDbType.DateTime).Value = modifiedDate.Date;
                    return (int)cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
