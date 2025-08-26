namespace AZMAdmin.CMS.Attachments.Dto
{
    public class HomeBannerAttachmentsDto
    {
        public HomeBannerAttachmentsDto()
        {
            Image = new AttachmentDto();
            Logo = new AttachmentDto();
        }
        public AttachmentDto? Image { get; set; }
        public AttachmentDto? Logo { get; set; }
    }
}