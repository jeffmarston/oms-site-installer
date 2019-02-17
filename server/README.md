TODO:

- breadcrumbs show server name
- Hook up machines specs
- turn  off polling when users disconnected
   - allow users to turn it on?


Environment:
 - GET:  /environment

Machines
 - SigR: chaneel="machines" object="params"

Service
 - GET:  /services/{machine}
 - POST: /service/{machine}/{svc}?cmd=start
 - POST: /service/{machine}/{svc}/?cmd=stop

 - SigR: channel="service" object="serviceParams"
 - SigR: channel="service" object="serviceParams-{name}"


