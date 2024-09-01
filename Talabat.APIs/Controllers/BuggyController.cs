using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{

    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _dbcontext;

        public BuggyController(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        [HttpGet("notfound")] //Get : api/buggy/notfound

        public ActionResult GetNotFound()
        {
            var product = _dbcontext.Products.Find(100);

            if (product is null) return NotFound();

            return Ok(product);
        }

        [HttpGet("servererror")] //Get : api/buggy/servererror
        public ActionResult GetServerError(int id)
        {
            var product = _dbcontext.Products.Find(100);

            var productToReturn = product.ToString();
            return Ok(productToReturn);

        }

        [HttpGet("badrequest")]//Get : api/buggy/badrequest
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }
                                            //to Make Error Send The Id As String..../one
        [HttpGet("badrequest/{id}")]//Get : api/buggy/badrequest/10
        public ActionResult GetBadRequest(int id)// Validation Error
        {
            return Ok();
        }


    }
}
