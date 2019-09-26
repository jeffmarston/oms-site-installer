<template>
  <div class="app">
    <AppHeader fixed>
      <SidebarToggler class="d-lg-none" display="md" mobile />
      <b-link class="navbar-brand" to="#">
        <img class="navbar-brand-full" src="img/brand/logo.svg" width="170" height="55" alt="Ezesoft Logo">
        <img class="navbar-brand-minimized" src="img/brand/sygnet.svg" width="30" height="30" alt="Ezesoft Logo">
      </b-link>
      <SidebarToggler class="d-md-down-none" display="lg" />
          
          <b-nav>
            <b-button variant="success" class="btn-pill">Prod</b-button>
            <b-button variant="ghost" class="btn-pill">UAT</b-button>
            <b-button variant="ghost" class="btn-pill">Dev</b-button>
          </b-nav>

      <b-navbar-nav class="ml-auto">
        <span>{{ username }}</span>
        <b-nav-item class="d-md-down-none" to="/alerts">
          <i class="icon-bell"></i>
          <b-badge pill variant="danger">3</b-badge>
        </b-nav-item>
        <DefaultHeaderDropdownAccnt/>
      </b-navbar-nav>
    </AppHeader>
    <div class="app-body">
      <AppSidebar fixed>
        <SidebarNav :navItems="nav"></SidebarNav>
      </AppSidebar>
      <main class="main">
        <Breadcrumb :list="list"/>
        <div class="container-fluid">
          <router-view :key="$route.fullPath"></router-view>
        </div>
      </main>
    </div>
  </div>
</template>

<script>
import nav from '@/_nav'
import { Header as AppHeader,Aside as AppAside, AsideToggler, Footer as TheFooter, Breadcrumb } from '@coreui/vue'
import { SidebarToggler, Sidebar as AppSidebar, SidebarFooter, SidebarForm, SidebarHeader, SidebarMinimizer, SidebarNav } from './Sidebar'
import DefaultAside from './DefaultAside'
import DefaultHeaderDropdownAccnt from './DefaultHeaderDropdownAccnt'

export default {
  name: 'DefaultContainer',
  components: {
    AsideToggler,
    AppHeader,
    AppSidebar,
    AppAside,
    TheFooter,
    Breadcrumb,
    DefaultAside,
    DefaultHeaderDropdownAccnt,
    SidebarForm,
    SidebarFooter,
    SidebarToggler,
    SidebarHeader,
    SidebarNav,
    SidebarMinimizer
  },
  data () {
    return {
      nav: nav.items
    }
  },
  computed: {
    name () {
      return this.$route.name
    },
    list () {
      return this.$route.matched.filter((route) => route.name || route.meta.label )
    },
    username () {
      let user =  localStorage.getItem('username');
      let role =  localStorage.getItem('role');
      return `${user} (${role})`;
    }
  }
}
</script>
