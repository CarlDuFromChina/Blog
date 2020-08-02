import recommendInfo from './recommendInfo';
import idea from './idea';
import myAdmin from './myAdmin';
import blog from './blog';

const adminRouter = [].concat(recommendInfo, idea, blog);
export { myAdmin, adminRouter };
