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
    '540A9C6E-785D-4E31-BD73-85B9CEB155A1',
    '启用草稿功能',
    'enable_draft',
    'true',
    '启用草稿功能',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_configid FROM sys_config WHERE sys_configid = '540A9C6E-785D-4E31-BD73-85B9CEB155A1'
);