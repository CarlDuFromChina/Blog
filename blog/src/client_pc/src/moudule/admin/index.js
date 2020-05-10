import blogRouter from './blog';

export default [
  {
    path: '/admin',
    name: 'admin',
    component: () => import('./admin')
  }
].concat(blogRouter);
