USE [EWallet]
GO

/****** Object: Table [dbo].[Users] Script Date: 11/9/2022 10:51:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    [Email]    NVARCHAR (MAX) NOT NULL,
    [Mobile]   NVARCHAR (MAX) NOT NULL,
    [Address]  NVARCHAR (MAX) NOT NULL,
    [Gender]   NVARCHAR (MAX) NOT NULL,
    [Password] NVARCHAR (MAX) NOT NULL,
    [Use]      NVARCHAR (MAX) NOT NULL,
    [Zipcode]  NVARCHAR (MAX) NOT NULL
);


