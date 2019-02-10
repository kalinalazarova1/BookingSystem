using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SitesController : Controller
    {
        BookingContext ctx;

        public SitesController(BookingContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var sites = ctx.Sites.ToList();
            return Ok(sites);
        }

        [HttpGet("{id}", Name = "SiteGet")]
        public IActionResult Get(int id)
        {
            var site = ctx.Sites.FirstOrDefault(s => s.SiteId == id);
            return Ok(site);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Site model)
        {
            this.ctx.Add(model);
            if (await this.ctx.SaveChangesAsync() > 0)
            {
                var url = Url.Link("SiteGet", new { id = model.SiteId });
                return Created(url, model);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]Site model)
        {
            var oldSite = this.ctx.Sites.FirstOrDefault(s => s.SiteId == id);
            if(oldSite == null)
            {
                return NotFound();
            }

            oldSite.OwnerId = model.OwnerId ?? oldSite.OwnerId;
            oldSite.Location = model.Location ?? oldSite.Location;
            this.ctx.Add(oldSite);

            if (await this.ctx.SaveChangesAsync() > 0)
            {
                return Ok(oldSite);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var site = this.ctx.Sites.FirstOrDefault(s => s.SiteId == id);
            if (site == null)
            {
                return NotFound();
            }

            if (await this.ctx.SaveChangesAsync() > 0)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}