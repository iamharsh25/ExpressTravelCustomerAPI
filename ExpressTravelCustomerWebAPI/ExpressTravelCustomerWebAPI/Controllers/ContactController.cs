using ExpressTravelCustomerWebAPI.Data;
using ExpressTravelCustomerWebAPI.Models;
using ExpressTravelCustomerWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpressTravelCustomerWebAPI.Controllers
{
    public class ContactController : Controller
    {
        public readonly CustomersContext _context;
        private readonly ICustomerService _customerService = new CustomerService();
        public IActionResult Index()
        {
            return View();
        }
        public ContactController(CustomersContext customersContext)
        {
            _context = customersContext;
        }

        [HttpGet("api/Reference")]
        public IActionResult GetReferenceEntity()
        {
            var temp = _customerService.GetReference(_context);

            if (temp == null)
                return StatusCode(404, "No Reference Found");
            else
                return Ok(temp);
        }

        [HttpGet("api/Contact")]
        public IActionResult GetContactEntity()
        {
            var temp = _customerService.GetContacts(_context);

            if (temp == null)
                return StatusCode(404, "No Contact Found");
            else
                return Ok(temp);
        }

        [HttpGet("api/Contact/{Id}")]
        public IActionResult GetContactEntityById([FromRoute]int Id)
        {
            var temp = _customerService.GetContactById(_context, Id);

            if (temp == null)
                return StatusCode(404, "No Contact Found");
            else
                return Ok(temp);
        }

        [HttpPost("api/Contact")]
        public IActionResult PostContactEntity([FromBody] ContactInfo entity)
        {
            var temp = _customerService.InsertContact(_context, entity);
            if (temp == null)
                return StatusCode(404, "Error: Unable to Insert Contact");
            else
                return Ok(temp);
        }

        [HttpPut("api/Contact/{Id}")]
        public IActionResult PutContactEntity([FromBody] ContactInfo entity, int Id)
        {
            var temp = _customerService.UpdateContact(_context, entity, Id);

            if (temp != null)
                return Ok(temp);
            else
                return StatusCode(404, "Error: Unable to Update Contact");
        }

        [HttpDelete("api/Contact/{Id}")]
        public IActionResult DeleteContactEntity(int Id)
        {
            var temp = _customerService.DeleteContact(_context, Id);
            if (!temp)
                return StatusCode(404, "Error: Unable to Delete User");
            else
                return Ok(temp);
        }
    }
}
