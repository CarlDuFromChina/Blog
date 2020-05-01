
import { blogRouter, blogsRouter } from './blog';
import system from './system';

export default [
  {
    path: '/home',
    name: 'home',
    component: () => import('./home'),
    children: []
      .concat(blogsRouter.map(item => item[0]))
      .concat(system.map(item => item[0]))
  }
].concat(blogRouter);
