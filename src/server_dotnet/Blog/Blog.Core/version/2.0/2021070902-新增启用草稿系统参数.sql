INSERT INTO sys_config (
    id,
    name,
    code,
    value,
    description,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
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
    SELECT id FROM sys_config WHERE id = '540A9C6E-785D-4E31-BD73-85B9CEB155A1'
);