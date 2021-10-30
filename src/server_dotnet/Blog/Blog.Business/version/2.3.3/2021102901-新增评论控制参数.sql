INSERT INTO sys_config (
    sys_configid,
    name,
    code,
    value,
    description,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
)
SELECT
    'B00741E8-F7E8-4726-96EA-9A30734586F6',
    '启用评论',
    'enable_comment',
    'true',
    '启用评论',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_configid FROM sys_config WHERE sys_configid = 'B00741E8-F7E8-4726-96EA-9A30734586F6'
);