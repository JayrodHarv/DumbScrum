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

/* =================================================================================

									Create Table Statements
 
==================================================================================*/

/* creating user table */
print '' print '*** creating user table ***'
GO
CREATE TABLE [dbo].[User] (
	[UserID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[Email]			[nvarchar] 	(100)					NOT NULL,
	[PasswordHash]	[nvarchar] 	(100)					NOT NULL	DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[DisplayName]	[nvarchar] 	(50)					NOT NULL	DEFAULT
		'New User',
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
	[ProjectID]		[nvarchar]	(50) 					NOT NULL,
	[ProjectOwner]	[nvarchar] 	(50)					NOT NULL,
	[DateCreated]	[date]								NOT NULL,
	[Status]		[nvarchar] 	(50)					NOT NULL,
	[Description]	[nvarchar]	(255)					NULL,		
	CONSTRAINT [pk_ProjectID] PRIMARY KEY ([ProjectID])
)
GO

/* creating feature table */
print '' print '*** creating feature table ***'
GO
CREATE TABLE [dbo].[Feature] (
	[FeatureID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[ProjectID]		[nvarchar] 	(50)					NOT NULL,
	[Name]			[nvarchar]	(50)					NOT NULL,	
	[Description]	[nvarchar]	(255)					NOT NULL,
	[Priority]		[nvarchar]	(20)					NOT NULL,
	[Status]		[nvarchar]	(50)					NOT NULL,
	CONSTRAINT	[fk_Feature_ProjectID]	FOREIGN KEY ([ProjectID])
		REFERENCES	[dbo].[Project] ([ProjectID]),
	CONSTRAINT [pk_FeatureID] PRIMARY KEY ([FeatureID])
)
GO

/* creating userstory table */
print '' print '*** creating userstory table ***'
GO
CREATE TABLE [dbo].[UserStory] (
	[StoryID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[FeatureID]		[int]								NOT NULL,
	[Person]		[nvarchar]	(100)					NOT NULL,	
	[Action]		[nvarchar]	(255)					NOT NULL,
	[Reason]		[nvarchar]	(255)					NOT NULL,
	CONSTRAINT	[fk_UserStory_FeatureID]	FOREIGN KEY ([FeatureID])
		REFERENCES	[dbo].[Feature] ([FeatureID]),
	CONSTRAINT [pk_StoryID] PRIMARY KEY ([StoryID])
)
GO

/* creating sprint table */
print '' print '*** creating sprint table ***'
GO
CREATE TABLE [dbo].[Sprint] (
	[SprintID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[FeatureID]		[int]								NOT NULL,
	[StartDate]		[date]								NOT NULL,	
	[EndDate]		[date]								NULL,
	[Active]		[bit]								NULL DEFAULT 1,
	CONSTRAINT	[fk_Sprint_FeatureID]	FOREIGN KEY ([FeatureID])
		REFERENCES	[dbo].[Feature] ([FeatureID]),
	CONSTRAINT [pk_SprintID] PRIMARY KEY ([SprintID])
)
GO

/* creating projectmember table */
print '' print '*** creating projectmember table ***'
GO
CREATE TABLE [dbo].[ProjectMember] (
	[MemberID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[UserID]		[int]								NOT NULL,
	[ProjectID]		[nvarchar] 	(50)					NOT NULL,
	[Role]			[nvarchar]	(50)					NOT NULL,	
	CONSTRAINT	[fk_ProjectMember_UserID]	FOREIGN KEY ([UserID])
		REFERENCES	[dbo].[User] ([UserID]),
	CONSTRAINT	[fk_ProjectMember_ProjectID]	FOREIGN KEY ([ProjectID])
		REFERENCES	[dbo].[Project] ([ProjectID]),
	CONSTRAINT [pk_MemberID] PRIMARY KEY ([MemberID])
)
GO

/* creating task table */
print '' print '*** creating task table ***'
GO
CREATE TABLE [dbo].[Task] (
	[TaskID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[SprintID]		[int]								NOT NULL,
	[StoryID]		[int]								NOT NULL,
	[UserID]		[int]								NOT NULL,
	[Status]		[nvarchar]	(50)					NOT NULL,
	CONSTRAINT	[fk_Task_SprintID]	FOREIGN KEY ([SprintID])
		REFERENCES	[dbo].[Sprint] ([SprintID]),
	CONSTRAINT	[fk_Task_StoryID]	FOREIGN KEY ([StoryID])
		REFERENCES	[dbo].[UserStory] ([StoryID]),
	CONSTRAINT	[fk_Task_UserID]	FOREIGN KEY ([UserID])
		REFERENCES	[dbo].[User] ([UserID]),
	CONSTRAINT [pk_TaskID] PRIMARY KEY ([TaskID])
)
GO

/* creating message table */
print '' print '*** creating message table ***'
GO
CREATE TABLE [dbo].[Message] (
	[MessageID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[UserID]		[int]								NOT NULL,
	[Text]			[Text]								NOT NULL,
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
	[ChatID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[MessageID]		[int]								NOT NULL,
	CONSTRAINT	[fk_Chat_MessageID]	FOREIGN KEY ([MessageID])
		REFERENCES	[dbo].[Message] ([MessageID]),
	CONSTRAINT [pk_ChatID] PRIMARY KEY ([ChatID])
)
GO

/* =================================================================================

									Stored Procedures
 
==================================================================================*/

/*----- User Stored Procedures -----*/
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

print '' print '*** creating sp_update_DisplayName ***'
GO
CREATE PROCEDURE [dbo].[sp_update_DisplayName] (
	@UserID				[int],
	@DisplayName		[nvarchar] (100)
)
AS
	BEGIN
		UPDATE [User]
		SET [DisplayName] = @DisplayName
		WHERE @UserID = [UserID]
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_insert_user ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_user] (
	@Email				[nvarchar] (100),
	@PasswordHash		[nvarchar] (100)
)
AS
	BEGIN
		INSERT INTO [dbo].[User]
			([Email], [PasswordHash])
		VALUES
			(@Email, @PasswordHash)
	END
GO

/*----- Project Stored Procedures -----*/
print '' print '*** creating sp_select_all_projects ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_projects]
AS
	BEGIN
		SELECT *
		FROM [Project]
	END
GO

print '' print '*** creating sp_select_user_projects ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_projects] (
	@UserID		[int]
)
AS
	BEGIN
		SELECT *
		FROM [Project]
		INNER JOIN [ProjectMember]
		ON [ProjectMember].[UserID] = @UserID
	END
GO

print '' print '*** creating sp_select_project_by_projectid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_project_by_projectid] (
	@ProjectID		[nvarchar] (50)
)
AS
	BEGIN
		SELECT	[ProjectID], [ProjectOwner], [DateCreated], [Status], [Description]
		FROM [Project]
		WHERE [ProjectID] = @ProjectID
	END
GO

/*----- Feature Stored Procedures -----*/
print '' print '*** creating sp_select_project_features ***'
GO
CREATE PROCEDURE [dbo].[sp_select_project_features] (
	@ProjectID		[nvarchar] (50)
)
AS
	BEGIN
		SELECT *
		FROM [Feature]
		WHERE [Feature].[ProjectID] = @ProjectID
	END
GO

print '' print '*** creating sp_insert_project_feature ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_project_feature] (
	@ProjectID		[nvarchar] 	(50),
	@Name			[nvarchar]	(50),	
	@Description	[nvarchar]	(255),	
	@Priority		[nvarchar]	(20),	
	@Status			[nvarchar]	(50)	
)
AS
	BEGIN
		INSERT INTO [dbo].[Feature]
			([ProjectID], [Name], [Description], [Priority], [Status])
		VALUES
			(@ProjectID, @Name, @Description, @Priority, @Status)
	END
GO

/*----- UserStory Stored Procedures -----*/
print '' print '*** creating sp_select_feature_user_stories ***'
GO
CREATE PROCEDURE [dbo].[sp_select_feature_user_stories] (
	@FeatureID		[int]
)
AS
	BEGIN
		SELECT *
		FROM [UserStory]
		WHERE [UserStory].[FeatureID] = @FeatureID
	END
GO

/*----- Sprint Stored Procedures -----*/
print '' print '*** creating sp_select_project_sprints ***'
GO
CREATE PROCEDURE [dbo].[sp_select_project_sprints] (
	@ProjectID		[nvarchar] (50)
)
AS
	BEGIN
		SELECT [SprintID], [Sprint].[FeatureID], [StartDate], [EndDate], [Active]
		FROM [Sprint]
		INNER JOIN [dbo].[Feature]
		ON [Feature].[FeatureID] = [Sprint].[FeatureID]
		WHERE [Feature].[ProjectID] = @ProjectID
	END
GO

print '' print '*** creating sp_select_sprint_by_sprintid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_sprint_by_sprintid] (
	@SprintID		[int]
)
AS
	BEGIN
		SELECT [SprintID], [Sprint].[FeatureID], [StartDate], [EndDate], [Active]
		FROM [Sprint]
		WHERE [SprintID] = @SprintID
	END
GO

/* =================================================================================

									Insert Test Data
 
==================================================================================*/

print '' print '*** inserting User test records ***'
GO
INSERT INTO [dbo].[User]
		([DisplayName], [Email], [Bio])
	VALUES
		('Jared Harvey', 'jared.harvey10@gmail.com', 'I am programmer'),
		('Joe Winters', 'joe-winters@weatherchannel.com', 'I am weather man'),
		('Barack Obama', 'barack-obama@whitehouse.org', 'I am former president')
GO

print '' print '*** inserting Project test records ***'
GO
INSERT INTO [dbo].[Project]
		([ProjectID], [ProjectOwner], [DateCreated], [Status], [Description])
	VALUES
		('Dumb Scrum 1', 'Jared Harvey', GETDATE(), 'In Progress', 'Test description'),
		('Dumb Scrum 2', 'Joe Winters', GETDATE(), 'In Progress', 'Test description'),
		('Dumb Scrum 3', 'Barack Obama', GETDATE(), 'Completed', 'Test description')
GO

print '' print '*** inserting ProjectMember test records ***'
GO
INSERT INTO [dbo].[ProjectMember]
		([UserID], [ProjectID], [Role])
	VALUES
		('100000', 'Dumb Scrum 2', 'Scrum Team Member'),
		('100001', 'Dumb Scrum 3', 'Scrum Team Member'),
		('100002', 'Dumb Scrum 1', 'Scrum Team Member')
GO

print '' print '*** inserting Feature test records ***'
GO
INSERT INTO [dbo].[Feature]
		([ProjectID], [Name], [Description], [Priority], [Status])
	VALUES
		('Dumb Scrum 1', 'Sign In/Sign Up', 'Test description', 'High', 'In Progress'),
		('Dumb Scrum 1', 'Blah', 'Test description', 'Low', 'Completed'),
		('Dumb Scrum 1', 'Blah2', 'Test description', 'Medium', 'Ready For Sprint'),
		('Dumb Scrum 2', 'Project Management', 'Test description', 'High', 'In Progress'),
		('Dumb Scrum 3', 'Task Management', 'Test description', 'High', 'In Progress')
GO

print '' print '*** inserting UserStory test records ***'
GO
INSERT INTO [dbo].[UserStory]
		([FeatureID], [Person], [Action], [Reason])
	VALUES
		('100000', 'User', 'Signs up for Dumb Scrum 1', 'So that they can access and use the app.'),
		('100000', 'User', 'Signs into their Dumb Scrum account', 'So that they can access their account an use the app.'),
		('100001', 'User', 'View a list of my projects', 'So that I can view my projects'),
		('100002', 'User', 'View a list of my tasks', 'So that I can view the tasks that I have to do')
GO

print '' print '*** inserting Sprint test records ***'
GO
INSERT INTO [dbo].[Sprint]
		([FeatureID], [StartDate], [EndDate], [Active])
	VALUES
		('100000', GETDATE(), GETDATE(), 1),
		('100001', GETDATE(), GETDATE(), 0),
		('100002', GETDATE(), GETDATE(), 0)
GO

print '' print '*** inserting Task test records ***'
GO
INSERT INTO [dbo].[Task]
		([SprintID], [StoryID], [UserID], [Status])
	VALUES
		('100000', '100000', '100000', 'Test Status'),
		('100000', '100000', '100000', 'Test Status'),
		('100001', '100001', '100001', 'Test Status'),
		('100002', '100002', '100002', 'Test Status')
GO