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
    'B6786EC6-4E66-4F01-9D42-D40351877E13',
    '分析中心',
    'dashboard',
    '5',
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = 'B6786EC6-4E66-4F01-9D42-D40351877E13'
);

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
    'CB578ECE-60AC-404F-BF77-C39A4F42D3E4',
    '6786EC6-4E66-4F01-9D42-D40351877E13',
    '分析中心',
    '网站分析',
    '100',
    1,
    '启用',
    'websiteAnalysis',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = 'CB578ECE-60AC-404F-BF77-C39A4F42D3E4'
);

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
    'FF6C145B-1C40-44D2-B2B5-CFD726C8FCB3',
    'B6786EC6-4E66-4F01-9D42-D40351877E13',
    '分析中心',
    '工作空间',
    '500',
    1,
    '启用',
    'workplace',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = 'FF6C145B-1C40-44D2-B2B5-CFD726C8FCB3'
);