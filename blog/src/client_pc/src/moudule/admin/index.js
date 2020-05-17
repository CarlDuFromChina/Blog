import blogRouter from './blog';
import recommendInfo from './recommendInfo';
import idea from './idea';

export default [
  {
    path: '/admin',
    name: 'admin',
    component: () => import('./admin'),
    children: [].concat(recommendInfo, idea)
  }
].concat(blogRouter);
