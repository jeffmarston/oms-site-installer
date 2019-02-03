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
              name: '<button></button>',
              url: '/prod/servers/summary',
              icon: 'icon-puzzle'
            },
            {
              name: 'eze-app01',
              url: '/prod/servers/eze-app01',
              icon: 'icon-puzzle'
            },
            {
              name: 'eze-app02',
              url: '/prod/servers/eze-app02',
              icon: 'icon-puzzle'
            },
            {
              name: 'eze-app03',
              url: '/prod/servers/eze-app03',
              icon: 'icon-puzzle'
            }
          ]
        },
        {
          name: 'Client',
          url: '/client',
          icon: 'icon-puzzle'
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
