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
    'E7D80743-081D-4B51-BFFF-149FFFF8E652',
    '任务状态',
    'job_state',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramgroupid FROM sys_paramgroup WHERE sys_paramgroupid = 'E7D80743-081D-4B51-BFFF-149FFFF8E652'
);

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
    'E944E20B-A463-4FE3-B2E6-ADE32C0709F3',
    '操作类型',
    'operation_type',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramgroupid FROM sys_paramgroup WHERE sys_paramgroupid = 'E944E20B-A463-4FE3-B2E6-ADE32C0709F3'
);

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
    'CE9753B3-E86F-4DF9-A63D-8B46AD8A187C',
    '权限',
    'privilege',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramgroupid FROM sys_paramgroup WHERE sys_paramgroupid = 'CE9753B3-E86F-4DF9-A63D-8B46AD8A187C'
);