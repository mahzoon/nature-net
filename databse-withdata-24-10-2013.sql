USE [nature-net]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 10/24/2013 13:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Location] ON
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (0, N'Default', N'Default Location')
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (1, N'Hub of Activities', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (2, N'Golden Eagle', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (3, N'A Safe Haven', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (4, N'Beavers', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (5, N'Outdoor Classroom', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (6, N'Past to Present', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (7, N'Overlook', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (8, N'Bird Hollow', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (9, N'Where Rivers Come Together', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (10, N'Birds of Prey', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (11, N'Journeys End', NULL)
SET IDENTITY_INSERT [dbo].[Location] OFF
/****** Object:  Table [dbo].[User]    Script Date: 10/24/2013 13:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[email] [nvarchar](64) NULL,
	[password] [nchar](128) NULL,
	[avatar] [nvarchar](64) NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (1, N'jimmypk', N'jkravitz@aspennature.org', NULL, N'nn_bearorange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (2, N'adam mccurdy', NULL, NULL, N'NN_SquirrelOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (3, N'Sam Cardick', N'samcardick@gmail.com', NULL, N'nn_bisongreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (4, N'denali', NULL, NULL, N'NN_SnakeGreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (5, N'bob romeo', NULL, NULL, N'NN_SquirrelOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (6, N'elin binck', NULL, NULL, N'NN_GatorRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (7, N'lily', N'lilybq@gmail.com', NULL, N'nn_caribougreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (8, N'James', NULL, NULL, N'nn_squirrelpurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (9, N'Bowman Leigh', NULL, NULL, N'NN_HorseOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (10, N'Markus', NULL, NULL, N'NN_TortoisePurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (11, N'Kaz', NULL, NULL, N'NN_SnakeGreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (12, N'Olivia Siegel', NULL, NULL, N'nn_squirrelpurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (13, N'bunny', NULL, NULL, N'NN_HarePurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (14, N'Abby', NULL, NULL, N'NN_FrogRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (15, N'jenny', NULL, NULL, N'NN_HorseOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (16, N'Jamie', NULL, NULL, N'NN_BisonOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (17, N'karen', NULL, NULL, N'NN_FrogRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (18, N'Tom Cardamone', NULL, NULL, N'NN_HarePurple.png', NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[Feedback_Type]    Script Date: 10/24/2013 13:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback_Type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[description] [nvarchar](max) NULL,
	[data_type] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Feedback_Type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Feedback_Type] ON
INSERT [dbo].[Feedback_Type] ([id], [name], [description], [data_type]) VALUES (1, N'Comment', NULL, N'String')
INSERT [dbo].[Feedback_Type] ([id], [name], [description], [data_type]) VALUES (2, N'Like', NULL, N'Boolean')
INSERT [dbo].[Feedback_Type] ([id], [name], [description], [data_type]) VALUES (3, N'Rate', NULL, N'Integer')
SET IDENTITY_INSERT [dbo].[Feedback_Type] OFF
/****** Object:  Table [dbo].[Activity]    Script Date: 10/24/2013 13:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](32) NOT NULL,
	[description] [nvarchar](max) NULL,
	[creation_date] [datetime] NOT NULL,
	[expire_date] [datetime] NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Activity] ON
INSERT [dbo].[Activity] ([id], [name], [description], [creation_date], [expire_date], [technical_info]) VALUES (0, N'Free Observation', NULL, CAST(0x0000A25E00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[Activity] ([id], [name], [description], [creation_date], [expire_date], [technical_info]) VALUES (1, N'Design Ideas', NULL, CAST(0x0000A25E00000000 AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Activity] OFF
/****** Object:  Table [dbo].[Collection]    Script Date: 10/24/2013 13:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collection](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[description] [nvarchar](max) NULL,
	[activity_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[date] [datetime] NOT NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Collection] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Collection] ON
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (1, N'26 Sept 2013', NULL, 0, 2, CAST(0x0000A24500CC9ED0 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (2, N'27 Sept 2013', NULL, 0, 2, CAST(0x0000A24600914B50 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (3, N'30 Sept 2013', NULL, 0, 2, CAST(0x0000A2490092AAE0 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (4, N'30 Sept 2013', NULL, 0, 4, CAST(0x0000A24900B54640 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (5, N'4 Oct 2013', NULL, 0, 5, CAST(0x0000A24D00DC0050 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (6, N'4 Oct 2013', NULL, 0, 2, CAST(0x0000A24D00DC46A0 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (7, N'4 Oct 2013', NULL, 0, 6, CAST(0x0000A24D00DC46A0 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (8, N'7 Oct 2013', NULL, 0, 9, CAST(0x0000A25000BAC480 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (9, N'8 Oct 2013', NULL, 1, 11, CAST(0x0000A25101137B70 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (10, N'9 Oct 2013', NULL, 1, 12, CAST(0x0000A25200F77790 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (11, N'10 Oct 2013', NULL, 1, 8, CAST(0x0000A25300F31290 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (12, N'10 Oct 2013', NULL, 1, 12, CAST(0x0000A25301133520 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Collection] OFF
/****** Object:  Table [dbo].[Feedback_Activity]    Script Date: 10/24/2013 13:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback_Activity](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[note] [nvarchar](max) NOT NULL,
	[date] [datetime] NOT NULL,
	[type_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[parent_id] [int] NOT NULL,
	[activity_id] [int] NOT NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Feedback_Activity] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback_User]    Script Date: 10/24/2013 13:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback_User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[note] [nvarchar](max) NOT NULL,
	[date] [datetime] NOT NULL,
	[type_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[parent_id] [int] NOT NULL,
	[v_user_id] [int] NOT NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Feedback_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback_Collection]    Script Date: 10/24/2013 13:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback_Collection](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[note] [nvarchar](max) NOT NULL,
	[date] [datetime] NOT NULL,
	[type_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[parent_id] [int] NOT NULL,
	[collection_id] [int] NOT NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Feedback_Collection] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Media]    Script Date: 10/24/2013 13:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Media](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[note] [nvarchar](max) NULL,
	[media_url] [nvarchar](max) NULL,
	[tags] [nvarchar](max) NULL,
	[date] [datetime] NOT NULL,
	[location_id] [int] NOT NULL,
	[collection_id] [int] NOT NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Media] ON
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (1, NULL, N'1380218582271.jpg', N'Animal', CAST(0x0000A24500CC9ED0 AS DateTime), 2, 1, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (2, NULL, N'1380219051751.jpg', N'Plant', CAST(0x0000A24500D6C860 AS DateTime), 6, 1, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (3, NULL, N'1380218748963.jpg', N'Landscape', CAST(0x0000A245010D7090 AS DateTime), 3, 1, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (4, NULL, N'1380218865847.jpg', N'Plant', CAST(0x0000A24600914B50 AS DateTime), 5, 2, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (5, NULL, N'1380219544910.jpg', N'Animal', CAST(0x0000A24600914B50 AS DateTime), 10, 2, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (6, NULL, N'1380219166696.jpg', N'Plant', CAST(0x0000A24600914B50 AS DateTime), 7, 2, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (7, NULL, N'1380218690979.jpg', N'Animal', CAST(0x0000A24600914B50 AS DateTime), 3, 2, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (8, NULL, N'1380219766692.jpg', N'Insect', CAST(0x0000A24600914B50 AS DateTime), 11, 2, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (9, NULL, N'1380219716995.jpg', N'Animal', CAST(0x0000A24600914B50 AS DateTime), 10, 2, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (10, NULL, N'1380219525204.jpg', N'Plant', CAST(0x0000A24600914B50 AS DateTime), 8, 2, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (11, NULL, N'1380219248874.jpg', N'Landscape', CAST(0x0000A24600914B50 AS DateTime), 9, 2, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (12, NULL, N'1380218954983.jpg', N'Animal', CAST(0x0000A246009A1550 AS DateTime), 5, 2, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (13, NULL, N'1380218923624.jpg', N'Landscape', CAST(0x0000A2490092AAE0 AS DateTime), 4, 3, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (14, NULL, N'1380218657954.jpg', N'Plant', CAST(0x0000A2490092AAE0 AS DateTime), 2, 3, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (15, N'thistle gone to seed', N'1380560221183.jpg', N'Plant', CAST(0x0000A24900B54640 AS DateTime), 1, 4, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (16, N'golden eagle', N'1380822975830.jpg', N'other', CAST(0x0000A24D00DC0050 AS DateTime), 1, 5, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (17, NULL, N'1380814353134.jpg', N'Animal', CAST(0x0000A24D00DC46A0 AS DateTime), 2, 6, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (18, N'snails in the indoor trout stream', N'1380814114675.jpg', N'Insect', CAST(0x0000A24D00DC46A0 AS DateTime), 2, 7, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (19, NULL, N'1381165589057.jpg', N'Plant', CAST(0x0000A25000BAC480 AS DateTime), 3, 8, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (20, NULL, N'1381178149410.jpg', N'other', CAST(0x0000A25000F77790 AS DateTime), 4, 8, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (22, N'When I enter a design idea I have to select the default text manually. That shouldnt really happen.', NULL, NULL, CAST(0x0000A25101137B70 AS DateTime), 0, 9, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (23, N'(The same happens to the post-submission text)', NULL, NULL, CAST(0x0000A2510113C1C0 AS DateTime), 0, 9, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (24, N'Why does the shift key on this keyboard go blue some times', NULL, NULL, CAST(0x0000A2510115ADF0 AS DateTime), 0, 9, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (25, N'I cant type a question mark using this keyboard.', NULL, NULL, CAST(0x0000A2510115F440 AS DateTime), 0, 9, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (26, N'curation fxn - ability to recategorize images or comments when necessary', NULL, NULL, CAST(0x0000A25200F77790 AS DateTime), 0, 10, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (27, N'allow users to contribute images from their own smart phones', NULL, NULL, CAST(0x0000A25200F890D0 AS DateTime), 0, 10, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (28, N'stack program with excellent examples of good user data so people have guidance', NULL, NULL, CAST(0x0000A25200FB9640 AS DateTime), 0, 10, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (29, N'text wrapping fix so long names still show', NULL, NULL, CAST(0x0000A25200FCAF80 AS DateTime), 0, 10, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (30, N'rubber band effect on right side of image scrolling', NULL, NULL, CAST(0x0000A25200FCF5D0 AS DateTime), 0, 10, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (31, N'This is a test of the new bluetooth keyboard.', NULL, NULL, CAST(0x0000A25300F31290 AS DateTime), 0, 11, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (32, N'only the photos taken by user tom cardamone inside the building loaded', NULL, NULL, CAST(0x0000A25301133520 AS DateTime), 0, 12, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (33, N'i became more engaged in using the app when i was participating in a guided task - ie take a picture of an insect', NULL, NULL, CAST(0x0000A25301140810 AS DateTime), 0, 12, NULL)
INSERT [dbo].[Media] ([id], [note], [media_url], [tags], [date], [location_id], [collection_id], [technical_info]) VALUES (34, N'tasks should be tied to season - ie take a picture of an insect is more difficult in winter', NULL, NULL, CAST(0x0000A253011494B0 AS DateTime), 0, 12, NULL)
SET IDENTITY_INSERT [dbo].[Media] OFF
/****** Object:  StoredProcedure [dbo].[GetObservations]    Script Date: 10/24/2013 13:01:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetObservations]
    @Username nvarchar(64)
AS 

    SET NOCOUNT ON;
    SELECT dbo.Media.*
    FROM  dbo.Activity INNER JOIN
          dbo.Collection ON dbo.Activity.id = dbo.Collection.activity_id INNER JOIN
          dbo.Media ON dbo.Collection.id = dbo.Media.collection_id INNER JOIN
          dbo.[User] ON dbo.Collection.user_id = @Username
    WHERE dbo.Activity.name = 'Free Observations'
GO
/****** Object:  StoredProcedure [dbo].[GetDesignIdeas]    Script Date: 10/24/2013 13:01:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDesignIdeas]
    @Username nvarchar(64)
AS 

    SET NOCOUNT ON;
    SELECT dbo.Media.*
    FROM  dbo.Activity INNER JOIN
          dbo.Collection ON dbo.Activity.id = dbo.Collection.activity_id INNER JOIN
          dbo.Media ON dbo.Collection.id = dbo.Media.collection_id INNER JOIN
          dbo.[User] ON dbo.Collection.user_id = @Username
    WHERE dbo.Activity.name = 'Design Ideas'
GO
/****** Object:  View [dbo].[Design_Ideas]    Script Date: 10/24/2013 13:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Design_Ideas]
AS
SELECT     dbo.Media.*, dbo.[User].name, dbo.[User].avatar
FROM         dbo.Activity INNER JOIN
                      dbo.Collection ON dbo.Activity.id = dbo.Collection.activity_id INNER JOIN
                      dbo.Media ON dbo.Collection.id = dbo.Media.collection_id INNER JOIN
                      dbo.[User] ON dbo.Collection.user_id = dbo.[User].id
WHERE     (dbo.Activity.name = 'Design Ideas')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Activity"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Collection"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 125
               Right = 396
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Media"
            Begin Extent = 
               Top = 6
               Left = 434
               Bottom = 125
               Right = 594
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 245
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Design_Ideas'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Design_Ideas'
GO
/****** Object:  Table [dbo].[Feedback_Media]    Script Date: 10/24/2013 13:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback_Media](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[note] [nvarchar](max) NOT NULL,
	[date] [datetime] NOT NULL,
	[type_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[parent_id] [int] NOT NULL,
	[media_id] [int] NOT NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Feedback_Media] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Collection_Activity]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Collection]  WITH CHECK ADD  CONSTRAINT [FK_Collection_Activity] FOREIGN KEY([activity_id])
REFERENCES [dbo].[Activity] ([id])
GO
ALTER TABLE [dbo].[Collection] CHECK CONSTRAINT [FK_Collection_Activity]
GO
/****** Object:  ForeignKey [FK_Collection_User]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Collection]  WITH CHECK ADD  CONSTRAINT [FK_Collection_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Collection] CHECK CONSTRAINT [FK_Collection_User]
GO
/****** Object:  ForeignKey [FK_Feedback_Activity_Activity]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Activity_Activity] FOREIGN KEY([activity_id])
REFERENCES [dbo].[Activity] ([id])
GO
ALTER TABLE [dbo].[Feedback_Activity] CHECK CONSTRAINT [FK_Feedback_Activity_Activity]
GO
/****** Object:  ForeignKey [FK_Feedback_Activity_Feedback_Activity]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Activity_Feedback_Activity] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Feedback_Activity] ([id])
GO
ALTER TABLE [dbo].[Feedback_Activity] CHECK CONSTRAINT [FK_Feedback_Activity_Feedback_Activity]
GO
/****** Object:  ForeignKey [FK_Feedback_Activity_Feedback_Type]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Activity_Feedback_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[Feedback_Type] ([id])
GO
ALTER TABLE [dbo].[Feedback_Activity] CHECK CONSTRAINT [FK_Feedback_Activity_Feedback_Type]
GO
/****** Object:  ForeignKey [FK_Feedback_Activity_User]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Activity_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Feedback_Activity] CHECK CONSTRAINT [FK_Feedback_Activity_User]
GO
/****** Object:  ForeignKey [FK_Feedback_Collection_Collection]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_Collection]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Collection_Collection] FOREIGN KEY([collection_id])
REFERENCES [dbo].[Collection] ([id])
GO
ALTER TABLE [dbo].[Feedback_Collection] CHECK CONSTRAINT [FK_Feedback_Collection_Collection]
GO
/****** Object:  ForeignKey [FK_Feedback_Collection_Feedback_Collection]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_Collection]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Collection_Feedback_Collection] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Feedback_Collection] ([id])
GO
ALTER TABLE [dbo].[Feedback_Collection] CHECK CONSTRAINT [FK_Feedback_Collection_Feedback_Collection]
GO
/****** Object:  ForeignKey [FK_Feedback_Collection_Feedback_Type]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_Collection]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Collection_Feedback_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[Feedback_Type] ([id])
GO
ALTER TABLE [dbo].[Feedback_Collection] CHECK CONSTRAINT [FK_Feedback_Collection_Feedback_Type]
GO
/****** Object:  ForeignKey [FK_Feedback_Collection_User]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_Collection]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Collection_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Feedback_Collection] CHECK CONSTRAINT [FK_Feedback_Collection_User]
GO
/****** Object:  ForeignKey [FK_Feedback_Media_Feedback_Media]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_Media]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Media_Feedback_Media] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Feedback_Media] ([id])
GO
ALTER TABLE [dbo].[Feedback_Media] CHECK CONSTRAINT [FK_Feedback_Media_Feedback_Media]
GO
/****** Object:  ForeignKey [FK_Feedback_Media_Feedback_Type]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_Media]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Media_Feedback_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[Feedback_Type] ([id])
GO
ALTER TABLE [dbo].[Feedback_Media] CHECK CONSTRAINT [FK_Feedback_Media_Feedback_Type]
GO
/****** Object:  ForeignKey [FK_Feedback_Media_Media]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_Media]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Media_Media] FOREIGN KEY([media_id])
REFERENCES [dbo].[Media] ([id])
GO
ALTER TABLE [dbo].[Feedback_Media] CHECK CONSTRAINT [FK_Feedback_Media_Media]
GO
/****** Object:  ForeignKey [FK_Feedback_Media_User]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_Media]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Media_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Feedback_Media] CHECK CONSTRAINT [FK_Feedback_Media_User]
GO
/****** Object:  ForeignKey [FK_Feedback_User_Feedback_Type]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_User]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_User_Feedback_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[Feedback_Type] ([id])
GO
ALTER TABLE [dbo].[Feedback_User] CHECK CONSTRAINT [FK_Feedback_User_Feedback_Type]
GO
/****** Object:  ForeignKey [FK_Feedback_User_Feedback_User]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_User]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_User_Feedback_User] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Feedback_User] ([id])
GO
ALTER TABLE [dbo].[Feedback_User] CHECK CONSTRAINT [FK_Feedback_User_Feedback_User]
GO
/****** Object:  ForeignKey [FK_Feedback_User_User]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_User]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_User_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Feedback_User] CHECK CONSTRAINT [FK_Feedback_User_User]
GO
/****** Object:  ForeignKey [FK_Feedback_User_User_V]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Feedback_User]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_User_User_V] FOREIGN KEY([v_user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Feedback_User] CHECK CONSTRAINT [FK_Feedback_User_User_V]
GO
/****** Object:  ForeignKey [FK_Media_Collection]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Media]  WITH CHECK ADD  CONSTRAINT [FK_Media_Collection] FOREIGN KEY([collection_id])
REFERENCES [dbo].[Collection] ([id])
GO
ALTER TABLE [dbo].[Media] CHECK CONSTRAINT [FK_Media_Collection]
GO
/****** Object:  ForeignKey [FK_Media_Location]    Script Date: 10/24/2013 13:01:15 ******/
ALTER TABLE [dbo].[Media]  WITH CHECK ADD  CONSTRAINT [FK_Media_Location] FOREIGN KEY([location_id])
REFERENCES [dbo].[Location] ([id])
GO
ALTER TABLE [dbo].[Media] CHECK CONSTRAINT [FK_Media_Location]
GO
