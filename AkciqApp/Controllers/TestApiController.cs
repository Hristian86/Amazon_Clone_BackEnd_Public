namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

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
        [Authorize]
        //[IgnoreAntiforgeryToken]
        public ActionResult AntiForgeryToken()
        {
           var token = this.antiforgery.ValidateRequestAsync(this.HttpContext);

            return this.Ok("token valid");
        }
    }
}