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
    '41FC5E03-3FC7-4044-9947-5711DE8D7AD9',
    '微信后台',
    'wechat',
    '20',
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
    SELECT id FROM sys_menu WHERE id = '41FC5E03-3FC7-4044-9947-5711DE8D7AD9'
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
    'A40BCEF0-039F-4982-812A-79E3F63119DC',
    '41FC5E03-3FC7-4044-9947-5711DE8D7AD9',
    '微信后台',
    '图文素材库',
    '1000',
    1,
    '启用',
    'newsMaterial',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = 'A40BCEF0-039F-4982-812A-79E3F63119DC'
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
    '3E4691A3-D9BC-43A1-B0ED-C2E3440EC354',
    '41FC5E03-3FC7-4044-9947-5711DE8D7AD9',
    '微信后台',
    '素材库',
    '1002',
    1,
    '启用',
    'material',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '3E4691A3-D9BC-43A1-B0ED-C2E3440EC354'
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
    '451F9C81-0C01-40A1-8FD4-B04E17D4D99B',
    '41FC5E03-3FC7-4044-9947-5711DE8D7AD9',
    '微信后台',
    '自动回复',
    '1005',
    1,
    '启用',
    'autoReply',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '451F9C81-0C01-40A1-8FD4-B04E17D4D99B'
);