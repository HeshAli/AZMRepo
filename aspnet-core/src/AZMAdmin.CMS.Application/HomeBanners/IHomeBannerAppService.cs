using System.Threading.Tasks;

namespace AZMAdmin.CMS.HomeBanners
{
    public interface IHomeBannerAppService
    {
        Task ActivateDeactivateHomeBanner(int id);
    }
}