<template>
 <splitpanes horizontal style="height: 100%">
    <div class="panel-top">
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
    </div>

    <footer>
      <codemirror v-if="code" v-model="code" :options="cmOptions"></codemirror>
    </footer>
  </splitpanes>
</template>

<script>

import Splitpanes from "splitpanes";
import "splitpanes/dist/splitpanes.css";
import { codemirror } from "vue-codemirror";
import "codemirror/lib/codemirror.css";
import "codemirror/mode/clike/clike.js";
import "codemirror/theme/darcula.css";
import { setTimeout } from "timers";
import pluginCode from "./PluginData";
import { AgGridVue } from "ag-grid-vue";

export default {
  name: "Users",
  components: { codemirror, AgGridVue, Splitpanes },
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
      code: "// nothing here",
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

.search-wrapper {
  margin: 10px 5px;
  padding: 0;
  display: flex;
  width: 400px;
  input {
    padding: 5px;
    flex: 1 1 auto;
    border: 1px solid #ccc;
    border-radius: 2px;
  }
  button {
    background: #ccc;
    margin: 0;
    box-shadow: none;
    display: none;
  }
}
</style>

<style lang="scss">S
.btn-outline-success {
  padding: 2px 8px;
  margin: 0;
}
.checkmark {
  color: #4dbd74;
  height: 20px;
}

.nice-table {
  height: 200px;
  margin: 5px;
  border: 1px solid #ccc;
  background: rgb(29,66,97);
  table {
    display: flex;
    flex-flow: column;
    height: 100%;
    width: 100%;
    th {
    border-bottom: 1px solid transparent;
      font-weight: 400;
      color: white;
    }
    td {
      padding: 3px;
    }
    button {
      margin: 0;
      padding: 2px 14px;
      background: white;
    } 
    i {
      margin-left: 18px;
      font-size: 16px;
      // height: 23px;
    }
  }
  tbody {
    flex: 1 1 auto;
    display: block;
    overflow-y: scroll;
    background: #fff;
    tr {
      width: 100%;
      //outline: 1px solid lightblue;
    }
  }
  thead {
    flex: 0 0 auto;
    width: calc(100% - 20px);
  }
  thead,
  tbody tr {
    display: table;
    table-layout: fixed;
  }
  th,
  td {
    border: none;
    white-space: nowrap;
    overflow-x: hidden;
  }
}

.splitpanes--horizontal > .splitpanes__splitter {
  min-height: 8px;
  border: 1px solid #ccc;
}
.splitpanes--horizontal > .splitpanes__splitter:hover{
  background: #ccc;
  transition: background linear 200ms;
}
</style>
