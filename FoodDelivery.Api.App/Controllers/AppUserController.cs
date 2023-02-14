using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Api.DAL.Common.Entities.Auth;
using FoodDelivery.Api.DAL.EF;
using Microsoft.AspNetCore.Authorization;

namespace FoodDelivery.Api.App.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly FoodDeliveryDbContext _context;

        public AppUserController(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        // GET: api/AppUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserEntity>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/AppUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUserEntity>> GetAppUserEntity(Guid id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var appUserEntity = await _context.Users.FindAsync(id);

            if (appUserEntity == null)
            {
                return NotFound();
            }

            return appUserEntity;
        }

        // PUT: api/AppUser/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUserEntity(Guid id, AppUserEntity appUserEntity)
        {
            if (id != appUserEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(appUserEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AppUser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppUserEntity>> PostAppUserEntity(AppUserEntity appUserEntity)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'FoodDeliveryDbContext.Users'  is null.");
          }
            _context.Users.Add(appUserEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppUserEntity", new { id = appUserEntity.Id }, appUserEntity);
        }

        // DELETE: api/AppUser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppUserEntity(Guid id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var appUserEntity = await _context.Users.FindAsync(id);
            if (appUserEntity == null)
            {
                return NotFound();
            }

            _context.Users.Remove(appUserEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppUserEntityExists(Guid id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
