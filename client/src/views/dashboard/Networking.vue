<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col md="12">
        <b-card>
          <b-row>
            <b-col sm="5">
              <h4 id="traffic" class="card-title mb-0">WCF Message Traffic</h4>
              <div class="small text-muted">Feb 20, 2019</div>
            </b-col>
            <b-col sm="7" class="d-none d-md-block">
              <b-button type="button" variant="primary" class="float-right">
                <i class="icon-cloud-download"></i>
              </b-button>
              <b-button-toolbar class="float-right" aria-label="Toolbar with buttons group">
                <b-form-radio-group
                  class="mr-3"
                  id="radiosBtn"
                  buttons
                  button-variant="outline-secondary"
                  v-model="selected"
                  name="radiosBtn"
                >
                  <b-form-radio class="mx-0" value="Day">Day</b-form-radio>
                  <b-form-radio class="mx-0" value="Month">Month</b-form-radio>
                  <b-form-radio class="mx-0" value="Year">Year</b-form-radio>
                </b-form-radio-group>
              </b-button-toolbar>
            </b-col>
          </b-row>
          <main-chart-example
            chartId="main-chart-01"
            class="chart-wrapper"
            style="height:300px;margin-top:40px;"
            height="300"
          ></main-chart-example>
          <div slot="footer">
            <b-row class="text-center">
              <b-col class="mb-sm-2 mb-0">
                <div class="text-muted">Visits</div>
                <strong>29.703 Users (40%)</strong>
                <b-progress
                  height="{}"
                  class="progress-xs mt-2"
                  :precision="1"
                  variant="success"
                  :value="40"
                ></b-progress>
              </b-col>
              <b-col class="mb-sm-2 mb-0 d-md-down-none">
                <div class="text-muted">Unique</div>
                <strong>24.093 Users (20%)</strong>
                <b-progress
                  height="{}"
                  class="progress-xs mt-2"
                  :precision="1"
                  variant="info"
                  :value="20"
                ></b-progress>
              </b-col>
              <b-col class="mb-sm-2 mb-0">
                <div class="text-muted">Pageviews</div>
                <strong>78.706 Views (60%)</strong>
                <b-progress
                  height="{}"
                  class="progress-xs mt-2"
                  :precision="1"
                  variant="warning"
                  :value="60"
                ></b-progress>
              </b-col>
              <b-col class="mb-sm-2 mb-0">
                <div class="text-muted">New Users</div>
                <strong>22.123 Users (80%)</strong>
                <b-progress
                  height="{}"
                  class="progress-xs mt-2"
                  :precision="1"
                  variant="danger"
                  :value="80"
                ></b-progress>
              </b-col>
              <b-col class="mb-sm-2 mb-0 d-md-down-none">
                <div class="text-muted">Bounce Rate</div>
                <strong>Average Rate (40.15%)</strong>
                <b-progress height="{}" class="progress-xs mt-2" :precision="1" :value="40"></b-progress>
              </b-col>
            </b-row>
          </div>
        </b-card>
      </b-col>
    </b-row>

    <b-row>
      <b-col md="4">
        <b-card>
          <h4 id="traffic" class="card-title mb-0">OMS Clients online</h4>
          <div class="small text-muted">Feb 20, 2019</div>

          <b-row>
            <b-col sm="12">
              <b-row>
                <b-col sm="6">
                  <Callout variant="info">
                    <small class="text-muted">User 1</small>
                    <br>
                    <strong class="h4">9,123</strong>
                    <div class="chart-wrapper" :style="{ top: '-10px'}">
                      <!--<callout-chart-example :data="[35, 23, 56, 22, 97, 23, 64]" variant="#20a8d8" width="80" height="30" />-->
                      <callout-chart-example
                        chartId="callout-chart-01"
                        :data="[35, 23, 56, 22, 97, 23, 64]"
                        variant="info"
                        width="80"
                        height="30"
                      />
                    </div>
                  </Callout>
                </b-col>
                <b-col sm="6">
                  <Callout variant="danger">
                    <small class="text-muted">User 2</small>
                    <br>
                    <strong class="h4">22,643</strong>
                    <div class="chart-wrapper" :style="{ top: '-10px'}">
                      <callout-chart-example
                        chartId="callout-chart-02"
                        :data="[65, 59, 84, 84, 51, 55, 40]"
                        variant="danger"
                        width="80"
                        height="30"
                      />
                    </div>
                  </Callout>
                </b-col>
              </b-row>
            </b-col>
          </b-row>
        </b-card>
      </b-col>

    </b-row>
  </div>
