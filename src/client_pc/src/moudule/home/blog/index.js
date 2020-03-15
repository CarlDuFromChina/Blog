export default [
  {
    path: '/blog/:id?',
    name: 'blog',
    component: () => import('./blog')
  }
];
