<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col cols="12" xl="6">
        <transition name="slide">
          <b-card header="caption">
            <b-table
              hover
              striped
              responsive="sm"
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
        </transition>
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
      servername: "EZE-APP01",
      registered: "2012/01/01",
      status: "Active",
      link: "eze-app01"
    },
    {
      servername: "EZE-APP02",
      registered: "2012/02/01",
      status: "Stopped",
      link: "eze-app02"
    },
    {
      servername: "EZE-APP03",
      registered: "2012/02/01",
      status: "Inactive",
      link: "eze-app03"
    },
    {
      servername: "EZE-APP04",
      registered: "2012/03/01",
      status: "Pending",
      link: "eze-app03"
    },
    {
      servername: "EZE-APP05",
      registered: "2012/01/21",
      status: "Active",
      link: "eze-app03"
    },
    {
      servername: "EZE-APP06",
      registered: "2012/01/01",
      status: "Active",
      link: "eze-app03"
    },
    {
      servername: "EZE-APP07",
      registered: "2012/02/01",
      status: "Stopped",
      link: "eze-app03"
    },
    {
      servername: "EZE-APP08",
      registered: "2012/02/01",
      status: "Inactive",
      link: "eze-app03"
    }
  ]);

export default {
  name: "servers",
  components: { cTable },
  data: () => {
    return {
      items: serverData,
      itemsArray: serverData(),
      fields: [
        { key: "servername", label: "Server", sortable: true },
        { key: "registered", label: "Up Since" },
        { key: "status", sortable: true }
      ]
    };
  },
  methods: {
    rowClicked(item) {
      this.$router.push({ path: item.link });
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
