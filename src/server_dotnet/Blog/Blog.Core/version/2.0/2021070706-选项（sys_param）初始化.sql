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
    'EC95AF46-41AD-4DB7-9CA8-31BB370DCE90',
    '正常',
    '0',
    'E7D80743-081D-4B51-BFFF-149FFFF8E652',
    '任务状态',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = 'EC95AF46-41AD-4DB7-9CA8-31BB370DCE90'
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
    '7963E073-C4B7-4293-B5B7-511A7D4C85AE',
    '暂停',
    '1',
    'E7D80743-081D-4B51-BFFF-149FFFF8E652',
    '任务状态',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = '7963E073-C4B7-4293-B5B7-511A7D4C85AE'
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
    'EEF3EEF7-0DDF-4D7C-8067-09F5BF6F29FF',
    '完成',
    '2',
    'E7D80743-081D-4B51-BFFF-149FFFF8E652',
    '任务状态',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = 'EEF3EEF7-0DDF-4D7C-8067-09F5BF6F29FF'
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
    '95F893DD-401C-46B7-8BFF-A5D3B64DC25C',
    '错误',
    '3',
    'E7D80743-081D-4B51-BFFF-149FFFF8E652',
    '任务状态',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())s
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = '95F893DD-401C-46B7-8BFF-A5D3B64DC25C'
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
    'A286FD47-A4AF-4DBB-8F2A-11AFD12669B6',
    '阻塞',
    '4',
    'E7D80743-081D-4B51-BFFF-149FFFF8E652',
    '任务状态',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = 'A286FD47-A4AF-4DBB-8F2A-11AFD12669B6'
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
    'F76A6F62-7E7B-42B4-8657-195C3841E997',
    '不存在',
    '-1',
    'E7D80743-081D-4B51-BFFF-149FFFF8E652',
    '任务状态',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = 'F76A6F62-7E7B-42B4-8657-195C3841E997'
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
    'D1897BBD-7C79-4796-B27F-58AD02712F74',
    '全部',
    'all',
    'CE9753B3-E86F-4DF9-A63D-8B46AD8A187C',
    '权限',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = 'D1897BBD-7C79-4796-B27F-58AD02712F74'
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
    'A4BC8015-8759-4A52-97F9-B2CF3493C5EB',
    '个人',
    'user',
    'CE9753B3-E86F-4DF9-A63D-8B46AD8A187C',
    '权限',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = 'A4BC8015-8759-4A52-97F9-B2CF3493C5EB'
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
    '981B4FB8-FFBE-4C5D-A49B-3FA58803EA5C',
    '分组',
    'group',
    'CE9753B3-E86F-4DF9-A63D-8B46AD8A187C',
    '权限',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = '981B4FB8-FFBE-4C5D-A49B-3FA58803EA5C'
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
    'B1656E3B-DCB5-427A-8A8F-0C10763007B5',
    '游客',
    'guest',
    'CE9753B3-E86F-4DF9-A63D-8B46AD8A187C',
    '权限',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = 'B1656E3B-DCB5-427A-8A8F-0C10763007B5'
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
    '0ED53489-469B-4CB2-A4F2-61DEBD361952',
    '写',
    'write',
    'E944E20B-A463-4FE3-B2E6-ADE32C0709F3',
    '操作类型',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = '0ED53489-469B-4CB2-A4F2-61DEBD361952'
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
    'F097E80C-119C-4488-A272-6E92EB34C844',
    '读',
    'read',
    'E944E20B-A463-4FE3-B2E6-ADE32C0709F3',
    '操作类型',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = 'F097E80C-119C-4488-A272-6E92EB34C844'
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
    '9E817601-9959-4388-9879-4F7086D53343',
    '删除',
    'delete',
    'E944E20B-A463-4FE3-B2E6-ADE32C0709F3',
    '操作类型',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = '9E817601-9959-4388-9879-4F7086D53343'
);