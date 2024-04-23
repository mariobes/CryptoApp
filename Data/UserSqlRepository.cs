using System.Text.Json;
using System.Data.SqlClient;
using CryptoApp.Models;
using System.Data;

namespace CryptoApp.Data
{
    public class UserSqlRepository : IUserRepository
    {

        private readonly string _connectionString;

        public UserSqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void AddUser(User user)
        {
           //INSERT INTO SQL STRING
        }

//TODO ASYNC & LIST<BankAccount>
        /*public User GetUser(string userId)
        {
            var account = new BankAccount();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlString = "SELECT Number, Owner FROM BankAccounts WHERE Number=" + accountNumber;
                var command = new SqlCommand(sqlString, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        account = new BankAccount
                        {
                            Number = reader["Number"].ToString(),
                            Owner = reader["Owner"].ToString(),
                            //Balance = (decimal)reader[2]
                        };
                    }
                }
                
            }

            return account;
        }*/

        public void UpdateUser(User user)
        {
            //UPDATE SQL STRING
        }

        public void SaveChanges()
        {
            //DO NOTHING (BY NOW)
        }

        public User GetUser(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

    }
}
