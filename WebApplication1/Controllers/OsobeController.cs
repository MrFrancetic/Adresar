using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Domain;

namespace WebApplication1.Controllers
{
    public class OsobeController : Controller
    {
        private readonly MVCDbContext mvcDbContext;

        public OsobeController(MVCDbContext mvcDbContext) {

            this.mvcDbContext = mvcDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var osobe = await mvcDbContext.Adresar.ToListAsync();
			return View(osobe);

		}


		[HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddOsobaViewModel dodajOsobuRequest)
        {
            var osoba = new Osoba()
            {
                Id = Guid.NewGuid(),
                Ime = dodajOsobuRequest.Ime,
                Prezime = dodajOsobuRequest.Prezime,
                GlavniBr = dodajOsobuRequest.GlavniBr,
                BrojMob = dodajOsobuRequest.BrojMob,
                Fax = dodajOsobuRequest.Fax,
                Ulica = dodajOsobuRequest.Ulica,
                KucniBr = dodajOsobuRequest.KucniBr,
                Grad = dodajOsobuRequest.Grad,
                PostanskiBr = dodajOsobuRequest.PostanskiBr,
                Email = dodajOsobuRequest.Email
            };

            await mvcDbContext.Adresar.AddAsync(osoba);
            await mvcDbContext.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var osoba= await mvcDbContext.Adresar.FirstOrDefaultAsync(x => x.Id == id);

            if (osoba != null)
             { 
                var viewModel = new UpdateOsobaViewModel()
                {
                    Id = Guid.NewGuid(),
                    Ime = osoba.Ime,
                    Prezime = osoba.Prezime,
                    GlavniBr = osoba.GlavniBr,
                    BrojMob = osoba.BrojMob,
                    Fax = osoba.Fax,
                    Ulica = osoba.Ulica,
                    KucniBr = osoba.KucniBr,
                    Grad = osoba.Grad,
                    PostanskiBr = osoba.PostanskiBr,
                    Email = osoba.Email
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateOsobaViewModel model)
        {
            var osoba = await mvcDbContext.Adresar.FindAsync(model.Id);

            if(osoba != null)
            {
                osoba.Ime = model.Ime;
                osoba.Prezime = model.Prezime;
                osoba.GlavniBr= model.GlavniBr;
                osoba.BrojMob= model.BrojMob;
                osoba.Fax = model.Fax;
                osoba.Ulica = model.Ulica;
                osoba.KucniBr = model.KucniBr;
                osoba.Grad = model.Grad;
                osoba.PostanskiBr= model.PostanskiBr;
                osoba.Email = model.Email;

                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateOsobaViewModel model)
        {
            var osoba= await mvcDbContext.Adresar.FindAsync(model.Id);
        
            if(osoba != null )
            {
                mvcDbContext.Adresar.Remove(osoba);
                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
