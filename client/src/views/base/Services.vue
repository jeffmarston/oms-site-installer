<template>
  <div class="animated fadeIn">
    <h4>CPU {{ serverStats.cpu }} %</h4>
    <h4>Memory {{ serverStats.memory }} MB</h4>
    <b-row>
      <b-col sm="12">
        <div>
          <b-button :disabled="isButtonEnabled(selectedRow, 'start')" @click="startSvc()">Start</b-button>&nbsp;
          <b-button :disabled="isButtonEnabled(selectedRow, 'stop')" @click="stopSvc()">Stop</b-button>&nbsp;
          <a href="./analytics.log" download>
            <b-button :disabled="isButtonEnabled(selectedRow, 'logs')">Logs</b-button>
          </a>&nbsp;
          <!-- <b-button @click="sendMsg(selectedRow.service)">Send</b-button>&nbsp; -->
        </div>

        <ag-grid-vue
          style
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

      <b-col sm="1" v-if="selectedRow">
        <b-card></b-card>
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
      rowClassRules: null,
      serverStats: { cpu: 0, memory: 0 }
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

    this.rowData = [];
    this.gridOptions = {};
  },
  methods: {
    onGridReady(params) {
      this.gridApi = params.api;
      this.columnApi = params.columnApi;
      this.populateGrid();
      this.subscribeToServiceChange(this.gridApi, this.serverStats);
    },
    subscribeToServiceChange(gridApi, serverStats) {
      conn.on("Response", function(cmd, svc) {
        if (cmd == "serverStats") {
          serverStats.cpu = svc.cpuPercent;
          serverStats.memory = svc.memoryMb.toLocaleString("en-US");
        }

        if (cmd == "ServiceChanged") {
          gridApi.forEachNodeAfterFilter(row => {
            if (row.data.name === svc.name) {
              row.data = svc;
              gridApi.redrawRows(row);
            }
          });
        }
      });
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
    isButtonEnabled(row, buttonType) {
      if (!row) return true;
      if (buttonType === "start") {
        return row.status === "Running";
      } else if (buttonType === "stop") {
        return row.status !== "Running";
      } else if (buttonType === "logs") {
        return true;
      }
    },
    onRowSelected() {
      this.selectedRow = this.gridApi.getSelectedRows()[0];
    },
    getRowNodeId(data) {
      return data.name;
    },
    startSvc() {
      fetch("http://localhost:5000/serviceInfo/startService", {
        method: "POST",
        mode: "cors",
        cache: "no-cache",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(this.selectedRow)
      }).then(response => response.json());
    },
    stopSvc() {
      fetch("http://localhost:5000/serviceInfo/stopService", {
        method: "POST",
        mode: "cors",
        cache: "no-cache",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(this.selectedRow)
      }).then(response => response.json());
    }
  }
};
</script>
