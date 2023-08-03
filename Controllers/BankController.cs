using BankSystem.APPDBCONTEXT;
using BankSystem.Entities;
using BankSystem.EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BankController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBanks()
        {
            var query = await _context.banks
                              .Include(x => x.accounts)
                              .Include(x => x.customers)
                              .Select(x => new { x.Id, x.NameBank, x.address, x.phone, x.NumberBranches})
                              .ToListAsync();

            if (query is null)
                return BadRequest("not exist Banks");

            return Ok(query);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankById(int id)
        {
            var query = await _context.banks
                              .Include(x => x.accounts)
                              .Include(x => x.customers)
                              .Select(x => new { x.Id, x.NameBank, x.address, x.phone, x.NumberBranches })
                              .SingleOrDefaultAsync(x=>x.Id==id);

            if (query is null)
                return BadRequest("Bank is not exist");

            return Ok(query);
        }



        [HttpPost]
        public async Task<IActionResult> addBank([FromBody] BankDTO dto)
        {
           
            Bank bank = new Bank()
            {
                NameBank = dto.NameBank,
                NumberBranches = dto.NumberBranches,
                phone = dto.phone,
                address=dto.address

            };

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (bank is null)
                return BadRequest("bank is null");

            else
            {
                await _context.banks.AddAsync(bank);
                _context.SaveChanges();
            }

            return Created("", bank);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> updateBank([FromBody] BankDTO dto, int id)
        {
            var search = await _context.banks.FindAsync(id);

            if (search is null)
                return BadRequest("Bank is null");

            if (dto is null)
                return BadRequest("BankDTO is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            else
            {

                search.NameBank = dto.NameBank;
                search.phone = dto.phone;
                search.NumberBranches = dto.NumberBranches;
                search.address = dto.address;

                _context.Update(search);
                _context.SaveChanges();
            }

            return Ok(search);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteStudent(int id)
        {
            var search = await _context.banks.FindAsync(id);

            if (search is null)
                return BadRequest("Bank is not exist");

            return Ok(search);
        }



    }
}
