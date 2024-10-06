namespace ToDoListApp.DTOs
{
    public class LoginResponseDto
    {
        public int userId { get; set; }
        public string token { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }
}
