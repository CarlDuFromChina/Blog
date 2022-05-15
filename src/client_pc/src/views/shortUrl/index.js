export default [
  {
    name: 'short-url',
    path: '/short/:shortid',
    component: () => import('./short')
  }
];
