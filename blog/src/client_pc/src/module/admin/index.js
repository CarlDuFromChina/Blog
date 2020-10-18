import recommendInfo from './recommendInfo';
import idea from './idea';
import myAdmin from './myAdmin';
import blog from './blog';
import draft from './draft';
import classification from './classification';
import series from './series';
import wechat from './wechat';

const adminRouter = [].concat(recommendInfo, idea, blog, draft, classification, series, wechat);
export { myAdmin, adminRouter };