</template>

<script>
import CardLine1ChartExample from "./CardLine1ChartExample";
import CardLine2ChartExample from "./CardLine2ChartExample";
import CardLine3ChartExample from "./CardLine3ChartExample";
import CardBarChartExample from "./CardBarChartExample";
import MainChartExample from "./MainChartExample";
import SocialBoxChartExample from "./SocialBoxChartExample";
import CalloutChartExample from "./CalloutChartExample";
import { Callout } from "@coreui/vue";

export default {
  name: "system-usage",
  components: {
    Callout,
    CardLine1ChartExample,
    CardLine2ChartExample,
    CardLine3ChartExample,
    CardBarChartExample,
    MainChartExample,
    SocialBoxChartExample,
    CalloutChartExample
  },
  data: function() {
    return {
      selected: "Month",
      tableItems: [
        {
          avatar: { url: "img/avatars/1.jpg", status: "success" },
          user: {
            name: "Yiorgos Avraamu",
            new: true,
            registered: "Jan 1, 2015"
          },
          country: { name: "USA", flag: "us" },
          usage: { value: 50, period: "Jun 11, 2015 - Jul 10, 2015" },
          payment: { name: "Mastercard", icon: "fa fa-cc-mastercard" },
          activity: "10 sec ago"
        },
        {
          avatar: { url: "img/avatars/2.jpg", status: "danger" },
          user: {
            name: "Avram Tarasios",
            new: false,
            registered: "Jan 1, 2015"
          },
          country: { name: "Brazil", flag: "br" },
          usage: { value: 22, period: "Jun 11, 2015 - Jul 10, 2015" },
          payment: { name: "Visa", icon: "fa fa-cc-visa" },
          activity: "5 minutes ago"
        },
        {
          avatar: { url: "img/avatars/3.jpg", status: "warning" },
          user: { name: "Quintin Ed", new: true, registered: "Jan 1, 2015" },
          country: { name: "India", flag: "in" },
          usage: { value: 74, period: "Jun 11, 2015 - Jul 10, 2015" },
          payment: { name: "Stripe", icon: "fa fa-cc-stripe" },
          activity: "1 hour ago"
        },
        {
          avatar: { url: "img/avatars/4.jpg", status: "" },
          user: { name: "Enéas Kwadwo", new: true, registered: "Jan 1, 2015" },
          country: { name: "France", flag: "fr" },
          usage: { value: 98, period: "Jun 11, 2015 - Jul 10, 2015" },
          payment: { name: "PayPal", icon: "fa fa-paypal" },
          activity: "Last month"
        },
        {
          avatar: { url: "img/avatars/5.jpg", status: "success" },
          user: {
            name: "Agapetus Tadeáš",
            new: true,
            registered: "Jan 1, 2015"
          },
          country: { name: "Spain", flag: "es" },
          usage: { value: 22, period: "Jun 11, 2015 - Jul 10, 2015" },
          payment: { name: "Google Wallet", icon: "fa fa-google-wallet" },
          activity: "Last week"
        },
        {
          avatar: { url: "img/avatars/6.jpg", status: "danger" },
          user: {
            name: "Friderik Dávid",
            new: true,
            registered: "Jan 1, 2015"
          },
          country: { name: "Poland", flag: "pl" },
          usage: { value: 43, period: "Jun 11, 2015 - Jul 10, 2015" },
          payment: { name: "Amex", icon: "fa fa-cc-amex" },
          activity: "Last week"
        }
      ],
      tableFields: {
        avatar: {
          label: '<i class="icon-people"></i>',
          class: "text-center"
        },
        user: {
          label: "User"
        },
        country: {
          label: "Country",
          class: "text-center"
        },
        usage: {
          label: "Usage"
        },
        payment: {
          label: "Payment method",
          class: "text-center"
        },
        activity: {
          label: "Activity"
        }
      }
    };
  },
  methods: {
    variant(value) {
      let $variant;
      if (value <= 25) {
        $variant = "info";
      } else if (value > 25 && value <= 50) {
        $variant = "success";
      } else if (value > 50 && value <= 75) {
        $variant = "warning";
      } else if (value > 75 && value <= 100) {
        $variant = "danger";
      }
      return $variant;
    },
    flag(value) {
      return "flag-icon flag-icon-" + value;
    }
  }
};
</script>

<style>
/* IE fix */
#card-chart-01,
#card-chart-02 {
  width: 100% !important;
}
</style>
