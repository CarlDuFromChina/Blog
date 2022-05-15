INSERT INTO sys_menu (
    id,
    parentid,
    parentId_name,
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
    SELECT id FROM sys_menu WHERE id = '72941895-810C-40DD-AEF9-A9CCF9276962'
);
