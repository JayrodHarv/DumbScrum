print '' print '*** using database dumb_scrum_db ***'
GO
	USE [dumb_scrum_db]
GO

print '' print '*** Creating Stored Procedures ***'

/* =================================================================================
									ROLE
==================================================================================*/
print '' print '*** creating sp_get_all_roles ***'
GO
CREATE PROCEDURE [dbo].[sp_get_all_roles]
AS
	BEGIN
		SELECT RoleID FROM Role
	END
GO

print '' print '*** creating sp_delete_user_role ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_user_role] (	
	@UserID		[int],
	@RoleID		[nvarchar] (100)
)
AS
	BEGIN
		DELETE FROM UserRole 
		WHERE UserID = @UserID
		AND RoleID = @RoleID
	END
GO

print '' print '*** creating sp_add_user_role ***'
GO
CREATE PROCEDURE [dbo].[sp_add_user_role] (	
	@UserID		[int],
	@RoleID		[nvarchar] (100)
)
AS
	BEGIN
		INSERT INTO UserRole
			(UserID, RoleID)
		VALUES
			(@UserID, @RoleID)
	END
GO

/* =================================================================================
										USER
==================================================================================*/
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

print '' print '*** creating sp_select_all_users ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_users]
AS
	BEGIN
		SELECT [UserID], [DisplayName], [Email], [Pfp], [Bio], [Active]
		FROM [User]
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
	@DisplayName		[nvarchar] (50),
	@Pfp				[varbinary] (max)
)
AS
	BEGIN
		INSERT INTO [dbo].[User]
			([Email], [PasswordHash], [DisplayName], [Pfp])
		VALUES
			(@Email, @PasswordHash, @DisplayName, @Pfp)
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

/* =================================================================================
									PROJECT
==================================================================================*/
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
		SELECT p.[ProjectID], p.[UserID], [DateCreated], [Status], [Description], u.[DisplayName]
		FROM [Project] AS p
		INNER JOIN [ProjectMember] AS pm
		ON pm.[ProjectID] = p.[ProjectID]
		AND pm.Active = 1
		LEFT JOIN [User] AS u
		ON u.[UserID] = p.[UserID]
		WHERE pm.[UserID] = @UserID
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
		BEGIN TRY
			BEGIN TRANSACTION
			
				DECLARE @last_inserted TABLE (Id int);
				
				INSERT INTO [dbo].[Project]
					([ProjectID], [UserID], [DateCreated], [Description])
				VALUES
					(@ProjectID, @UserID, GETDATE(), @Description)
					
				INSERT INTO ProjectRole (
					RoleName, 
					ProjectID, 
					FeaturePrivileges,		
					UserStoryPrivileges,		
					SprintPlanningPrivileges,
					FeedMessagingPrivileges,	
					TaskPrivileges,			
					TaskReviewingPrivileges,
					ProjectManagementPrivileges,
					Description
				)
				OUTPUT inserted.ProjectRoleID INTO @last_inserted(Id)
				VALUES (
					"Project Owner", 
					@ProjectID,
					1,		
					1,		
					1,
					1,	
					1,			
					1,
					1,
					"The project creator and manager."
				)
				
				INSERT INTO [dbo].[ProjectMember]
					([UserID], [ProjectID], [ProjectRoleID])
				VALUES
					(@UserID, @ProjectID, (SELECT Id FROM @last_inserted))
		
			COMMIT TRANSACTION
			SELECT @@ROWCOUNT
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
			SELECT @@ROWCOUNT
		END CATCH
	END
GO

print '' print '*** creating sp_update_project ***'
GO
CREATE PROCEDURE [dbo].[sp_update_project] (
	@ProjectID nvarchar(50),
	@Status nvarchar(50),
	@Description nvarchar(255)
)
AS
	BEGIN
		UPDATE Project
		SET ProjectID = @ProjectID,
			Status = @Status,
			Description = @Description
		WHERE ProjectID = @ProjectID
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

/* =================================================================================
									PROJECT ROLE
==================================================================================*/
print '' print '*** creating sp_select_project_roles ***'
GO
CREATE PROCEDURE [dbo].[sp_select_project_roles] (
	@ProjectID [nvarchar] (50)
)
AS
	BEGIN
		SELECT pr.ProjectRoleID, pr.RoleName, COUNT(pm.UserID) AS 'MembersWithRole', pr.Description				
		FROM ProjectRole AS pr
		LEFT JOIN ProjectMember AS pm
		ON pm.ProjectRoleID = pr.ProjectRoleID
		AND pm.Active = 1
		WHERE pr.ProjectID = @ProjectID
		GROUP BY pr.ProjectRoleID, pr.RoleName, pr.Description
	END
GO

