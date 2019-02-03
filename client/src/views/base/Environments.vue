<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col sm="12" lg="12">
        <b-alert show variant="success">
          <h3>Prod</h3>
          <h5>5.7 SR10 P18.4</h5>
        </b-alert>
      </b-col>
    </b-row>

    <b-row>
      <b-col sm="6">
        <b-card header="Available Versions">
          <ul>
            <li v-for="item in availableInstallers">{{item}}</li>
          </ul>
        </b-card>
      </b-col>

      <b-col sm="6">
        <b-card>
          <div slot="header">
            <strong>Prepare Upgrade</strong>
          </div>
          <b-form-group label="Select upgrade version" label-for="elementsAppendButton">
            <b-input-group>
              <b-form-select
                id="elementsAppendButton"
                size="lg"
                :plain="true"
                :options="['Please select','2018.8', '2019.1', '2019.2']"
                value="Please select"
              ></b-form-select>
              <b-input-group-append>
                <b-button variant="primary" @click="downloadClicked()">Download</b-button>
              </b-input-group-append>
            </b-input-group>
          </b-form-group>
          <b-alert :show="isDownloading" variant="info">
              <i class="fa fa-spinner fa-spin fa-lg mt-4"></i>
              Downloading installers for 2018.8
          </b-alert>
        </b-card>
      </b-col>
    </b-row>

    <b-row>
      <b-col sm="12">
        <b-card>
          <div slot="header">
            <strong>Upgrade Progress</strong>
          </div>

          <b-table hover striped :items="items" :fields="fields" @row-clicked="rowClicked">
            <template slot="role" slot-scope="data">
              <router-link :to="data.item.link">
                <strong>{{data.item.role}}</strong>
              </router-link>
            </template>
            <template slot="progress" slot-scope="data">
              <b-progress
                :value="data.item.numUpgraded"
                :max="data.item.numMachines"
                show-progress
                :animated="!data.item.upgradeComplete"
                :variant="data.item.upgradeComplete ? 'success' : 'primary'"
              ></b-progress>
            </template>
            <template slot="upgrade" slot-scope="data">
              <b-button primary @click="upgradeClicked(data.item)">Upgrade</b-button>
            </template>
          </b-table>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import { shuffleArray } from "@/shared/utils";
import cTable from "./Table.vue";

const serverData = () =>
  shuffleArray([
    {
      role: "Clients",
      onTarget: "12/14",
      numMachines: 14,
      numUpgraded: 12,
      link: "/prod/clients"
    },
    {
      role: "Servers",
      onTarget: "8/8",
      numMachines: 8,
      numUpgraded: 5,
      link: "/prod/servers"
    },
    {
      role: "Database",
      onTarget: "1/1",
      numMachines: 1,
      numUpgraded: 0,
      link: "/prod/database"
    }
  ]);

export default {
  name: "servers",
  components: { cTable },
  data: () => {
    return {
      isDownloading: false,
      availableInstallers: ['OMS 2018.5', 'OMS 2018.6'],
      items: serverData,
      itemsArray: serverData(),
      fields: [
        { key: "role", label: "Role", sortable: true },
        { key: "onTarget", label: "On Target Version" },
        { key: "progress", label: "Progress" },
        { key: "upgrade", label: "Upgrade" }
      ]
    };
  },
  methods: {
    downloadClicked() {
      this.isDownloading = true;
      setTimeout(() => {
        this.availableInstallers.push('OMS 2018.8');
        this.isDownloading = false;
      }, 3000);
    },
    rowClicked(item) {
      // this.$router.push({ path: item.link });
    },
    upgradeClicked(item) {
      if (item.numMachines === item.numUpgraded) {
        item.upgradeComplete = true;
      } else {
        item.numUpgraded++;
        if (item.numMachines === item.numUpgraded) {
          item.upgradeComplete = true;
        }
      }
    },
    getBadge(status) {
      return status === "Active"
        ? "success"
        : status === "Inactive"
        ? "secondary"
        : status === "Pending"
        ? "warning"
        : status === "Stopped"
        ? "danger"
        : "primary";
    }
  }
};
</script>
