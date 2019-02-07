

let items = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    icon: 'icon-speedometer',
  },
  {
    title: true,
    name: 'Roles',
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
    children: [{
      name: 'Summary',
      url: '/prod/servers/summary',
      icon: 'icon-speedometer',
    }]
  },
  {
    name: 'Client',
    url: '/client',
    icon: 'icon-puzzle',
    children: [
      {
        name: 'Upgrade',
        url: '/prod/servers/summary',
        icon: 'icon-arrow-up-circle'
      },
      {
        name: 'marston9020b',
        url: '/prod/servers/eze-app01',
        icon: 'icon-screen-desktop'
      },
      {
        name: 'cippy9020',
        url: '/prod/servers/eze-app02',
        icon: 'icon-screen-desktop'
      },
      {
        name: 'orzechowski7090',
        url: '/prod/servers/eze-app03',
        icon: 'icon-screen-desktop'
      }
    ]
  },
  {
    name: 'Database',
    url: '/base/carousels',
    icon: 'icon-puzzle'
  }
]

for (let i = 0; i < 8; i++) {
  items[2].children.push({
    name: 'eze-app0' + i,
    url: '/prod/servers/eze-app0' + i,
    icon: 'icon-puzzle'
  });
}


export default {
  items: items
}