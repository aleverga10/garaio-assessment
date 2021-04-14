using Garaio_Assessment.Models;
using Garaio_Assessment.Models.Parameters;

namespace Garaio_Assessment.Controller
{
    public class ParameterFactory
    {
        // singleton pattern
        private static ParameterFactory instance; 

        private ParameterFactory() 
        { 
        }

        public static ParameterFactory Get()
        {
            if (instance is null)
                instance = new ParameterFactory();
            return instance; 

        }

        public IIS_Parameter CreateParameter(string param)
        {
            switch (param)
            {
                // only params needed for this assessment have a class
                case ClientIP.PARAM_NAME: return new ClientIP();
                case CS_Username.PARAM_NAME: return new CS_Username();
                
                default: return null;
            }
        }

    }
}