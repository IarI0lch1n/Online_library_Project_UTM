namespace Online_lib.Domain.Entities.User
{
    public class UserLoginResult
    {
        public bool Status { get; set; }
        public string StatusMsg { get; set; }
        public UDbTable User { get; set; }         
        public string HmacToken { get; set; }     

    }
}
