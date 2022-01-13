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
    SELECT id FROM sys_menu WHERE id = '8201EFED-76E2-4CD1-A522-4803D52D4D92'
);
