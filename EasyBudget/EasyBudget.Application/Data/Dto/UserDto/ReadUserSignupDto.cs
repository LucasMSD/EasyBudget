namespace EasyBudget.Data.Dto.UserDto
{
    public class ReadUserSignupDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birth { get; set; }
    }
}
