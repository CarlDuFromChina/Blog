ALTER TABLE sys_role_privilege RENAME COLUMN sys_entityid TO objectid;
ALTER TABLE sys_role_privilege RENAME COLUMN sys_entityidname TO objectidname;
ALTER TABLE sys_role_privilege ADD COLUMN IF NOT EXISTS object_type VARCHAR(100);

UPDATE sys_attrs
SET code = 'objectid'
WHERE code = 'sys_entityid' AND entityidname = '角色权限';

UPDATE sys_attrs
SET code = 'objectidname'
WHERE code = 'sys_entityidname' AND entityidname = '角色权限';

UPDATE sys_role_privilege
SET object_type = 'sys_entity';