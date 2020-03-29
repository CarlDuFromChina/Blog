export default [
  {
    path: '/home/sysmenu',
    name: 'sysmenuList',
    component: () => import('./sysmenuList')
  },
  {
    path: '/home/sysmenuEdit/:id?',
    name: 'sysmenuEdit',
    component: () => import('./sysmenuEdit')
  }
];
