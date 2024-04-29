color 0a
@ECHO OFF
ECHO Creating Database

sqlcmd -S localhost -E -i masterhomelessdb.sql
sqlcmd -S localhost -E -i masterhomelessdbinserts.sql
sqlcmd -S localhost -E -i masterhomelessstoredprocedures.sql

rem server is localhost

ECHO Database created if no errors occured
PAUSE
