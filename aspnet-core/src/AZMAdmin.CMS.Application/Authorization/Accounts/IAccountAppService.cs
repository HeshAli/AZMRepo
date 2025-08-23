using System.Threading.Tasks;
using Abp.Application.Services;
using AZMAdmin.CMS.Authorization.Accounts.Dto;

namespace AZMAdmin.CMS.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
