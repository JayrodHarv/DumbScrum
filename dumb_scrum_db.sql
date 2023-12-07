
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

/* =================================================================================

									Create Table Statements
 
==================================================================================*/

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
	[Active]		[bit]								NOT NULL	DEFAULT 1,
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
		REFERENCES	[dbo].[Project] ([ProjectID]) ON DELETE CASCADE,
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
		REFERENCES	[dbo].[Feature] ([FeatureID]) ON DELETE CASCADE,
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
		REFERENCES	[dbo].[Feature] ([FeatureID]) ON DELETE CASCADE,
	CONSTRAINT [pk_Sprint] PRIMARY KEY ([SprintID])
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
		REFERENCES	[dbo].[Project] ([ProjectID]) ON DELETE CASCADE,
	CONSTRAINT [pk_ProjectMember] PRIMARY KEY ([UserID], [ProjectID])
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
		REFERENCES	[dbo].[Sprint] ([SprintID]) ON DELETE CASCADE,
	CONSTRAINT	[fk_Task_StoryID]	FOREIGN KEY ([StoryID])
		REFERENCES	[dbo].[UserStory] ([StoryID]) ON DELETE NO ACTION,
	CONSTRAINT	[fk_Task_UserID]	FOREIGN KEY ([UserID])
		REFERENCES	[dbo].[User] ([UserID]) ON DELETE NO ACTION,
	CONSTRAINT [pk_TaskID] PRIMARY KEY ([TaskID])
)
GO

/* creating filestore table */
print '' print '*** creating filestore table ***'
GO
CREATE TABLE [dbo].[FileStore] (
	[FileID]		[int]		IDENTITY(100000, 1)		NOT NULL,
	[Data]			[varbinary]	(max)					NOT NULL,
	[Extension]		[nvarchar]	(10)					NOT NULL,
	[TaskID]		[int]								NULL,
	[ProjectID]		[nvarchar]	(50)					NULL,
	[FileName]		[nvarchar]	(100)					NOT NULL,
	[Type]			[nvarchar]	(50)					NOT NULL,
	[LastEdited]	[datetime]							NOT NULL,
	CONSTRAINT [fk_FileStore_TaskID] FOREIGN KEY ([TaskID])
		REFERENCES [dbo].[Task] ([TaskID]) ON DELETE CASCADE,
	CONSTRAINT [fk_FileStore_ProjectID] FOREIGN KEY ([ProjectID])
		REFERENCES [dbo].[Project] ([ProjectID]) ON DELETE NO ACTION,
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
		REFERENCES	[dbo].[Sprint] ([SprintID]),
	CONSTRAINT	[fk_Message_UserID]	FOREIGN KEY ([UserID])
		REFERENCES	[dbo].[User] ([UserID]),
	CONSTRAINT [pk_MessageID] PRIMARY KEY ([MessageID])
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
		SELECT [UserID], [DisplayName], [Email], [Pfp], [Bio], [Active]
		FROM [User]
		WHERE @Email = [Email]
	END
GO

print '' print '*** creating sp_select_user_by_userid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_userid] (
	@UserID			[int]
)
AS
	BEGIN
		SELECT [UserID], [DisplayName], [Email], [Pfp], [Bio], [Active]
		FROM [User]
		WHERE [UserID] = @UserID
	END
GO

print '' print '*** creating sp_select_project_members ***'
GO
CREATE PROCEDURE [dbo].[sp_select_project_members] (
	@ProjectID			[nvarchar] (50)
)
AS
	BEGIN
		SELECT [ProjectMember].[UserID], [DisplayName], [Email], [Pfp], [Bio], [Active],
			   [ProjectMember].[Role]
		FROM [ProjectMember]
		INNER JOIN [User]
		ON [User].[UserID] = [ProjectMember].[UserID]
		WHERE [ProjectID] = @ProjectID
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
	@PasswordHash		[nvarchar] (100),
	@Pfp				[varbinary] (max)
)
AS
	BEGIN
		INSERT INTO [dbo].[User]
			([Email], [PasswordHash], [Pfp])
		VALUES
			(@Email, @PasswordHash, @Pfp)
	END
