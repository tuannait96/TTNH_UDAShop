using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TTNH_UDAShop.Service;
using TTNH_UDAShop.Web.Infrastructure.Core;

namespace TTNH_UDAShop.Web.Api
{
    [RoutePrefix("api/home")]
    // buộc phải đăng nhập
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        // controller nào cũng phải có kế thừa contractor
        IErrorService _errorService;
        public HomeController(IErrorService errorService) : base(errorService)
        {
            this._errorService = errorService;
        }//

        [HttpGet]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Hello world. ";
        }
    }
}