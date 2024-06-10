using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjMongoDBAPI.Models;
using ProjMongoDBAPI.Models.DTO;
using ProjMongoDBAPI.Services;

namespace ProjMongoDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersControllers : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly AddressService _addressService;
        public CustomersControllers(CustomerService customerService,AddressService addressService)
        {
            _customerService = customerService;   
            _addressService = addressService;
           
        }
        [HttpGet]
        public ActionResult<List<Customer>> Get() => _customerService.GetAll();

        [HttpGet("{id:length(24)}", Name = "GetCustomer")]
        public ActionResult<Customer> Get(string id) => _customerService.GetById(id);
        [HttpGet("{cep:length(8)}",Name = "GetAddressByAPI")]
        public ActionResult<AddressDTO> GetPostOffice(string cep)
        {
            return PostOfficeServices.GetAddress(cep).Result;
        }

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            customer.Address = _addressService.Create(customer.Address);
            _customerService.Create(customer);
            return customer == null ? BadRequest() : CreatedAtRoute("GetCustomer", new { id = customer.Id.ToString() }, customer);
           
        }
    }
}
