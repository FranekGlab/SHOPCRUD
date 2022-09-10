    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHOPCRUD.Data;
using SHOPCRUD.Models;
using SHOPCRUD.Models.DomainModels;

namespace SHOPCRUD.Controllers
{
    public class WatchController : Controller
    {
        private readonly DataBaseContext dataBaseContext;

        public WatchController(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

           var watch = await dataBaseContext.Watches.ToListAsync();
           return View(watch);
        }

        [HttpGet("{BrandName}")]
        public async Task<IActionResult> Index(string Watchsearch)
        {
            ViewData["Getwatchdetails"] = Watchsearch;

            var watchquery = from x in dataBaseContext.Watches select x;
            if (!String.IsNullOrEmpty(Watchsearch))
            {
                watchquery = watchquery.Where(x => x.BrandName.Contains(Watchsearch) || x.ModelName.Contains(Watchsearch)); 
            }
            return View(await watchquery.AsNoTracking().ToListAsync());
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddWatchViewModel addWatchRequest)
        {
            var watch = new Watch()
            {
                Id = Guid.NewGuid(),
                BrandName = addWatchRequest.BrandName,
                ModelName = addWatchRequest.ModelName,
                Price = addWatchRequest.Price,
                ReferenceNumber = addWatchRequest.ReferenceNumber
            };

            await dataBaseContext.Watches.AddAsync(watch);
            await dataBaseContext.SaveChangesAsync();
            return RedirectToAction("Index"); 

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var watch = await dataBaseContext.Watches.FirstOrDefaultAsync(x => x.Id == id);

            if (watch != null)
            {
                var viewModel = new UpdateWatch()
                {
                    Id = watch.Id,
                    BrandName = watch.BrandName,
                    ModelName = watch.ModelName,
                    Price = watch.Price,
                    ReferenceNumber = watch.ReferenceNumber
                };

                return await Task.Run(() => View("View", viewModel));
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateWatch model)
        {
            var watch = await dataBaseContext.Watches.FindAsync(model.Id);

            if (watch != null)
            {
                watch.BrandName = model.BrandName;
                watch.ModelName = model.ModelName;
                watch.Price = model.Price;
                watch.ReferenceNumber = model.ReferenceNumber;

                await dataBaseContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateWatch model)
        {
            var watch = await dataBaseContext.Watches.FindAsync(model.Id);

            if (watch != null)
            {
                dataBaseContext.Watches.Remove(watch);
                await dataBaseContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
