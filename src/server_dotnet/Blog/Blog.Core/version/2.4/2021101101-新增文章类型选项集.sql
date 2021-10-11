INSERT INTO sys_paramgroup (
    sys_paramgroupid,
    name,
    code,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_paramgroupid FROM sys_paramgroup WHERE sys_paramgroupid = '9e3c42c1-bbd9-4bb0-8701-9c9c01fe65b2'
);

INSERT INTO sys_param (
    sys_paramid,
    name,
    code,
    sys_paramgroupid,
    sys_paramgroupidname,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = '01f8d2a9-4e81-4a8d-9315-4ddcc3e66253'
);
INSERT INTO sys_param (
    sys_paramid,
    name,
    code,
    sys_paramgroupid,
    sys_paramgroupidname,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = '4c6b9bb0-6284-4eb0-8d7b-4468034a1fb2'
);
INSERT INTO sys_param (
    sys_paramid,
    name,
    code,
    sys_paramgroupid,
    sys_paramgroupidname,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = '70271b50-476e-42b6-a7f2-c0fed5df98eb'
);
