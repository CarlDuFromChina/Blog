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
    SELECT sys_paramgroupid FROM sys_paramgroup WHERE sys_paramgroupid = '1062CA39-9CEF-4AA4-919C-F5BD5752BC9E'
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
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = '15F05AAF-7C47-4376-B8DF-C59FB58D7FFB'
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
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = '7EC0D436-A2E6-4659-BD74-0BDEDF3581A2'
);
