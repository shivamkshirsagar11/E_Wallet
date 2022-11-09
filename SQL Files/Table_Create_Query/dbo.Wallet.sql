USE [EWallet]
GO

/****** Object: Table [dbo].[Wallet] Script Date: 11/9/2022 10:51:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Wallet] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Balance]      FLOAT (53)     NOT NULL,
    [BankI]        INT            NOT NULL,
    [LastOperated] NVARCHAR (MAX) NOT NULL,
    [TakeANote]    NVARCHAR (MAX) NOT NULL,
    [UserI]        INT            NOT NULL
);


