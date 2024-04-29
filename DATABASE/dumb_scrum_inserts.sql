print '' print '*** using database dumb_scrum_db ***'
GO
	USE [dumb_scrum_db]
GO

print '' print '*** Inserting Test Records ***'

/* =================================================================================

									Insert Test Data
 
==================================================================================*/

print '' print '*** inserting role records ***'
GO
INSERT INTO [dbo].[Role]
		([RoleID])
	VALUES
		('Admin'),
		('User'),
		('Moderator'),
		('Guest')
GO

/*
print '' print '*** inserting User test records ***'
GO
INSERT INTO [dbo].[User]
		([DisplayName], [Email], [Bio])
	VALUES
		('Jared Harvey', 'jared.harvey10@gmail.com', 'I am programmer')
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