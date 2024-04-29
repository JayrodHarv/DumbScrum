color 0a
@ECHO OFF
ECHO Creating Database

sqlcmd -S localhost -E -i dumb_scrum_tables.sql
sqlcmd -S localhost -E -i dumb_scrum_inserts.sql
sqlcmd -S localhost -E -i dumb_scrum_stored_procedures.sql

rem server is localhost

ECHO Database created if no errors occured
PAUSE
