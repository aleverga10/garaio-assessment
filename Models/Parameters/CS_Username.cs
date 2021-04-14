namespace Garaio_Assessment.Models.Parameters
{
    public class CS_Username : IIS_Parameter
    {
        public const string PARAM_NAME = "cs-username";
        //public const string PARAM_NAME = "cs(Referer)";
        

        public CS_Username()
        {
            this.paramType = IIS_Parameters.Username;
            this.logOrder = -1;
            this.lastContent = "";
        }

        public override bool IsParseable(string token)
        {
            token.Trim();
            // no restrictions on usernames i guess 
            return true;
        }
    }
}
