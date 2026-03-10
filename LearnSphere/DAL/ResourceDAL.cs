using System.Collections.Generic;
using System.Data;

namespace LearnSphere.DAL
{
    public class ResourceDAL
    {
        public static DataTable GetAllDomains()
        {
            string sql = "SELECT ID, DomainName FROM Domain ORDER BY DomainName";
            return DbHelper.ExecuteQuery(sql, null);
        }

        public static DataTable GetResourceById(int id)
        {
            string sql = @"
                SELECT ID, Title, Author, PublicationYear, Category, Locator, DomainName, SharerID
                FROM LearningResource
                WHERE ID = @ID";

            return DbHelper.ExecuteQuery(sql,
                new Dictionary<string, object>
                {
                    { "@ID", id }
                });
        }

        public static int InsertResource(string title, string author, int publicationYear, string category, string locator, string domainName, int sharerId)
        {
            string sql = @"
                INSERT INTO LearningResource
                (Title, Author, PublicationYear, Category, Locator, DomainName, SharerID)
                VALUES
                (@Title, @Author, @PublicationYear, @Category, @Locator, @DomainName, @SharerID)";

            return DbHelper.ExecuteNonQuery(sql,
                new Dictionary<string, object>
                {
                    { "@Title", title },
                    { "@Author", author },
                    { "@PublicationYear", publicationYear },
                    { "@Category", category },
                    { "@Locator", locator },
                    { "@DomainName", domainName },
                    { "@SharerID", sharerId }
                });
        }

        public static int UpdateResource(int id, string title, string author, int publicationYear, string category, string locator, string domainName)
        {
            string sql = @"
                UPDATE LearningResource
                SET Title = @Title,
                    Author = @Author,
                    PublicationYear = @PublicationYear,
                    Category = @Category,
                    Locator = @Locator,
                    DomainName = @DomainName
                WHERE ID = @ID";

            return DbHelper.ExecuteNonQuery(sql,
                new Dictionary<string, object>
                {
                    { "@ID", id },
                    { "@Title", title },
                    { "@Author", author },
                    { "@PublicationYear", publicationYear },
                    { "@Category", category },
                    { "@Locator", locator },
                    { "@DomainName", domainName }
                });
        }

        public static int DeleteResource(int id)
        {
            string sql = "DELETE FROM LearningResource WHERE ID = @ID";

            return DbHelper.ExecuteNonQuery(sql,
                new Dictionary<string, object>
                {
                    { "@ID", id }
                });
        }
    }
}