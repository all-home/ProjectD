using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LOP.Models;
using Microsoft.AspNetCore.Authorization;

namespace LOP.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class SearchPersonController : Controller
    {
        private readonly PersonContext _context;

        public SearchPersonController(PersonContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async  Task<IActionResult> Index()
        {
            return View("Index");

        }
                   
        public async Task<IActionResult> Result(int? TagId)
        {
            if (TagId == null)
            {
                return View("Index");
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.TagId == TagId);
            if (person == null)
            {
               return View("Index");
            }
            else

            {
                return View(person);
            }
        }
                                
               
    }
}
