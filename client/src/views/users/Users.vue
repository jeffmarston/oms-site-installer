<template>
  <b-row>
    <b-table
      hover
      striped
      small
      :fixed="fixed"
      responsive="sm"
      :items="items"
      :fields="fields"
      :current-page="currentPage"
      :per-page="perPage"
      @row-clicked="rowClicked"
    >
      <template slot="status" slot-scope="data">
        <b-badge :variant="getBadge(data.item.status)">{{data.item.status}}</b-badge>
      </template>
    </b-table>

    <b-col v-if="selectedRow" cols="12" xl="6">
      <b-card>
        <div>
        <small class="float-right">Last Logged in: 2019-3-12 4:33:02 PM</small>
        <h4 class="">{{ selectedRow.name}}</h4>
          </div>
        <b-form>
          <b-form-group label="Email" label-for="horizEmail" :label-cols="3" :horizontal="true">
            <b-form-input
              id="horizEmail"
              type="email"
              placeholder="Enter Email.."
              autocomplete="username email"
            ></b-form-input>
          </b-form-group>

          <b-form-group
            label="Entitlements"
            label-for="portEntitlementsMultiSelect"
            :label-cols="3"
            :horizontal="true"
          >
            <b-form-select
              id="portEntitlementsMultiSelect"
              :plain="true"
              :multiple="true"
              :options="[
                { text: 'Port0001', value: 'Port0001'}, 
                { text: 'Port0002', value: 'Port0002'}, 
                { text: 'Port0003', value: 'Port0003'}, 
                { text: 'Port0004', value: 'Port0004'}, 
                { text: 'Port0005', value: 'Port0005'}, 
                { text: 'Port_Master', value: 'Port_Master',disabled: true}
              ]"
              :value="[null,'c']"
            ></b-form-select>
          </b-form-group>

          <div slot="footer" class="float-right">
            <b-button type="submit" size="sm" variant="primary">Save</b-button>
            <b-button type="reset" size="sm" variant="secondary">Reset</b-button>
          </div>
        </b-form>
      </b-card>
    </b-col>
  </b-row>
</template>

<script>
import usersData from "./UsersData";
export default {
  name: "Users",
  props: {
    caption: {
      type: String,
      default: "Users"
    },
    hover: {
      type: Boolean,
      default: true
    },
    striped: {
      type: Boolean,
      default: true
    },
    bordered: {
      type: Boolean,
      default: false
    },
    small: {
      type: Boolean,
      default: false
    },
    fixed: {
      type: Boolean,
      default: false
    }
  },
  data: () => {
    return {
      items: usersData.filter(user => user.id < 42),
      selectedRow: null,
      fields: [
        { key: "name" },
        { key: "lastLogin" },
        { key: "role" },
        { key: "status" }
      ],
      currentPage: 1,
      perPage: 5,
      totalRows: 0
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
    userLink(id) {
      return `users/${id.toString()}`;
    },
    rowClicked(item) {
      this.selectedRow = item;
    }
  }
};
</script>

<style scoped>
.card-body >>> table > tbody > tr > td {
  cursor: pointer;
}
</style>
