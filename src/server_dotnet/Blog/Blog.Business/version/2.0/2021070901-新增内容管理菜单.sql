INSERT INTO sys_menu (
    id,
    name,
    icon,
    menu_index,
    statecode,
    statecodename,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '9F44851D-9D95-453C-A69A-BB196A8C120A',
    '内容管理',
    'container',
    '10',
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
    SELECT id FROM sys_menu WHERE id = '9F44851D-9D95-453C-A69A-BB196A8C120A'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '8243B4B5-E622-4361-91CA-13ED458B924C',
    '9F44851D-9D95-453C-A69A-BB196A8C120A',
    '内容管理',
    '系列管理',
    '1900',
    1,
    '启用',
    'series',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '8243B4B5-E622-4361-91CA-13ED458B924C'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    'D7F5968F-CC68-4897-88B3-25FB26925845',
    '9F44851D-9D95-453C-A69A-BB196A8C120A',
    '内容管理',
    '文章管理',
    '2000',
    1,
    '启用',
    'blogs',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = 'D7F5968F-CC68-4897-88B3-25FB26925845'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '13DD9EAC-52B4-4046-A9E1-565C1E2C1009',
    '9F44851D-9D95-453C-A69A-BB196A8C120A',
    '内容管理',
    '专栏管理',
    '2005',
    1,
    '启用',
    'classification',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '13DD9EAC-52B4-4046-A9E1-565C1E2C1009'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    'ED81B1D7-C135-43C6-90E4-7C9A5E0EA284',
    '9F44851D-9D95-453C-A69A-BB196A8C120A',
    '内容管理',
    '草稿管理',
    '2010',
    1,
    '启用',
    'drafts',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = 'ED81B1D7-C135-43C6-90E4-7C9A5E0EA284'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '1C98872F-5C05-4ED5-930C-8A1446B77522',
    '9F44851D-9D95-453C-A69A-BB196A8C120A',
    '内容管理',
    '想法管理',
    '2020',
    1,
    '启用',
    'idea',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '1C98872F-5C05-4ED5-930C-8A1446B77522'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '9FC13449-9EB3-485A-A712-57BD39ACB015',
    '9F44851D-9D95-453C-A69A-BB196A8C120A',
    '内容管理',
    '读书笔记管理',
    '2025',
    1,
    '启用',
    'readingNote',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '9FC13449-9EB3-485A-A712-57BD39ACB015'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '3C2162BA-EA12-458E-8C50-15251F2A95EE',
    '9F44851D-9D95-453C-A69A-BB196A8C120A',
    '内容管理',
    '推荐信息管理',
    '2030',
    1,
    '启用',
    'recommendInfo',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '3C2162BA-EA12-458E-8C50-15251F2A95EE'
);
