<template>
  <div class="animated fadeIn">
    <!-- <h4>CPU {{ serverStats.cpu }} %</h4>
    <h4>Memory {{ serverStats.memory }} MB</h4>-->
    <b-row>
      <b-col cols="12" sm="6" lg="3">
        <b-card :no-body="true">
          <b-card-body class="p-3 clearfix">
            <i class="fa fa-cogs bg-info p-3 font-2xl mr-3 float-left"></i>
            <div class="h5 text-info mb-0 mt-2">{{ serverStats.cpu }} %</div>
            <div class="text-muted text-uppercase font-weight-bold font-xs">CPU</div>
          </b-card-body>
        </b-card>
      </b-col>
      <b-col cols="12" sm="6" lg="3">
        <b-card :no-body="true">
          <b-card-body class="p-3 clearfix">
            <i class="fa fa-laptop bg-danger p-3 font-2xl mr-3 float-left"></i>
            <div class="h5 text-danger mb-0 mt-2">{{ serverStats.memory }} MB</div>
            <div class="text-muted text-uppercase font-weight-bold font-xs">Memory</div>
          </b-card-body>
        </b-card>
      </b-col>
    </b-row>

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
const env = require("../../environment.config.json");

let signalrHub = null;
let onReady = function() {
  signalrHub.subscribe("111", "222");
};

import SignalrHub from "../../services/SignalrHub";
signalrHub = new SignalrHub(onReady);
let conn = signalrHub.connection;
let today = new Date();
let todaysDate =
  today.getMonth() + 1 + "/" + today.getDate() + "/" + today.getFullYear();

import { AgGridVue } from "ag-grid-vue";
import Router from "vue-router";

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
      serverStats: { cpu: 0, memory: 0 },
      routeName: null
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
      this.subscribeToServiceChange();
    },
    subscribeToServiceChange() {

      conn.on("machine", (machineName, machineData) => {
        console.log(`machine: ${machineName}`);
        this.serverStats.cpu = machineData.cpuPercent;
        this.serverStats.memory = machineData.memoryMb.toLocaleString("en-US");
      });

      conn.on("service", (machineName, svcName, svcData) => {
        console.log(`service: ${machineName}, ${svcName}`);
        if (machineName === this.$route.params.name) {
          this.gridApi.forEachNodeAfterFilter(row => {
            if (row.data.name === svcName) {
              row.data = svcData;
              this.gridApi.redrawRows(row);
            }
          });
        }
      });
    },
    populateGrid() {
      fetch(env.serverAddress + "/api/services/" + this.$route.params.name, {
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
      fetch(
        env.serverAddress +
          "/api/services/" +
          this.$route.params.name +
          "/start/" +
          this.selectedRow.name,
        {
          method: "POST",
          mode: "cors",
          cache: "no-cache",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(this.selectedRow)
        }
      ).then(response => response.json());
    },
    stopSvc() {
      fetch(
        env.serverAddress +
          "/api/services/" +
          this.$route.params.name +
          "/stop/" +
          this.selectedRow.name,
        {
          method: "POST",
          mode: "cors",
          cache: "no-cache",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(this.selectedRow)
        }
      ).then(response => response.json());
    }
  }
};
</script>
