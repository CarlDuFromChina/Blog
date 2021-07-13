INSERT INTO sys_role (
    sys_roleid,
    name,
    is_basic,
    description,
    is_sys,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
)
SELECT
    '00000000-0000-0000-0000-000000000000',
    '系统管理员',
    1,
    '系统管理员',
    1,
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_roleid FROM sys_role WHERE sys_roleid = '00000000-0000-0000-0000-000000000000'
);

INSERT INTO sys_role (
    sys_roleid,
    name,
    is_basic,
    description,
    is_sys,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
)
SELECT
    '111111111-11111-1111-1111-111111111111',
    '系统',
    1,
    '系统',
    1,
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_roleid FROM sys_role WHERE sys_roleid = '111111111-11111-1111-1111-111111111111'
);

INSERT INTO sys_role (
    sys_roleid,
    name,
    is_basic,
    description,
    is_sys,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
)
SELECT
    '222222222-22222-2222-2222-222222222222',
    '访客',
    1,
    '访客',
    1,
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_roleid FROM sys_role WHERE sys_roleid = '222222222-22222-2222-2222-222222222222'
);

INSERT INTO sys_role (
    sys_roleid,
    name,
    is_basic,
    description,
    is_sys,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
)
SELECT
    '333333333-33333-3333-3333-333333333333',
    '用户',
    1,
    '用户',
    1,
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_roleid FROM sys_role WHERE sys_roleid = '333333333-33333-3333-3333-333333333333'
);
