<template style="backgroud: red;">
  <div class="panel-container">
      <div class="panel-left">
        <form class="header-bar">
          <div class="search-wrapper">
            <input placeholder="search" class="search-box" v-model="searchText" />
            <b-button variant="ghost" class="search-button">
              <i class="fa fa-search"></i>
            </b-button>
          </div>
          <b-button variant="primary" class="tool-button" v-b-modal.showNewModal>
            <i class="fa fa-plus"></i>Add
          </b-button>
        </form>

      <b-table
        class="nice-table"
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

    <div class="panel-right">
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
import { pluginCode, columns }  from "./PluginData";
import { AgGridVue } from "ag-grid-vue";

export default {
  name: "ColumnPluginView",
  components: { codemirror, AgGridVue },
  data: () => {
    return {
      items: columns,
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
      this.code = pluginCode[Math.floor(Math.random() * 3)];
    }
  }
};
</script>

<style scoped lang="scss">
.panel-container {
  display: flex;
  flex-direction: row;
}
.panel-left {
  flex: 0 0 auto;
  width: 450px;
  display: flex;
  flex-direction: column;
  overflow-y: auto;
}
.panel-right {
  flex: 1 1 auto;
  overflow: auto;
}
.header-bar {
  display: flex;
  padding: 10px 15px;
}
.tool-button {
  margin: 0 0 0 6px;
  width: 60px;
  i {
    margin: 0 4px;
  }
}
.search-wrapper {
  flex: 1 1 auto;
  padding: 0;
  display: flex;
  border: 1px solid #ccc;
  input {
    padding: 5px;
    flex: 1 1 auto;
    border: none;
    border-radius: 2px;
  }
  .search-button {
    background: #fff;
    margin: 0;
    box-shadow: none;
  }
}
</style>
