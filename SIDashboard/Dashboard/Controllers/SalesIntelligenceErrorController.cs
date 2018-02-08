using Dashboard.Shared;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.BusinessAI.Dashboard.Common.Shared;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Dashboard.Controllers
{
    public class SalesIntelligenceErrorController : ApiController
    {
        // GET: api/SalesIntelligenceError
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", "value3" };
        }

        // GET: api/SalesIntelligenceError/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SalesIntelligenceError
        [HttpPost]
        public void Post([FromBody]string body)
        {
            throw new NotImplementedException();
        }

        // PUT: api/SalesIntelligenceError/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/SalesIntelligenceError/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
