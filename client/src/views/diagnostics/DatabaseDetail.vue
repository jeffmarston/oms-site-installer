<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col sm="12" v-if="showDiagnostics">
        <b-card>
          <div slot="header">Diagnostics</div>
          <pre>
          {{ diagContent }}
        </pre>
        </b-card>
      </b-col>
    </b-row>
    <b-row>
      <b-nav pills>
        <b-nav-item @click="runDiagnostics()" size="sm">Diagnostics</b-nav-item>
        <b-nav-item @click="whatsRunning()" size="sm">Active Queries</b-nav-item>
        
        <b-nav-item-dropdown id="nav7_ddown" text="Dropdown" right>
          <b-dropdown-item @click="whatsRunning()">sp_BlitzWho</b-dropdown-item>
          <b-dropdown-item>two</b-dropdown-item>
          <b-dropdown-divider></b-dropdown-divider>
          <b-dropdown-item>three</b-dropdown-item>
        </b-nav-item-dropdown>
      </b-nav>
    </b-row>
    <b-row>
      <ag-grid-vue
        style="width: 100%;"
        class="ag-theme-balham"
        :columnDefs="columnDefs"
        :gridOptions="gridOptions"
        :rowData="rowData"
        suppressCellSelection
        @grid-ready="onGridReady"
      ></ag-grid-vue>
    </b-row>
  </div>
</template>

<script>
const env = require("../../environment.config.json");
import { AgGridVue } from "ag-grid-vue";

export default {
  name: "database",
  components: { AgGridVue },
  data() {
    return {
      showDiagnostics: false,
      showRunning: false,
      diagContent: "",
      whatsRunningContent: "",

      rowData: null,
      columnDefs: null,
      gridOptions: null
    };
  },
  beforeMount() {
    this.columnDefs = [
      {
        headerName: "Login",
        field: "loginName",
        sortable: true,
        filter: true,
        resizable: true
      },
      {
        headerName: "Program",
        field: "programName",
        sortable: true,
        filter: true,
        resizable: true
      },
      {
        headerName: "Query",
        field: "queryText",
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
      this.rowData = [];
    },
    runDiagnostics() {
      this.gridOptions.api.showLoadingOverlay();
      this.showRunning = true;
      this.diagContent = `
        Running Diagnostics...
      `;

      fetch(env.serverAddress + "/api/database/diagnostics", {
        mode: "cors"
      }).then(response => {
        if (response.status !== 200) {
          console.error("!! Status Code: " + response.status);
          console.error(response);
          return;
        }
        // Examine the text in the response
        response.json().then(data => {
          if (data.length > 0) {

            this.rowData = (data);
          }
          this.whatsRunningContent = data;
        });
      });
    },

    whatsRunning() {
      this.showRunning = true;
      this.whatsRunningContent = `
        Gathering Data...
      `;

      fetch(env.serverAddress + "/api/database/whatsrunning", {
        mode: "cors"
      }).then(response => {
        if (response.status !== 200) {
          console.error("!! Status Code: " + response.status);
          console.error(response);
          return;
        }

        // Examine the text in the response
        response.json().then(data => {
          if (data.queryText) {
            this.rowData.push(data);
          }
          this.whatsRunningContent = data;
        });
      });
    }
  }
};
</script>
