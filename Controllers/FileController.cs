using Garaio_Assessment.Controller;
using Garaio_Assessment.Models;
using Garaio_Assessment.Models.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static Garaio_Assessment.Models.IIS_Parameter;

namespace Garaio_Assessment.Controllers
{
    public class FileController
    {
        // singleton pattern
        private static FileController instance;

        private FileController()
        {
        }

        public static FileController Get()
        {
            if (instance is null)
                instance = new FileController();
            return instance;
        }


        private List<IIS_Parameter> importantParams = new List<IIS_Parameter>();

        private const string PATH = @"Garaio-Assessment\assessment\IISLog.log";
        private StreamReader reader;
        private FileStream fileStream;

        // file open and close
        private void Open()
        {
            try 
            {
                //string path = Path.Combine(Environment.CurrentDirectory, PATH);
                string path = @"C:\Users\Ale\source\repos\Garaio-Assessment\assessment\IISLog.log";
                FileInfo info = new FileInfo(path);
                fileStream = info.OpenRead();
                reader = new StreamReader(fileStream);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Close()
        {
            reader.Close();
            fileStream.Close();
        }


        private String ReadLine()
        {
            String line = null;
            try { line = reader.ReadLine(); }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }

            return line;
        }

        private void ParseLine(String line)
        {
            // ignore string starting with "#" unless it is #Fields (else branch always gets executed first)
            if (line.StartsWith("#") == false)
            {
                // tokenize the line
                List<string> tokens = line.Split(' ').ToList<string>();

                string currIP = "", currFQDN = ""; 

                // we should already know our important param position in the string, so we try to parse it
                foreach (IIS_Parameter importantParam in importantParams)
                {
                    string content = tokens[importantParam.logOrder];
                    // update results (= num of calls for this ip, fqdn)
                    if (importantParam.paramType == IIS_Parameters.ClientIP)
                    {
                        if (importantParam.IsParseable(content))
                            currIP = content; // save it to map in the dict                                  
                        else
                        {
                            // content isnt parseable, so it isnt what we expected to be (ie. it isnt an IP address)
                            Console.WriteLine("ERROR: param " + importantParam.paramType + " content is not parseable");
                            // are these things errors ? like "::1%0" what even are these
                            currIP = "ANOMALY: " + content;
                            currFQDN = "is this an error?";
                        }

                    }
                    else
                    {
                        if ((importantParam.paramType == IIS_Parameters.Username) && (content != "-"))
                        {
                            if (importantParam.IsParseable(content))
                                currFQDN = content; // save it to map in the dict
                        }

                        // other important params go here, maybe use a switch case
                    }

                    // overwrite param last content
                    //if (importantParam.lastContent.Equals(content) == false)
                    //    importantParam.lastContent = content; // unused but may be useful

                }

                
                ResultsController.Get().UpdateResults(currIP, currFQDN);

            }

            else
            {
                // we can use this line to establish the param order, so that we know exatcly "where" to find the param we need ( = which token number)
                if (line.StartsWith("#Fields:"))
                {
                    importantParams.Clear();

                    List<string> fields = line.Split(' ').ToList<string>();

                    foreach (string field in fields)
                    {
                        IIS_Parameter token = ParameterFactory.Get().CreateParameter(field);
                        if (token != null)
                        {
                            token.logOrder = fields.IndexOf(field) -1; // -1 bc of "#Fields:"
                            importantParams.Add(token);
                        }
                    }

                }
            }
        }

        private void ErrorCheck(string content)
        {
            // after parsing line check for each importantParam their content
        }

        public void AnalyzeFile()
        {
            Open();
            if (reader != null)
            {
                string line = ReadLine();
                while (line != null)
                {
                    ParseLine(line);
                    line = ReadLine(); // read next line
                }
            }

        }
    }
}