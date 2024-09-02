using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
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


        [HttpGet("notfound")] //Get : api/Buggy/notfound

        public ActionResult GetNotFound()
        {
            var product = _dbcontext.Products.Find(100);

            if (product is null) return NotFound(new ApiResponse(404));

            return Ok(product);
        }

        [HttpGet("servererror")] //Get : api/Buggy/servererror
        public ActionResult GetServerError(int id)
        {
            var product = _dbcontext.Products.Find(100);

            var productToReturn = product.ToString();
            return Ok(productToReturn);

        }

        [HttpGet("badrequest")]//Get : api/Buggy/badrequest
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
                                            //to Make Error Send The Id As String..../one
        [HttpGet("badrequest/{id}")]//Get : api/Buggy/badrequest/10
        public ActionResult GetBadRequest(int id)// Validation Error
        {
            return Ok();
        }

        [HttpGet("unauthorized")]//Get : api/Buggy/unauthorized
        public ActionResult GetUnauthorizedError()
        {
            return Unauthorized(new ApiResponse(401));
        }


    }
}
