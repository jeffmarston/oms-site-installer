<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col sm="6" lg="3">
        <b-card no-body class="bg-primary">
          <b-card-body class="pb-0">
            <h4 class="mb-0">Current Version</h4>
            <p>SR10 P18.4</p>
          </b-card-body>
        </b-card>
      </b-col>
    </b-row>

    <b-row>
      <b-col sm="6" lg="3">
        <b-card no-body class="bg-primary">
          <b-card-body class="pb-0">
            <h4 class="mb-0">Download latest version</h4>
          </b-card-body>
        </b-card>
      </b-col>
    </b-row>

    <b-row>
      <b-col sm="12">
        <c-table
          :table-data="itemsArray"
          :per-page="15"
          bordered
          small
          fixed
          caption="<i class='fa fa-align-justify'></i> Roles">
        </c-table>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import { shuffleArray } from "@/shared/utils";
import cTable from "./Table.vue";

const someData = () =>
  shuffleArray([
    { role: "Clients", onTarget: "12/14", link: "/prod/clients" },
    { role: "Servers", onTarget: "8/8", link: "/prod/servers" },
    { role: "Database", onTarget: "1/1", link: "/prod/database" }
  ]);

export default {
  name: "servers",
  components: { cTable },
  data: () => {
    return {
      items: someData,
      itemsArray: someData(),
      fields: [
        { key: "role", label: "Role", sortable: true },
        { key: "onTarget", label: "On Target Version" }
      ]
    };
  }
};
</script>
