using System.Threading.Tasks;
using AZMAdmin.CMS.Models.TokenAuth;
using AZMAdmin.CMS.Web.Controllers;
using Shouldly;
using Xunit;

namespace AZMAdmin.CMS.Web.Tests.Controllers
{
    public class HomeController_Tests: CMSWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}