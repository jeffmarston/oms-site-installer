<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col sm="6">
        <b-card>
          <div slot="header">Database</div>
          <b-form>
            <b-form-group>
              <b-input-group>
                <b-input-group-prepend>
                  <b-input-group-text>
                    <i class="cui-screen-smartphone"></i>
                  </b-input-group-text>
                </b-input-group-prepend>
                <b-form-input type="text" placeholder="Server" v-model="dbSettings.server"></b-form-input>
              </b-input-group>
            </b-form-group>
            <b-form-group>
              <b-input-group>
                <b-input-group-prepend>
                  <b-input-group-text>
                    <i class="fa fa-database"></i>
                  </b-input-group-text>
                </b-input-group-prepend>
                <b-form-input type="text" placeholder="Database" v-model="dbSettings.database"></b-form-input>
              </b-input-group>
            </b-form-group>
            <b-form-group>
              <b-input-group>
                <b-input-group-prepend>
                  <b-input-group-text>
                    <i class="fa fa-user"></i>
                  </b-input-group-text>
                </b-input-group-prepend>
                <b-form-input type="text" placeholder="Username" v-model="dbSettings.username"></b-form-input>
              </b-input-group>
            </b-form-group>
            <b-form-group>
              <b-input-group>
                <b-input-group-prepend>
                  <b-input-group-text>
                    <i class="fa fa-asterisk"></i>
                  </b-input-group-text>
                </b-input-group-prepend>
                <b-form-input type="password" placeholder="Password" v-model="dbSettings.password"></b-form-input>
              </b-input-group>
            </b-form-group>
            <div slot="footer">
              <b-button
                  type="submit"
                  variant="success"
                  @click="save"
                >Save</b-button>
                <b-button type="submit" @click="clear">Clear</b-button>
            </div>
          </b-form>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script>
export default {
  name: "forms",
  data() {
    return {
      dbSettings: { }
    };
  },
  beforeMount() {
    let dbSettingsString = localStorage.getItem("dbSettings");
    if (dbSettingsString) {
      this.dbSettings = JSON.parse(dbSettingsString);
    }
  },
  methods: {
    save() {
      localStorage.setItem("dbSettings", JSON.stringify(this.dbSettings));
    },
    clear() {
      localStorage.removeItem("dbSettings");
      this.dbSettings = {};
    }
  }
};
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
