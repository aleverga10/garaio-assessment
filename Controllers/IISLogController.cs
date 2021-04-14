using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Garaio_Assessment.Controllers
{
    public class IISLogController : ApiController
    {
        public IEnumerable<ResultsController.Result> Get()
        {
            
            FileController.Get().AnalyzeFile();

            return ResultsController.Get().results; 
        }
    }
}