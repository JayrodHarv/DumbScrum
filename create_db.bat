ECHO off

sqlcmd -S localhost -E -i dumb_scrum_db.sql

rem server is localhost

ECHO .
ECHO if no errors appear DB was created
PAUSE