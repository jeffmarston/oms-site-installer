import Vue from 'vue'
import Router from 'vue-router'

// Containers
const DefaultContainer = () => import('@/containers/DefaultContainer')

// Views
const Dashboard = () => import('@/views/Dashboard')

const Colors = () => import('@/views/theme/Colors')
const Typography = () => import('@/views/theme/Typography')

const Charts = () => import('@/views/Charts')
const Widgets = () => import('@/views/Widgets')

// Views - Components
const Cards = () => import('@/views/base/Cards')
const Forms = () => import('@/views/base/Forms')
const Switches = () => import('@/views/base/Switches')
const Tables = () => import('@/views/base/Tables')
const Servers = () => import('@/views/base/Servers')
const Environments = () => import('@/views/base/Environments')
const Machines = () => import('@/views/base/Machines')
const Tabs = () => import('@/views/base/Tabs')
const Breadcrumbs = () => import('@/views/base/Breadcrumbs')
const Carousels = () => import('@/views/base/Carousels')
const Collapses = () => import('@/views/base/Collapses')
const Jumbotrons = () => import('@/views/base/Jumbotrons')
const ListGroups = () => import('@/views/base/ListGroups')
const Navs = () => import('@/views/base/Navs')
const Navbars = () => import('@/views/base/Navbars')
const Paginations = () => import('@/views/base/Paginations')
const Popovers = () => import('@/views/base/Popovers')
const ProgressBars = () => import('@/views/base/ProgressBars')
const Tooltips = () => import('@/views/base/Tooltips')

// Views - Buttons
const StandardButtons = () => import('@/views/buttons/StandardButtons')
const ButtonGroups = () => import('@/views/buttons/ButtonGroups')
const Dropdowns = () => import('@/views/buttons/Dropdowns')
const BrandButtons = () => import('@/views/buttons/BrandButtons')

// Views - Icons
const Flags = () => import('@/views/icons/Flags')
const FontAwesome = () => import('@/views/icons/FontAwesome')
const SimpleLineIcons = () => import('@/views/icons/SimpleLineIcons')
const CoreUIIcons = () => import('@/views/icons/CoreUIIcons')

// Views - Notifications
const Alerts = () => import('@/views/notifications/Alerts')
const Badges = () => import('@/views/notifications/Badges')
const Modals = () => import('@/views/notifications/Modals')

// Views - Pages
const Page404 = () => import('@/views/pages/Page404')
const Page500 = () => import('@/views/pages/Page500')
const Login = () => import('@/views/pages/Login')
const Register = () => import('@/views/pages/Register')

// Users
const Users = () => import('@/views/users/Users')
const User = () => import('@/views/users/User')

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
        path: '500',
        name: 'Page500',
        component: Page500
      },
      {
        path: 'login',
        name: 'Login',
        component: Login
      },
      {
        path: 'register',
        name: 'Register',
        component: Register
      }
    ]
  }
];

let tree = [
  {
    env: "prod",
    servers: [
      { name: "ezeapp01" },
      { name: "ezeapp02" },
      { name: "ezeapp03" }
    ],
    clients: [
      { name: "marston9020b" }
    ]
  }
];

function createMachine(num) {
  return {
    path: 'eze-app0' + num,
    name: 'eze-app0' + num,
    component: Machines
  };
}

function createEnvironment(envObj) {
  let roles = [{
    path: 'upgrade',
    component: Environments
  },
  {
    path: 'servers',
    name: 'servers',
    redirect: 'servers/summary',      
    component: {
      render (c) { return c('router-view') }
    },
    children: [{
      path: 'summary',
      component: Cards
    }]
  }];
  for (let i = 0; i < 8; i++) {
    roles[1].children.push(createMachine(i));
  }

  let env = {
    path: envObj.env.toLowerCase(),
    name: envObj.env.toLowerCase(),
    redirect: envObj.env.toLowerCase() + '/upgrade',
    component: {
      render(c) { return c('router-view') }
    },
    children: roles
  };
  return env;
}

console.log(routes);
tree.forEach(env => {
  routes[0].children.push(createEnvironment(env));
});
console.log(routes);

export default new Router({
  mode: 'hash', // https://router.vuejs.org/api/#mode
  linkActiveClass: 'open active',
  scrollBehavior: () => ({ y: 0 }),
  routes: routes
})
