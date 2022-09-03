using SHOPCRUD.Models;
using SHOPCRUD.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using SHOPCRUD.Data;

namespace SHOPCRUD.Controllers
{
    public class ClientController : Controller
    {
        private readonly DataBaseContext databaseContext;

        public ClientController(DataBaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClientViewModel addClientRequest)
        {
            var client = new Client()
            {
                Id = Guid.NewGuid(),
                Name = addClientRequest.Name,
                Email = addClientRequest.Email,
                Address = addClientRequest.Address,
                TelNubmer = addClientRequest.TelNubmer,
                DateOfBirth = addClientRequest.DateOfBirth

            };

            await databaseContext.Clients.AddAsync(client);
            await databaseContext.SaveChangesAsync();
            return RedirectToAction("Add"); 

        }
    }
}
