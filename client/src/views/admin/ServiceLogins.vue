<template>
  <b-row>
    <b-col xl="12">
      <b-button variant="primary">
        <i class="icon-refresh"/>&nbsp;Generate Passwords
      </b-button>
    </b-col>
    <b-col xl="4">
      <b-table hover striped small :items="items" :fields="fields" @row-clicked="rowClicked"></b-table>
    </b-col>

    <b-col v-if="selectedRow" xl="8">
      <b-card>
        <div>
          <small class="float-right">Last Logged in: 2019-3-12 4:33:02 PM</small>
          <h4 class>{{selectedRow.name}}</h4>
        </div>
        <b-form>
          <b-form-group label="Username" label-for="usernameInput" :label-cols="3">
            <b-form-input id="usernameInput" :value="selectedRow.name"></b-form-input>
          </b-form-group>

          <b-form-group label="Password" label-for="passwordInput" :label-cols="3">
            <div>
              <b-form-input type="password" id="passwordInput" :value="selectedRow.name"></b-form-input>
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
import itemData from "./ServiceLoginData";
export default {
  name: "ServiceLogins",
  data: () => {
    return {
      items: itemData,
      username: null,
      selectedRow: null,
      fields: [{ key: "name" }, { key: "role" }]
    };
  },
  computed: {},
  methods: {
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