GO

print '' print '*** creating sp_update_user ***'
GO
CREATE PROCEDURE [dbo].[sp_update_user] (
	@UserID				[int],
	@NewDisplayName		[nvarchar] 	(50),
	@NewPfp				[varbinary] (max)
)
AS
	BEGIN
		UPDATE [User]
		SET [DisplayName] = @NewDisplayName,
			[Pfp] = @NewPfp
		WHERE [UserID] = @UserID
	END
GO

/*----- Project Stored Procedures -----*/
print '' print '*** creating sp_select_all_projects ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_projects]
AS
	BEGIN
		SELECT [ProjectID], [Project].[UserID], [DateCreated], [Status], [Description], [User].[DisplayName]
		FROM [Project]
		INNER JOIN [User]
		ON [Project].[UserID] = [User].[UserID]
	END
GO

print '' print '*** creating sp_select_user_projects ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_projects] (
	@UserID		[int]
)
AS
	BEGIN
		SELECT [Project].[ProjectID], [Project].[UserID], [DateCreated], [Status], [Description], [User].[DisplayName]
		FROM [Project]
		INNER JOIN [ProjectMember]
		ON [ProjectMember].[ProjectID] = [Project].[ProjectID]
		INNER JOIN [User]
		ON [User].[UserID] = [Project].[UserID]
		WHERE [ProjectMember].[UserID] = @UserID
	END
GO

print '' print '*** creating sp_select_project_by_projectid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_project_by_projectid] (
	@ProjectID		[nvarchar] (50)
)
AS
	BEGIN
		SELECT [ProjectID], [Project].[UserID], [DateCreated], [Status], [Description], [User].[DisplayName]
		FROM [Project]
		INNER JOIN [User]
		ON [Project].[UserID] = [User].[UserID]
		WHERE [ProjectID] = @ProjectID
	END
GO

print '' print '*** creating sp_insert_project ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_project] (
	@ProjectID				[nvarchar] 	(50),
	@UserID					[int],	
	@Description			[nvarchar]	(255)
)
AS
	BEGIN
		INSERT INTO [dbo].[Project]
			([ProjectID], [UserID], [DateCreated], [Description])
		VALUES
			(@ProjectID, @UserID, GETDATE(), @Description)
			
		INSERT INTO [dbo].[ProjectMember]
			([UserID], [ProjectID], [Role])
		VALUES
			(@UserID, @ProjectID, "Project Owner")
	END
GO

print '' print '*** creating sp_delete_project ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_project] (
	@ProjectID			[nvarchar] (50)
)
AS
	BEGIN
		DELETE FROM [Project]
		WHERE [ProjectID] = @ProjectID
	END
GO

print '' print '*** creating sp_user_leave_project ***'
GO
CREATE PROCEDURE [dbo].[sp_user_leave_project] (
	@UserID			[int],
	@ProjectID		[nvarchar] (50)
)
AS
	BEGIN
		DELETE FROM [ProjectMember]
		WHERE [ProjectMember].[UserID] = @UserID
			AND	[ProjectMember].[ProjectID] = @ProjectID	
	END
GO

print '' print '*** creating sp_user_join_project ***'
GO
CREATE PROCEDURE [dbo].[sp_user_join_project] (
	@UserID			[int],
	@ProjectID		[nvarchar] (50)
)
AS
	BEGIN
		INSERT INTO [dbo].[ProjectMember]
			([UserID], [ProjectID], [Role])
		VALUES
			(@UserID, @ProjectID, "Contributor")
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
	@FeatureID		[nvarchar]	(50),
	@ProjectID		[nvarchar] 	(50),
	@Name			[nvarchar]	(50),	
	@Description	[nvarchar]	(255),	
	@Priority		[nvarchar]	(20)	
)
AS
	BEGIN
		INSERT INTO [dbo].[Feature]
			([FeatureID], [ProjectID], [Name], [Description], [Priority])
		VALUES
			(@FeatureID, @ProjectID, @Name, @Description, @Priority)
	END
GO

