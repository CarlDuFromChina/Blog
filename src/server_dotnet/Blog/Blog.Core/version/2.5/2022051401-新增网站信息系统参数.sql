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
    'FB80866E-C5C0-404F-82B7-DCA9545AA9E9',
    '网站信息',
    'website_info',
    '{"author": "Karl Du","email": "18556906294@163.com","record_no": ""}',
    '网站信息：作者、邮箱、备案号等',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_config WHERE id = 'FB80866E-C5C0-404F-82B7-DCA9545AA9E9'
);