INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
)
SELECT
    '72941895-810C-40DD-AEF9-A9CCF9276962',
    '9F44851D-9D95-453C-A69A-BB196A8C120A',
    '内容管理',
    '链接信息',
    '2040',
    1,
    '启用',
    'link',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = '72941895-810C-40DD-AEF9-A9CCF9276962'
);
