INSERT INTO sys_menu (
    sys_menuid,
    name,
    icon,
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
    '8201EFED-76E2-4CD1-A522-4803D52D4D92',
    '博客管理',
    'book',
    '8',
    1,
    '启用',
    NULL,
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = '8201EFED-76E2-4CD1-A522-4803D52D4D92'
);
