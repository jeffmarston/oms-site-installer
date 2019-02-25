<template>
  <b-row>
    <b-col cols="12" xl="6">
      <transition name="slide">
        <b-card header-tag="header" footer-tag="footer">
          <div slot="header">
            <strong>Service Logins</strong>
            <div class="card-header-actions">
              <b-button variant="primary">Rotate passwords</b-button>
            </div>
          </div>
          <b-table
            hover
            striped
            small
            responsive="sm"
            :items="items"
            :fields="fields"
            :current-page="currentPage"
            :per-page="perPage"
            @row-clicked="rowClicked"
          >
          </b-table>
          <nav>
            <b-pagination
              size="sm"
              :total-rows="getRowCount(items)"
              :per-page="perPage"
              v-model="currentPage"
              prev-text="Prev"
              next-text="Next"
              hide-goto-end-buttons
            />
          </nav>
        </b-card>
      </transition>
    </b-col>

    <b-col v-if="selectedRow" cols="12" xl="6">
      <b-card>
        <h4>{{ selectedRow.name}}</h4>
        <b-form>
          <b-form-group label="Username" label-for="horizEmail" :label-cols="3" :horizontal="true">
            <b-form-input id="horizEmail" :value="selectedRow.name"></b-form-input>
          </b-form-group>
          <b-form-group label="Password" label-for="horizPass" :label-cols="3" :horizontal="true">
            <b-form-input id="horizPass" type="password"></b-form-input>
          </b-form-group>
          <div slot="footer">
            <b-button type="submit" size="sm" variant="primary">
              <i class="fa fa-dot-circle-o"></i> Submit
            </b-button>
            <b-button type="reset" size="sm" variant="danger">
              <i class="fa fa-ban"></i> Reset
            </b-button>
          </div>
        </b-form>
      </b-card>
    </b-col>
  </b-row>
</template>

<script>
import usersData from "./ServiceLoginData";
export default {
  name: "ServiceLogins",
  props: {
    caption: {
      type: String,
      default: "Services"
    }
  },
  data: () => {
    return {
      items: usersData,
      selectedRow: null,
      fields: [{ key: "name" }, { key: "password" }, { key: "role" }],
      currentPage: 1,
      perPage: 10,
      totalRows: 0
    };
  },
  computed: {},
  methods: {
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
