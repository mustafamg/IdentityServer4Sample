using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System;
using IdentityServer4WithAspNetIdentity.Models;
using Dapper;
using System.Collections.Generic;
using System.Threading;

namespace IdentityServer4WithAspNetIdentity.CustomIdentity
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly SqlConnection _connection;
        Func<string,string> selectUserStmnt = (param)=> $"SELECT * FROM dbo.CustomUserLogins WHERE {param} = @{param};";
        const string userLoginsTable= "dbo.CustomUserLogins";
        public UserLoginRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IdentityResult> AddLoginAsync(Guid userId, UserLoginInfo login)
        {
            string sql = $"INSERT INTO {userLoginsTable} VALUES (@LoginProvider, @ProviderKey, @ProviderDisplayName, @userId)";

            int rows = await _connection.ExecuteAsync(sql, new {login.LoginProvider, login.ProviderKey, login.ProviderDisplayName, userId });

            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {login.LoginProvider} login." });
        }        
        
        public async Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey)
        {
            string sql = $"SELECT * FROM {userLoginsTable} WHERE loginProvider = @LoginProvider and providerKey=@ProviderKey;";

            return await _connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new
            {
                LoginProvider= loginProvider,
                ProviderKey = providerKey
            });
        }       

        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> RemoveLoginAsync(Guid userId, string loginProvider, string providerKey)
        {
            string sql = $"DELETE FROM {userLoginsTable} WHERE UserId = @UserId and @loginProvider and @providerKey";
            int rows = await _connection.ExecuteAsync(sql, new { userId, loginProvider, providerKey });

            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not delete user {loginProvider}." });
        }
    }
}
