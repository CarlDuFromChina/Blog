INSERT INTO sys_config (
    id,
    name,
    code,
    value,
    description,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
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
    SELECT id FROM sys_config WHERE id = '525BC22A-B892-46CC-BA66-0416A9BC865A'
);