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
    SELECT id FROM sys_paramgroup WHERE id = 'E7D80743-081D-4B51-BFFF-149FFFF8E652'
);

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
    SELECT id FROM sys_paramgroup WHERE id = 'E944E20B-A463-4FE3-B2E6-ADE32C0709F3'
);

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
    SELECT id FROM sys_paramgroup WHERE id = 'CE9753B3-E86F-4DF9-A63D-8B46AD8A187C'
);