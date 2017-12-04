using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System;
using IdentityServer4WithAspNetIdentity.Models;
using Dapper;

namespace IdentityServer4WithAspNetIdentity.CustomIdentity
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _connection;
        Func<string,string> selectUserStmnt = (param)=> $"SELECT * FROM dbo.CustomUser WHERE {param} = @{param};";

        public UserRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        #region createuser
        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            string sql = "INSERT INTO dbo.CustomUser VALUES (@id, @Email, @EmailConfirmed, @PasswordHash, @UserName)";

            int rows = await _connection.ExecuteAsync(sql, new { user.Id, user.Email, user.EmailConfirmed, user.PasswordHash, user.UserName });

            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." });
        }
        #endregion

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            string sql = "DELETE FROM dbo.CustomUser WHERE Id = @Id";
            int rows = await _connection.ExecuteAsync(sql, new { user.Id });

            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not delete user {user.Email}." });
        }


        public async Task<ApplicationUser> FindByIdAsync(Guid userId)
        {
            string sql = selectUserStmnt("Id");

            return await _connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new
            {
                Id = userId
            });
        }


        public async Task<ApplicationUser> FindByUserNameAsync(string userName)
        {
            string sql = selectUserStmnt("UserName");

            return await _connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new
            {
                UserName = userName
            });
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            string sql = "Update dbo.CustomUser set Id=@id, Email = @Email, EmailConfirmed = @EmailConfirmed"+
                ", PasswordHash= @PasswordHash, UserName = @UserName)";

            int rows = await _connection.ExecuteAsync(sql, new { user.Id, user.Email, user.EmailConfirmed, user.PasswordHash, user.UserName });

            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." });
        }
    }
}