print '' print '*** creating sp_update_feature_status ***'
GO
CREATE PROCEDURE [dbo].[sp_update_feature_status] (
	@FeatureID		[nvarchar]	(50),
	@Status			[nvarchar]	(50)
)
AS
	BEGIN
		UPDATE [Feature]
		SET [Status] = @Status
		WHERE [FeatureID] = @FeatureID
	END
GO

/*----- UserStory Stored Procedures -----*/
print '' print '*** creating sp_select_feature_user_stories ***'
GO
CREATE PROCEDURE [dbo].[sp_select_feature_user_stories] (
	@FeatureID		[nvarchar] (50)
)
AS
	BEGIN
		SELECT *
		FROM [UserStory]
		WHERE [UserStory].[FeatureID] = @FeatureID
	END
GO

print '' print '*** creating sp_insert_feature_userstory ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_feature_userstory] (
	@StoryID 		[nvarchar]	(50),
	@FeatureID		[nvarchar]	(50),
	@Person			[nvarchar]	(100),	
	@Action			[nvarchar]	(255),	
	@Reason			[nvarchar]	(255)	
)
AS
	BEGIN
		INSERT INTO [dbo].[UserStory]
			([StoryID], [FeatureID], [Person], [Action], [Reason])
		VALUES
			(@StoryID, @FeatureID, @Person, @Action, @Reason)
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
		SELECT 	[SprintID], [Sprint].[FeatureID], [Sprint].[Name], [StartDate], [EndDate], [Active], 
				[Feature].[Name]
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
		SELECT [SprintID], [FeatureID], [Name], [StartDate], [EndDate], [Active]
		FROM [Sprint]
		WHERE [SprintID] = @SprintID
	END
GO

print '' print '*** creating sp_select_sprint_by_featureid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_sprint_by_featureid] (
	@FeatureID		[nvarchar] (50)
)
AS
	BEGIN
		SELECT [SprintID], [FeatureID], [Name], [StartDate], [EndDate], [Active]
		FROM [Sprint]
		WHERE [FeatureID] = @FeatureID
	END
GO


print '' print '*** creating sp_insert_project_sprint ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_sprint] (
	@Name			[nvarchar] (50),
	@FeatureID		[nvarchar] (50),
	@StartDate		[datetime],
	@EndDate		[datetime]	
)
AS
	BEGIN
		INSERT INTO [dbo].[Sprint]
			([Name], [FeatureID], [StartDate], [EndDate])
		VALUES
			(@Name, @FeatureID, @StartDate, @EndDate)
	END
GO

print '' print '*** creating sp_update_sprint ***'
GO
CREATE PROCEDURE [dbo].[sp_update_sprint] (
	@SprintID		[int],
	@OldName		[nvarchar] (50),
	@OldStartDate	[datetime],
	@OldEndDate		[datetime],
	@NewName		[nvarchar] (50),
	@NewStartDate	[datetime],
	@NewEndDate		[datetime]
)
AS
	BEGIN
		UPDATE [Sprint]
		SET [Name] = @NewName,
			[StartDate] = @NewStartDate,
			[EndDate] = @NewEndDate
		WHERE [SprintID] = @SprintID
		AND [Name] = @OldName
		AND	[StartDate] = @OldStartDate
		AND [EndDate] = @OldEndDate
	END
GO

print '' print '*** creating sp_delete_project_sprint ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_project_sprint] (
	@SprintID		[int]	
)
AS
	BEGIN
		DELETE FROM [Sprint]
		WHERE [SprintID] = @SprintID
	END
GO

/*----- Task Stored Procedures -----*/
print '' print '*** creating sp_insert_task ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_task] (
	@SprintID		[int],
	@StoryID		[nvarchar] (50),
	@Status			[nvarchar] (50)
)
AS
	BEGIN
		INSERT INTO [dbo].[Task]
			([SprintID], [StoryID], [Status])
		VALUES
			(@SprintID, @StoryID, @Status)
	END
GO

print '' print '*** creating sp_update_task_userid ***'
GO
CREATE PROCEDURE [dbo].[sp_update_task_userid] (
	@TaskID			[int],
	@UserID			[int]
)
AS
	BEGIN
		UPDATE [Task]
		SET [UserID] = @UserID,
			[Status] = "In Progress"
		WHERE [TaskID] = @TaskID
		AND [UserID] IS NULL
	END
