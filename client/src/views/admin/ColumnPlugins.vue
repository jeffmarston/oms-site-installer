<template style="backgroud: red;">
  <div class="panel-container">
    <div class="panel-left">
      <form>
        <div class="search-wrapper drop-shadow">
          <input placeholder="search" class="search-box" v-model="searchText" />
          <b-button variant="ghost">
            <i class="fa fa-search"></i>
          </b-button>
        </div>
      </form>

      <b-table
        class="nice-table drop-shadow"
        hover
        small
        responsive="sm"
        :items="filteredItems"
        :fields="fields"
        @row-clicked="rowClicked"
      >
        <template slot="status" slot-scope="data">
          <b-button
            v-if="!data.item.status"
            size="sm"
            variant="outline-success"
            @click="downloadPlugin(data.item)"
          >Add</b-button>
          <i v-if="data.item.status==='loading'" class="fa fa-spin fa-spinner checkmark" />
          <i v-if="data.item.status==='installed'" class="fa fa-check checkmark" />
        </template>
      </b-table>

      <!-- <ag-grid-vue
        style="width: 100%;"
        class="ag-theme-balham drop-shadow"
        :columnDefs="fields"
        :gridOptions="gridOptions"
        :rowData="items"
        suppressCellSelection
        @grid-ready="onGridReady"
      ></ag-grid-vue>-->
    </div>

    <div v-if="selectedRow" class="panel-right">
      <codemirror v-model="code" :options="cmOptions"></codemirror>
    </div>
  </div>
</template>

<script>
import { codemirror } from "vue-codemirror";
import "codemirror/lib/codemirror.css";
import "codemirror/mode/clike/clike.js";
import "codemirror/theme/darcula.css";
import { setTimeout } from "timers";
import pluginCode from "./PluginData";
import { AgGridVue } from "ag-grid-vue";

export default {
  name: "Users",
  components: { codemirror, AgGridVue },
  data: () => {
    return {
      items: [
        {
          author: "jmarston",
          calcName: "InTheMoney",
          type: "Blotter",
          status: null
        },
        {
          author: "shyde",
          calcName: "absoluteValue",
          type: "Blotter",
          status: null
        },
        {
          author: "dfredlund",
          calcName: "shortP&L",
          type: "Analytics",
          status: "installed"
        },
        {
          author: "dfredlund",
          calcName: "longP&L",
          type: "Analytics",
          status: null
        },
        {
          author: "shyde",
          calcName: "absoluteValue",
          type: "Blotter",
          status: null
        },
        {
          author: "dfredlund",
          calcName: "shortP&L",
          type: "Analytics",
          status: "installed"
        },
        {
          author: "dfredlund",
          calcName: "longP&L",
          type: "Analytics",
          status: null
        }
      ],
      selectedRow: null,
      searchText: "",
      fields: [
        { key: "author" },
        { key: "calcName" },
        { key: "type" },
        { key: "status" }
      ],
      code: "",
      cmOptions: {
        tabSize: 4,
        mode: "text/x-csharp",
        theme: "default",
        lineNumbers: true,
        line: true
      }
    };
  },
  computed: {
    filteredItems() {
      return this.items.filter(item => {
        let concat = item.calcName + "|" + item.author + "|" + item.type;
        return concat.toLowerCase().indexOf(this.searchText.toLowerCase()) > -1;
      });
    }
  },
  methods: {
    downloadPlugin(plugin) {
      console.log(plugin.status);
      plugin.status = "loading";
      setTimeout(() => {
        plugin.status = "installed";
        console.log(plugin.status);
      }, 1000);
    },
    getRowCount(items) {
      return items.length;
    },
    rowClicked(item, index) {
      this.selectedRow = item;
      this.code = pluginCode[index];
    }
  }
};
</script>

<style scoped lang="scss">
.panel-container {
  height: 100%;
  display: flex;
  flex-direction: row;
}

.panel-left {
  flex: 1 1;
  width: 100%;
  height: 100%;
  padding: 0 15px;
  display: flex;
  flex-direction: column;
}

.panel-right {
  flex: 1 1;
  overflow: auto;
}

.nice-table {
  flex: 1 1 auto;
}

.CodeMirror {
  height: 100%
}
.search-wrapper {
  margin: 10px 5px;
  padding: 0;
  display: flex;
  border: 1px solid #ccc;
  input {
    padding: 5px;
    flex: 1 1 auto;
    border: none;
    border-radius: 2px;
  }
  button {
    background: #fff;
    margin: 0;
    box-shadow: none;
  }
}
</style>
