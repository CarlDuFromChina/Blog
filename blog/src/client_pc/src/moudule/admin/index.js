import blogRouter from './blog';
import recommandBlog from './recommandBlog';
import idea from './idea';

export default [
  {
    path: '/admin',
    name: 'admin',
    component: () => import('./admin'),
    children: [].concat(recommandBlog, idea)
  }
].concat(blogRouter);
