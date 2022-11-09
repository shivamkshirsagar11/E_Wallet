USE [EWallet]
GO

/****** Object: Table [dbo].[Passbook] Script Date: 11/9/2022 10:51:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Passbook] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Message] NVARCHAR (MAX) NOT NULL,
    [UserI]   INT            NOT NULL,
    [Date]    NVARCHAR (MAX) NOT NULL,
    [Action]  NVARCHAR (MAX) NOT NULL
);


