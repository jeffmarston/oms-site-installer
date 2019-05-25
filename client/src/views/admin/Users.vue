<template>
  <b-row>
    <b-col xl="12">
      <b-button variant="primary"><i class="fa fa-plus" />&nbsp;Add User</b-button>
    </b-col>
    <b-col xl="4">
      <b-table
        hover
        small
        responsive="sm"
        :items="items"
        :fields="fields" 
        @row-clicked="rowClicked"
      >
        <template slot="status" slot-scope="data">
          <b-badge :variant="getBadge(data.item.status)">{{data.item.status}}</b-badge>
        </template>
      </b-table>
    </b-col>

    <b-col v-if="selectedRow" xl="8">
      <b-card>
        <div>
          <small class="float-right">Last Logged in: 2019-3-12 4:33:02 PM</small>
          <h4 class>{{ selectedRow.name}}</h4>
        </div>
        <b-form>
          <b-form-group label="Username" label-for="usernameInput" :label-cols="3">
            <b-form-input
              id="usernameInput"
              placeholder="Enter Username..."
              autocomplete
              :value="getUsername(selectedRow.name)"
            ></b-form-input>
          </b-form-group>

          <b-form-group
            label="Roles"
            label-for="rbsTagInputMultiSelect"
            :label-cols="3"
          >
            <vue-tags-input
              id="rbsTagInput"
              v-model="rbsModel.Tag"
              :tags="rbsModel.selected"
              :autocomplete-items="rbsModel.all"
              :add-only-from-autocomplete="true"
              @tags-changed="newTags => tags = newTags"
            />
          </b-form-group>
          <b-form-group
            label="Entitlements"
            label-for="portEntitlementsMultiSelect"
            :label-cols="3"
          >
            <vue-tags-input
              id="portfolioTagInput"
              v-model="portfolioModel.Tag"
              :tags="portfolioModel.selected"
              :autocomplete-items="portfolioModel.all"
              :add-only-from-autocomplete="true"
              @tags-changed="newTags2 => tags = newTags2"
            />
          </b-form-group>

          <b-form-group label="Views" label-for="ViewComponent" :label-cols="3">
            <div>
              <b-form-input id="usernameInput"></b-form-input>
              <b-button size="sm" variant="secondary">Import</b-button>
              <b-button size="sm" variant="secondary">Export</b-button>
            </div>
          </b-form-group>

          <div slot="footer" class="float-right">
            <b-button type="reset" size="sm" variant="secondary">Delete User</b-button>
            <b-button type="reset" size="sm" variant="secondary">Reset</b-button>
            <b-button type="submit" size="sm" variant="primary">Save</b-button>
          </div>
        </b-form>
      </b-card>
    </b-col>
  </b-row>
</template>

<script>
import VueTagsInput from "@johmun/vue-tags-input";
import usersData from "./UsersData";
export default {
  name: "Users",
  components: { VueTagsInput },
  props: {},
  data: () => {
    return {
      items: usersData,
      username: null,
      selectedRow: null,
      fields: [
        { key: "name" },
        { key: "role" },
        { key: "status" }
      ],
      portfolioModel: {
        Tag: "",
        selected: [],
        all: [
          { text: "PortGroup01", value: "Port0001" },
          { text: "PortGroup02", value: "Port0002" },
          { text: "PortGroup03", value: "Port0003" },
          { text: "PortGroup04", value: "Port0004" },
          { text: "PortGroup05", value: "Port0005" },
          { text: "Port_Master", value: "Port_Master", disabled: true }
        ]
      },
      rbsModel: {
        Tag: "",
        selected: [],
        all: [
          { text: "Admin", value: "Admin" },
          { text: "Trader", value: "Trader" },
          { text: "PM", value: "PM" },
          { text: "Analyst", value: "Analyst" },
          { text: "Operations", value: "Operations" }
        ]
      }
    };
  },
  computed: {},
  methods: {
    getBadge(status) {
      return status === "Active" ? "success" : "Inactive";
    },
    getRowCount(items) {
      return items.length;
    },
    getUsername(id) {
      var nameParts = id.split(" ");
      return (nameParts[0][0] + nameParts[1]).toLowerCase();
    },
    rowClicked(item) {
      this.selectedRow = item;
    }
  }
};
</script>

<style scoped>
.vue-tags-input >>> .ti-tag {
  background-color: #295d88;
}
.vue-tags-input {
  max-width: 100%;
}
.card-body >>> table > tbody > tr > td {
  cursor: pointer;
}
</style>
