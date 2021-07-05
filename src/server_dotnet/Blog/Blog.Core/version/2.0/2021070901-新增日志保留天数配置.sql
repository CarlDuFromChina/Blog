INSERT INTO sys_config (
    sys_configid,
    name,
    code,
    value,
    description,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
)
SELECT
    '525BC22A-B892-46CC-BA66-0416A9BC865A',
    '日志保留天数',
    'log_backup_days',
    '30',
    '日志保留天数',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_configid FROM sys_config WHERE sys_configid = '525BC22A-B892-46CC-BA66-0416A9BC865A'
);