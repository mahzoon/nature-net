USE [nature-net]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 10/21/2013 23:14:18 ******/
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
/****** Object:  Table [dbo].[Location]    Script Date: 10/21/2013 23:14:18 ******/
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
SET IDENTITY_INSERT [dbo].[Location] OFF
/****** Object:  Table [dbo].[User]    Script Date: 10/21/2013 23:14:18 ******/
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
/****** Object:  Table [dbo].[Feedback_Type]    Script Date: 10/21/2013 23:14:18 ******/
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
/****** Object:  Table [dbo].[Feedback_Activity]    Script Date: 10/21/2013 23:14:18 ******/
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
/****** Object:  Table [dbo].[Collection]    Script Date: 10/21/2013 23:14:18 ******/
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
/****** Object:  Table [dbo].[Feedback_User]    Script Date: 10/21/2013 23:14:18 ******/
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
/****** Object:  Table [dbo].[Feedback_Collection]    Script Date: 10/21/2013 23:14:18 ******/
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
/****** Object:  Table [dbo].[Media]    Script Date: 10/21/2013 23:14:18 ******/
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
/****** Object:  Table [dbo].[Feedback_Media]    Script Date: 10/21/2013 23:14:18 ******/
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
/****** Object:  ForeignKey [FK_Collection_Activity]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Collection]  WITH CHECK ADD  CONSTRAINT [FK_Collection_Activity] FOREIGN KEY([activity_id])
REFERENCES [dbo].[Activity] ([id])
GO
ALTER TABLE [dbo].[Collection] CHECK CONSTRAINT [FK_Collection_Activity]
GO
/****** Object:  ForeignKey [FK_Collection_User]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Collection]  WITH CHECK ADD  CONSTRAINT [FK_Collection_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Collection] CHECK CONSTRAINT [FK_Collection_User]
GO
/****** Object:  ForeignKey [FK_Feedback_Activity_Activity]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Activity_Activity] FOREIGN KEY([activity_id])
REFERENCES [dbo].[Activity] ([id])
GO
ALTER TABLE [dbo].[Feedback_Activity] CHECK CONSTRAINT [FK_Feedback_Activity_Activity]
GO
/****** Object:  ForeignKey [FK_Feedback_Activity_Feedback_Activity]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Activity_Feedback_Activity] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Feedback_Activity] ([id])
GO
ALTER TABLE [dbo].[Feedback_Activity] CHECK CONSTRAINT [FK_Feedback_Activity_Feedback_Activity]
GO
/****** Object:  ForeignKey [FK_Feedback_Activity_Feedback_Type]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Activity_Feedback_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[Feedback_Type] ([id])
GO
ALTER TABLE [dbo].[Feedback_Activity] CHECK CONSTRAINT [FK_Feedback_Activity_Feedback_Type]
GO
/****** Object:  ForeignKey [FK_Feedback_Activity_User]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Activity_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Feedback_Activity] CHECK CONSTRAINT [FK_Feedback_Activity_User]
GO
/****** Object:  ForeignKey [FK_Feedback_Collection_Collection]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_Collection]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Collection_Collection] FOREIGN KEY([collection_id])
REFERENCES [dbo].[Collection] ([id])
GO
ALTER TABLE [dbo].[Feedback_Collection] CHECK CONSTRAINT [FK_Feedback_Collection_Collection]
GO
/****** Object:  ForeignKey [FK_Feedback_Collection_Feedback_Collection]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_Collection]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Collection_Feedback_Collection] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Feedback_Collection] ([id])
GO
ALTER TABLE [dbo].[Feedback_Collection] CHECK CONSTRAINT [FK_Feedback_Collection_Feedback_Collection]
GO
/****** Object:  ForeignKey [FK_Feedback_Collection_Feedback_Type]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_Collection]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Collection_Feedback_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[Feedback_Type] ([id])
GO
ALTER TABLE [dbo].[Feedback_Collection] CHECK CONSTRAINT [FK_Feedback_Collection_Feedback_Type]
GO
/****** Object:  ForeignKey [FK_Feedback_Collection_User]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_Collection]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Collection_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Feedback_Collection] CHECK CONSTRAINT [FK_Feedback_Collection_User]
GO
/****** Object:  ForeignKey [FK_Feedback_Media_Feedback_Media]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_Media]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Media_Feedback_Media] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Feedback_Media] ([id])
GO
ALTER TABLE [dbo].[Feedback_Media] CHECK CONSTRAINT [FK_Feedback_Media_Feedback_Media]
GO
/****** Object:  ForeignKey [FK_Feedback_Media_Feedback_Type]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_Media]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Media_Feedback_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[Feedback_Type] ([id])
GO
ALTER TABLE [dbo].[Feedback_Media] CHECK CONSTRAINT [FK_Feedback_Media_Feedback_Type]
GO
/****** Object:  ForeignKey [FK_Feedback_Media_Media]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_Media]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Media_Media] FOREIGN KEY([media_id])
REFERENCES [dbo].[Media] ([id])
GO
ALTER TABLE [dbo].[Feedback_Media] CHECK CONSTRAINT [FK_Feedback_Media_Media]
GO
/****** Object:  ForeignKey [FK_Feedback_Media_User]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_Media]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Media_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Feedback_Media] CHECK CONSTRAINT [FK_Feedback_Media_User]
GO
/****** Object:  ForeignKey [FK_Feedback_User_Feedback_Type]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_User]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_User_Feedback_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[Feedback_Type] ([id])
GO
ALTER TABLE [dbo].[Feedback_User] CHECK CONSTRAINT [FK_Feedback_User_Feedback_Type]
GO
/****** Object:  ForeignKey [FK_Feedback_User_Feedback_User]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_User]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_User_Feedback_User] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Feedback_User] ([id])
GO
ALTER TABLE [dbo].[Feedback_User] CHECK CONSTRAINT [FK_Feedback_User_Feedback_User]
GO
/****** Object:  ForeignKey [FK_Feedback_User_User]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_User]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_User_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Feedback_User] CHECK CONSTRAINT [FK_Feedback_User_User]
GO
/****** Object:  ForeignKey [FK_Feedback_User_User_V]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Feedback_User]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_User_User_V] FOREIGN KEY([v_user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Feedback_User] CHECK CONSTRAINT [FK_Feedback_User_User_V]
GO
/****** Object:  ForeignKey [FK_Media_Collection]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Media]  WITH CHECK ADD  CONSTRAINT [FK_Media_Collection] FOREIGN KEY([collection_id])
REFERENCES [dbo].[Collection] ([id])
GO
ALTER TABLE [dbo].[Media] CHECK CONSTRAINT [FK_Media_Collection]
GO
/****** Object:  ForeignKey [FK_Media_Location]    Script Date: 10/21/2013 23:14:18 ******/
ALTER TABLE [dbo].[Media]  WITH CHECK ADD  CONSTRAINT [FK_Media_Location] FOREIGN KEY([location_id])
REFERENCES [dbo].[Location] ([id])
GO
ALTER TABLE [dbo].[Media] CHECK CONSTRAINT [FK_Media_Location]
GO
