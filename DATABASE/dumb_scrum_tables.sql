
/* check to see if the database exits, if so, drop it */
IF EXISTS (SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'dumb_scrum_db')
BEGIN	
	DROP DATABASE dumb_scrum_db
	print '' print '*** dropping database dumb_scrum_db ***'
END
GO

print '' print '*** creating database dumb_scrum_db ***'
GO
	CREATE DATABASE dumb_scrum_db
GO

print '' print '*** using database dumb_scrum_db ***'
GO
	USE [dumb_scrum_db]
GO

print '' print '*** Creating Tables ***'

/* =================================================================================

								Create Table Statements
 
==================================================================================*/

/* creating role table */
print '' print '*** creating role table ***'
GO
CREATE TABLE [dbo].[Role] (
	[RoleID]		[nvarchar] (100) NOT NULL,
	CONSTRAINT [pk_Role] PRIMARY KEY ([RoleID])
)
GO

/* creating user table */
print '' print '*** creating user table ***'
GO
CREATE TABLE [dbo].[User] (
	[UserID]		[int] 		IDENTITY(100000, 1)		NOT NULL,
	[Email]			[nvarchar] 	(100)					NOT NULL,
	[PasswordHash]	[nvarchar] 	(100)					NOT NULL	DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[DisplayName]	[nvarchar] 	(50)					NOT NULL	DEFAULT
		'New User',
	[Pfp]			[varbinary] (max)					NOT NULL,
	[Bio]			[nvarchar]	(255)					NULL,
	[RoleID]		[nvarchar]	(100)					NOT NULL DEFAULT "User",
	[Active]		[bit]								NOT NULL	DEFAULT 1,
	CONSTRAINT	[fk_User_RoleID]	FOREIGN KEY ([RoleID])
		REFERENCES	[dbo].[Role] ([RoleID]),
	CONSTRAINT [pk_UserID] PRIMARY KEY ([UserID]),
	CONSTRAINT [ak_User_Email] UNIQUE([Email])
)
GO

/* creating project table */
print '' print '*** creating project table ***'
GO
CREATE TABLE [dbo].[Project] (
	[ProjectID]				[nvarchar]	(50) 					NOT NULL,
	[UserID]				[int]								NOT NULL,
	[DateCreated]			[date]								NOT NULL,
	[Status]				[nvarchar] 	(50)					NOT NULL DEFAULT "Getting a grip...",
	[Description]			[nvarchar]	(255)					NOT NULL,		
	CONSTRAINT [pk_ProjectID] PRIMARY KEY ([ProjectID])
)
GO

/* creating projectrole table */
print '' print '*** creating projectrole table ***'
GO
CREATE TABLE [dbo].[ProjectRole] (
	ProjectRoleID				int	IDENTITY(1,100000)		NOT NULL,
	ProjectID					nvarchar(50)				NOT NULL,
	RoleName					nvarchar(100)				NOT NULL,
	FeaturePrivileges			bit							NOT NULL DEFAULT 0, 
	UserStoryPrivileges			bit							NOT NULL DEFAULT 0, 
	SprintPlanningPrivileges	bit							NOT NULL DEFAULT 0, 
	FeedMessagingPrivileges		bit							NOT NULL DEFAULT 0,
	TaskPrivileges				bit							NOT NULL DEFAULT 0,
	TaskReviewingPrivileges		bit							NOT NULL DEFAULT 0,
	ProjectManagementPrivileges	bit							NOT NULL DEFAULT 0,
	Description					nvarchar(255)				NOT NULL,
	CONSTRAINT	[fk_ProjectRole_ProjectID]	FOREIGN KEY ([ProjectID])
		REFERENCES	[dbo].[Project] ([ProjectID]) 
		ON DELETE CASCADE
		ON UPDATE CASCADE,	
	CONSTRAINT [pk_ProjectRole] PRIMARY KEY ([ProjectRoleID]),
	CONSTRAINT [ak_ProjectRole] UNIQUE ([RoleName], [ProjectID])
)
GO

/* creating projectmember table */
print '' print '*** creating projectmember table ***'
GO
CREATE TABLE [dbo].[ProjectMember] (
	[UserID]		int								NOT NULL,
	[ProjectID]		nvarchar(50)					NOT NULL,
	[ProjectRoleID] int								NULL,
	[Active]		bit								NOT NULL DEFAULT 1,
	CONSTRAINT	[fk_ProjectMember_UserID]	FOREIGN KEY ([UserID])
		REFERENCES	[dbo].[User] ([UserID]),
	CONSTRAINT	[fk_ProjectMember_ProjectID]	FOREIGN KEY ([ProjectID])
		REFERENCES	[dbo].[Project] ([ProjectID]) 
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT	[fk_ProjectMember_ProjectRoleID]	FOREIGN KEY ([ProjectRoleID])
		REFERENCES	[dbo].[ProjectRole] ([ProjectRoleID])
		ON DELETE NO ACTION,
	CONSTRAINT [pk_ProjectMember] PRIMARY KEY ([UserID], [ProjectID])
)
GO

