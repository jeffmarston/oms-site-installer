

Environment:
 - GET:  /environment

Server
 - GET:  /servers
 - SigR: /serverStats

Service
 - GET:  /services
 - POST: /service/{name}?cmd=start
 - POST: /service/{name}/?cmd=stop
 - SigR: /serviceChanges
