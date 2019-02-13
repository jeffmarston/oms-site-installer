<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col sm="6">
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
      </b-col>

      <b-col sm="6" v-if="selectedRow">
        <b-card>
          <h4>{{ selectedRow.name}}</h4>
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
const _ = require("lodash");

let signalrHub = null;
let onReady = function() {
  signalrHub.getServices();
};

import SignalrHub from "../../services/SignalrHub";
signalrHub = new SignalrHub(onReady);
let conn = signalrHub.connection;

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

conn.on("Response", function(cmd, svc) {
  console.log("==================");
  console.log(svc);
  let serviceDelta = _.find(serviceData, o => o.service === svc.name);
  if (serviceDelta) {
    serviceDelta.status = svc.status;
  }
});

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
        headerName: "Status",
        field: "status",
        sortable: true,
        filter: true,
        resizable: true,
        cellStyle: function(params) {
          return params.data.status == "Stopped"
            ? { color: "white", backgroundColor: "#f64846" }
            : params.data.status == "Starting"
            ? { color: "black", backgroundColor: "#ffc107" }
            : params.data.status == "Running"
            ? { color: "white", backgroundColor: "#4dbd74" }
            : null;
        }
      },
      {
        headerName: "Name",
        field: "name",
        sortable: true,
        filter: true,
        resizable: true
      },
      {
        headerName: "PID",
        field: "pid",
        sortable: true,
        filter: true,
        resizable: true
      },
      {
        headerName: "CPU",
        field: "cpu",
        sortable: true,
        filter: true,
        resizable: true
      },
      {
        headerName: "Memory",
        field: "memory",
        sortable: true,
        filter: true,
        resizable: true
      },
      {
        headerName: "Path",
        field: "path",
        sortable: true,
        filter: true,
        resizable: true
      }
    ];

    this.rowData = [
      { name: "Discovery", status: "Running", cpu: 0 },
      { name: "Analytics", status: "Stopped", cpu: 0 },
      { name: "Notification", status: "Running", cpu: 0 },
      { name: "Compliance", status: "Running", cpu: 0 },
      { name: "DataFeed", status: "Running", cpu: 0 }
    ];
    this.gridOptions = {};
    // this.gridOptions.getRowClass = function(params) {
    //   return params.data.status == "Stopped"
    //     ? "danger"
    //     : params.data.status == "Starting"
    //     ? "warning"
    //     : "";
    // };

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
      this.populateGrid();
      setInterval(() => {
        this.gridApi.forEachNodeAfterFilter(row => {
          let r = Math.random();
          if (r < 0.6 && row.data.status === "Running") {
            row.data.cpu = Math.floor(Math.random() * 101);
            this.gridApi.redrawRows(row);
          }
        });
      }, 1000);
    },
    populateGrid() {
      fetch("http://localhost:5000/serviceInfo/services", {
        mode: "cors"
      }).then(response => {
        if (response.status !== 200) {
          console.error("!! Status Code: " + response.status);
          console.error(response);
          return;
        }

        // Examine the text in the response
        response.json().then(data => {
          data.memory = data.memory / 1024;
          this.rowData = data;
        });
      });
    },
    onRowSelected() {
      this.selectedRow = this.gridApi.getSelectedRows()[0];
    },
    getRowNodeId(data) {
      return data.name;
    },
    startSvc() {
      //signalrHub.send("start", this.selectedRow.service);

      let svc = this.selectedRow;
      fetch("http://localhost:5000/serviceInfo/startService", {
        method: "POST",
        mode: "cors",
        cache: "no-cache",
        headers: {
          "Content-Type": "application/json"
          // "Content-Type": "application/x-www-form-urlencoded",
        },
        body: JSON.stringify(svc) // body data type must match "Content-Type" header
      }).then(response => response.json()); // parses response to JSON

      // let row = this.selectedRow;
      // row.status = "Starting";
      // this.gridApi.redrawRows();

      // setTimeout(() => {
      //   row.status = "Running";
      //   this.gridApi.redrawRows();
      // }, 2000);
    },
    stopSvc() {
      fetch("http://localhost:5000/serviceInfo/stopService", {
        method: "POST",
        mode: "cors",
        cache: "no-cache",
        headers: {
          "Content-Type": "application/json"
          // "Content-Type": "application/x-www-form-urlencoded",
        },
        body: JSON.stringify(svc) // body data type must match "Content-Type" header
      }).then(response => response.json()); // parses response to JSON

      //signalrHub.send("stop", this.selectedRow.service);
      // let row = this.selectedRow;
      // setTimeout(() => {
      //   row.status = "Stopped";
      //   this.gridApi.redrawRows();
      // }, 500);
    },
    getButtonVariant(status, button) {
      if (button === "Start") {
        return status === "Stopped" ? "primary" : "";
      } else if (button === "Stop") {
        return status === "Running" ? "primary" : "";
      }
    },
    sendMsg(svc) {
      //signalrHub.send("start", svc);
    }
  }
};
</script>
