using APIWebCS5.DTOs.Account;

namespace APIWebCS5.Mapper
{
    public static class AccountMappers
    {
        public static AccountDTO ToAccountDto(this Account account)
        {
            return new AccountDTO
            {
                Id = account.Id,
                UserName = account.UserName,
                Email = account.Email,
                Phone = account.PhoneNumber,
                FullName = account.FullName,
                Address = account.Address,
                AccountType = false,
            };
        }
        public static Account ToAccountFromCreateDTO(this CreateAccountRequestDTO acc)
        {
            return new Account
            {
                Id = acc.Id,
                UserName = acc.UserName,
                Password = acc.Password,
                Email = acc.Email,
                PhoneNumber = acc.Phone,
                FullName = acc.FullName,
                Address = acc.Address,


            };
        }
    }
}