print '' print '*** creating sp_select_project_role ***'
GO
CREATE PROCEDURE [dbo].[sp_select_project_role] (
	@ProjectRoleID int
)
AS
	BEGIN
		SELECT 	ProjectRoleID,
				RoleName,
				FeaturePrivileges,		
				UserStoryPrivileges,		
				SprintPlanningPrivileges,
				FeedMessagingPrivileges,	
				TaskPrivileges,			
				TaskReviewingPrivileges,
				ProjectManagementPrivileges,
				Description				
		FROM ProjectRole
		WHERE ProjectRoleID = @ProjectRoleID
	END
GO

print '' print '*** creating sp_insert_project_role ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_project_role] (
	@RoleName nvarchar(100),
	@ProjectID nvarchar(50),
	@FeaturePrivileges bit,	
	@UserStoryPrivileges bit,	
	@SprintPlanningPrivileges bit,	
	@FeedMessagingPrivileges bit,	
	@TaskPrivileges bit,
	@TaskReviewingPrivileges bit,	
	@ProjectManagementPrivileges bit,	
	@Description nvarchar(255)
)
AS
	BEGIN
		INSERT INTO ProjectRole (
			RoleName, 
			ProjectID, 
			FeaturePrivileges,		
			UserStoryPrivileges,		
			SprintPlanningPrivileges,
			FeedMessagingPrivileges,	
			TaskPrivileges,			
			TaskReviewingPrivileges,
			ProjectManagementPrivileges,
			Description
		)
		VALUES (
			@RoleName, 
			@ProjectID,
			@FeaturePrivileges,		
			@UserStoryPrivileges,		
			@SprintPlanningPrivileges,
			@FeedMessagingPrivileges,	
			@TaskPrivileges,			
			@TaskReviewingPrivileges,
			@ProjectManagementPrivileges,
			@Description
		)
	END
GO

print '' print '*** creating sp_update_project_role ***'
GO
CREATE PROCEDURE [dbo].[sp_update_project_role] (
	@ProjectRoleID int,
	@RoleName nvarchar(100),
	@FeaturePrivileges bit,	
	@UserStoryPrivileges bit,	
	@SprintPlanningPrivileges bit,	
	@FeedMessagingPrivileges bit,	
	@TaskPrivileges bit,
	@TaskReviewingPrivileges bit,	
	@ProjectManagementPrivileges bit,	
	@Description nvarchar(255)
)
AS
	BEGIN
		UPDATE ProjectRole
		SET RoleName = @RoleName,
			FeaturePrivileges = @FeaturePrivileges,		
			UserStoryPrivileges = @UserStoryPrivileges,		
			SprintPlanningPrivileges = @SprintPlanningPrivileges,
			FeedMessagingPrivileges = @FeedMessagingPrivileges,	
			TaskPrivileges = @TaskPrivileges,			
			TaskReviewingPrivileges = @TaskReviewingPrivileges,
			ProjectManagementPrivileges = @ProjectManagementPrivileges,
			Description = @Description
		WHERE ProjectRoleID = @ProjectRoleID
	END
GO

print '' print '*** creating sp_delete_project_role ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_project_role] (
	@ProjectRoleID int
)
AS
	BEGIN
		DELETE FROM ProjectRole
		WHERE ProjectRoleID = @ProjectRoleID
	END
GO

/* =================================================================================
									PROJECT MEMBER
==================================================================================*/
print '' print '*** creating sp_select_project_members ***'
GO
CREATE PROCEDURE [dbo].[sp_select_project_members] (
	@ProjectID			[nvarchar] (50)
)
AS
	BEGIN
		SELECT  u.UserID, u.Email, u.DisplayName, u.Pfp, pr.ProjectRoleID, pr.RoleName, pm.Active
		FROM ProjectMember AS pm
		INNER JOIN [User] AS u
		ON u.[UserID] = pm.[UserID]
		INNER JOIN ProjectRole AS pr
		ON pr.ProjectRoleID = pm.ProjectRoleID
		WHERE pr.ProjectID = @ProjectID
		AND pm.Active = 1
	END
GO

print '' print '*** creating sp_select_project_member ***'
GO
CREATE PROCEDURE [dbo].[sp_select_project_member] (
	@UserID	int,
	@ProjectID nvarchar(50)
)
AS
	BEGIN
		SELECT 	u.UserID, u.Email, u.DisplayName, u.Pfp, 
				pm.Active, pr.ProjectRoleID, pr.RoleName,
				pr.FeaturePrivileges, pr.UserStoryPrivileges,
				pr.SprintPlanningPrivileges, pr.FeedMessagingPrivileges,
				pr.TaskPrivileges, pr.TaskReviewingPrivileges,
				pr.ProjectManagementPrivileges
		FROM ProjectMember AS pm
		INNER JOIN [User] AS u
		ON u.[UserID] = pm.[UserID]
		INNER JOIN ProjectRole AS pr
		ON pr.[ProjectRoleID] = pm.[ProjectRoleID]
		WHERE pm.ProjectID = @ProjectID
		AND pm.UserID = @UserID
		AND pm.Active = 1
	END
