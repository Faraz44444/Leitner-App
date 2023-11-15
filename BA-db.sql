USE [BA_STAGING]
GO
/****** Object:  Table [dbo].[ACTION_LOG_TAB]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACTION_LOG_TAB](
	[ActionLogId] [bigint] NOT NULL,
	[Operationtype] [int] NOT NULL,
	[Name] [varchar](500) NULL,
	[EntityType] [int] NOT NULL,
	[EntityJson] [varchar](max) NULL,
	[EntityId] [bigint] NULL,
	[AlternateEntityId] [bigint] NULL,
	[ClientId] [bigint] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedByUserId] [bigint] NULL,
	[CreatedByFirstName] [varchar](50) NULL,
	[CreatedByLastName] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BUSINESS_TAB]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BUSINESS_TAB](
	[BusinessId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreatedByUserId] [bigint] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ClientId] [bigint] NOT NULL,
	[CreatedByFirstName] [varchar](50) NULL,
	[CreatedByLastName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_BUSINESS_TAB] PRIMARY KEY CLUSTERED 
(
	[BusinessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BUSINESS_TAB$]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BUSINESS_TAB$](
	[BusinessId] [float] NULL,
	[BusinessName] [nvarchar](max) NULL,
	[CreatedByUserId] [float] NULL,
	[CreatedAt] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CATEGORY_TAB]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORY_TAB](
	[CategoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Priority] [int] NOT NULL,
	[MonthlyLimit] [float] NULL,
	[WeeklyLimit] [float] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedByUserId] [bigint] NOT NULL,
	[CreatedByFirstName] [varchar](50) NULL,
	[CreatedByLastName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
	[ClientId] [bigint] NOT NULL,
 CONSTRAINT [PK_CATEGORY_TAB] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CATEGORY_TAB$]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORY_TAB$](
	[CategoryId] [float] NULL,
	[CategoryName] [nvarchar](max) NULL,
	[CategoryPriority] [float] NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedByUserId] [float] NULL,
	[MonthlyLimit] [float] NULL,
	[WeeklyLimit] [float] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CLIENT_TAB]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLIENT_TAB](
	[ClientId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[OrganizationNo] [varchar](250) NULL,
	[Email] [varchar](50) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedByUserId] [bigint] NOT NULL,
	[CreatedByFirstName] [varchar](50) NOT NULL,
	[CreatedByLastName] [varchar](50) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_CLIENT_TAB] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MONTHLY_PAYMENT_STATS_TAB]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MONTHLY_PAYMENT_STATS_TAB](
	[MonthlyPaymentStatsId] [bigint] NOT NULL,
	[Price] [float] NOT NULL,
	[Date] [date] NOT NULL,
	[IsDeposit] [bit] NOT NULL,
	[Description] [varchar](255) NULL,
	[CreatedByUserId] [bigint] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedByFirstName] [varchar](50) NULL,
	[CreatedByLastName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
	[ClientId] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MONTHLY_PAYMENT_STATS_TAB$]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MONTHLY_PAYMENT_STATS_TAB$](
	[MonthlyPaymentStatsId] [float] NULL,
	[Price] [float] NULL,
	[Date] [nvarchar](255) NULL,
	[IsDeposit] [float] NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedByUserId] [float] NULL,
	[CreatedAt] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Old-User]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Old-User](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [bigint] NULL,
	[Username] [varchar](200) NULL,
	[Email] [varchar](200) NULL,
	[PasswordHash] [varchar](200) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[UserType] [int] NULL,
	[LastLogInAt] [datetime] NULL,
	[PhoneNumber] [int] NULL,
	[Active] [bit] NULL,
	[CssFontSize] [float] NULL,
	[ForcePasswordReset] [bit] NULL,
	[ForceUserInformationUpdate] [bit] NULL,
	[UserInitials] [varchar](200) NULL,
	[PasswordLastSetAt] [datetime] NULL,
	[PasswordIsAutogenerated] [bit] NULL,
	[PasswordResetGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_USER_TAB] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAYMENT_PRIORITY_TAB]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAYMENT_PRIORITY_TAB](
	[PaymentPriorityId] [bigint] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedByUserId] [bigint] NOT NULL,
	[CreatedByFirstName] [varchar](50) NULL,
	[CreatedByLastName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
	[ClientId] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAYMENT_PRIORITY_TAB$]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAYMENT_PRIORITY_TAB$](
	[PaymentPriorityId] [float] NULL,
	[PaymentPriorityName] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedByUserId] [float] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAYMENT_TAB]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAYMENT_TAB](
	[PaymentId] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[PaymentPriorityId] [bigint] NOT NULL,
	[BusinessId] [bigint] NOT NULL,
	[IsDeposit] [bit] NOT NULL,
	[IsPaidToPerson] [bit] NOT NULL,
	[CategoryId] [bigint] NOT NULL,
	[Price] [float] NOT NULL,
	[Date] [datetime] NOT NULL,
	[IsShared] [bit] NOT NULL,
	[IsPaidForSomeoneElse] [bit] NOT NULL,
	[CreatedByUserId] [bigint] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ApprovedAt] [datetime] NULL,
	[ClientId] [bigint] NOT NULL,
	[CreatedByFirstName] [varchar](50) NULL,
	[CreatedbyLastName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_PAYMENT_TAB] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAYMENT_TAB$]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAYMENT_TAB$](
	[PaymentId] [float] NULL,
	[Title] [nvarchar](max) NULL,
	[PaymentPriorityId] [float] NULL,
	[BusinessId] [float] NULL,
	[IsDeposit] [float] NULL,
	[IsPaidToPerson] [float] NULL,
	[CategoryId] [float] NULL,
	[Price] [float] NULL,
	[Date] [datetime] NULL,
	[CreatedByUserId] [float] NULL,
	[CreatedAt] [datetime] NULL,
	[ApprovedAt] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAYMENT_TOTAL_TAB]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAYMENT_TOTAL_TAB](
	[PaymentTotalId] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[BusinessId] [bigint] NULL,
	[Price] [float] NULL,
	[Date] [datetime] NOT NULL,
	[IsDeposit] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedByUserId] [bigint] NOT NULL,
	[ClientId] [bigint] NOT NULL,
	[CreatedByFirstName] [varchar](50) NULL,
	[CreatedbyLastName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_PAYMENT_TOTAL_TAB] PRIMARY KEY CLUSTERED 
(
	[PaymentTotalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAYMENT_TOTAL_TAB$]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAYMENT_TOTAL_TAB$](
	[PaymentTotalId] [float] NULL,
	[Title] [nvarchar](max) NULL,
	[BusinessId] [float] NULL,
	[Price] [float] NULL,
	[Date] [datetime] NULL,
	[IsDeposit] [float] NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedByUserId] [float] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ROLE_PERMISSION_TAB]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLE_PERMISSION_TAB](
	[RoleId] [bigint] NOT NULL,
	[Permission] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
	[ClientId] [bigint] NOT NULL,
	[CreatedByUserId] [bigint] NOT NULL,
	[CreatedByFirstName] [varchar](50) NULL,
	[CreatedByLastName] [varchar](50) NULL,
	[CreatedAt] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ROLE_PERMISSION_TAB$]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLE_PERMISSION_TAB$](
	[RolePermissionId] [float] NULL,
	[PermissionType] [float] NULL,
	[RoleId] [float] NULL,
	[IsLocked] [float] NULL,
	[Name] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ROLE_TAB]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLE_TAB](
	[RoleId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[ClientId] [bigint] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedByUserId] [bigint] NOT NULL,
	[CreatedByFirstName] [varchar](50) NULL,
	[CreatedByLastName] [varchar](50) NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedAt] [nchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ROLE_TAB$]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLE_TAB$](
	[RoleId] [float] NULL,
	[Name] [nvarchar](max) NULL,
	[IsLocked] [float] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USER_CLIENT_TAB]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USER_CLIENT_TAB](
	[UserId] [bigint] NOT NULL,
	[ClientId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USER_TAB]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USER_TAB](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](200) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[CurrentClientId] [bigint] NOT NULL,
	[IsSystemUser] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[CssFontSize] [float] NULL,
	[ForcePasswordReset] [bit] NOT NULL,
	[ForceUserInformationUpdate] [bit] NOT NULL,
	[UserInitials] [varchar](200) NULL,
	[PasswordSalt] [varchar](max) NULL,
	[PasswordLastSetAt] [datetime] NULL,
	[PasswordIsAutogenerated] [bit] NULL,
	[PasswordResetGuid] [uniqueidentifier] NULL,
	[LastLogInAt] [datetime] NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedByUserId] [bigint] NOT NULL,
	[CreatedByFirstName] [varchar](50) NULL,
	[CreatedByLastName] [varchar](50) NULL,
	[PasswordHash] [varchar](max) NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
	[LastUpdateAt] [datetime] NULL,
	[PasswordResetToken] [varchar](max) NULL,
	[PasswordResetTokenGeneratedAt] [datetime] NULL,
 CONSTRAINT [PK_USER_TAB_1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USER_TAB$]    Script Date: 11/1/2023 9:50:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USER_TAB$](
	[UserId] [float] NULL,
	[RoleId] [float] NULL,
	[Username] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[UserType] [float] NULL,
	[LastLogInAt] [datetime] NULL,
	[PhoneNumber] [float] NULL,
	[Active] [float] NULL,
	[CssFontSize] [float] NULL,
	[ForcePasswordReset] [float] NULL,
	[ForceUserInformationUpdate] [float] NULL,
	[UserInitials] [nvarchar](max) NULL,
	[PasswordLastSetAt] [datetime] NULL,
	[PasswordIsAutogenerated] [float] NULL,
	[PasswordResetGuid] [nvarchar](255) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[BUSINESS_TAB] ADD  CONSTRAINT [DF_BUSINESS_TAB_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[CATEGORY_TAB] ADD  CONSTRAINT [DF_CATEGORY_TAB_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[CLIENT_TAB] ADD  CONSTRAINT [DF_CLIENT_TAB_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[MONTHLY_PAYMENT_STATS_TAB] ADD  CONSTRAINT [DF_MONTHLY_PAYMENT_STATS_TAB_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[PAYMENT_PRIORITY_TAB] ADD  CONSTRAINT [DF_PAYMENT_PRIORITY_TAB_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[PAYMENT_TAB] ADD  CONSTRAINT [DF_PAYMENT_TAB_IsShared]  DEFAULT ((0)) FOR [IsShared]
GO
ALTER TABLE [dbo].[PAYMENT_TAB] ADD  CONSTRAINT [DF_PAYMENT_TAB_IsPaidForSomeoneElse]  DEFAULT ((0)) FOR [IsPaidForSomeoneElse]
GO
ALTER TABLE [dbo].[PAYMENT_TAB] ADD  CONSTRAINT [DF_PAYMENT_TAB_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[PAYMENT_TOTAL_TAB] ADD  CONSTRAINT [DF_PAYMENT_TOTAL_TAB_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[ROLE_PERMISSION_TAB] ADD  CONSTRAINT [DF_ROLE_PERMISSION_TAB_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[ROLE_TAB] ADD  CONSTRAINT [DF_ROLE_TAB_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[USER_TAB] ADD  CONSTRAINT [DF_USER_TAB_IsSystemUser]  DEFAULT ((0)) FOR [IsSystemUser]
GO
ALTER TABLE [dbo].[USER_TAB] ADD  CONSTRAINT [DF_USER_TAB_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[USER_TAB] ADD  CONSTRAINT [DF_USER_TAB_ForcePasswordReset]  DEFAULT ((1)) FOR [ForcePasswordReset]
GO
ALTER TABLE [dbo].[USER_TAB] ADD  CONSTRAINT [DF_USER_TAB_ForceUserInformationUpdate]  DEFAULT ((0)) FOR [ForceUserInformationUpdate]
GO
ALTER TABLE [dbo].[USER_TAB] ADD  CONSTRAINT [DF_USER_TAB_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[CLIENT_TAB]  WITH CHECK ADD  CONSTRAINT [FK_CLIENT_TAB_USER_TAB] FOREIGN KEY([CreatedByUserId])
REFERENCES [dbo].[Old-User] ([UserId])
GO
ALTER TABLE [dbo].[CLIENT_TAB] CHECK CONSTRAINT [FK_CLIENT_TAB_USER_TAB]
GO
