<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col sm="12">
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
    </b-row>
    <b-row v-if="selectedRow">
      <b-col sm="12">
        <b-card>
          <h4>{{ selectedRow.service}}</h4>
          <div>
            <b-button :variant="getButtonVariant(selectedRow.status, 'Start')" @click="startSvc()">Start</b-button>&nbsp;
            <b-button :variant="getButtonVariant(selectedRow.status, 'Stop')" @click="stopSvc()">Stop</b-button>&nbsp;
            <a href="./analytics.log" download>
              <b-button>Download Logs</b-button>
            </a>&nbsp;
          </div>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import { shuffleArray } from "@/shared/utils";
import cTable from "./Table.vue";

let today = new Date();
let todaysDate =
  today.getMonth() + 1 + "/" + today.getDate() + "/" + today.getFullYear();

const serviceData = () => [
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

export default {
  name: "servers",
  components: { cTable },
  data: () => {
    return {
      items: serviceData,
      itemsArray: serviceData(),
      selectedRow: null,
      fields: [
        { key: "service", label: "Service", sortable: true },
        { key: "registered", label: "Up Since" },
        { key: "status", sortable: true }
      ]
    };
  },
  methods: {
    rowClicked(item) {
      this.selectedRow = item;
    },
    startSvc() {
      let row = this.selectedRow;
      row.status = "Starting";
      row._rowVariant = this.getBadge(row.status);
      setTimeout(() => {
        row.status = "Running";
        row._rowVariant = this.getBadge(row.status);
      }, 2000);
    },
    stopSvc() {
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
        return status === "Stopped"
        ? "primary"
        : "";
      } else if (button === "Stop") {
        return status === "Running"
        ? "primary"
        : "";
      }
    },
    downloadLogs() {}
  }
};
</script>
