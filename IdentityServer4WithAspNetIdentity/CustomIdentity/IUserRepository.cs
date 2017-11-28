using System;
using System.Threading.Tasks;
using IdentityServer4WithAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer4WithAspNetIdentity.CustomIdentity
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
        Task<ApplicationUser> FindByIdAsync(Guid userId);
        Task<ApplicationUser> FindByUserNameAsync(string userName);
    }
}