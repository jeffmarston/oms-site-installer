import Vue from 'vue'
import Router from 'vue-router'

// Containers
const DefaultContainer = () => import('@/containers/DefaultContainer')

// Views - Components
const SystemUsageDash = () => import('@/views/dashboard/SystemUsage')
const NetworkingDash = () => import('@/views/dashboard/Networking')
const UpgradeEnvironment = () => import('@/views/base/UpgradeEnvironment')
const Services = () => import('@/views/base/Services')
const DatabaseDetail = () => import('@/views/base/DatabaseDetail')
const Users = () => import('@/views/users/Users')
const ServiceLogins = () => import('@/views/users/ServiceLogins')
const Settings = () => import('@/views/base/Settings')
const Alerts = () => import('@/views/notifications/Alerts')

// Views - Pages
const Page404 = () => import('@/views/pages/Page404')
const Login = () => import('@/views/pages/Login')

const envProvider = require('@/shared/environmentProvider');
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
          redirect: '/dashboard/system',
          component: {
            render(c) { return c('router-view') }
          },
          children: [
            {
              path: 'system',
              name: 'system',
              component: SystemUsageDash
            },
            {
              path: 'network',
              name: 'network',
              component: NetworkingDash
            }
          ]
        },
        {
          path: 'admin',
          name: 'administration',
          redirect: '/admin/services',
          component: {
            render(c) { return c('router-view') }
          },
          children: [
            {
              path: 'services',
              name: 'services',
              component: Services
            },
            {
              path: 'logs',
              name: 'log management',
              component: Page404
            },
            {
              path: 'userRoles',
              name: 'users and roles',
              component: Users
            },
            {
              path: 'serviceLogins',
              name: 'service logins',
              component: ServiceLogins
            },
            {
              path: 'apis',
              name: 'API portal',
              component: Page404
            }
          ]
        },
        {
          path: 'diagnostics',
          name: 'diagnostics',
          redirect: '/diagnostics/database',
          component: {
            render(c) { return c('router-view') }
          },
          children: [
            {
              path: 'database',
              name: 'database',
              component: DatabaseDetail
            },
            {
              path: 'dataintegrity',
              name: 'data integrity',
              component: Page404
            },
            {
              path: 'ezemonitor',
              name: 'Eze Monitor',
              component: Page404
            }
          ]
        },
        {
          path: 'version',
          name: 'version',
          redirect: '/version/upgrade',
          component: {
            render(c) { return c('router-view') }
          },
          children: [
            {
              path: 'upgrade',
              name: 'upgrade',
              component: UpgradeEnvironment
            },
            {
              path: 'releasenotes',
              name: 'release notes',
              component: Page404
            }
          ]
        },
        {
          path: 'settings',
          name: 'settings',
          component: Settings
        },
        {
          path: 'alerts',
          name: 'alerts',
          component: Alerts
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
