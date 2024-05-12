using AuthoWorkAppp.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AuthoWorkAppp.Repositories
{
    internal class PlanerEventRepository : RepositoryBase, IPlanerRepository
    {
        public void Add(PlanerEventModel planerEvent, string userId)
        {
            using(var connection = GetConnection())
            using(var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO PlanerEvent(UserId, EventName, Description, Data)" +
                    "Values(@UserId, @EventName, @Description, @Data)";

                command.Parameters.Add("@UserId", System.Data.SqlDbType.UniqueIdentifier).Value = userId;
                command.Parameters.Add("@EventName", System.Data.SqlDbType.NVarChar).Value = planerEvent.EventName;
                command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar).Value = planerEvent.Description;
                command.Parameters.Add("@Data", System.Data.SqlDbType.NVarChar).Value = planerEvent.Date;

                command.ExecuteScalar();

                MessageBox.Show("Успешно");
            }
        }

        public void Edit(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlanerEventModel> GetAll(string userId)
        {
            var planerModels = new List<PlanerEventModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT Id, EventName, Description, Date FROM [PlanerEvent] WHERE UserId = @UserId";

                Guid userGuid;
                if (!Guid.TryParse(userId, out userGuid))
                {
                    throw new ArgumentException("UserId is not a valid GUID");
                }

                command.Parameters.Add("@UserId", System.Data.SqlDbType.UniqueIdentifier).Value = userGuid;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var planerModel = new PlanerEventModel()
                        {
                            Id = reader["Id"].ToString(),
                            EventName = reader["EventName"].ToString(),
                            Description = reader["Description"].ToString(),
                            Date = reader["Date"].ToString()
                        };
                        planerModels.Add(planerModel);
                    }
                }
            }
            return planerModels;
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
