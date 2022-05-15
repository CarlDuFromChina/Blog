INSERT INTO sys_menu (
    id,
    name,
    icon,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    'DD0FCF4C-10E6-4DB5-8255-35984F5DB134',
    '系统设置',
    'setting',
    '30',
    1,
    '启用',
    NULL,
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = 'DD0FCF4C-10E6-4DB5-8255-35984F5DB134'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '77E973D5-7EC0-4904-A43C-C6623B02D9FC',
    'DD0FCF4C-10E6-4DB5-8255-35984F5DB134',
    '系统设置',
    '菜单管理',
    '3000',
    1,
    '启用',
    'sysmenu',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '77E973D5-7EC0-4904-A43C-C6623B02D9FC'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    'C64951C4-9FE0-432D-B899-3225DA3A64FF',
    'DD0FCF4C-10E6-4DB5-8255-35984F5DB134',
    '系统设置',
    '实体',
    '3010',
    1,
    '启用',
    'sysEntity',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = 'C64951C4-9FE0-432D-B899-3225DA3A64FF'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '202FEB91-90E1-467B-9DDA-086A636DECD2',
    'DD0FCF4C-10E6-4DB5-8255-35984F5DB134',
    '系统设置',
    '作业管理',
    '3020',
    1,
    '启用',
    'job',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '202FEB91-90E1-467B-9DDA-086A636DECD2'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    'A0C5D935-DF51-4476-B4B0-73C3A33264DB',
    'DD0FCF4C-10E6-4DB5-8255-35984F5DB134',
    '系统设置',
    '异步管理',
    '3030',
    1,
    '启用',
    'async',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = 'A0C5D935-DF51-4476-B4B0-73C3A33264DB'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '417A0A4D-44ED-43BA-B693-BA65079A8C62',
    'DD0FCF4C-10E6-4DB5-8255-35984F5DB134',
    '系统设置',
    '用户信息',
    '3040',
    1,
    '启用',
    'userInfo',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '417A0A4D-44ED-43BA-B693-BA65079A8C62'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '1C403F1C-F83C-4AF2-8260-AC686C5211C1',
    'DD0FCF4C-10E6-4DB5-8255-35984F5DB134',
    '系统设置',
    '选项集',
    '3050',
    1,
    '启用',
    'sysParamGroup',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '1C403F1C-F83C-4AF2-8260-AC686C5211C1'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '35F740A3-F094-4A33-BD53-D43B489EB28E',
    'DD0FCF4C-10E6-4DB5-8255-35984F5DB134',
    '系统设置',
    '系统参数',
    '3060',
    1,
    '启用',
    'sysConfig',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '35F740A3-F094-4A33-BD53-D43B489EB28E'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '9118991A-9B9E-4F1B-8858-57515AC32763',
    'DD0FCF4C-10E6-4DB5-8255-35984F5DB134',
    '系统设置',
    '角色管理',
    '3070',
    1,
    '启用',
    'role',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '9118991A-9B9E-4F1B-8858-57515AC32763'
);

INSERT INTO sys_menu (
    id,
    name,
    icon,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '12D15EB7-BF19-4D23-B6EA-7353B1808B03',
    '机器人管理',
    'robot',
    '21',
    1,
    '启用',
    NULL,
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '12D15EB7-BF19-4D23-B6EA-7353B1808B03'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    'DA0A4366-07EF-4693-8064-0A73F4FD8BEC',
    '12D15EB7-BF19-4D23-B6EA-7353B1808B03',
    '机器人管理',
    '机器人',
    '100',
    1,
    '启用',
    'robot',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = 'DA0A4366-07EF-4693-8064-0A73F4FD8BEC'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '58104351-642F-4C9B-BDD9-8507A8C51B62',
    '12D15EB7-BF19-4D23-B6EA-7353B1808B03',
    '机器人管理',
    '机器人消息任务',
    '200',
    1,
    '启用',
    'robotMessageTask',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '58104351-642F-4C9B-BDD9-8507A8C51B62'
);

INSERT INTO sys_menu (
    id,
    name,
    icon,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    'A22CBC18-F186-43F4-804F-7C90E9A468E9',
    '资源管理',
    'folder',
    '25',
    1,
    '启用',
    NULL,
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = 'A22CBC18-F186-43F4-804F-7C90E9A468E9'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '0AC19F87-91AE-4A36-AC9D-3429AB90D7E4',
    'A22CBC18-F186-43F4-804F-7C90E9A468E9',
    '资源管理',
    '文件管理',
    '500',
    1,
    '启用',
    'fileManage',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = '0AC19F87-91AE-4A36-AC9D-3429AB90D7E4'
);

INSERT INTO sys_menu (
    id,
    parentid,
    parentid_name,
    name,
    menu_index,
    statecode,
    statecode_name,
    router,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    'D2C3C24F-F57C-429D-8EC7-40FA7B8D29D3',
    'A22CBC18-F186-43F4-804F-7C90E9A468E9',
    '资源管理',
    '图库',
    '1000',
    1,
    '启用',
    'gallery',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_menu WHERE id = 'D2C3C24F-F57C-429D-8EC7-40FA7B8D29D3'
);