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
    'A41CD339-97E3-4494-B814-B2D0A9461C8E',
    'Github Client ID',
    'github_oauth_client_id',
    '',
    'Github Client ID',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_config WHERE id = 'A41CD339-97E3-4494-B814-B2D0A9461C8E'
);

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
    '323AB13D-F25A-4CD4-8BD9-78FC83697EB7',
    'Github Client Secret',
    'github_oauth_client_secret',
    '',
    'Github Client Secret',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_config WHERE id = '323AB13D-F25A-4CD4-8BD9-78FC83697EB7'
);