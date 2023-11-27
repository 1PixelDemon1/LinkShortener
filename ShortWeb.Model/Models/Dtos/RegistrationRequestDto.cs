namespace ShortWeb.Model.Models.Dtos
{
    // When somebody tries to Register new user
    // he must pass this model.
    public class RegistrationRequestDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
