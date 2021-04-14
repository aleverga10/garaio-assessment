using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garaio_Assessment.Controllers
{
    public class ResultsController
    {
        // singleton pattern
        private static ResultsController instance;

        private ResultsController()
        {
            this.results = new List<Result>();
        }

        public static ResultsController Get()
        {
            if (instance is null)
                instance = new ResultsController();
            return instance;

        }


        // results to be shown -> list with for each ip:
        // - client ip address
        // - fully qualified domain name of the ip ( ??? )
        // - number of calls 
        public struct Result
        {
            public string IPAddress; // like a "key" in dict or db
            public string FQDN;
            public long NumberOfCalls;

            // two results are the same if they have the same ip address
            public override bool Equals(object obj)
            {
                return this.IPAddress.Equals(((Result)obj).IPAddress);
            }

            public Result(string IP)
            {
                this.IPAddress = IP;
                this.FQDN = "";
                this.NumberOfCalls = -1;
            }
        }


        public List<Result> results; 

        public void UpdateResults(string IPAddress, string fqdn)
        {
            // do we have a results entry with this ip address?
            if (this.results.Exists(x => x.Equals(new Result(IPAddress))))
            {
                // if we do, update its number of calls / fqdn 
                int i = this.results.FindIndex(x => x.Equals(new Result(IPAddress)));

                Result result = new Result(results[i].IPAddress);
                result.NumberOfCalls = this.results[i].NumberOfCalls + 1;
                result.FQDN = this.results[i].FQDN;
                if (fqdn != "") 
                    result.FQDN = fqdn;

                // we acted on a copy of the list entry......
                this.results[i] = result;
            }

            else
            {
                // if we dont, add this ip entry
                this.results.Add(new Result(IPAddress));
                int i = this.results.FindIndex(x => x.Equals(new Result(IPAddress)));

                Result result = new Result(results[i].IPAddress);
                result.NumberOfCalls = 1;
                if (fqdn != "")
                    result.FQDN = fqdn;

                // we acted on a copy of the list entry......
                this.results[i] = result;
            }
        }

    }
} 