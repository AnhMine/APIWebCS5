namespace APIWebCS5.DTOs.Account
{
    public class AccountDTO
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public string? FullName { get; set; }

        public string? Address { get; set; }

        public bool? AccountType { get; set; }
    }
}
