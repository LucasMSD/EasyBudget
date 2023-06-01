namespace EasyBudget.Data.Dto.UserDto
{
    public class UserSignupDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime Birth { get; set; }
    }
}
