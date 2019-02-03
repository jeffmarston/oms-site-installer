export default {
  items: [
    {
      name: 'Dashboard',
      url: '/dashboard',
      icon: 'icon-speedometer',
    },
    {
      title: true,
      name: 'Environments',
      class: '',
      wrapper: {
        element: '',
        attributes: {}
      }
    },
    {
      name: 'Prod',
      url: '/prod',
      icon: 'icon-puzzle',
      children: [
        {
          name: 'Servers',
          url: '/prod/servers',
          icon: 'icon-puzzle',
          children: [
            {
              name: 'Summary',
              url: '/prod/servers/summary',
              icon: 'icon-arrow-up-circle'
            },
            {
              name: 'eze-app01',
              url: '/prod/servers/eze-app01',
              icon: 'icon-screen-smartphone'
            },
            {
              name: 'eze-app02',
              url: '/prod/servers/eze-app02',
              icon: 'icon-screen-smartphone'
            },
            {
              name: 'eze-app03',
              url: '/prod/servers/eze-app03',
              icon: 'icon-screen-smartphone'
            }
          ]
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
    }
  ]
}
