import Vue from 'vue'
import Router from 'vue-router'

// Containers
const DefaultContainer = () => import('@/containers/DefaultContainer')

// Views
const Dashboard = () => import('@/views/Dashboard')

// Views - Components
const UpgradeEnvironment = () => import('@/views/base/UpgradeEnvironment')
const Services = () => import('@/views/base/Services')
const ClientDetail = () => import('@/views/base/ClientDetail')
const DatabaseDetail = () => import('@/views/base/DatabaseDetail')

// Views - Pages
const Page404 = () => import('@/views/pages/Page404')
const Login = () => import('@/views/pages/Login')

const navTreeData = require('../nav-data.json');
const _ = require('lodash');

function createSubRoute(items, component) {
  return _.map(items, machine => {
    return {
      path: machine.name,
      name: machine.name,
      component: component
    };
  });
}



Vue.use(Router)

let routes = [
  {
    path: '/',
    redirect: '/dashboard',
    name: 'home',
    component: DefaultContainer,
    children: [
      {
        path: 'dashboard',
        name: 'dashboard',
        component: Dashboard
      },
      {
        path: 'servers',
        name: 'servers',
        component: {
          render(c) { return c('router-view') }
        },
        children: createSubRoute(navTreeData.servers, Services)
      },
      {
        path: 'clients',
        name: 'clients',
        component: {
          render(c) { return c('router-view') }
        },
        children: createSubRoute(navTreeData.clients, ClientDetail)
      },
      {
        path: 'database',
        name: 'database',
        component: {
          render(c) { return c('router-view') }
        },
        children: createSubRoute(navTreeData.database, DatabaseDetail)
      },
      {
        path: 'upgrade',
        name: 'upgrade',
        component: UpgradeEnvironment
      }
    ]
  },
  {
    path: '/pages',
    redirect: '/pages/404',
    name: 'Pages',
    component: {
      render(c) { return c('router-view') }
    },
    children: [
      {
        path: '404',
        name: 'Page404',
        component: Page404
      },
      {
        path: 'login',
        name: 'Login',
        component: Login
      }
    ]
  }
];

export default new Router({
  mode: 'hash', // https://router.vuejs.org/api/#mode
  linkActiveClass: 'open active',
  scrollBehavior: () => ({ y: 0 }),
  routes: routes
})
