<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col cols="12" md="6">
        <b-card header-tag="header" footer-tag="footer">
          <div slot="header">
            <strong>Bootstrap Alert</strong>
          </div>
          <div>
          </div>
          <div>
            <b-alert show variant="primary">Primary Alert</b-alert>
            <b-alert show variant="secondary">Secondary Alert</b-alert>
            <b-alert show variant="success">Success Alert</b-alert>
            <b-alert show variant="danger">Danger Alert</b-alert>
            <b-alert show variant="warning">Warning Alert</b-alert>
            <b-alert show variant="info">Info Alert</b-alert>
            <b-alert show variant="light">Light Alert</b-alert>
            <b-alert show variant="dark">Dark Alert</b-alert>
          </div>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script>
export default {
  name: "data-integrity",
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
