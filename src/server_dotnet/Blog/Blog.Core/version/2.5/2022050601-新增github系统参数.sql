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
    '2CA9AC24-8316-488E-9F5C-DC3A743BEC3F',
    'Github OAuth',
    'github_oauth',
    '',
    'Github OAuth',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_config WHERE id = '2CA9AC24-8316-488E-9F5C-DC3A743BEC3F'
);