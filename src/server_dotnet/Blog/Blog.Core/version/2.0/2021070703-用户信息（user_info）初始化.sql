﻿INSERT INTO "public"."user_info" (
	"id",
	"code",
	"gender",
	"gender_name",
	"realname",
	"mailbox",
	"introduction",
	"cellphone",
	"avatar",
	"roleid",
	"roleid_name",
	"statecode",
	"statecode_name",
	"name",
	"created_by",
	"created_by_name",
	"created_at",
	"updated_by",
	"updated_by_name",
	"updated_at" 
)
SELECT
	'00000000-0000-0000-0000-000000000000',
	'Admin',
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	'00000000-0000-0000-0000-000000000000',
	'系统管理员',
	1,
	'启用',
	'系统管理员',
	'111111111-11111-1111-1111-111111111111',
	'系统',
	'2021-09-05 20:06:21.578549',
	'111111111-11111-1111-1111-111111111111',
	'系统',
	'2021-09-05 20:06:21.578554' 
WHERE NOT EXISTS (
	SELECT id FROM user_info WHERE id =  '00000000-0000-0000-0000-000000000000'
);
