<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col sm="12">
        <c-table
          :table-data="itemsArray"
          :per-page="15"
          hover
          striped
          bordered
          small
          fixed
          caption="<i class='fa fa-align-justify'></i> OMS Services"
        ></c-table>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import { shuffleArray } from "@/shared/utils";
import cTable from "./Table.vue";

const someData = () =>
  shuffleArray([
    {
      service: "Discovery",
      registered: "2012/01/01",
      status: "Active",
      _rowVariant: "success"
    },
    {
      service: "Analytics",
      registered: "2012/02/01",
      status: "Stopped",
      _rowVariant: "danger"
    },
    {
      service: "Notification",
      registered: "2012/02/01",
      status: "Inactive",
      _rowVariant: "info"
    },
    { service: "SOD", registered: "2012/03/01", status: "Pending" },
    { service: "Compliance", registered: "2012/01/21", status: "Active" },
    { service: "DataFeed", registered: "2012/01/01", status: "Active" }
  ]);

export default {
  name: "servers",
  components: { cTable },
  data: () => {
    return {
      items: someData,
      itemsArray: someData(),
      fields: [
        { key: "service", label: "Service", sortable: true },
        { key: "registered", label: "Up Since" },
        { key: "status", sortable: true }
      ]
    };
  }
};
</script>
