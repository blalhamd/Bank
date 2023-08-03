using BankSystem.APPDBCONTEXT;
using BankSystem.Entities;
using BankSystem.EntitiesDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var query = await _context.accounts
                                      .Include(x=>x.customer)
                                      .Include(x=>x.bank) 
                                      .Select
                                          (
                                             x=> new {x.Id,x.accountNumber,x.balance,x.bankId,
                                             x.customer.Name,x.customerId,x.bank.NameBank,
                                             x.bank.address,x.bank.NumberBranches,x.bank.phone }
                                          )
                                          .ToListAsync();

            if (query is null)
                return BadRequest("not exist Accounts");

            return Ok(query);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var query = await _context.accounts.
                                       Include(x => x.customer)
                                      .Include(x => x.bank)
                                      .Select
                                       (
                                          x => new {
                                              x.Id, x.balance, x.accountNumber,
                                              x.bankId, x.customer.Name, x.customerId,
                                              x.bank.NameBank, x.bank.address, x.bank.NumberBranches,
                                              x.bank.phone

                                       }).SingleOrDefaultAsync(x=>x.Id==id);
      

            if (query is null)
                return BadRequest("Account is not exist");

            return Ok(query);
        }



        [HttpPost]
        public async Task<IActionResult> addAccount([FromBody] AccountDTO dto)
        {

            Account account = new Account()
            {
                customerId=dto.customerId,
                accountNumber=dto.accountNumber,
                balance=dto.balance,
                bankId= dto.bankId,
                
            };

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (account is null)
                return BadRequest("account is null");

            else
            {
                await _context.accounts.AddAsync(account);
                _context.SaveChanges();
            }

            return Created("", account);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> updateAccount([FromBody] AccountDTO dto, int id)
        {
            var search = await _context.accounts.FindAsync(id);

            if (search is null)
                return BadRequest("account is null");

            if (dto is null)
                return BadRequest("accountDTo is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");
            else
            {

                search.customerId = dto.customerId;
                search.accountNumber = dto.accountNumber;
                search.balance = dto.balance;
                search.bankId = dto.bankId;

                _context.accounts.Update(search);
                _context.SaveChanges();
            }

            return Ok(search);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteAccount(int id)
        {
            var search = await _context.accounts.FindAsync(id);

            if (search is null)
                return BadRequest("account is not exist");

            return Ok(search);
        }


    }
}
