using BankSystem.APPDBCONTEXT;
using BankSystem.Entities;
using BankSystem.EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CustomerController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var query = await _context.customers
                                      .Include(x => x.bank)
                                      .Include(x => x.Transactions)
                                      .Select(x => new { x.Id, x.Name, x.gmail, x.phone, x.dateOfBirth, x.address, x.accountId, x.BandId})
                                      .OrderBy(x => x.Name).ToListAsync();

            if (query is null)
                return BadRequest("not exist Customers");

            return Ok(query);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var query = await _context.customers
                                      .Include(x => x.bank)
                                      .Include(x => x.Transactions)
                                      .Select(x => new { x.Id, x.Name, x.gmail, x.phone, x.dateOfBirth, x.address, x.accountId, x.BandId })
                                      .OrderBy(x => x.Name)
                                      .SingleOrDefaultAsync(x=>x.Id==id);

            if (query is null)
                return BadRequest("Customer is not exist");

            return Ok(query);
        }



        [HttpPost]
        public async Task<IActionResult> addCustomer([FromBody] CustomerDTO dto)
        {

            Customer customer = new Customer()
            {
                Name = dto.Name,
                accountId = dto.accountId,
                gmail = dto.gmail,
                address = dto.address,
                phone= dto.phone,
                BandId=dto.BandId,
                dateOfBirth = dto.dateOfBirth
                
            };

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (customer is null)
                return BadRequest("customer is null");

            else
            {
                await _context.customers.AddAsync(customer);
                _context.SaveChanges();
            }

            return Created("", customer);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> updateCustomer([FromBody] CustomerDTO dto, int id)
        {
            var search = await _context.customers.FindAsync(id);

            if (search is null)
                return BadRequest("Customer is null");

            if (dto is null)
                return BadRequest("CustomerDTO is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");
            else
            {

                search.Name = dto.Name;
                search.accountId = dto.accountId;
                search.gmail = dto.gmail;
                search.address = dto.address;
                search.phone = dto.phone;
                search.BandId = dto.BandId;
                search.dateOfBirth = dto.dateOfBirth;

                _context.Update(search);
                _context.SaveChanges();
            }

            return Ok(search);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCustomer(int id)
        {
            var search = await _context.customers.FindAsync(id);

            if (search is null)
                return BadRequest("customer is not exist");

            return Ok(search);
        }

    }
}
