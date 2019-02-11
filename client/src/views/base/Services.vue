<template>
  <div class="animated fadeIn">
    <b-row class="ag-theme-balham">
      <b-col sm="6">
        <b-card>
          <ag-grid-vue
            style="height: 500px;"
            class="ag-theme-balham"
            :columnDefs="columnDefs"
            :rowData="rowData"
            :gridOptions="gridOptions"
            rowSelection="single"
            suppressCellSelection
            animate-rows
            @grid-ready="onGridReady"
            @row-selected="onRowSelected"
            :getRowNodeId="getRowNodeId"
          ></ag-grid-vue>
        </b-card>
      </b-col>
      <!-- <b-col sm="6">
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
      </b-col>-->
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
//import SignalrHub from "../../services/SignalrHub";
const _ = require("lodash");

let signalrHub = null;
let onReady = function() {
  signalrHub.getServices();
};

// signalrHub = new SignalrHub(onReady);
// let conn = signalrHub.connection;

// conn.on("AllServices", function(svcList) {
//   serviceData.length = 0;
//   svcList.forEach(svc => {
//     serviceData.push({
//       service: svc.name,
//       status: svc.status,
//       machine: svc.machineName
//     });
//   });
// });

// conn.on("Response", function (cmd, svc) {
//   console.log(svc);
//   let serviceDelta = _.find(serviceData, o => o.service === svc.name);
//   if (serviceDelta) {
//     serviceDelta.status = svc.status;
//   }
// });

let today = new Date();
let todaysDate =
  today.getMonth() + 1 + "/" + today.getDate() + "/" + today.getFullYear();

import { AgGridVue } from "ag-grid-vue";

export default {
  name: "services",
  components: { AgGridVue },
  data: () => {
    return {
      columnDefs: null,
      rowData: null,
      selectedRow: null,
      gridOptions: null,
      rowClassRules: null
    };
  },
  beforeMount() {
    this.columnDefs = [
      {
        headerName: "Service",
        field: "service",
        sortable: true,
        filter: true,
        resizable: true
      },
      {
        headerName: "Status",
        field: "status",
        sortable: true,
        filter: true,
        resizable: true
      }
    ];

    this.rowData = [
      { service: "Discovery", registered: todaysDate, status: "Running" },
      { service: "Analytics", status: "Stopped", _rowVariant: "danger" },
      { service: "Notification", status: "Running" },
      { service: "Compliance", status: "Running" },
      { service: "DataFeed", status: "Running" }
    ];
    this.gridOptions = {};
    this.gridOptions.getRowClass = function(params) {
      return params.data.status == "Stopped"
        ? "danger"
        : params.data.status == "Starting"
        ? "warning"
        : "";
    };

    // this.rowClassRules = {
    //   danger: params => {
    //     var status = params.data.status;
    //     console.log(status);
    //     return status == "Stopped";
    //   }
    // };
  },
  methods: {
    onGridReady(params) {
      this.gridApi = params.api;
      this.columnApi = params.columnApi;
    },
    onRowSelected() {
      this.selectedRow = this.gridApi.getSelectedRows()[0];
    },
    getRowNodeId(data) {
      return data.service;
    },
    startSvc() {
      //signalrHub.send("start", this.selectedRow.service);

      let row = this.selectedRow;
      row.status = "Starting";
      this.gridApi.redrawRows();

      setTimeout(() => {
        row.status = "Running";
        this.gridApi.redrawRows();
      }, 2000);
    },
    stopSvc() {
      //signalrHub.send("stop", this.selectedRow.service);
      let row = this.selectedRow;
      setTimeout(() => {
        row.status = "Stopped";
        this.gridApi.redrawRows();
      }, 500);
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
