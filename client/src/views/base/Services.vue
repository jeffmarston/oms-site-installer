<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col sm="6">
        <b-card no-header>
          <div slot="header">
            <strong>Services</strong>
          </div>
          <b-table
            hover
            responsive="sm"
            small
            :items="items"
            :fields="fields"
            @row-clicked="rowClicked"
          >
            <template slot="servername" slot-scope="data">
              <strong>{{data.item.servername}}</strong>
            </template>
            <template slot="status" slot-scope="data">
              <b-badge :variant="getBadge(data.item.status)">{{data.item.status}}</b-badge>
            </template>
          </b-table>
        </b-card>
      </b-col>
      
      <b-col sm="6" v-if="selectedRow">
        <b-card>
          <h4>{{ selectedRow.service}}</h4>
          <div>
            <b-button
              :variant="getButtonVariant(selectedRow.status, 'Start')"
              @click="startSvc()"
            >Start</b-button>&nbsp;
            <b-button
              :variant="getButtonVariant(selectedRow.status, 'Stop')"
              @click="stopSvc()"
            >Stop</b-button>&nbsp;
            <a href="./analytics.log" download>
              <b-button>Download Logs</b-button>
            </a>&nbsp;
            <b-button @click="sendMsg(selectedRow.service)">Send</b-button>&nbsp;
          </div>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import SignalrHub from "../../services/SignalrHub";
const _ = require('lodash');

let signalrHub = null;
let onReady = function() {
  signalrHub.getServices();
};
let serviceData = [
  {
    service: "Discovery",
    registered: todaysDate,
    status: "Running"
  },
  {
    service: "Analytics",
    registered: todaysDate,
    status: "Stopped",
    _rowVariant: "danger"
  },
  {
    service: "Notification",
    registered: todaysDate,
    status: "Running"
  },
  {
    service: "SOD",
    registered: todaysDate,
    status: "Running"
  },
  { service: "Compliance", registered: todaysDate, status: "Running" },
  { service: "DataFeed", registered: todaysDate, status: "Running" }
];

signalrHub = new SignalrHub(onReady);
let conn = signalrHub.connection;

conn.on("AllServices", function(svcList) {
  serviceData.length = 0;
  svcList.forEach(svc => {
    serviceData.push({
      service: svc.name,
      status: svc.status,
      machine: svc.machineName
    });
  });
});


conn.on("Response", function (cmd, svc) {
  console.log(svc);
  let serviceDelta = _.find(serviceData, o => o.service === svc.name);
  if (serviceDelta) {
    serviceDelta.status = svc.status;
  }
});


let today = new Date();
let todaysDate =
  today.getMonth() + 1 + "/" + today.getDate() + "/" + today.getFullYear();

export default {
  name: "services",
  data: () => {
    return {
      items: serviceData,
      itemsArray: serviceData,
      selectedRow: null,
      fields: [
        { key: "service", label: "Service", sortable: true },
        { key: "machine", label: "machine" },
        { key: "status", sortable: true }
      ]
    };
  },
  methods: {
    rowClicked(item) {
      this.selectedRow = item;
    },
    startSvc() {
      //signalrHub.send("start", this.selectedRow.service);

      let row = this.selectedRow;
      row.status = "Starting";
      row._rowVariant = this.getBadge(row.status);
      setTimeout(() => {
        row.status = "Running";
        row._rowVariant = this.getBadge(row.status);
      }, 2000);
    },
    stopSvc() {
      //signalrHub.send("stop", this.selectedRow.service);
      let row = this.selectedRow;
      setTimeout(() => {
        row.status = "Stopped";
        row._rowVariant = this.getBadge(row.status);
      }, 500);
    },
    getBadge(status) {
      return status === "Running"
        ? ""
        : status === "Starting"
        ? "warning"
        : status === "Stopped"
        ? "danger"
        : "primary";
    },
    getButtonVariant(status, button) {
      if (button === "Start") {
        return status === "Stopped" ? "primary" : "";
      } else if (button === "Stop") {
        return status === "Running" ? "primary" : "";
      }
    },
    sendMsg(svc) {
      signalrHub.send("start", svc);
    }
  }
};
</script>
