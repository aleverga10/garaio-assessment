using System;

namespace Garaio_Assessment.Models.Parameters
{
    public class ClientIP : IIS_Parameter
    {
        public const string PARAM_NAME = "c-ip";

        public ClientIP()
        {
            this.paramType = IIS_Parameters.ClientIP;
            this.logOrder = -1;
            this.lastContent = ""; 
        }

        public override bool IsParseable(string token)
        {
            // an ip address has a fixed string structure
            token.Trim();

            String[] tokens = token.Split('.');
            if (tokens.Length == 4)
            {
                foreach (String num in tokens)
                {
                    // check ip numbers to see if this is an ip
                    if ((int.TryParse(num, out int n) == false) || (n < 0) || (n > 255))
                        return false;
                }

            }
            else
            {
                // it is not a valid ip address
                // but it could still be "::1", which is valid, its like localhost
                if (token.Equals("::1"))
                    return true;

                return false;

            }

            return true;
        }
    }
}