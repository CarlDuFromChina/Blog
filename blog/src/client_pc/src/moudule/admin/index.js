import blogRouter from './blog';
import recommandBlog from './recommandBlog';

export default [
  {
    path: '/admin',
    name: 'admin',
    component: () => import('./admin'),
    children: [].concat(recommandBlog)
  }
].concat(blogRouter);
