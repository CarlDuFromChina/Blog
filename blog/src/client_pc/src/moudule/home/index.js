import blogRouter from './blog';

export default [
  {
    path: '/home',
    name: 'home',
    component: () => import('./home'),
    children: []
  }
].concat(blogRouter);
