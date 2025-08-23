using System.Threading.Tasks;
using Abp.Application.Services;
using AZMAdmin.CMS.Sessions.Dto;

namespace AZMAdmin.CMS.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
