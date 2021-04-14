# garaio-assessment
 
To execute the assessment, open the solution with visual studio and run it from there; then <b>visit /api/iislog</b>.

You should be presented with the requested list, formatted in JSON, which i also provided as a pastebin to mr. Chirra.


<b>Assumption</b>: there wasn't a FQDN field among the logged parameters. In the official iis log file documentation ( https://docs.microsoft.com/en-us/previous-versions/iis/6.0-sdk/ms524845(v=vs.90) ), nor in this tutorial i consulted ( https://stackify.com/how-to-interpret-iis-logs/ ) was ever a FQDN field mentioned. As far as i know, a Fully Qualified Domain Name is an url: the only url available in this log file are https(:)//10.10.10.10/ (shared among many different ips) https(:)//localhost and https(:)//wiki.garaio.com/ (shared among many ips). 
 Since this was not a satisfying answer and had too many overlappings, the requested parameter was assumed to be "cs-username" . 
