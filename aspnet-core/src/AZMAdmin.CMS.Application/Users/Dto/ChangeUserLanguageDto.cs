using System.ComponentModel.DataAnnotations;

namespace AZMAdmin.CMS.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}