using System.Threading.Tasks;

namespace AZMAdmin.CMS.Contents
{
    public interface IContentAppService
    {
        Task ActivateDeactivateContent(int id);
    }
}