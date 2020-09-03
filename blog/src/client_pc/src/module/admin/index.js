import recommendInfo from './recommendInfo';
import idea from './idea';
import myAdmin from './myAdmin';
import blog from './blog';
import draft from './draft';
import classfication from './classfication';

const adminRouter = [].concat(recommendInfo, idea, blog, draft, classfication);
export { myAdmin, adminRouter };
