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
    'B00741E8-F7E8-4726-96EA-9A30734586F6',
    '启用评论',
    'enable_comment',
    'true',
    '启用评论',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_config WHERE id = 'B00741E8-F7E8-4726-96EA-9A30734586F6'
);