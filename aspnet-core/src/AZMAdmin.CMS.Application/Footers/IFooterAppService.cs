using System.Threading.Tasks;

namespace AZMAdmin.CMS.Footers
{
    public interface IFooterAppService
    {
        Task ActivateDeactivateFooter(int id);
    }
}