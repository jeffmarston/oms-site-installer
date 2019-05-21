//const navTreeData = require('./nav-data.json');

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
    url: '/servers',
    icon: 'cui-screen-smartphone',
    children: []
  },
  {
    name: 'Clients',
    url: '/clients',
    icon: 'cui-screen-desktop',
    children: []
  },
  {
    name: 'Database',
    url: '/base/carousels',
    icon: 'fa fa-database',
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
    name: 'Services',
    url: '/services',
    icon: 'cui-screen-smartphone'
  },
  {
    name: 'Credentials',
    url: '/credentials',
    icon: 'icon-settings',
    children: [{
      name: 'Users',
      url: '/credentials/users'
    },
    {
      name: 'Services',
      url: '/credentials/services'
    }]
  },
  {
    name: 'Upgrade',
    url: '/upgrade',
    icon: 'icon-arrow-up-circle'
  }
]


const envProvider = require('@/services/environmentProvider');

envProvider.getNavTree().then(navTreeData => {
  navTreeData.servers.forEach(svr => {
    items[2].children.push({
      name: svr.name,
      url: '/servers/' + svr.name
    });
  });
  navTreeData.clients.forEach(cli => {
    items[3].children.push({
      name: cli.name,
      url: '/clients/' + cli.name
    });
  });
  navTreeData.database.forEach(db => {
    items[4].children.push({
      name: db.name,
      url: '/database/' + db.name
    });
  });
});

export default {
  items: items
}