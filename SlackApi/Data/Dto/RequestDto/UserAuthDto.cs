namespace SlackApi.Data.Dto.RequestDto
{
    public class UserAuthDto
    {

        public string UserName { get; set; }

        public string Password { get; set; }    =string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;

        public string PhotoUrl { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }



    }
}
