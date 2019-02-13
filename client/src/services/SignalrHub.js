const signalR = require('@aspnet/signalr');

var readyFunction = null;

let connection = new signalR.HubConnectionBuilder()
.withUrl("http://localhost:5000/ServiceMgmtHub")
.build();

console.log('(1)');
connection.start().then(function() {
  readyFunction();
  console.log('(2)');
});

export default class SignalrHub {
  constructor(readyFunc) {
    readyFunction = readyFunc;
    console.log('(3)');
  }

  connection = connection;

  send(cmd, servicename) {
    connection.invoke("Request", cmd, servicename).catch(err => console.error(err)); 
  }
  
  getServices() {
    connection.invoke("GetServices").catch(err => console.error(err));        
  }
  
  }