GO

print '' print '*** creating sp_update_task_status ***'
GO
CREATE PROCEDURE [dbo].[sp_update_task_status] (
	@TaskID			[int],
	@Status			[nvarchar] (50)
)
AS
	BEGIN
		UPDATE [Task]
		SET [Status] = @Status
		WHERE [TaskID] = @TaskID
	END
GO

print '' print '*** creating sp_select_sprint_tasks_by_status ***'
GO
CREATE PROCEDURE [dbo].[sp_select_sprint_tasks] (
	@SprintID		[int]
)
AS
	BEGIN
		SELECT 	[TaskID], [SprintID], [Task].[StoryID], [UserID], [Task].[Status],
				[Feature].[ProjectID], [Feature].[Name], [UserStory].[Person],
				[UserStory].[Action], [UserStory].[Reason]
		FROM [Task]
		INNER JOIN [dbo].[UserStory]
		ON [UserStory].[StoryID] = [Task].[StoryID]
		INNER JOIN [dbo].[Feature]
		ON [Feature].[FeatureID] = [UserStory].[FeatureID]
		WHERE [SprintID] = @SprintID
	END
GO

print '' print '*** creating sp_select_taskvms_by_userid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_taskvms_by_userid] (
	@UserID		[int]
)
AS
	BEGIN
		SELECT 	[TaskID], [SprintID], [Task].[StoryID], [UserID], [Task].[Status],
				[Feature].[ProjectID], [Feature].[Name], [UserStory].[Person],
				[UserStory].[Action], [UserStory].[Reason]
		FROM [Task]
		INNER JOIN [dbo].[UserStory]
		ON [UserStory].[StoryID] = [Task].[StoryID]
		INNER JOIN [dbo].[Feature]
		ON [Feature].[FeatureID] = [UserStory].[FeatureID]
		WHERE [UserID] = @UserID
	END
GO

print '' print '*** creating sp_select_taskvm_by_taskid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_taskvm_by_taskid] (
	@TaskID		[int]
)
AS
	BEGIN
		SELECT 	[TaskID], [SprintID], [Task].[StoryID], [UserID], [Task].[Status],
				[Feature].[ProjectID], [Feature].[Name], [UserStory].[Person],
				[UserStory].[Action], [UserStory].[Reason]
		FROM [Task]
		INNER JOIN [dbo].[UserStory]
		ON [UserStory].[StoryID] = [Task].[StoryID]
		INNER JOIN [dbo].[Feature]
		ON [Feature].[FeatureID] = [UserStory].[FeatureID]
		WHERE [TaskID] = @TaskID
	END
GO

/* FileStore stored procedures */
print '' print '*** creating sp_insert_task_file ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_task_file] (	
	@Data			[varbinary] (max),
	@Extension		[nvarchar] (10),
	@TaskID			[int],
	@FileName		[nvarchar] (100),
	@Type			[nvarchar] (50),
	@LastEdited		[datetime]
)
AS
	BEGIN
		INSERT INTO [dbo].[FileStore]
			([Data], [Extension], [TaskID], [FileName], [Type], [LastEdited])
		VALUES
			(@Data, @Extension, @TaskID, @FileName, @Type, @LastEdited)
	END
GO

print '' print '*** creating sp_insert_template_file ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_template_file] (	
	@Data			[varbinary] (max),
	@Extension		[nvarchar] (10),
	@ProjectID		[nvarchar] (50),
	@FileName		[nvarchar] (100),
	@Type			[nvarchar] (50),
	@LastEdited		[datetime]
)
AS
	BEGIN
		INSERT INTO [dbo].[FileStore]
			([Data], [Extension], [ProjectID], [FileName], [Type], [LastEdited])
		VALUES
			(@Data, @Extension, @ProjectID, @FileName, @Type, @LastEdited)
	END
GO

