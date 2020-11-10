using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace customers.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CustomersController : ControllerBase
  {
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "customer 1", "customer 2", "customer 3" };
    }
  }
}
