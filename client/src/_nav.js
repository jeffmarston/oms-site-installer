const navTreeData = require('./nav-data.json');

let items = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    icon: 'icon-speedometer',
  },
  {
    title: true,
    name: 'Runtime',
    class: '',
    wrapper: {
      element: '',
      attributes: {}
    }
  },
  {
    name: 'Servers',
    url: '/prod/servers',
    icon: 'icon-puzzle',
    children: []
  },
  {
    name: 'Clients',
    url: '/clients',
    icon: 'icon-puzzle',
    children: []
  },
  {
    name: 'Database',
    url: '/base/carousels',
    icon: 'icon-puzzle',
    children: []
  },
  {
    title: true,
    name: 'Administration',
    class: '',
    wrapper: {
      element: '',
      attributes: {}
    }
  },
  {
    name: 'Upgrade',
    url: '/upgrade',
    icon: 'icon-arrow-up-circle'
  }
]

let currentEnv = navTreeData;
currentEnv.servers.forEach(svr => {
  items[2].children.push({
    name: svr.name,
    url: '/servers/' + svr.name
  });
});
currentEnv.clients.forEach(cli => {
  items[3].children.push({
    name: cli.name,
    url: '/clients/' + cli.name
  });
});
currentEnv.database.forEach(db => {
  items[4].children.push({
    name: db.name,
    url: '/database/' + db.name
  });
});

export default {
  items: items
}