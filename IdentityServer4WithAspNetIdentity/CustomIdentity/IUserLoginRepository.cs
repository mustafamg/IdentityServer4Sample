using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4WithAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer4WithAspNetIdentity.CustomIdentity
{
    public interface IUserLoginRepository
    {
        Task<IdentityResult> AddLoginAsync(Guid userId, UserLoginInfo login);
        Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey);
        Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user);
        Task<IdentityResult> RemoveLoginAsync(Guid userId, string loginProvider, string providerKey);
    }
}