import sysEntity from './sysEntity';
import sysMenu from './sysMenu';
import sysParamGroup from './sysParamGroup';
import userInfo from './userInfo';
import job from './job';
import sysConfig from './sysConfig';
import wechat from './wechat';
import gallery from './gallery';
import fileManage from './fileManage';
import robot from './robot';
import role from './role';
import admin from './admin';
import notification from './notification';

export { admin };
export default [].concat(sysEntity, sysMenu, sysParamGroup, userInfo, job, sysConfig, wechat, gallery, fileManage, robot, role, notification);
