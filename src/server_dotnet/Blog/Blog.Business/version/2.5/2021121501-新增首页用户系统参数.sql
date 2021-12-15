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
    '45CD5927-E5D7-4DE9-841A-6CEA4D2A1CF3',
    '主页用户',
    'index_user',
    '',
    '主页用户编码',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_configid FROM sys_config WHERE sys_configid = '45CD5927-E5D7-4DE9-841A-6CEA4D2A1CF3'
);