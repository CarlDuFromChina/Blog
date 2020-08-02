import recommendInfo from './recommendInfo';
import idea from './idea';
import myAdmin from './myAdmin';
import blog from './blog';
import draft from './draft';

const adminRouter = [].concat(recommendInfo, idea, blog, draft);
export { myAdmin, adminRouter };
