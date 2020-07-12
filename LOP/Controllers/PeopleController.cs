using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LOP.Models;
using LOP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Xml.Linq;


namespace LOP.Controllers
{
    [Authorize(Roles = "admin")]
    public class PeopleController : Controller
    {
        private readonly PersonContext _context;


        public PeopleController(PersonContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await _context.Persons.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Surname, Patronymic, Tel, Position,TagId,Image")] PersonModel person)
        {
            Person _person = new Person { Name = person.Name, Surname = person.Surname, Patronymic = person.Patronymic,
                Tel = person.Tel, Position = person.Position, TagId = person.TagId };
                       
            if (ModelState.IsValid && RidCheck(person.TagId))
            {
               
                if (person.Image != null)
                {
                    byte[] imageData = null;
                    
                    using (var binaryReader = new BinaryReader(person.Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)person.Image.Length);
                    }
                    
                    _person.Image = imageData;
                }
                _context.Add(_person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
 
            var person = await _context.Persons.FindAsync(id);
            PersonModel _person = new PersonModel{ Name = person.Name, Position = person.Position, TagId = person.TagId };

          

            if (_person == null)
            {
                return NotFound();
            }
            return View(_person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Position,Image,TagID")] PersonModel _person)
        {
            Person person = new Person { Name = _person.Name, Position = _person.Position, TagId = _person.TagId, id = id };
            if (id != person.id)
            {
                return NotFound();
            }
            if (RidCheck(person.TagId))
            {

                if (_person.Image != null)
                {
                    byte[] imageData = null;

                    using (var binaryReader = new BinaryReader(_person.Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)person.Image.Length);
                    }

                    person.Image = imageData;
                }
            }


                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(person);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PersonExists(person.id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(person);
            

        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.id == id);
        }

        private bool RidCheck(int TagID)
        {
            var person =  _context.Persons
                 .FirstOrDefault(m => m.TagId == TagID);
            if (person == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
