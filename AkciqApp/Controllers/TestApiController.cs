using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkciqApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestApiController : ControllerBase
    {
        private readonly IAntiforgery antiforgery;

        public TestApiController(IAntiforgery antiforgery)
        {
            this.antiforgery = antiforgery;
        }

        [HttpPost]
        //[IgnoreAntiforgeryToken]
        public async Task<ActionResult> AntiForgeryToken()
        {
            var cookies = this.HttpContext.Request;

            var token = await this.antiforgery.IsRequestValidAsync(this.HttpContext);

            return this.Ok("token valid");
        }
    }
}