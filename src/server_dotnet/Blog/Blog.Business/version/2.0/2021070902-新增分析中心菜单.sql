INSERT INTO sys_menu (
    id,
    name,
    icon,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
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
    SELECT id FROM sys_menu WHERE id = 'B6786EC6-4E66-4F01-9D42-D40351877E13'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
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
    SELECT id FROM sys_menu WHERE id = 'CB578ECE-60AC-404F-BF77-C39A4F42D3E4'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
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
    SELECT id FROM sys_menu WHERE id = 'FF6C145B-1C40-44D2-B2B5-CFD726C8FCB3'
);