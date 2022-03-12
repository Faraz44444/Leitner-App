namespace TbxPortal.Web.Dto.Account
{
    public class UpdatePasswordRequestDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmedNewPassword { get; set; }
    }
}