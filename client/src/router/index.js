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
const Users = () => import('@/views/users/Users')
const ServiceLogins = () => import('@/views/users/ServiceLogins')
const Settings = () => import('@/views/base/Settings')
const Icons = () => import('@/views/icons/CoreUIIcons');

// Views - Pages
const Page404 = () => import('@/views/pages/Page404')
const Login = () => import('@/views/pages/Login')

const envProvider = require('@/services/environmentProvider');
const _ = require('lodash');

Vue.use(Router)

const router = new Router({
  mode: 'hash', // https://router.vuejs.org/api/#mode
  linkActiveClass: 'open active',
  scrollBehavior: () => ({ y: 0 }),
  routes: []
})


envProvider.getNavTree().then(navTreeData => {
  router.addRoutes([
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
          path: '/servers/:name',
          name: 'servers',
          component: Services
        },
        {
          path: '/clients/:name',
          name: 'clients',
          component: ClientDetail
        },
        {
          path: '/database/:name',
          name: 'database',
          component: DatabaseDetail
        },
        {
          path: '/upgrade',
          name: 'upgrade',
          component: UpgradeEnvironment
        },
        {
          path: '/credentials',
          name: 'credentials',
          component: {
            render(c) { return c('router-view') }
          },
          children: [
            {
              path: '/users',
              name: 'users',
              component: Users
            },
            {
              path: '/services',
              name: 'services',
              component: ServiceLogins
            }
          ]
        },
        {
          path: 'icons',
          name: 'icons',
          component: Icons
        },
        {
          path: 'settings',
          name: 'settings',
          component: Settings
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
  ]);
});

export default router;
