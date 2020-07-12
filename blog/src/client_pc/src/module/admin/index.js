import recommendInfo from './recommendInfo';
import idea from './idea';
import myAdmin from './myAdmin';

const blog = [{
  path: '/admin/blog/:type',
  name: 'blog',
  component: () => import('./blog/blogList')
}];

const adminRouter = [].concat(blog, recommendInfo, idea);
export { myAdmin, adminRouter };
