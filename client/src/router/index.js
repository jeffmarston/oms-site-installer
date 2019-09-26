import Vue from 'vue';
import Router from 'vue-router';
import store from '../store/store'

// Containers
const DefaultContainer = () => import('@/containers/DefaultContainer');

// Views - Components
const SystemUsageDash = () => import('@/views/dashboard/SystemUsage');
const NetworkingDash = () => import('@/views/dashboard/Networking');
const UpgradeEnvironment = () => import('@/views/version/UpgradeEnvironment');
const Services = () => import('@/views/admin/Services');
const DatabaseDetail = () => import('@/views/diagnostics/DatabaseDetail');
const Logs = () => import('@/views/diagnostics/Logs');
const DataIntegrity = () => import('@/views/diagnostics/DataIntegrity');
const Users = () => import('@/views/admin/Users');
const ServiceLogins = () => import('@/views/admin/ServiceLogins');
const Settings = () => import('@/views/general/Settings');
const Alerts = () => import('@/views/notifications/Alerts');
const ColumnPlugins = () => import('@/views/admin/ColumnPlugins');
const CompliancePlugins = () => import('@/views/admin/CompliancePlugins');

// Views - Pages
const Page404 = () => import('@/views/pages/Page404');
const Login = () => import('@/views/pages/Login');

const envProvider = require('@/shared/environmentProvider');
const _ = require('lodash');

Vue.use(Router)

const router = new Router({
  mode: 'hash', // https://router.vuejs.org/api/#mode
  linkActiveClass: 'open active',
  scrollBehavior: () => ({ y: 0 }),
  routes: []
})

function guard(to, from, next) {
  let username = localStorage.getItem('username');
  console.log("== Here is my store ===========");
  console.log(username);
  console.log("===============================");

  if (username != null) {
    next(); // allow to enter route
  } else {
    next('/login'); // go to '/login';
  }
}

envProvider.getNavTree().then(navTreeData => {
  router.beforeEach((to, from, next) => {
      let username = localStorage.getItem('username');
      if (username != null || to.name === 'Login') {
        next(); // allow to enter route
      } else {
        next('/login'); // redirect to login page;
      }
  })

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
              beforeEnter: guard, // Using guard before entering the route
              component: Services
            },
            {
              path: 'userroles',
              name: 'users and roles',
              component: Users
            },
            {
              path: 'servicelogins',
              name: 'service logins',
              component: ServiceLogins
            },
            {
              path: 'plugins',
              name: 'plugins',
              component: {
                render(c) { return c('router-view') }
              },
              children: [
                {
                  path: 'columns',
                  name: 'columns',
                  component: ColumnPlugins
                },
                {
                  path: 'compliance',
                  name: 'compliance',
                  component: CompliancePlugins
                }
              ]
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
              path: 'logs',
              name: 'logs',
              component: Logs
            },
            {
              path: 'dataintegrity',
              name: 'data integrity',
              component: DataIntegrity
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
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
      path: '/404',
      name: 'Page404',
      component: Page404
    }
  ]);
});

export default router;
