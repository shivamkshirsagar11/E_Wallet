USE [EWallet]
GO

/****** Object: Table [dbo].[Card] Script Date: 11/9/2022 10:51:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Card] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserI]      INT            NOT NULL,
    [CardNo]     NVARCHAR (MAX) NOT NULL,
    [Expire]     NVARCHAR (MAX) NOT NULL,
    [Cvv]        NVARCHAR (MAX) NOT NULL,
    [BankName]   NVARCHAR (MAX) NOT NULL,
    [Holdername] NVARCHAR (MAX) NOT NULL,
    [Ifsc]       NVARCHAR (MAX) NOT NULL
);


