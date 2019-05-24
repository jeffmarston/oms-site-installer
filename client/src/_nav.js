//const navTreeData = require('./nav-data.json');

let items = [
  {
    name: 'Dashboards', title: true
  },
  {
    name: 'System',
    url: '/dashboard/system',
    icon: 'icon-speedometer'
  },
  {
    name: 'Networking',
    url: '/dashboard/network',
    icon: 'icon-speedometer'
  },
  {
    name: 'Administration', title: true
  },
  {
    name: 'Services',
    url: '/admin/services',
    icon: 'cui-screen-smartphone'
  },
  {
    name: 'Log Management',
    url: '/admin/logs',
    icon: 'cui-file'
  },
   {
    name: 'Users and Roles',
    url: '/admin/userroles',
    icon: 'icon-people'
  },
  {
    name: 'Services Logins',
    url: '/admin/servicelogins',
    icon: 'icon-settings'
  },
  {
    name: 'API Portal',
    url: '/admin/apis',
    icon: 'icon-list'
  },
  {
    name: 'Diagnostics', title: true
  },
  {
    name: 'Database',
    url: '/diagnostics/database',
    icon: 'fa fa-database'
  },
  {
    name: 'Data Integrity',
    url: '/diagnostics/dataintegrity',
    icon: 'fa fa-code'
  },
  {
    name: 'Eze Monitor',
    url: '/diagnostics/ezemonitor',
    icon: 'icon-bell'
  },
  {
    name: 'Version', title: true
  },
  {
    name: 'Upgrade',
    url: '/version/upgrade',
    icon: 'icon-arrow-up-circle'
  },
  {
    name: 'Release Notes',
    url: '/version/releasenotes',
    icon: 'icon-note'
  }
]


// const envProvider = require('@/services/environmentProvider');

// envProvider.getNavTree().then(navTreeData => {
//   navTreeData.servers.forEach(svr => {
//     items[2].children.push({
//       name: svr.name,
//       url: '/servers/' + svr.name
//     });
//   });
//   navTreeData.clients.forEach(cli => {
//     items[3].children.push({
//       name: cli.name,
//       url: '/clients/' + cli.name
//     });
//   });
//   navTreeData.database.forEach(db => {
//     items[4].children.push({
//       name: db.name,
//       url: '/database/' + db.name
//     });
//   });
// });

export default {
  items: items
}