/* creating feature table */
print '' print '*** creating feature table ***'
GO
CREATE TABLE [dbo].[Feature] (
	[FeatureID] 	[nvarchar] (50)						NOT NULL,
	[ProjectID] 	[nvarchar] (50) 					NOT NULL,
	[Name] 			[nvarchar] (50) 					NOT NULL,
	[Description]	[nvarchar] (255)					NOT NULL,
	[Priority]		[nvarchar] (20)						NOT NULL,
	[Status]		[nvarchar] (50)						NOT NULL DEFAULT "Awaiting Sprint",
	CONSTRAINT	[fk_Feature_ProjectID]	FOREIGN KEY ([ProjectID])
		REFERENCES	[dbo].[Project] ([ProjectID]) 
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT [pk_Feature] PRIMARY KEY ([FeatureID])
)
GO

/* creating userstory table */
print '' print '*** creating userstory table ***'
GO
CREATE TABLE [dbo].[UserStory] (
	[StoryID]		[nvarchar]	(50)					NOT NULL,
	[FeatureID]		[nvarchar]	(50)					NOT NULL,
	[Person]		[nvarchar]	(100)					NOT NULL,	
	[Action]		[nvarchar]	(255)					NOT NULL,
	[Reason]		[nvarchar]	(255)					NOT NULL,
	CONSTRAINT	[fk_UserStory_FeatureID]	FOREIGN KEY ([FeatureID])
		REFERENCES	[dbo].[Feature] ([FeatureID]) 
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT [pk_Story] PRIMARY KEY ([StoryID])
)
GO

/* creating sprint table */
print '' print '*** creating sprint table ***'
GO
CREATE TABLE [dbo].[Sprint] (
	[SprintID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[FeatureID]		[nvarchar]	(50)					NOT NULL,
	[Name]			[nvarchar]	(50)					NOT NULL,
	[StartDate]		[date]								NOT NULL,	
	[EndDate]		[date]								NOT NULL,
	[Active]		[bit]								NOT NULL DEFAULT 1,
	CONSTRAINT	[fk_Sprint_FeatureID]	FOREIGN KEY ([FeatureID])
		REFERENCES	[dbo].[Feature] ([FeatureID]) 
		ON DELETE CASCADE,
	CONSTRAINT [pk_Sprint] PRIMARY KEY ([SprintID])
)
GO

/* creating task table */
print '' print '*** creating task table ***'
GO
CREATE TABLE [dbo].[Task] (
	[TaskID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[SprintID]		[int]								NOT NULL,
	[StoryID]		[nvarchar]	(50)					NOT NULL,
	[UserID]		[int]								NULL,
	[Status]		[nvarchar]	(50)					NOT NULL,
	CONSTRAINT	[fk_Task_SprintID]	FOREIGN KEY ([SprintID])
		REFERENCES	[dbo].[Sprint] ([SprintID]) 
		ON DELETE CASCADE,
	CONSTRAINT	[fk_Task_StoryID]	FOREIGN KEY ([StoryID])
		REFERENCES	[dbo].[UserStory] ([StoryID]) 
		ON DELETE NO ACTION,
	CONSTRAINT	[fk_Task_UserID]	FOREIGN KEY ([UserID])
		REFERENCES	[dbo].[User] ([UserID]),
	CONSTRAINT [pk_TaskID] PRIMARY KEY ([TaskID])
)
GO

/* creating filestore table */
print '' print '*** creating filestore table ***'
GO
CREATE TABLE [dbo].[FileStore] (
	[FileID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[Data]			[varbinary]	(max)					NOT NULL,
	[Extension]		[nvarchar]	(255)					NOT NULL,
	[TaskID]		[int]								NULL,
	[ProjectID]		[nvarchar]	(50)					NULL,
	[FileName]		[nvarchar]	(100)					NOT NULL,
	[Type]			[nvarchar]	(50)					NOT NULL,
	[LastEdited]	[datetime]							NOT NULL,
	CONSTRAINT [fk_FileStore_TaskID] FOREIGN KEY ([TaskID])
		REFERENCES [dbo].[Task] ([TaskID]) 
		ON DELETE CASCADE,
	CONSTRAINT [fk_FileStore_ProjectID] FOREIGN KEY ([ProjectID])
		REFERENCES [dbo].[Project] ([ProjectID]) 
		ON DELETE NO ACTION,
	CONSTRAINT [pk_FileStore] PRIMARY KEY ([FileID])
)
GO

/* creating FeedMessage table */
print '' print '*** creating FeedMessage table ***'
GO
CREATE TABLE [dbo].[FeedMessage] (
	[MessageID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[SprintID]		[int]								NOT NULL,
	[UserID]		[int]								NOT NULL,
	[Text]			[Text]								NOT NULL,
	[SentAt]		[datetime]							NOT NULL,
	CONSTRAINT	[fk_Message_SprintID]	FOREIGN KEY ([SprintID])
		REFERENCES	[dbo].[Sprint] ([SprintID]) ON DELETE CASCADE,
	CONSTRAINT	[fk_Message_UserID]	FOREIGN KEY ([UserID])
		REFERENCES	[dbo].[User] ([UserID]) ON DELETE CASCADE,
	CONSTRAINT [pk_MessageID] PRIMARY KEY ([MessageID])
)
GO