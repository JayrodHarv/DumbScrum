/* check to see if the database exits, if so, drop it */
IF EXISTS (SELECT 1 FROM master.dbo.sysdatabases
	WHERE name = 'dumb_scrum_db')
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

/* creating user table */
print '' print '*** creating user table ***'
GO
CREATE TABLE [dbo].[User] (
	[UserID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[Email]			[nvarchar] 	(100)					NOT NULL,
	[PasswordHash]	[nvarchar] 	(100)					NOT NULL	DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[DisplayName]	[nvarchar] 	(50)					NOT NULL,
	[Bio]			[nvarchar]	(255)					NULL,
	[Active]		[bit]								NOT NULL	DEFAULT 1,
	CONSTRAINT [pk_UserID] PRIMARY KEY ([UserID]),
	CONSTRAINT [ak_User_Email] UNIQUE([Email])
)
GO

/* creating project table */
print '' print '*** creating project table ***'
GO
CREATE TABLE [dbo].[Project] (
	[ProjectID]		[nvarchar]	(50)					NOT NULL,
	[ProjectOwner]	[nvarchar] 	(50)					NOT NULL,
	[DateCreated]	[date]								NOT NULL,
	[Status]		[nvarchar] 	(50)					NOT NULL,
	[Description]	[nvarchar]	(255)					NULL,		
	CONSTRAINT [pk_ProjectID] PRIMARY KEY ([ProjectID])
)
GO

/* creating projectmember table */
print '' print '*** creating projectmember table ***'
GO
CREATE TABLE [dbo].[ProjectMember] (
	[UserID]		[int]								NOT NULL,
	[ProjectID]		[nvarchar] 	(50)					NOT NULL,
	[Role]			[nvarchar]	(50)					NOT NULL,	
	CONSTRAINT	[fk_ProjectMember_UserID]	FOREIGN KEY ([UserID])
		REFERENCES	[dbo].[User] ([UserID]),
	CONSTRAINT	[fk_ProjectMember_ProjectID]	FOREIGN KEY ([ProjectID])
		REFERENCES	[dbo].[Project] ([ProjectID]),
	CONSTRAINT [pk_ProjectMember] PRIMARY KEY ([UserID], [ProjectID])
)
GO

/* creating feature table */
print '' print '*** creating feature table ***'
GO
CREATE TABLE [dbo].[Feature] (
	[FeatureID]		[int]								NOT NULL,
	[ProjectID]		[nvarchar] 	(50)					NOT NULL,
	[Name]			[nvarchar]	(50)					NOT NULL,	
	[Description]	[nvarchar]	(255)					NOT NULL,
	[Status]		[nvarchar]	(50)					NOT NULL,
	CONSTRAINT	[fk_Feature_ProjectID]	FOREIGN KEY ([ProjectID])
		REFERENCES	[dbo].[Project] ([ProjectID]),
	CONSTRAINT [pk_Feature] PRIMARY KEY ([FeatureID])
)
GO

/* creating userstory table */
print '' print '*** creating userstory table ***'
GO
CREATE TABLE [dbo].[UserStory] (
	[StoryID]		[int]								NOT NULL,
	[FeatureID]		[int]								NOT NULL,
	[Person]		[nvarchar]	(50)					NOT NULL,	
	[Action]		[nvarchar]	(50)					NOT NULL,
	[Reason]		[nvarchar]	(50)					NOT NULL,
	CONSTRAINT	[fk_UserStory_FeatureID]	FOREIGN KEY ([FeatureID])
		REFERENCES	[dbo].[Feature] ([FeatureID]),
	CONSTRAINT [pk_StoryID] PRIMARY KEY ([StoryID])
)
GO

/* creating sprint table */
print '' print '*** creating sprint table ***'
GO
CREATE TABLE [dbo].[Sprint] (
	[SprintID]		[int]								NOT NULL,
	[FeatureID]		[int]								NOT NULL,
	[StartDate]		[date]								NOT NULL,	
	[EndDate]		[date]								NOT NULL,
	CONSTRAINT	[fk_Sprint_FeatureID]	FOREIGN KEY ([FeatureID])
		REFERENCES	[dbo].[Feature] ([FeatureID]),
	CONSTRAINT [pk_SprintID] PRIMARY KEY ([SprintID])
)
GO

/* creating sprintitem table */
print '' print '*** creating sprintitem table ***'
GO
CREATE TABLE [dbo].[SprintItem] (
	[ItemID]		[int]								NOT NULL,
	[SprintID]		[int]								NOT NULL,
	[StoryID]		[int]								NOT NULL,
	[UserID]		[int]								NOT NULL,
	[Status]		[nvarchar]							NOT NULL,
	CONSTRAINT	[fk_SprintItem_SprintID]	FOREIGN KEY ([SprintID])
		REFERENCES	[dbo].[Sprint] ([SprintID]),
	CONSTRAINT	[fk_SprintItem_StoryID]	FOREIGN KEY ([StoryID])
		REFERENCES	[dbo].[UserStory] ([StoryID]),
	CONSTRAINT	[fk_SprintItem_UserID]	FOREIGN KEY ([UserID])
		REFERENCES	[dbo].[User] ([UserID]),
	CONSTRAINT [pk_ItemID] PRIMARY KEY ([ItemID])
)
GO

/* creating message table */
print '' print '*** creating message table ***'
GO
CREATE TABLE [dbo].[Message] (
	[MessageID]		[int]								NOT NULL,
	[UserID]		[int]								NOT NULL,
	[Text]			[nvarchar]							NOT NULL,
	[DateTime]		[datetime]							NOT NULL,
	CONSTRAINT	[fk_Message_UserID]	FOREIGN KEY ([UserID])
		REFERENCES	[dbo].[User] ([UserID]),
	CONSTRAINT [pk_MessageID] PRIMARY KEY ([MessageID])
)
GO

/* creating chat table */
print '' print '*** creating chat table ***'
GO
CREATE TABLE [dbo].[Chat] (
	[ChatID]		[int]								NOT NULL,
	[MessageID]		[int]								NOT NULL,
	CONSTRAINT	[fk_Chat_MessageID]	FOREIGN KEY ([MessageID])
		REFERENCES	[dbo].[Message] ([MessageID]),
	CONSTRAINT [pk_ChatID] PRIMARY KEY ([ChatID])
)
GO

/* Stored Procedures */
print '' print '*** creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user] (
	@Email			[nvarchar] (100),
	@PasswordHash	[nvarchar] (100)
)
AS
	BEGIN
		SELECT COUNT([UserID]) AS 'Authenticated'
		FROM [User]
		WHERE @Email = [Email]
			AND @PasswordHash = [PasswordHash]
			AND [Active] = 1
	END
GO

print '' print '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email] (
	@Email			[nvarchar] (100)
)
AS
	BEGIN
		SELECT [UserID], [DisplayName], [Email], [Bio], [Active]
		FROM [User]
		WHERE @Email = [Email]
	END
GO

print '' print '*** creating sp_update_PasswordHash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_PasswordHash] (
	@Email				[nvarchar] (100),
	@NewPasswordHash	[nvarchar] (100)
)
AS
	BEGIN
		UPDATE [User]
		SET [PasswordHash] = @NewPasswordHash
		WHERE @Email = [Email]
		RETURN @@ROWCOUNT
	END
GO


/* Insert test records */
print '' print '*** inserting User test records ***'
GO
INSERT INTO [dbo].[User]
		([DisplayName], [Email], [Bio])
	VALUES
		('Jared Harvey', 'jared.harvey10@gmail.com', 'I am programmer'),
		('Joe Winters', 'joe-winters@weatherchannel.com', 'I am weather man'),
		('Barack Obama', 'barack-obama@whitehouse.org', 'I am former president')
GO