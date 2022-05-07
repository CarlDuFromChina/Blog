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
    '294EC534-6B6A-404B-900D-6B8CEEE46734',
    'Gitee OAuth',
    'gitee_oauth',
    '',
    'Gitee OAuth',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_config WHERE id = '294EC534-6B6A-404B-900D-6B8CEEE46734'
);