GO

print '' print '*** creating sp_insert_project_member ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_project_member] (
	@UserID			int,
	@ProjectID		nvarchar(50),
	@ProjectRoleID	int
)
AS
	BEGIN
		INSERT INTO ProjectMember
			(UserID, ProjectID, ProjectRoleID)
		VALUES
			(@UserID, @ProjectID, @ProjectRoleID)
	END
GO

print '' print '*** creating sp_member_leave_project ***'
GO
CREATE PROCEDURE [dbo].[sp_member_leave_project] (
	@UserID	int,
	@ProjectID nvarchar(50)
)
AS
	BEGIN
		UPDATE ProjectMember
		SET Active = 0
		WHERE UserID = @UserID
		AND ProjectID = @ProjectID
	END
GO

print '' print '*** creating sp_update_member_role ***'
GO
CREATE PROCEDURE [dbo].[sp_update_member_role] (
	@UserID	int,
	@ProjectID nvarchar(50),
	@ProjectRoleID int
)
AS
	BEGIN
		UPDATE ProjectMember
		SET ProjectRoleID = @ProjectRoleID
		WHERE UserID = @UserID
		AND ProjectID = @ProjectID
	END
GO

/* =================================================================================
									FEATURE
==================================================================================*/
print '' print '*** creating sp_select_project_features ***'
GO
CREATE PROCEDURE [dbo].[sp_select_project_features] (
	@ProjectID		[nvarchar] (50)
)
AS
	BEGIN
		SELECT FeatureID, ProjectID, Name, Description, Priority, Status
		FROM Feature
		WHERE Feature.ProjectID = @ProjectID
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

/* =================================================================================
									USER STORY
==================================================================================*/
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

/* =================================================================================
									SPRINT
==================================================================================*/
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
		BEGIN TRY
			BEGIN TRANSACTION
				/* This aparently has to be prefixed with @ not # */
				DECLARE @story_id nvarchar(50)
				
				CREATE TABLE #last_inserted(Id int)
				
				/* Insert sprint & output the auto generated sprintid field into temp table */
				INSERT INTO [dbo].[Sprint]
					([Name], [FeatureID], [StartDate], [EndDate])
				OUTPUT inserted.SprintID INTO #last_inserted(Id)
				VALUES
					(@Name, @FeatureID, @StartDate, @EndDate)
				
				/* Creates cursor that loops though each user story for the sprint feature */
				DECLARE Story_Cursor CURSOR FOR
				SELECT StoryID FROM UserStory 
				WHERE FeatureID = @FeatureID;
				
				OPEN Story_Cursor;
				
				/* Gets initial story id before loop */
				FETCH NEXT FROM Story_Cursor INTO @story_id;
				
				/* While there are still user stories in temp table */
				WHILE @@FETCH_STATUS = 0
				BEGIN
					/* Insert Task */
					INSERT INTO Task
						(SprintID, StoryID, Status)
					VALUES
						((SELECT Id FROM #last_inserted), @story_id, "To Do")
					
					/* Gets next value, breaks loop if none left */
					FETCH NEXT FROM Story_Cursor INTO @story_id;
				END
			COMMIT TRANSACTION
			SELECT 1
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
			SELECT 0
		END CATCH
		/* Drop temp table */
		DROP TABLE #last_inserted
		/* Just to be explicit */
		RETURN @@ROWCOUNT
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

/* =================================================================================
									TASK
==================================================================================*/
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

print '' print '*** creating sp_select_sprint_tasks ***'
GO
CREATE PROCEDURE [dbo].[sp_select_sprint_tasks] (
	@SprintID		[int]
)
AS
	BEGIN
		SELECT 	t.TaskID, t.SprintID, t.StoryID, t.UserID, t.Status,
				f.ProjectID, f.Name, 
				us.Person, us.[Action], us.Reason, 
				u.DisplayName
		FROM Task AS t
		INNER JOIN UserStory AS us
		ON us.StoryID = t.StoryID
		INNER JOIN Feature AS f
		ON f.FeatureID = us.FeatureID
		LEFT JOIN [User] AS u
		ON u.UserID = t.UserID
		WHERE t.SprintID = @SprintID
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

/* =================================================================================
									FILE STORE
==================================================================================*/
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

/* =================================================================================
									FEED MESSAGE
==================================================================================*/
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