print '' print '*** creating sp_update_file ***'
GO
CREATE PROCEDURE [dbo].[sp_update_file] (	
	@FileID				[int],
	@OldData			[varbinary] (max),
	@OldFileName		[nvarchar] (100),
	@OldLastEdited		[datetime],
	@NewData			[varbinary] (max),
	@NewFileName		[nvarchar] (100),
	@NewLastEdited		[datetime]
)
AS
	BEGIN
		UPDATE [dbo].[FileStore]
		SET	[Data] = @NewData,
			[FileName] = @NewFileName,
			[LastEdited] = @NewLastEdited
		WHERE [FileID] = @FileID
		AND [Data] = @OldData
		AND	[FileName] = @OldFileName
		AND [LastEdited] = @OldLastEdited
	END
GO

print '' print '*** creating sp_delete_file ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_file] (	
	@FileID		[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[FileStore]
		WHERE [FileStore].[FileID] = @FileID
	END
GO

print '' print '*** creating sp_get_task_files ***'
GO
CREATE PROCEDURE [dbo].[sp_select_task_files_by_type] (
	@TaskID		[int],
	@Type		[nvarchar] (50)
)
AS
	BEGIN
		SELECT [FileID], [Data], [Extension], [TaskID], [FileName], [Type], [LastEdited]
		FROM [dbo].[FileStore]
		WHERE [FileStore].[TaskID] = @TaskID
		AND [FileStore].[Type] = @Type
	END
GO

print '' print '*** creating sp_get_project_template_file_by_type ***'
GO
CREATE PROCEDURE [dbo].[sp_get_project_template_file_by_type] (
	@ProjectID		[nvarchar] (50),
	@Type			[nvarchar] (50)
)
AS
	BEGIN
		SELECT [FileID], [Data], [Extension], [ProjectID], [FileName], [Type], [LastEdited]
		FROM [dbo].[FileStore]
		WHERE [FileStore].[ProjectID] = @ProjectID
		AND [FileStore].[Type] = @Type
	END
GO

/* FeedMessage stored procedures */
print '' print '*** creating sp_insert_feed_message ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_feed_message] (		
	@SprintID		[int],	
	@UserID			[int],
	@Text			[Text],
	@SentAt			[datetime]
)
AS
	BEGIN
		INSERT INTO [dbo].[FeedMessage]
			([SprintID], [UserID], [Text], [SentAt])
		VALUES
			(@SprintID, @UserID, @Text, @SentAt)
	END
GO

print '' print '*** creating sp_select_sprint_feed_messages ***'
GO
CREATE PROCEDURE [dbo].[sp_select_sprint_feed_messages] (		
	@SprintID		[int]
)
AS
	BEGIN
		SELECT [MessageID], [SprintID], [FeedMessage].[UserID], [Text], [SentAt], [User].[DisplayName], [User].[Pfp]
		FROM [dbo].[FeedMessage]
		INNER JOIN [User]
		ON [User].[UserID] = [FeedMessage].[UserID]
		WHERE [FeedMessage].[SprintID] = @SprintID
		ORDER BY [SentAt]
	END
GO

/* =================================================================================

									Insert Test Data
 
==================================================================================*/
/*
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
		('100000', 'Dumb Scrum 1', 'Scrum Team Member'),
		('100001', 'Dumb Scrum 3', 'Scrum Team Member'),
		('100002', 'Dumb Scrum 2', 'Scrum Team Member')
GO

print '' print '*** inserting Feature test records ***'
GO
INSERT INTO [dbo].[Feature]
		([ProjectID], [Name], [Description], [Priority])
	VALUES
		('Dumb Scrum 1', 'Sign In/Sign Up', 'Test description', 'High'),
		('Dumb Scrum 1', 'Blah', 'Test description', 'Low'),
		('Dumb Scrum 1', 'Blah2', 'Test description', 'Medium'),
		('Dumb Scrum 2', 'Project Management', 'Test description', 'High'),
		('Dumb Scrum 3', 'Task Management', 'Test description', 'High')
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
		([FeatureID], [StartDate], [EndDate])
	VALUES
		('100000', GETDATE(), GETDATE()),
		('100001', GETDATE(), GETDATE()),
		('100002', GETDATE(), GETDATE()),
		('100003', GETDATE(), GETDATE())
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
*/