using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SitesController : Controller
    {
        BookingContext ctx;

        public SitesController(BookingContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get()
        {
            var sites = ctx.Sites.ToList();
            return Ok(sites);
        }
    }
}