using APIWebCS5.DTOs.Account;
using APIWebCS5.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace APIWebCS5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly DogAndCatContext _context;
        public AccountController(DogAndCatContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllAccount()
        {
            var acc = _context.Accounts.ToList().Select(a => a.ToAccountDto());
            return Ok(acc); 
        }
        [HttpGet("{id}")]
        public IActionResult GetAccountById([FromRoute]int id)
            {
                var account = _context.Accounts.FirstOrDefault(a => a.Id == id);
                if (account == null)
                {
                    return NotFound();
                }
                return Ok(account);
            }
        //thêm tài khoản
        [HttpPost]
            public IActionResult Create([FromBody] CreateAccountRequestDTO accDTO)
            {

                var account = accDTO.ToAccountFromCreateDTO();
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetAccountById), new { id = account.Id }, account.ToAccountDto());
            }
            private bool Login(Account acc)
            {
                if(acc == null )
                {
                    return false;
                }

            return true;
        }
        [HttpPost("/Login")]
        public IActionResult CheckLogin([FromForm] string psw, string userName)
        {
            var acc = _context.Accounts.FirstOrDefault(a => a.UserName == userName && a.Password == psw);
            bool text = Login(acc);
            if (text)
            {
                var accNot = acc.ToAccountDto();
                return Ok(accNot);  
            }
            return BadRequest();
        }
    }

}
