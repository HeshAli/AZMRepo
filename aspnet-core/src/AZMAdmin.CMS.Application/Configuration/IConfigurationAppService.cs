using System.Threading.Tasks;
using AZMAdmin.CMS.Configuration.Dto;

namespace AZMAdmin.CMS.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
