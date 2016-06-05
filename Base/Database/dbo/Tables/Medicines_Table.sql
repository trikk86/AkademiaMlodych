CREATE TABLE [dbo].[Medicines_Table](
	[ID_leku] [int] IDENTITY(1,1) NOT NULL,
	[ID] [int] NULL,
	[Medicine_Name] [varchar](50) NOT NULL,
	[Dose] [varchar](50) NOT NULL,
	[Additional_Information] [varchar](max) NULL,
	[Beginning_Date] [datetime] NULL,
	[The_End_Date] [datetime] NULL,
	[Tolerance_Hour] [tinyint] NULL,
	[Iteration] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Leki_Tabela] PRIMARY KEY CLUSTERED 
(
	[ID_leku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Medicines_Table]  WITH CHECK ADD  CONSTRAINT [FK_Medicines_Table_Users_Table] FOREIGN KEY([ID])
REFERENCES [dbo].[Users_Table] ([ID])
GO

ALTER TABLE [dbo].[Medicines_Table] CHECK CONSTRAINT [FK_Medicines_Table_Users_Table]