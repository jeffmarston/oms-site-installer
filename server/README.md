

Environment:
 - GET:  /environment

Machines
 - SigR: chaneel="machines" object="params"

Service
 - GET:  /services
 - POST: /service/{name}?cmd=start
 - POST: /service/{name}/?cmd=stop

 - SigR: channel="service" object="serviceParams"
 - SigR: channel="service" object="serviceParams-{name}"


