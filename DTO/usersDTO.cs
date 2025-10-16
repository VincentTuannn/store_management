namespace store_management.DTO
{
    public class usersDTO
    {
        private int user_id;
        private string username;
        private string password;
        private string full_name;
        private string role;
        private DateTime created_at;

        public usersDTO(int user_id, string username, string password, string full_name, string role, DateTime created_at)
        {
            this.User_id = user_id;
            this.Username = username;
            this.Password = password;
            this.Full_name = full_name;
            this.Role = role;
            this.Created_at = created_at;
        }

        public int User_id { get => user_id; set => user_id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Full_name { get => full_name; set => full_name = value; }
        public string Role { get => role; set => role = value; }
        public DateTime Created_at { get => created_at; set => created_at = value; }
    }
}
