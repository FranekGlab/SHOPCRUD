using SHOPCRUD.Models;
using SHOPCRUD.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using SHOPCRUD.Data;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            var clients = await databaseContext.Clients.ToListAsync();
            return View(clients);
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
            TempData["Success"] = "Created Successfully";
            return RedirectToAction("Index");

        }


        [HttpGet]
        public async Task<IActionResult> ViewAsync(Guid id)
        {
            var client = await databaseContext.Clients.FirstOrDefaultAsync(x => x.Id == id);

            if (client != null)
            {
                var viewModel = new UpdateClient()
                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    Address = client.Address,
                    TelNubmer = client.TelNubmer,
                    DateOfBirth = client.DateOfBirth
                };
                TempData["Success"] = "Updated Successfully";
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateClient model)
        {
            var client = await databaseContext.Clients.FindAsync(model.Id);

            if(client != null)
            {
                client.Name = model.Name;
                client.Email = model.Email;
                client.Address = model.Address;
                client.TelNubmer = model.TelNubmer;
                client.DateOfBirth = model.DateOfBirth;

                await databaseContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateClient model)
        {
            var client = await databaseContext.Clients.FindAsync(model.Id);

            if (client != null)
            {
                databaseContext.Clients.Remove(client);
                await databaseContext.SaveChangesAsync();
                TempData["Success"] = "Deleted Successfully";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

    }
}
    