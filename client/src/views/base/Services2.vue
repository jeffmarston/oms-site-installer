<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col sm="12">
        <ag-grid-vue
          style="height: calc(100vh - 170px)"
          class="ag-theme-balham"
          :columnDefs="columnDefs"
          :rowData="rowData"
          :gridOptions="gridOptions"
          :floatingFilter="true"
          rowSelection="multiple"
          suppressCellSelection
          animate-rows
          @grid-ready="onGridReady"
          @row-selected="onRowSelected"
          :getRowNodeId="getRowNodeId"
          @cell-clicked="cellClick"
        ></ag-grid-vue>
      </b-col>
    </b-row>
  </div>
</template>

<script>
const _ = require("lodash");
const env = require("../../environment.config.json");

let signalrHub = null;
let onReady = function() {
  try {
    signalrHub.subscribe("localhost", "");
  } catch (e) {
    console.error("Failed to subscribe to SignalR updates: ");
    console.error(e);
  }
};

import SignalrHub from "../../services/SignalrHub";
signalrHub = new SignalrHub(onReady);
let conn = signalrHub.connection;
let today = new Date();
let todaysDate =
  today.getMonth() + 1 + "/" + today.getDate() + "/" + today.getFullYear();

import { AgGridVue } from "ag-grid-vue";
import Router from "vue-router";

function actionCellRendererFunc(params) {
  if (params.data.status === "Stopped") {
    return `<a class="icon-hover-hightlight"><i style="display: inline" class="icon-control-play icons"></i></a>`;
  }
  if (params.data.status === "Running") {
    return `<a class="icon-hover-hightlight"><i style="display: inline" class="icon-control-pause"></i></a>`;
  }
}

function logCellRendererFunc(params) {
  console.log();
  return `<a class="icon-hover-hightlight"><i style="display: inline" class="icon-paper-clip"></i></a>`;
}

const envProvider = require("@/services/environmentProvider");

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
        filter: 'agTextColumnFilter',
        resizable: true,
        cellStyle: function(params) {
          return params.data.status == "Stopped"
            ? { color: "white", backgroundColor: "#f64846" }
            : params.data.status == "StartPending" ||
              params.data.status == "StopPending"
            ? { color: "black", backgroundColor: "#ffc107" }
            : params.data.status == "Running"
            ? { color: "white", backgroundColor: "#4dbd74" }
            : null;
        }
      },
      {
        headerName: "Action",
        pinned: true,
        width: 40,
        cellRenderer: actionCellRendererFunc
      },
      {
        headerName: "Logs",
        pinned: true,
        width: 34,
        cellRenderer: logCellRendererFunc
      },
      {
        headerName: "Server",
        field: "server",
        sortable: true,
        filter: 'agTextColumnFilter',
        resizable: true
      },
      {
        headerName: "Name",
        field: "name",
        sortable: true,
        filter: 'agTextColumnFilter',
        floatingFilterComponentParams:{
          debounceMs:1000
        },
        resizable: true
      },
      {
        headerName: "PID",
        field: "pid",
        sortable: true,
        filter: 'agTextColumnFilter',
        resizable: true
      },
      {
        headerName: "FileName",
        field: "filename",
        sortable: true,
        filter: 'agTextColumnFilter',
        resizable: true
      },
      {
        headerName: "StartMode",
        field: "startMode",
        sortable: true,
        filter: 'agTextColumnFilter',
        resizable: true
      },
      {
        headerName: "Start Name",
        field: "startName",
        sortable: true,
        filter: 'agTextColumnFilter',
        resizable: true
      },
      {
        headerName: "Path",
        field: "path",
        sortable: true,
        filter: 'agTextColumnFilter',
        resizable: true
      }
    ];

    this.rowData = [];
    this.gridOptions = {};
  },
  methods: {
    cellClick(event) {
      console.log(event);
    },
    onGridReady(params) {
      this.gridApi = params.api;
      this.columnApi = params.columnApi;
      this.gridOptions.onRowDoubleClicked = this.onDoubleClick;
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
        //if (machineName === this.$route.params.name) {
        this.gridApi.forEachNodeAfterFilter(row => {
          if (row.data.name === svcName) {
            row.data = svcData;
            this.gridApi.redrawRows(row);
          }
        });
        //}
      });
    },
    populateGrid() {
      envProvider.getNavTree().then(navTreeData => {
        navTreeData.servers.forEach(svr => {
          fetch(
            env.serverAddress + "/api/services/" + svr.name,
            { mode: "cors" }
          ).then(response => {
            if (response.status !== 200) {
              console.error("!! Status Code: " + response.status);
              console.error(response);
              return;
            }
            // Examine the text in the response
            response.json().then(data => {
              data.forEach(service => {
                service.server = svr.name;
                this.rowData.push(service);
              });
            });
          });
        });
      });
    },
    disableButton(row, buttonType) {
      if (!row) return true;
      if (buttonType === "start") {
        return row.status === "Running";
      } else if (buttonType === "stop") {
        return row.status !== "Running";
      } else if (buttonType === "logs") {
        return false;
      }
    },
    onRowSelected() {
      this.selectedRow = this.gridApi.getSelectedRows()[0];
    },
    onDoubleClick() {
      if (this.selectedRow.status === "Running") {
        this.stopSvc();
      } else {
        this.startSvc();
      }
    },
    getRowNodeId(data) {
      return data.name+"|"+data.server;
    },
    startSvc() {
      fetch(
        env.serverAddress +
          "/api/services/" +
          this.selectedRow.server +
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
      );
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
      );
    }
  }
};
</script>
