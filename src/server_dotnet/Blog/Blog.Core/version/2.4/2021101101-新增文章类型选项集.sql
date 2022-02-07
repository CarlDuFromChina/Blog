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
    '9e3c42c1-bbd9-4bb0-8701-9c9c01fe65b2',
    '文章类型',
    'article_type',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_paramgroup WHERE id = '9e3c42c1-bbd9-4bb0-8701-9c9c01fe65b2'
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
    '01f8d2a9-4e81-4a8d-9315-4ddcc3e66253',
    '原创',
    'original',
    '9e3c42c1-bbd9-4bb0-8701-9c9c01fe65b2',
    '文章类型',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_param WHERE id = '01f8d2a9-4e81-4a8d-9315-4ddcc3e66253'
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
    '4c6b9bb0-6284-4eb0-8d7b-4468034a1fb2',
    '转载',
    'reproduction',
    '9e3c42c1-bbd9-4bb0-8701-9c9c01fe65b2',
    '文章类型',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_param WHERE id = '4c6b9bb0-6284-4eb0-8d7b-4468034a1fb2'
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
    '70271b50-476e-42b6-a7f2-c0fed5df98eb',
    '翻译',
    'translation',
    '9e3c42c1-bbd9-4bb0-8701-9c9c01fe65b2',
    '文章类型',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_param WHERE id = '70271b50-476e-42b6-a7f2-c0fed5df98eb'
);
