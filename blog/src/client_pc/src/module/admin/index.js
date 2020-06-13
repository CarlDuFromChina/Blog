import recommendInfo from './recommendInfo';
import idea from './idea';

const blog = [{
  path: '/admin/blog/:type',
  name: 'blog',
  component: () => import('./blog/blogList')
}];

export default [].concat(blog, recommendInfo, idea);
