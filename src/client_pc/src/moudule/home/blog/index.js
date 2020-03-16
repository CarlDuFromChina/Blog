export default [
  {
    path: '/blog/:id?',
    name: 'blog',
    component: () => import('./blogReadonly')
  },
  {
    path: '/blogEdit/:id?',
    name: 'blogEdit',
    component: () => import('./blogEdit')
  }
];
