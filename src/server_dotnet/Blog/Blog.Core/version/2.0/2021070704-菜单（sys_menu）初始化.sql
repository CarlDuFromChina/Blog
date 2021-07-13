INSERT INTO sys_menu (
    sys_menuid,
    name,
    icon,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = 'DD0FCF4C-10E6-4DB5-8255-35984F5DB134'
);

INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = '77E973D5-7EC0-4904-A43C-C6623B02D9FC'
);

INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = 'C64951C4-9FE0-432D-B899-3225DA3A64FF'
);

INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = '202FEB91-90E1-467B-9DDA-086A636DECD2'
);

INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = 'A0C5D935-DF51-4476-B4B0-73C3A33264DB'
);

INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = '417A0A4D-44ED-43BA-B693-BA65079A8C62'
);

INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = '1C403F1C-F83C-4AF2-8260-AC686C5211C1'
);

INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = '35F740A3-F094-4A33-BD53-D43B489EB28E'
);

INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = '9118991A-9B9E-4F1B-8858-57515AC32763'
);

INSERT INTO sys_menu (
    sys_menuid,
    name,
    icon,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = '12D15EB7-BF19-4D23-B6EA-7353B1808B03'
);

INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = 'DA0A4366-07EF-4693-8064-0A73F4FD8BEC'
);

INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = '58104351-642F-4C9B-BDD9-8507A8C51B62'
);

INSERT INTO sys_menu (
    sys_menuid,
    name,
    icon,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = 'A22CBC18-F186-43F4-804F-7C90E9A468E9'
);

INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = '0AC19F87-91AE-4A36-AC9D-3429AB90D7E4'
);

INSERT INTO sys_menu (
    sys_menuid,
    parentid,
    parentidname,
    name,
    menu_index,
    statecode,
    statecodename,
    router,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
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
    SELECT sys_menuid FROM sys_menu WHERE sys_menuid = 'D2C3C24F-F57C-429D-8EC7-40FA7B8D29D3'
);