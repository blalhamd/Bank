using BankSystem.APPDBCONTEXT;
using BankSystem.Entities;
using BankSystem.EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TransactionController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var query = await _context.transactions
                              .Include(x=>x.Customers)
                              .Select(x=>new {x.Id,x.amount,x.dateTime,x.Customers})
                              .ToListAsync();

            if (query is null)
                return BadRequest("not exist Transactions");

            return Ok(query);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var query = await _context.transactions
                              .Include(x => x.Customers)
                              .Select(x => new { x.Id, x.amount, x.dateTime, x.Customers })
                              .FirstOrDefaultAsync(x=>x.Id==id);

            if (query is null)
                return BadRequest("Transaction is not exist");

            return Ok(query);
        }



        [HttpPost]
        public async Task<IActionResult> addTransaction([FromBody] TransactionDTO dto)
        {

            Transaction transaction = new Transaction()
            {
                amount= dto.amount,
                dateTime= dto.dateTime

            };

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (transaction is null)
                return BadRequest("transaction is null");

            else
            {
                await _context.transactions.AddAsync(transaction);
                _context.SaveChanges();
            }

            return Created("", transaction);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> updateTransaction([FromBody] TransactionDTO dto, int id)
        {
            var search = await _context.transactions.FindAsync(id);

            if (search is null)
                return BadRequest("transaction is null");

            if (dto is null)
                return BadRequest("transactionDTO is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");
            else
            {

                search.amount = dto.amount;
                search.dateTime = dto.dateTime;

                _context.Update(search);
                _context.SaveChanges();
            }

            return Ok(search);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteTransaction(int id)
        {
            var search = await _context.transactions.FindAsync(id);

            if (search is null)
                return BadRequest("Transaction is not exist");

            return Ok(search);
        }

    
}
}
