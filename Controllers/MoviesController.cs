using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payment_Gateway.Data;
using Payment_Gateway.Models;
using Razorpay.Api;

namespace Payment_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;//Git Hub Sandy
        public MoviesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>>GetAllMovies()
        {
            var data = _context.Movies.ToListAsync();
            if (data == null) return NotFound();
            return await data;
            
        }
    }
}
