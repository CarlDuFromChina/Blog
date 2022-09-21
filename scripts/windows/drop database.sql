SELECT pg_terminate_backend(pg_stat_activity.pid) FROM pg_stat_activity WHERE datname='blog' AND pid<>pg_backend_pid();
create database blog;
