INSERT INTO auth_user (
    id,
    code,
    name,
    is_lock,
    is_lock_name,
    roleid,
    roleid_name,
    password,
    user_infoid,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '00000000-0000-0000-0000-000000000000',
    'Admin',
    '系统管理员',
    0,
    '否',
    '00000000-0000-0000-0000-000000000000',
    '系统管理员',
    '4297f44b13955235245b2497399d7a93',
    '00000000-0000-0000-0000-000000000000',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM auth_user WHERE id = '00000000-0000-0000-0000-000000000000'
);

INSERT INTO auth_user (
    id,
    code,
    name,
    is_lock,
    is_lock_name,
    roleid,
    roleid_name,
    password,
    user_infoid,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '111111111-11111-1111-1111-111111111111',
    'System',
    '系统',
    0,
    '否',
    '111111111-11111-1111-1111-111111111111',
    '访客',
    '4297f44b13955235245b2497399d7a93',
    '111111111-11111-1111-1111-111111111111',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM auth_user WHERE id = '111111111-11111-1111-1111-111111111111'
);

INSERT INTO auth_user (
    id,
    code,
    name,
    is_lock,
    is_lock_name,
    roleid,
    roleid_name,
    password,
    user_infoid,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '222222222-22222-2222-2222-222222222222',
    'Anonymous',
    '访客',
    0,
    '否',
    '222222222-22222-2222-2222-222222222222',
    '访客',
    '4297f44b13955235245b2497399d7a93',
    '222222222-22222-2222-2222-222222222222',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM auth_user WHERE id = '222222222-22222-2222-2222-222222222222'
);