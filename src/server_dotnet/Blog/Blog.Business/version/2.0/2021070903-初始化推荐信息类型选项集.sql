INSERT INTO sys_paramgroup (
    id,
    name,
    code,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '1062CA39-9CEF-4AA4-919C-F5BD5752BC9E',
    '推荐类型',
    'recommend_type',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_paramgroup WHERE id = '1062CA39-9CEF-4AA4-919C-F5BD5752BC9E'
);

INSERT INTO sys_param (
    id,
    name,
    code,
    sys_paramgroupid,
    sys_paramgroupid_name,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '15F05AAF-7C47-4376-B8DF-C59FB58D7FFB',
    '链接',
    'url',
    '1062CA39-9CEF-4AA4-919C-F5BD5752BC9E',
    '推荐类型',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_param WHERE id = '15F05AAF-7C47-4376-B8DF-C59FB58D7FFB'
);

INSERT INTO sys_param (
    id,
    name,
    code,
    sys_paramgroupid,
    sys_paramgroupid_name,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '7EC0D436-A2E6-4659-BD74-0BDEDF3581A2',
    '图片',
    'picture',
    '1062CA39-9CEF-4AA4-919C-F5BD5752BC9E',
    '推荐类型',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_param WHERE id = '7EC0D436-A2E6-4659-BD74-0BDEDF3581A2'
);
