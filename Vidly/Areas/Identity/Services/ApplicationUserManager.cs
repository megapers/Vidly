using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.Areas.Identity.Services
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<ApplicationUser> passwordHasher, 
            IEnumerable<IUserValidator<ApplicationUser>> userValidators, 
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, 
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            IServiceProvider services, 
            ILogger<UserManager<ApplicationUser>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, 
                  passwordValidators, keyNormalizer, errors, services, logger)
        {

        }

        public async Task<string> GetDrivingLicenseAsync(ClaimsPrincipal principal)
        {
            var user = await GetUserAsync(principal);
            return user.DrivingLicense;
        }

        public async Task<string> GetPhoneAsync(ClaimsPrincipal principal)
        {
            var user = await GetUserAsync(principal);
            return user.Phone;
        }

        public virtual async Task<IdentityResult> SetDrivingLicenseAsync(ApplicationUser user, string drivingLicense)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.DrivingLicense = drivingLicense;
            return await UpdateUserAsync(user);
        }

        public virtual async Task<IdentityResult> SetPhoneAsync(ApplicationUser user, string phone)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.Phone = phone;
            return await UpdateUserAsync(user);
        }
    }
}
