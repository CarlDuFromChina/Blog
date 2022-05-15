INSERT INTO sys_role (
    id,
    name,
    is_basic,
    description,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '00000000-0000-0000-0000-000000000000',
    '系统管理员',
    1,
    '系统管理员',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_role WHERE id = '00000000-0000-0000-0000-000000000000'
);

INSERT INTO sys_role (
    id,
    name,
    is_basic,
    description,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '111111111-11111-1111-1111-111111111111',
    '系统',
    1,
    '系统',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_role WHERE id = '111111111-11111-1111-1111-111111111111'
);

INSERT INTO sys_role (
    id,
    name,
    is_basic,
    description,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '222222222-22222-2222-2222-222222222222',
    '访客',
    1,
    '访客',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_role WHERE id = '222222222-22222-2222-2222-222222222222'
);

INSERT INTO sys_role (
    id,
    name,
    is_basic,
    description,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '333333333-33333-3333-3333-333333333333',
    '用户',
    1,
    '用户',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_role WHERE id = '333333333-33333-3333-3333-333333333333'
);
