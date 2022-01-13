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
    '45CD5927-E5D7-4DE9-841A-6CEA4D2A1CF3',
    '主页用户',
    'index_user',
    '',
    '主页用户编码',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_config WHERE id = '45CD5927-E5D7-4DE9-841A-6CEA4D2A1CF3'
);