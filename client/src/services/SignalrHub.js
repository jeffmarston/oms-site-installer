const signalR = require('@aspnet/signalr');

var readyFunction = null;

let connection = new signalR.HubConnectionBuilder()
.withUrl("http://localhost:50215/ServiceMgmtHub")
.build();

connection.start().then(function() {
  readyFunction();
});

export default class SignalrHub {
  constructor(readyFunc) {
    readyFunction = readyFunc;
  }

  connection = connection;

  send(cmd, servicename) {
    connection.invoke("Request", cmd, servicename).catch(err => console.error(err)); 
  }
  
  getServices() {
    connection.invoke("GetServices").catch(err => console.error(err));        
  }
  
  }