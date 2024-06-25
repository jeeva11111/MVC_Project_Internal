using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;


namespace MVC_Project_Internal.Filters
{
    public class UserAccountSettings : IUserAccount
    {
        private readonly string _connectionString;
        public UserAccountSettings(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ServerLink");
        }

        public async Task<int> AddUser(AddUserModel model)
        {
            string query = "INSERT INTO AddUserModels(Name,Email,password) VALUES (@Name,@Email,@password) ";
            using var configuration = new SqlConnection(_connectionString);
            var userModel = await configuration.ExecuteAsync(query, model);
            return userModel;
        }

        public async Task<int> DeleteUser(int Id)
        {
            string query = "DELETE FROM AddUserModels WHERE Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(query, new { id = Id });
            return result;
        }

        public Task<AddUserModel> GetUserById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AddUserModel>> GetUsers()
        {
            string query = "SELECT * FROM AddUserModels";
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.QueryAsync<AddUserModel>(query);
            return result;
        }

        public async Task<int> UpdateUser(AddUserModel model)
        {

            string query = "UPDATE AddUserModels SET Name = @Name, Email = @Email, Password = @Password WHERE Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(query, model);
            return result;
        }
    }
}