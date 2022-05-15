UPDATE sys_menu
SET router = replace(router, 'blogs', 'post')
WHERE router like '%blogs%';