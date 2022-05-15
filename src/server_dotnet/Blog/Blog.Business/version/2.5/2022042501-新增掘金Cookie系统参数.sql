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
    '7646C8B3-DB20-4000-B559-8681F2C81BBE',
    '掘金 Cookie',
    'juejin_cookie',
    '',
    '掘金 Cookie，有效期为一个月',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_config WHERE id = '7646C8B3-DB20-4000-B559-8681F2C81BBE'
);