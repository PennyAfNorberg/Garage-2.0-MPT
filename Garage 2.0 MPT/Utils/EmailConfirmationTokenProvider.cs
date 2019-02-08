using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Garage_2._0_MPT
{
    public class EmailConfirmationTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public EmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<DataProtectionTokenProviderOptions> options)
            : base(dataProtectionProvider, options)
        {
        }
        public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
        {

        }
    }
}
