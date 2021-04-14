# garaio-assessment
 
To execute the assessment, open the solution with visual studio and run it from there; then <b>visit /api/iislog</b>.

You should be presented with the requested list, formatted in JSON, which i also provided as a pastebin to mr. Chirra.

The starting point of code analysis is the class <b>IISLogController</b>, which inherits ApiController and listens to the get call.


### Assumption: 
there wasn't a FQDN field among the logged parameters. In the official iis log file documentation ( https://docs.microsoft.com/en-us/previous-versions/iis/6.0-sdk/ms524845(v=vs.90) ), nor in this tutorial i consulted ( https://stackify.com/how-to-interpret-iis-logs/ ) was ever a FQDN field mentioned. 
As far as i know, a Fully Qualified Domain Name is an url: the only url available in this log file are <u>https(:)//10.10.10.10/</u> (shared among many different ips) <u>https(:)//localhost</u> and <u>https(:)//wiki.garaio.com/</u> (shared among many ips). 
Since this was not a satisfying answer and had too many overlappings, the requested parameter was assumed to be "cs-username" . The logic behind the solution does not change. 
