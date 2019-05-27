<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col cols="12" md="12">
        <b-form>
          <b-form-group label="Command" label-for="commandText" :label-cols="2">
            <b-form-select
              id="commandText"
              :plain="true"
              :options="[
                  'Parse Trading Logs',
                  'Parse Analytics Logs',
                  'Parse Compliance Logs']"
            ></b-form-select>
          </b-form-group>
        </b-form>
        <b-textarea style="height: 700px"></b-textarea>
      </b-col>
    </b-row>
  </div>
</template>

<script>
export default {
  name: "data integrity",
  data() {
    return {
      dataChecks: [{ name: "", checked: false }]
    };
  },
  methods: {
    showAlert() {
      this.dismissCountDown = this.dismissSecs;
    }
  }
};
</script>
