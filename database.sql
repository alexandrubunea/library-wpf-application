USE [dbBook]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[AuthorId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuthorBook]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuthorBook](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuthorId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[NumberInList] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_AuthorBook] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[BookTypeId] [int] NOT NULL,
	[PublisherId] [int] NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[PublishYear] [date] NOT NULL,
	[Stock] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookType]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookType](
	[BookTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_BookType] PRIMARY KEY CLUSTERED 
(
	[BookTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publisher]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publisher](
	[PublisherId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Publisher] PRIMARY KEY CLUSTERED 
(
	[PublisherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[AccessLevel] [int] NOT NULL,
	[Password] [varchar](512) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserBook]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserBook](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[ReturnDate] [date] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_UserBook] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Author] ADD  CONSTRAINT [DF_Author_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[AuthorBook] ADD  CONSTRAINT [DF_AuthorBook_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BookType] ADD  CONSTRAINT [DF_BookType_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Publisher] ADD  CONSTRAINT [DF_Publisher_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_AccessLevel]  DEFAULT ((0)) FOR [AccessLevel]
GO
ALTER TABLE [dbo].[UserBook] ADD  CONSTRAINT [DF_UserBook_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[AuthorBook]  WITH CHECK ADD  CONSTRAINT [FK_AuthorBook_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Author] ([AuthorId])
GO
ALTER TABLE [dbo].[AuthorBook] CHECK CONSTRAINT [FK_AuthorBook_Author]
GO
ALTER TABLE [dbo].[AuthorBook]  WITH CHECK ADD  CONSTRAINT [FK_AuthorBook_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO
ALTER TABLE [dbo].[AuthorBook] CHECK CONSTRAINT [FK_AuthorBook_Book]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_BookType] FOREIGN KEY([BookTypeId])
REFERENCES [dbo].[BookType] ([BookTypeId])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_BookType]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Publisher] FOREIGN KEY([PublisherId])
REFERENCES [dbo].[Publisher] ([PublisherId])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Publisher]
GO
ALTER TABLE [dbo].[UserBook]  WITH CHECK ADD  CONSTRAINT [FK_UserBook_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO
ALTER TABLE [dbo].[UserBook] CHECK CONSTRAINT [FK_UserBook_Book]
GO
ALTER TABLE [dbo].[UserBook]  WITH CHECK ADD  CONSTRAINT [FK_UserBook_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserBook] CHECK CONSTRAINT [FK_UserBook_User]
GO
/****** Object:  StoredProcedure [dbo].[checkCredentials]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[checkCredentials] 
	-- Add the parameters for the stored procedure here
	@Email VARCHAR(50),
	@Password VARCHAR(512)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [UserId] FROM [User] WHERE [Email] = @Email AND [Password] = @Password;
END
GO
/****** Object:  StoredProcedure [dbo].[countAuthorBooks]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[countAuthorBooks] 
	-- Add the parameters for the stored procedure here
	@AuthorId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) 
    FROM [AuthorBook] AB
    WHERE AB.[AuthorId] = @AuthorId AND AB.[Active] = 1
    AND EXISTS (
        SELECT 1 
        FROM [Book] B 
        WHERE B.[BookId] = AB.[BookId] 
        AND B.Active = 1
		AND B.Stock > 0 )
END
GO
/****** Object:  StoredProcedure [dbo].[countBookTypeBooks]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[countBookTypeBooks]
	-- Add the parameters for the stored procedure here
	@BookTypeId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) FROM [Book] WHERE [BookTypeId] = @BookTypeId AND [Active] = 1 AND [Stock] > 0;;
END
GO
/****** Object:  StoredProcedure [dbo].[countPublisherBooks]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[countPublisherBooks]
	-- Add the parameters for the stored procedure here
	@PublisherId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) FROM [Book] WHERE [PublisherId] = @PublisherId AND [Active] = 1 AND [Stock] > 0;;
END
GO
/****** Object:  StoredProcedure [dbo].[createAuthor]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[createAuthor]
	-- Add the parameters for the stored procedure here
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@Date date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [Author]([FirstName], [LastName], [BirthDate]) VALUES(@FirstName, @LastName, @Date);
END
GO
/****** Object:  StoredProcedure [dbo].[createAuthorBook]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[createAuthorBook]
	-- Add the parameters for the stored procedure here
	@AuthorId INT,
	@BookId INT,
	@NumberInList INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [AuthorBook]([AuthorId], [BookId], [NumberInList]) VALUES(@AuthorId, @BookId, @NumberInList);
END
GO
/****** Object:  StoredProcedure [dbo].[createBook]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[createBook]
	-- Add the parameters for the stored procedure here
	@Title VARCHAR(50),
	@BookTypeId INT,
	@PublisherId INT,
	@PublishYear date,
	@Stock INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [Book]([Title], [BookTypeId], [PublisherId], [PublishYear], [Stock]) VALUES(@Title, @BookTypeId, @PublisherId, @PublishYear, @Stock);
END
GO
/****** Object:  StoredProcedure [dbo].[createBookType]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[createBookType] 
	-- Add the parameters for the stored procedure here
	@Name VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [BookType]([Name]) VALUES(@Name);
END
GO
/****** Object:  StoredProcedure [dbo].[createPublisher]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[createPublisher]
	-- Add the parameters for the stored procedure here
	@Name VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [Publisher]([Name]) VALUES(@Name);
END
GO
/****** Object:  StoredProcedure [dbo].[createUser]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[createUser](
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@Email VARCHAR(50),
	@Phone VARCHAR(50),
	@Password VARCHAR(512)
)
AS
BEGIN
	INSERT INTO [User]([FirstName], [LastName], [Email], [Phone], [Password]) VALUES(@FirstName, @LastName, @Email, @Phone, @Password);
END
GO
/****** Object:  StoredProcedure [dbo].[createUserBook]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[createUserBook]
	-- Add the parameters for the stored procedure here
	@UserId INT,
	@BookId INT,
	@StartDate date,
	@ReturnDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [UserBook]([UserId], [BookId], [StartDate], [ReturnDate]) VALUES(@UserId, @BookId, @StartDate, @ReturnDate);
END
GO
/****** Object:  StoredProcedure [dbo].[decreaseBookStock]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[decreaseBookStock] 
	-- Add the parameters for the stored procedure here
	@BookId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [Book] SET [Stock] = [Stock] - 1 WHERE [Stock] > 0 AND [BookId] = @BookId;
END
GO
/****** Object:  StoredProcedure [dbo].[doesAccountExists]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[doesAccountExists]
	-- Add the parameters for the stored procedure here
	@Email VARCHAR(50),
	@Phone VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) AS total_count FROM [User] WHERE [Email] = @Email OR [Phone] = @Phone;
END
GO
/****** Object:  StoredProcedure [dbo].[doesAuthorBookExists]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[doesAuthorBookExists]
	-- Add the parameters for the stored procedure here
	@BookId INT,
	@AuthorId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) FROM [AuthorBook] WHERE [BookId] = @BookId AND [AuthorId] = @AuthorId AND [Active] = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[doesAuthorExists]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[doesAuthorExists]
	-- Add the parameters for the stored procedure here
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) FROM [Author] WHERE [FirstName] = @FirstName AND [LastName] = @LastName;
END
GO
/****** Object:  StoredProcedure [dbo].[doesBookExists]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[doesBookExists]
	-- Add the parameters for the stored procedure here
	@Title VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) FROM [Book] WHERE [Title] = @Title;
END
GO
/****** Object:  StoredProcedure [dbo].[doesBookTypeExists]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[doesBookTypeExists]
	-- Add the parameters for the stored procedure here
	@Name VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) FROM [BookType] WHERE [Name] = @Name;
END
GO
/****** Object:  StoredProcedure [dbo].[doesBorrowExists]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[doesBorrowExists]
	-- Add the parameters for the stored procedure here
	@UserId INT,
	@BookId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) FROM [UserBook] WHERE [UserId] = @UserId AND [BookId] = @BookId AND [Active] = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[doesPublisherExists]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[doesPublisherExists]
	-- Add the parameters for the stored procedure here
	@Name VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) FROM [Publisher] WHERE [Name] = @Name;
END
GO
/****** Object:  StoredProcedure [dbo].[fetchId]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[fetchId]
	-- Add the parameters for the stored procedure here
	@Email VARCHAR(50),
	@Phone VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [UserId] FROM [User] WHERE [Email] = @Email AND [Phone] = @Phone;
END
GO
/****** Object:  StoredProcedure [dbo].[getAuthor]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getAuthor]
	-- Add the parameters for the stored procedure here
	@AuthorId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [Author] WHERE [AuthorId] = @AuthorId;
END
GO
/****** Object:  StoredProcedure [dbo].[getBookById]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getBookById]
	-- Add the parameters for the stored procedure here
	@BookId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [BookTypeId], [PublisherId], [Title], [PublishYear], [Stock], [Active] FROM [Book] WHERE [BookId] = @BookId;
END
GO
/****** Object:  StoredProcedure [dbo].[getBookType]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getBookType]
	-- Add the parameters for the stored procedure here
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [BookType] WHERE [BookTypeId] = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[getLastBookId]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getLastBookId] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ISNULL(IDENT_CURRENT('Book'), 0) AS LastBookId
END
GO
/****** Object:  StoredProcedure [dbo].[getPublisher]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getPublisher]
	-- Add the parameters for the stored procedure here
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [Publisher] WHERE [PublisherId] = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[getUserBooks]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getUserBooks]
	-- Add the parameters for the stored procedure here
	@UserId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [UserBook] WHERE [UserId] = @UserId AND [Active] = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[getUserById]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getUserById]
	-- Add the parameters for the stored procedure here
	@UserId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [FirstName],[LastName],[Email],[Phone],[Active] FROM [User] WHERE [UserId] = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[increaseBookStock]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[increaseBookStock] 
	-- Add the parameters for the stored procedure here
	@BookId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [Book] SET [Stock] = [Stock] + 1 WHERE [BookId] = @BookId;
END
GO
/****** Object:  StoredProcedure [dbo].[isBookActive]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[isBookActive]
	-- Add the parameters for the stored procedure here
	@BookId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) FROM [Book] WHERE [BookId] = @BookId AND [Active] = 1 AND [Stock] > 0;
END
GO
/****** Object:  StoredProcedure [dbo].[removeAuthorsForBook]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[removeAuthorsForBook]
	-- Add the parameters for the stored procedure here
	@BookId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [AuthorBook] SET [Active] = 0 WHERE [BookId] = @BookId;
END
GO
/****** Object:  StoredProcedure [dbo].[retriveActiveAuthors]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[retriveActiveAuthors]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [Author] WHERE [Active] = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[retriveActiveBooks]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[retriveActiveBooks]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [Book] WHERE [Active] = 1 AND [Stock] > 0;
END
GO
/****** Object:  StoredProcedure [dbo].[retriveActiveBookTypes]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[retriveActiveBookTypes]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [BookType] WHERE [Active] = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[retriveActivePublishers]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[retriveActivePublishers]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [Publisher] WHERE [Active] = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[retriveAuthors]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[retriveAuthors]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [Author];
END
GO
/****** Object:  StoredProcedure [dbo].[retriveBookAuthors]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[retriveBookAuthors]
	-- Add the parameters for the stored procedure here
	@BookId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [AuthorId] FROM [AuthorBook] WHERE [BookId] = @BookId AND [Active] = 1 ORDER BY [NumberInList] ASC;
END
GO
/****** Object:  StoredProcedure [dbo].[retriveBooks]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[retriveBooks]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [Book];
END
GO
/****** Object:  StoredProcedure [dbo].[retriveBookTypes]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[retriveBookTypes]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [BookType];
END
GO
/****** Object:  StoredProcedure [dbo].[retrivePublishers]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[retrivePublishers]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [Publisher]
END
GO
/****** Object:  StoredProcedure [dbo].[retriveUserBook]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[retriveUserBook]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [UserBook] WHERE [Active] = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[retriveUserData]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[retriveUserData]
	-- Add the parameters for the stored procedure here
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [User] WHERE [UserId] = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[retriveUsers]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[retriveUsers]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [UserId],[FirstName],[LastName],[Email],[Phone],[Active] FROM [User];
END
GO
/****** Object:  StoredProcedure [dbo].[setAuthorStatus]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[setAuthorStatus] 
	-- Add the parameters for the stored procedure here
	@Id int,
	@Active bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [Author] SET [Active] = @Active WHERE [AuthorId] = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[setBookActiveStatus]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[setBookActiveStatus]
	-- Add the parameters for the stored procedure here
	@BookId INT,
	@Status bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [Book] SET [Active] = @Status WHERE [BookId] = @BookId;
END
GO
/****** Object:  StoredProcedure [dbo].[setBookTypeActiveStatus]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[setBookTypeActiveStatus]
	-- Add the parameters for the stored procedure here
	@Id INT,
	@Active bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [BookType] SET [Active] = @Active WHERE [BookTypeId] = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[setBorrowActiveStatus]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[setBorrowActiveStatus]
	-- Add the parameters for the stored procedure here
	@BorrowId INT,
	@Status bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [UserBook] SET [Active] = @Status WHERE [Id] = @BorrowId;
END
GO
/****** Object:  StoredProcedure [dbo].[setPublisherActiveStatus]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[setPublisherActiveStatus]
	-- Add the parameters for the stored procedure here
	@Id INT,
	@Active bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [Publisher] SET [Active] = @Active WHERE [PublisherId] = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[setUserActiveStatus]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[setUserActiveStatus]
	-- Add the parameters for the stored procedure here
	@Id INT,
	@Active bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [User] SET [Active] = @Active WHERE [UserId] = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[updateAuthor]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[updateAuthor]
	-- Add the parameters for the stored procedure here
	@AuthorId INT,
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@BirthDate DATE,
	@Active BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [Author] SET 
	[FirstName] = @FirstName, 
	[LastName] = @LastName, 
	[BirthDate] = @BirthDate, 
	[Active] = @Active
	WHERE [AuthorId] = @AuthorId;
END
GO
/****** Object:  StoredProcedure [dbo].[updateBook]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[updateBook]
	-- Add the parameters for the stored procedure here
	@BookId INT,
	@BookTypeId INT,
	@PublisherId INT,
	@Title VARCHAR(50),
	@PublishYear DATE,
	@Stock INT,
	@Active BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [Book] SET 
	[BookTypeId] = @BookTypeId, 
	[PublisherId] = @PublisherId, 
	[Title] = @Title, 
	[PublishYear] = @PublishYear, 
	[Stock] = @Stock, 
	[Active] = @Active 
	WHERE [BookId] = @BookId;
END
GO
/****** Object:  StoredProcedure [dbo].[updateBookType]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[updateBookType]
	-- Add the parameters for the stored procedure here
	@BookTypeId INT,
	@Name VARCHAR(50),
	@Active BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [BookType] SET 
	[Name] = @Name, 
	[Active] = @Active
	WHERE [BookTypeId] = @BookTypeId;
END
GO
/****** Object:  StoredProcedure [dbo].[updatePassword]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[updatePassword]
	-- Add the parameters for the stored procedure here
	@Email VARCHAR(50),
	@Password VARCHAR(512)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [User] SET [Password] = @Password WHERE [Email] = @Email;
END
GO
/****** Object:  StoredProcedure [dbo].[updatePublisher]    Script Date: 11.04.2024 05:47:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[updatePublisher]
	-- Add the parameters for the stored procedure here
	@PublisherId INT,
	@Name VARCHAR(50),
	@Active BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [Publisher] SET 
	[Name] = @Name, 
	[Active] = @Active
	WHERE [PublisherId] = @PublisherId;
END
GO
