using System.Threading.Tasks;

namespace AZMAdmin.CMS.ContentCategories
{
    public interface IContentCategoryAppService
    {
        Task ActivateDeactivateContentCategory(int id);
    }
}