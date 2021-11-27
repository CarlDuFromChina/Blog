DROP TABLE IF EXISTS friend_blog;
DELETE FROM sys_attrs WHERE entityid = (SELECT sys_entityid FROM sys_entity WHERE code = 'friend_blog');
DELETE FROM sys_entity WHERE code = 'friend_blog';
