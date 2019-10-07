//const navTreeData = require('./nav-data.json');

var currentUserRole = localStorage.getItem('role');

function roleIn(roles) {
    return (roles.indexOf(currentUserRole) > -1);
}

let items = [
  {
    hidden: !roleIn(['basic', 'admin', 'eze']),
    name: 'Dashboards', title: true
  },
  {
    hidden: !roleIn(['basic', 'eze']),
    name: 'System',
    url: '/dashboard/system',
    icon: 'icon-speedometer'
  },
  {
    hidden: !roleIn(['admin', 'eze']),
    name: 'Networking',
    url: '/dashboard/network',
    icon: 'icon-speedometer'
  },
  {
    hidden: !roleIn(['basic','admin', 'eze']),
    name: 'Administration', title: true,
  },
  {
    hidden: !roleIn(['basic', 'admin', 'eze']),
    name: 'Configuration',
    url: '/admin/config',
    icon: 'fa fa-cog',
    children: [{
      hidden: !roleIn(['basic', 'admin', 'eze']),
        name: 'FIX Setup',
        url: '/admin/fixsetup' 
      },
      {
        hidden: !roleIn(['basic', 'admin', 'eze']),
        name: 'Settings Browser',
        url: '/admin/settingsbrowser'
      },
      {
        hidden: !roleIn(['basic', 'admin', 'eze']),
        name: 'Network Communicator',
        url: '/admin/communicator'
      },
      {
        hidden: !roleIn(['basic', 'admin', 'eze']),
        name: 'Logging',
        url: '/admin/logging'
      }]
  },
  {
    hidden: !roleIn(['admin', 'eze']),
    name: 'Services',
    url: '/admin/services',
    icon: 'cui-screen-smartphone'
  },
  {
    hidden: !roleIn(['admin', 'eze']),
    name: 'Accounts',
    url: '/admin/acounts',
    icon: 'icon-people',
    children: [
      {
        hidden: !roleIn(['admin', 'eze']),
        name: 'Users and Roles',
        url: '/admin/userroles'
      },
      {
        hidden: !roleIn(['admin', 'eze']),
        name: 'Services Logins',
        url: '/admin/servicelogins'
      }]
  },
  {
    hidden: !roleIn(['basic', 'admin', 'eze']),
    name: 'Get Plugins',
    url: '/admin/plugins',
    icon: 'fa fa-plug',
    children: [
      {
        hidden: !roleIn(['basic', 'admin', 'eze']),
        name: 'Columns',
        url: '/admin/plugins/columns'
      },
      {
        hidden: !roleIn(['basic', 'admin', 'eze']),
        name: 'Compliance',
        url: '/admin/plugins/compliance'
      },
      {
        hidden: !roleIn(['basic', 'admin', 'eze']),
        name: 'Trade Rules',
        url: '/admin/plugins/traderules'
      }
    ]
  },
  {
    hidden: !roleIn(['eze']),
    name: 'API Portal',
    url: '/admin/apis',
    icon: 'icon-list'
  },
  {
    hidden: !roleIn(['eze']),
    name: 'Diagnostics', title: true
  },
  {
    hidden: !roleIn(['eze']),
    name: 'Database',
    url: '/diagnostics/database',
    icon: 'fa fa-database'
  },
  {
    hidden: !roleIn(['eze']),
    name: 'Logs',
    url: '/diagnostics/logs',
    icon: 'cui-file'
  },
  {
    hidden: !roleIn(['eze']),
    name: 'Data Integrity',
    url: '/diagnostics/dataintegrity',
    icon: 'fa fa-code'
  },
  {
    hidden: !roleIn(['eze']),
    name: 'Eze Monitor',
    url: '/diagnostics/ezemonitor',
    icon: 'icon-bell'
  },
  {
    hidden: !roleIn(['basic', 'admin', 'eze']),
    name: 'Version', title: true
  },
  {
    hidden: !roleIn(['admin', 'eze']),
    name: 'Upgrade',
    url: '/version/upgrade',
    icon: 'icon-arrow-up-circle'
  },
  {
    hidden: !roleIn(['basic', 'admin', 'eze']),
    name: 'Release Notes',
    url: '/version/releasenotes',
    icon: 'icon-note'
  }
]

export default {
  items: items
}