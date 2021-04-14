using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garaio_Assessment.Models
{
    public abstract class IIS_Parameter
    {
        public enum IIS_Parameters
        { 
            Date, Time, ClientIP, Username, ServerIP, ServerPort, Method, URI_Stem, URI_Query, HTTP_Status, UserAgent, Protocol_Substatus, // defaults
            Server_Sitename, ServerName, Win32Status, BytesSent, BytesReceived, TimeTaken, Protocol_Version, HostHeader, Cookie, Referrer  // optionals
            // source: provided log file + stackify.com/how-to-interpret-iis-logs/
        }

        public /*readonly*/ IIS_Parameters paramType { get; set; } // readonly is c# 8.0...

        public int logOrder { get; set; } // this gets specified in the file with #Fields
        public abstract Boolean IsParseable(string token); // is the string in input a param of this type ? (eg. is this an ip address?)
        public string lastContent; // last found content for this parameter (e.g. "127.0.0.1" for our client ip field)

        public override bool Equals(object obj)
        {
            // two params are equal if they are of the same type
            return this.paramType.Equals((obj as IIS_Parameter).paramType);
        }
    }
}