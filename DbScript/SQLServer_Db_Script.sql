USE [yplatform]
GO
/****** Object:  Table [dbo].[ActivityOrders]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[PromotionId] [int] NOT NULL,
	[AType] [int] NOT NULL,
	[Reward] [money] NOT NULL,
	[Status] [int] NOT NULL,
	[Ip] [varchar](15) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[RewardTime] [datetime] NOT NULL,
	[CreateDate] [varchar](10) NOT NULL,
	[SourceId] [varchar](128) NULL,
 CONSTRAINT [PK_ActivityRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityOrdersDetails]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityOrdersDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[MerchantId] [int] NOT NULL,
	[PromotionId] [int] NOT NULL,
	[AType] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Reward] [money] NOT NULL,
	[Status] [int] NOT NULL,
	[Description] [nvarchar](32) NOT NULL,
	[CreateDate] [varchar](10) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[RewardTime] [datetime] NOT NULL,
	[SourceId] [varchar](32) NOT NULL,
 CONSTRAINT [PK_ActivityOrdersDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityStatistic]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityStatistic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[PromotionId] [int] NOT NULL,
	[AType] [int] NOT NULL,
	[ActivityIn] [int] NOT NULL,
	[EffectiveIn] [int] NOT NULL,
	[Reward] [money] NOT NULL,
 CONSTRAINT [PK_ActivityStatistic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AgentDailyReportStatistic]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgentDailyReportStatistic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[MerchantId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[Pay] [money] NOT NULL,
	[PayNo] [int] NOT NULL,
	[Withdrawals] [money] NOT NULL,
	[WithdrawalsNo] [int] NOT NULL,
	[GameBetAmount] [money] NOT NULL,
	[GameValidBet] [money] NOT NULL,
	[GameMoney] [money] NOT NULL,
	[PromoMoney] [money] NOT NULL,
 CONSTRAINT [PK_AgentDailyReportStatistic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AgentRebate]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgentRebate](
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_AgentRebate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Domains]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Domains](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[DoType] [smallint] NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[IsHttps] [bit] NOT NULL,
	[Marks] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_Domians] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameApiRequestLog]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameApiRequestLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[TypeStr] [varchar](32) NOT NULL,
	[RequestData] [varchar](max) NOT NULL,
	[ResultData] [varchar](max) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreateTime] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_GameApiRequestLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameApiTimestamps]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameApiTimestamps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[TypeStr] [varchar](32) NOT NULL,
	[QueryTime] [datetime] NOT NULL,
	[Timestamps] [bigint] NOT NULL,
	[Mark] [varchar](32) NOT NULL,
 CONSTRAINT [PK_GameApiTimestamps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameDailyReportStatistic]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameDailyReportStatistic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[Type] [int] NOT NULL,
	[MerchantId] [int] NOT NULL,
	[PlayerName] [varchar](32) NOT NULL,
	[BetAmount] [money] NOT NULL,
	[ValidBet] [money] NOT NULL,
	[Money] [money] NOT NULL,
 CONSTRAINT [PK_GameDailyReportStattistic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameDailyReportStatisticByGameRules]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameDailyReportStatisticByGameRules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[Type] [int] NOT NULL,
	[MerchantId] [int] NOT NULL,
	[PlayerName] [varchar](32) NOT NULL,
	[BetAmount] [money] NOT NULL,
	[ValidBet] [money] NOT NULL,
	[Money] [money] NOT NULL,
 CONSTRAINT [PK_GameDailyReportStatisticByGameRules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameDic]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameDic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[keys] [nvarchar](32) NOT NULL,
	[vals] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_GameDic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameInfo]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](18) NOT NULL,
	[CategoryType] [tinyint] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[TypeStr] [nvarchar](18) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[DefaultRate] [money] NOT NULL,
	[IsTransferWallet] [bit] NOT NULL,
	[ApiTimeZone] [int] NOT NULL,
	[Config] [varchar](1024) NOT NULL,
 CONSTRAINT [PK_GameInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameLogs]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[GameCategory] [tinyint] NOT NULL,
	[GameTypeStr] [varchar](18) NOT NULL,
	[MemberId] [int] NOT NULL,
	[PlayerName] [varchar](32) NOT NULL,
	[SourceId] [varchar](64) NOT NULL,
	[BetAmount] [money] NOT NULL,
	[ValidBet] [money] NOT NULL,
	[Money] [money] NOT NULL,
	[AwardAmount] [money] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[SourceOrderCreateTime] [datetime2](0) NOT NULL,
	[SourceOrderAwardTime] [datetime2](0) NOT NULL,
	[OrderCreateTimeUtc8] [datetime2](0) NOT NULL,
	[OrderAwardTimeUtc8] [datetime2](0) NOT NULL,
	[CreateTimeUtc8] [datetime2](0) NULL,
	[SettlementTimeUtc8] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_GameLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameLogsChess]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameLogsChess](
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_GameLogsChess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameLogsEsport]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameLogsEsport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GameCategory] [tinyint] NOT NULL,
	[GameTypeStr] [varchar](18) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[PlayerName] [varchar](32) NOT NULL,
	[SourceId] [varchar](32) NOT NULL,
	[IsSeries] [bit] NOT NULL,
	[SeriesInfo] [varchar](max) NOT NULL,
	[Event] [nvarchar](16) NOT NULL,
	[LeagueName] [nvarchar](128) NOT NULL,
	[MatchName] [nvarchar](128) NOT NULL,
	[BetItem] [nvarchar](50) NOT NULL,
	[BetContent] [nvarchar](50) NOT NULL,
	[Results] [nvarchar](50) NOT NULL,
	[BetAmount] [money] NOT NULL,
	[ValidBet] [money] NOT NULL,
	[Money] [money] NOT NULL,
	[AwardAmount] [money] NOT NULL,
	[SourceOrderCreateTime] [datetime2](0) NOT NULL,
	[SourceOrderAwardTime] [datetime2](0) NOT NULL,
	[OrderCreateTimeUtc8] [datetime2](0) NOT NULL,
	[OrderAwardTimeUtc8] [datetime2](0) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[Stage] [tinyint] NOT NULL,
	[SourceOdds] [money] NOT NULL,
	[SourceOddsType] [tinyint] NOT NULL,
	[DEOdds] [money] NOT NULL,
	[CreateTime] [datetime2](0) NOT NULL,
	[UpdateTime] [datetime2](0) NOT NULL,
	[SettlementTime] [datetime2](0) NOT NULL,
	[Raw] [varchar](max) NOT NULL,
 CONSTRAINT [PK_GameLogsEsport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameLogsFinance]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameLogsFinance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GameCategory] [tinyint] NOT NULL,
	[GameTypeStr] [varchar](18) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[PlayerName] [varchar](32) NOT NULL,
	[SourceId] [varchar](32) NOT NULL,
	[MatchName] [nvarchar](128) NOT NULL,
	[BetItem] [nvarchar](50) NOT NULL,
	[BetContent] [nvarchar](50) NOT NULL,
	[Results] [nvarchar](50) NOT NULL,
	[BetAmount] [money] NOT NULL,
	[ValidBet] [money] NOT NULL,
	[Money] [money] NOT NULL,
	[AwardAmount] [money] NOT NULL,
	[SourceOrderCreateTime] [datetime2](0) NOT NULL,
	[SourceOrderAwardTime] [datetime2](0) NOT NULL,
	[OrderCreateTimeUtc8] [datetime2](0) NOT NULL,
	[OrderAwardTimeUtc8] [datetime2](0) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[Stage] [tinyint] NOT NULL,
	[SourceOdds] [money] NOT NULL,
	[SourceOddsType] [tinyint] NOT NULL,
	[DEOdds] [money] NOT NULL,
	[CreateTime] [datetime2](0) NOT NULL,
	[UpdateTime] [datetime2](0) NOT NULL,
	[SettlementTime] [datetime2](0) NOT NULL,
	[Raw] [varchar](max) NOT NULL,
 CONSTRAINT [PK_GameLogsFinance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameLogsHunt]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameLogsHunt](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[GameTypeStr] [varchar](18) NOT NULL,
	[UserId] [int] NOT NULL,
	[PlayerName] [varchar](32) NOT NULL,
	[SourceId] [varchar](32) NOT NULL,
	[BetAmount] [money] NOT NULL,
	[ValidBet] [money] NOT NULL,
	[Money] [money] NOT NULL,
	[AwardAmount] [money] NOT NULL,
	[SourceOrderCreateTime] [datetime2](0) NOT NULL,
	[SourceOrderAwardTime] [datetime2](0) NOT NULL,
	[OrderCreateTimeUtc8] [datetime2](0) NOT NULL,
	[OrderAwardTimeUtc8] [datetime2](0) NOT NULL,
	[CreateTimeUtc8] [datetime2](0) NOT NULL,
	[SettlementTimeUtc8] [datetime2](0) NOT NULL,
	[Device] [varchar](18) NOT NULL,
	[Ip] [varchar](20) NOT NULL,
	[Raw] [varchar](max) NOT NULL,
	[RoomName] [nvarchar](32) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_GameLogsHunt] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameLogsLive]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameLogsLive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[GameTypeStr] [varchar](18) NOT NULL,
	[UserId] [int] NOT NULL,
	[PlayerName] [varchar](32) NOT NULL,
	[SourceId] [varchar](32) NOT NULL,
	[BetAmount] [money] NOT NULL,
	[ValidBet] [money] NOT NULL,
	[Money] [money] NOT NULL,
	[AwardAmount] [money] NOT NULL,
	[SourceOrderCreateTime] [datetime2](0) NOT NULL,
	[SourceOrderAwardTime] [datetime2](0) NOT NULL,
	[OrderCreateTimeUtc8] [datetime2](0) NOT NULL,
	[OrderAwardTimeUtc8] [datetime2](0) NOT NULL,
	[CreateTimeUtc8] [datetime2](0) NOT NULL,
	[SettlementTimeUtc8] [datetime2](0) NOT NULL,
	[Device] [varchar](18) NOT NULL,
	[Ip] [varchar](20) NOT NULL,
	[Status] [bit] NOT NULL,
	[Raw] [varchar](max) NOT NULL,
	[CreditBefore] [money] NOT NULL,
	[CreditAfter] [money] NOT NULL,
	[GameName] [nvarchar](12) NOT NULL,
	[GameCode] [nvarchar](32) NOT NULL,
	[Bet] [nvarchar](12) NOT NULL,
	[Results] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_GameLogsLive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameLogsLottery]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameLogsLottery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_GameLogsLottery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GamelogsMd5Cache]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GamelogsMd5Cache](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GameTypeStr] [varchar](18) NOT NULL,
	[SourceId] [varchar](32) NOT NULL,
	[RawMd5] [varchar](32) NOT NULL,
 CONSTRAINT [PK_GamelogsMd5Cache] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameLogsSlot]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameLogsSlot](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[GameTypeStr] [varchar](18) NOT NULL,
	[UserId] [int] NOT NULL,
	[PlayerName] [varchar](32) NOT NULL,
	[SourceId] [varchar](32) NOT NULL,
	[BetAmount] [money] NOT NULL,
	[ValidBet] [money] NOT NULL,
	[Money] [money] NOT NULL,
	[AwardAmount] [money] NOT NULL,
	[SourceOrderCreateTime] [datetime2](0) NOT NULL,
	[SourceOrderAwardTime] [datetime2](0) NOT NULL,
	[OrderCreateTimeUtc8] [datetime2](0) NOT NULL,
	[OrderAwardTimeUtc8] [datetime2](0) NOT NULL,
	[CreateTimeUtc8] [datetime2](0) NOT NULL,
	[SettlementTimeUtc8] [datetime2](0) NOT NULL,
	[SlotName] [nvarchar](50) NOT NULL,
	[Device] [varchar](18) NOT NULL,
	[Ip] [varchar](20) NOT NULL,
	[Status] [bit] NOT NULL,
	[Raw] [varchar](max) NOT NULL,
	[CreditBefore] [money] NOT NULL,
	[CreditAfter] [money] NOT NULL,
 CONSTRAINT [PK_GameLogsSlot] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameLogsSport]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameLogsSport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GameCategory] [int] NOT NULL,
	[GameTypeStr] [varchar](18) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[PlayerName] [varchar](32) NOT NULL,
	[SourceId] [varchar](32) NOT NULL,
	[IsSeries] [bit] NOT NULL,
	[SeriesInfo] [nvarchar](max) NOT NULL,
	[Event] [nvarchar](16) NULL,
	[LeagueName] [varchar](128) NOT NULL,
	[MatchName] [varchar](128) NOT NULL,
	[BetItem] [varchar](128) NOT NULL,
	[BetContent] [varchar](32) NOT NULL,
	[Results] [varchar](32) NOT NULL,
	[BetAmount] [money] NOT NULL,
	[ValidBet] [money] NOT NULL,
	[Money] [money] NOT NULL,
	[AwardAmount] [money] NOT NULL,
	[SourceOrderCreateTime] [datetime2](0) NOT NULL,
	[SourceOrderAwardTime] [datetime2](0) NOT NULL,
	[OrderCreateTimeUtc8] [datetime2](0) NOT NULL,
	[OrderAwardTimeUtc8] [datetime2](0) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[Stage] [tinyint] NOT NULL,
	[SourceOdds] [money] NOT NULL,
	[SourceOddsType] [tinyint] NOT NULL,
	[DEOdds] [money] NOT NULL,
	[CreateTime] [datetime2](0) NOT NULL,
	[UpdateTime] [datetime2](0) NOT NULL,
	[SettlementTime] [datetime2](0) NULL,
	[Raw] [varchar](max) NOT NULL,
 CONSTRAINT [PK_GameLogsSport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameMerchant]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameMerchant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[TypeStr] [nvarchar](18) NOT NULL,
	[TypeDesc] [nvarchar](12) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Config] [varchar](256) NOT NULL,
	[Rate] [money] NOT NULL,
	[SysEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_GameMerchant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameTransferLogs]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameTransferLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[TypeStr] [varchar](18) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Money] [money] NOT NULL,
	[TransType] [tinyint] NOT NULL,
	[Raw] [nvarchar](1024) NOT NULL,
 CONSTRAINT [PK_GameTransferLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameUsers]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeStr] [varchar](18) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[PlayerName] [varchar](32) NOT NULL,
	[PlayerPsw] [varchar](32) NOT NULL,
	[Balance] [money] NOT NULL,
	[IsLock] [bit] NOT NULL,
	[IsTest] [bit] NOT NULL,
	[RegisterTime] [datetime] NOT NULL,
	[LastLoginTime] [datetime] NOT NULL,
 CONSTRAINT [PK_GameUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameUsersDailyReportStatistic]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameUsersDailyReportStatistic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[GameCategory] [tinyint] NOT NULL,
	[GameTypeStr] [varchar](18) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[PlayerName] [varchar](32) NOT NULL,
	[BetAmount] [money] NOT NULL,
	[ValidBet] [money] NOT NULL,
	[Money] [money] NOT NULL,
	[BetOrderCount] [int] NOT NULL,
	[SettlementOrderCount] [int] NOT NULL,
 CONSTRAINT [PK_GameUsersDailyReportStatistic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameUsersDailyReportStatisticByGameRules]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameUsersDailyReportStatisticByGameRules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[GameTypeStr] [varchar](18) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[PlayerName] [varchar](32) NOT NULL,
	[BetAmount] [money] NOT NULL,
	[ValidBet] [money] NOT NULL,
	[Money] [money] NOT NULL,
 CONSTRAINT [PK_GameUsersDailyReportStatisticByGameRules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HelpArea]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HelpArea](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeId] [smallint] NOT NULL,
	[MerchantId] [int] NOT NULL,
	[Title] [nvarchar](24) NOT NULL,
	[Tcontent] [nvarchar](max) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ShowIndexPage] [bit] NOT NULL,
	[Alias] [varchar](255) NOT NULL,
	[SortNo] [int] NOT NULL,
 CONSTRAINT [PK_HelpArea] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HelpAreaType]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HelpAreaType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[Title] [varchar](12) NOT NULL,
	[IsHref] [bit] NOT NULL,
	[IsOpen] [bit] NOT NULL,
	[Href] [varchar](128) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IconImg] [varchar](255) NOT NULL,
	[SortNo] [int] NOT NULL,
 CONSTRAINT [PK_HelpAreaType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Merchant]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Merchant](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](12) NOT NULL,
	[GameCredit] [money] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[PageSectionConfig] [varchar](1024) NOT NULL,
	[Domains] [varchar](128) NOT NULL,
	[IpWhitelist] [varchar](1024) NOT NULL,
	[VipsConfig] [varchar](1024) NOT NULL,
	[PcTempletStr] [varchar](32) NOT NULL,
	[H5TempletStr] [varchar](32) NOT NULL,
	[SignupConfig] [varchar](1024) NOT NULL,
	[CustomerConfig] [nvarchar](2048) NOT NULL,
 CONSTRAINT [PK_Merchant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoticeArea]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoticeArea](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[Type] [smallint] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[CreateTime] [datetime2](0) NOT NULL,
	[GroupId] [varchar](64) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[HomeDisplay] [bit] NOT NULL,
	[IsBrowsed] [bit] NOT NULL,
 CONSTRAINT [PK_Notice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoticeInfo]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoticeInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[NoticeAreaId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[Title] [nvarchar](32) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IsRead] [bit] NOT NULL,
	[CreateTime] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_NoticeInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayCallbackLog]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayCallbackLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[SysOrderId] [varchar](32) NOT NULL,
	[PlatformOrderId] [varchar](32) NOT NULL,
	[Contents] [int] NOT NULL,
	[CreateTme] [datetime] NOT NULL,
 CONSTRAINT [PK_PayCallbackLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayCategory]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayCategory](
	[Id] [int] IDENTITY(100,1) NOT NULL,
	[PayType] [varchar](32) NOT NULL,
	[Name] [nvarchar](12) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[ConfigStr] [nvarchar](2048) NOT NULL,
	[IpWhiteList] [varchar](128) NOT NULL,
 CONSTRAINT [PK_PayCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayMerchant]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayMerchant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[PayCategoryId] [int] NOT NULL,
	[PayTypeId] [int] NOT NULL,
	[PayCategory] [varchar](32) NOT NULL,
	[Name] [nvarchar](12) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[ConfigStr] [nvarchar](1024) NOT NULL,
	[ValidationStr] [nvarchar](1024) NOT NULL,
	[Img] [varchar](255) NULL,
	[Desc] [varchar](255) NULL,
 CONSTRAINT [PK_PayMerchant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayOrder]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[PayMerchantId] [int] NOT NULL,
	[ReqDepositAmount] [money] NOT NULL,
	[DepositAmount] [money] NOT NULL,
	[IsFinish] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ConfirmTime] [datetime] NOT NULL,
	[IP] [varchar](20) NOT NULL,
	[PayMerchantOrderId] [varchar](32) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_PayOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRequestLog]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRequestLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[SysOrderId] [varchar](32) NOT NULL,
	[PlatformOrderId] [varchar](32) NOT NULL,
	[Contents] [int] NOT NULL,
	[CreateTme] [datetime] NOT NULL,
 CONSTRAINT [PK_PayRequestLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayTypeCategory]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayTypeCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[Name] [nvarchar](12) NOT NULL,
	[PicUrl] [varchar](128) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[IsVirtualCurrency] [bit] NOT NULL,
 CONSTRAINT [PK_PayTypeCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PromotionsConfig]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PromotionsConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TagId] [int] NOT NULL,
	[MerchantId] [int] NOT NULL,
	[AType] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Config] [varchar](max) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Cover] [varchar](64) NOT NULL,
	[Visible] [bit] NOT NULL,
	[H5Cover] [varchar](64) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[HomeCover] [varchar](128) NOT NULL,
	[HomeDisplay] [bit] NOT NULL,
	[PcCover] [varchar](255) NOT NULL,
	[IndexPageCover] [varchar](128) NOT NULL,
	[SortNo] [int] NOT NULL,
 CONSTRAINT [PK_PromotionsConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PromotionsTag]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PromotionsTag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Sort] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_PromotionsType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RebatesConfig]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RebatesConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](16) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[GoupId] [int] NOT NULL,
	[Configs] [varchar](1024) NOT NULL,
	[IsManual] [bit] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Rebates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RebatesOrder]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RebatesOrder](
	[Id] [int] NOT NULL,
	[ConfigId] [int] NOT NULL,
	[MerchantId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[Amount] [money] NOT NULL,
	[SourceId] [varchar](8) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_RebatesOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RebatesOrderPlan]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RebatesOrderPlan](
	[Id] [int] NOT NULL,
	[MerchantId] [int] NOT NULL,
	[Title] [nvarchar](32) NOT NULL,
	[StartTime] [date] NOT NULL,
	[EndTime] [date] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreatorId] [int] NOT NULL,
 CONSTRAINT [PK_RebatesOrderPlan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SectionDetail]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SectionDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SectionId] [int] NOT NULL,
	[MerchantId] [int] NOT NULL,
	[Name] [nvarchar](18) NOT NULL,
	[Alias] [nvarchar](18) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[PcImgUrl] [varchar](128) NOT NULL,
	[H5ImgUrl] [varchar](128) NOT NULL,
	[PageUrl] [varchar](128) NOT NULL,
	[HasSubMenu] [bit] NOT NULL,
	[SKey] [varchar](32) NOT NULL,
	[Tcontent] [text] NOT NULL,
 CONSTRAINT [PK_SiteMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SectionKey]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SectionKey](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[DetailType] [tinyint] NOT NULL,
	[SKey] [varchar](32) NOT NULL,
	[Description] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_SectionKey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SignInLog]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SignInLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[CreateTime] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_SignInLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TempletGame]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempletGame](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GameStr] [varchar](50) NOT NULL,
	[TempletStr] [varchar](30) NOT NULL,
	[GameCate] [varchar](18) NOT NULL,
	[H5ImgStr] [varchar](225) NOT NULL,
	[PcImgStr] [varchar](225) NOT NULL,
 CONSTRAINT [PK_TempletGame] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TempletKey]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempletKey](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[DetailType] [tinyint] NOT NULL,
	[SKey] [varchar](32) NOT NULL,
	[Description] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_TempletKey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserHierarchy]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserHierarchy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[SubId] [int] NOT NULL,
	[Level] [int] NOT NULL,
 CONSTRAINT [PK_UserHierarchy] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(10000,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[AccountName] [varchar](24) NOT NULL,
	[IdName] [nvarchar](24) NOT NULL,
	[Pasw] [varchar](64) NOT NULL,
	[FPasw] [varchar](64) NOT NULL,
	[AgentId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Mobile] [varchar](15) NOT NULL,
	[BirthDate] [datetime2](0) NOT NULL,
	[AgentSetting] [varchar](1024) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Email] [varchar](64) NOT NULL,
	[Gender] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersBank]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersBank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[AccountName] [nvarchar](24) NOT NULL,
	[AccountNo] [varchar](24) NOT NULL,
	[BankName] [nvarchar](32) NULL,
	[CreateTime] [datetime] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_UsersBank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersFunds]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersFunds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[TotalFunds] [money] NOT NULL,
	[LockFunds] [money] NOT NULL,
	[TotalRechargedFunds] [money] NOT NULL,
	[TotalRechargedFundsCount] [int] NOT NULL,
	[TotalWithdrawalFunds] [money] NOT NULL,
	[TotalWithdrawalCount] [int] NOT NULL,
	[TotalBetFunds] [money] NOT NULL,
	[TotalProfitAndLoss] [money] NOT NULL,
	[PromotionsFunds] [money] NOT NULL,
	[OtherFunds] [money] NOT NULL,
 CONSTRAINT [PK_UsersFunds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersFundsLog]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersFundsLog](
	[Id] [int] IDENTITY(10000,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[Amount] [money] NOT NULL,
	[LockedAmount] [money] NOT NULL,
	[Balance] [money] NOT NULL,
	[FundsType] [tinyint] NOT NULL,
	[SubFundsType] [tinyint] NOT NULL,
	[TransType] [tinyint] NOT NULL,
	[SourceId] [varchar](64) NOT NULL,
	[Marks] [nvarchar](64) NULL,
	[IP] [varchar](20) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UsersMoneyLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersLoginLog]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersLoginLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[LoginTime] [smalldatetime] NOT NULL,
	[IP] [varchar](20) NOT NULL,
	[IPCn] [nvarchar](64) NOT NULL,
	[IsLastLogin] [bit] NOT NULL,
	[Sucess] [bit] NOT NULL,
	[RawData] [varchar](1024) NOT NULL,
 CONSTRAINT [PK_UsersLoginLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersSession]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersSession](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Session] [varchar](32) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UsersSession] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersSignInLog]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersSignInLog](
	[Id] [int] NOT NULL,
	[MerchantId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsAssist] [bit] NOT NULL,
 CONSTRAINT [PK_UsersSignInLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VipGroups]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VipGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[GroupName] [nvarchar](12) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[PaySetting] [varchar](1024) NOT NULL,
	[WithdrawalSetting] [varchar](1024) NOT NULL,
	[GroupSetting] [varchar](1024) NOT NULL,
	[PointSetting] [varchar](1024) NOT NULL,
	[Description] [nvarchar](64) NOT NULL,
	[SortNo] [tinyint] NOT NULL,
	[CreateTime] [datetime2](0) NOT NULL,
	[Img] [varchar](255) NOT NULL,
 CONSTRAINT [PK_VipGoups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WashOrder]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WashOrder](
	[Id] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[FundsType] [varchar](12) NOT NULL,
	[Amount] [money] NOT NULL,
	[WashAmount] [money] NOT NULL,
	[Mark] [nvarchar](32) NOT NULL,
	[Ended] [bit] NOT NULL,
	[CreateTime] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_WashOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WashOrderDetail]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WashOrderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[Amount] [money] NOT NULL,
	[Balance] [money] NOT NULL,
	[Mark] [nvarchar](18) NOT NULL,
	[SourceOrderId] [varchar](32) NOT NULL,
	[CreateTime] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_WashOrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WithdrawMerchant]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WithdrawMerchant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[Name] [nvarchar](16) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[ConfigStr] [nvarchar](1024) NOT NULL,
	[Description] [nvarchar](64) NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[TypeStr] [varchar](32) NOT NULL,
	[CreateTime] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_WithdrawMerchant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WithdrawOrder]    Script Date: 2024/5/30 13:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WithdrawOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[UserBankId] [int] NOT NULL,
	[WithdrawMerchantId] [int] NOT NULL,
	[ReqWithdrawAmount] [money] NOT NULL,
	[WithdrawAmount] [money] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[IsFinish] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ConfirmTime] [datetime] NOT NULL,
	[IP] [varchar](20) NOT NULL,
	[WithdrawMerchantOrderId] [varchar](32) NOT NULL,
	[Marks] [varchar](50) NOT NULL,
 CONSTRAINT [PK_WithdrawalOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Domains] ON 
GO
INSERT [dbo].[Domains] ([Id], [MerchantId], [DoType], [Name], [Enabled], [IsHttps], [Marks]) VALUES (1, 1000, 0, N'merchant.yplatform.com', 1, 0, N'""')
GO
INSERT [dbo].[Domains] ([Id], [MerchantId], [DoType], [Name], [Enabled], [IsHttps], [Marks]) VALUES (2, 0, 0, N'sys.yplatform.com', 1, 0, N'""')
GO
SET IDENTITY_INSERT [dbo].[Domains] OFF
GO
SET IDENTITY_INSERT [dbo].[Merchant] ON 
GO
INSERT [dbo].[Merchant] ([Id], [Name], [GameCredit], [Status], [CreateDate], [PageSectionConfig], [Domains], [IpWhitelist], [VipsConfig], [PcTempletStr], [H5TempletStr], [SignupConfig], [CustomerConfig]) VALUES (1000, N'悟空', 0.0000, 1, CAST(N'2023-12-10T16:44:18.303' AS DateTime), N'', N'merchant.yplatform.com', N'', N'', N'One', N'One', N'{"EnableValidCode":false,"EnableCode":false,"EnableFPasw":false,"EnablePhone":false,"EnableIdName":false}', N'{"PcLogo":null,"H5Logo":null,"ServiceLink":null,"DownloadQRCode":null,"H5SiteUrl":null,"EnabledH5Visit":false,"EnabledAgentPattern":false}')
GO
SET IDENTITY_INSERT [dbo].[Merchant] OFF
GO
SET IDENTITY_INSERT [dbo].[PayTypeCategory] ON 
GO
INSERT [dbo].[PayTypeCategory] ([Id], [MerchantId], [Name], [PicUrl], [Enabled], [IsVirtualCurrency]) VALUES (1, 1000, N'未定义', N'', 0, 0)
GO
INSERT [dbo].[PayTypeCategory] ([Id], [MerchantId], [Name], [PicUrl], [Enabled], [IsVirtualCurrency]) VALUES (2, 1000, N'支付宝', N'', 0, 0)
GO
INSERT [dbo].[PayTypeCategory] ([Id], [MerchantId], [Name], [PicUrl], [Enabled], [IsVirtualCurrency]) VALUES (3, 1000, N'微信', N'', 0, 0)
GO
INSERT [dbo].[PayTypeCategory] ([Id], [MerchantId], [Name], [PicUrl], [Enabled], [IsVirtualCurrency]) VALUES (4, 1000, N'银行', N'', 0, 0)
GO
INSERT [dbo].[PayTypeCategory] ([Id], [MerchantId], [Name], [PicUrl], [Enabled], [IsVirtualCurrency]) VALUES (5, 1000, N'云闪付', N'', 0, 0)
GO
SET IDENTITY_INSERT [dbo].[PayTypeCategory] OFF
GO
ALTER TABLE [dbo].[Domains] ADD  CONSTRAINT [DF_Domians_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [dbo].[GameLogs] ADD  CONSTRAINT [DF_GameLogs_SettlementTimeUtc8]  DEFAULT ('1900-01-01') FOR [SettlementTimeUtc8]
GO
ALTER TABLE [dbo].[GameLogsEsport] ADD  CONSTRAINT [DF_GameLogsEsport_Stage]  DEFAULT ((1)) FOR [Stage]
GO
ALTER TABLE [dbo].[GameLogsFinance] ADD  CONSTRAINT [DF_GameLogsFinance_Stage]  DEFAULT ((1)) FOR [Stage]
GO
ALTER TABLE [dbo].[GameLogsHunt] ADD  CONSTRAINT [DF_GameLogsHunt_SettlementTimeUtc8]  DEFAULT ('1900-01-01') FOR [SettlementTimeUtc8]
GO
ALTER TABLE [dbo].[GameLogsLive] ADD  CONSTRAINT [DF_GameLogsLive_SettlementTimeUtc8]  DEFAULT ('1900-01-01') FOR [SettlementTimeUtc8]
GO
ALTER TABLE [dbo].[GameLogsSlot] ADD  CONSTRAINT [DF_GameLogsSlot_SettlementTimeUtc8]  DEFAULT ('1900-01-01') FOR [SettlementTimeUtc8]
GO
ALTER TABLE [dbo].[GameLogsSport] ADD  CONSTRAINT [DF_GameLogsSport_SeriesInfo]  DEFAULT ('') FOR [SeriesInfo]
GO
ALTER TABLE [dbo].[GameLogsSport] ADD  CONSTRAINT [DF_GameLogsSport_Event]  DEFAULT ('') FOR [Event]
GO
ALTER TABLE [dbo].[GameLogsSport] ADD  CONSTRAINT [DF_GameLogsSport_Stage]  DEFAULT ((1)) FOR [Stage]
GO
ALTER TABLE [dbo].[GameMerchant] ADD  CONSTRAINT [DF_GameMerchant_SysDisabled]  DEFAULT ((0)) FOR [SysEnabled]
GO
ALTER TABLE [dbo].[Merchant] ADD  CONSTRAINT [DF_Merchant_CustomerConfig]  DEFAULT ('') FOR [CustomerConfig]
GO
ALTER TABLE [dbo].[NoticeArea] ADD  CONSTRAINT [DF_NoticeArea_HomeDisplay]  DEFAULT ((0)) FOR [HomeDisplay]
GO
ALTER TABLE [dbo].[PayMerchant] ADD  CONSTRAINT [DF_PayMerchant_PayTypeId]  DEFAULT ((0)) FOR [PayTypeId]
GO
ALTER TABLE [dbo].[PayOrder] ADD  CONSTRAINT [DF_PayOrder_PayMerchantOrderId]  DEFAULT ((0)) FOR [PayMerchantOrderId]
GO
ALTER TABLE [dbo].[PayTypeCategory] ADD  CONSTRAINT [DF_PayTypeCategory_Name]  DEFAULT (NULL) FOR [Name]
GO
ALTER TABLE [dbo].[PayTypeCategory] ADD  CONSTRAINT [DF_PayTypeCategory_Enabled]  DEFAULT ((0)) FOR [Enabled]
GO
ALTER TABLE [dbo].[PromotionsConfig] ADD  CONSTRAINT [DF_PromotionsConfig_Visible]  DEFAULT ((0)) FOR [Visible]
GO
ALTER TABLE [dbo].[PromotionsConfig] ADD  CONSTRAINT [DF_PromotionsConfig_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[PromotionsConfig] ADD  CONSTRAINT [DF_PromotionsConfig_HomeCover]  DEFAULT ('') FOR [HomeCover]
GO
ALTER TABLE [dbo].[PromotionsConfig] ADD  CONSTRAINT [DF_PromotionsConfig_HomeDisplay]  DEFAULT ((0)) FOR [HomeDisplay]
GO
ALTER TABLE [dbo].[SectionDetail] ADD  CONSTRAINT [DF_SectionDetail_ImgUrl]  DEFAULT ('') FOR [PcImgUrl]
GO
ALTER TABLE [dbo].[SectionDetail] ADD  CONSTRAINT [DF_SectionDetail_PageUrl]  DEFAULT ('') FOR [PageUrl]
GO
ALTER TABLE [dbo].[SectionDetail] ADD  CONSTRAINT [DF_SectionDetail_HasSubMenu]  DEFAULT ((0)) FOR [HasSubMenu]
GO
ALTER TABLE [dbo].[SectionDetail] ADD  CONSTRAINT [DF_SectionDetail_SKey]  DEFAULT ('') FOR [SKey]
GO
ALTER TABLE [dbo].[SectionKey] ADD  CONSTRAINT [DF_SectionKey_MerchantId]  DEFAULT ((0)) FOR [MerchantId]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IdName]  DEFAULT ('') FOR [IdName]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_BirthDate]  DEFAULT ('1900-01-01') FOR [BirthDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_AgentSetting]  DEFAULT ('') FOR [AgentSetting]
GO
ALTER TABLE [dbo].[WithdrawOrder] ADD  CONSTRAINT [DF_WithdrawOrder_WithdrawMerchantOrderId]  DEFAULT ((0)) FOR [WithdrawMerchantOrderId]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivityOrders', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivityOrders', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠活动Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivityOrders', @level2type=N'COLUMN',@level2name=N'PromotionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivityOrders', @level2type=N'COLUMN',@level2name=N'AType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'派奖金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivityOrders', @level2type=N'COLUMN',@level2name=N'Reward'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态. 成功、失败' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivityOrders', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动Ip' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivityOrders', @level2type=N'COLUMN',@level2name=N'Ip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivityOrders', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivityOrders', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'派奖时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivityOrders', @level2type=N'COLUMN',@level2name=N'RewardTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'源Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivityOrders', @level2type=N'COLUMN',@level2name=N'SourceId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivityStatistic', @level2type=N'COLUMN',@level2name=N'AType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'Pay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付订单笔数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'PayNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出款金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'Withdrawals'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'取款订单笔数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'WithdrawalsNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'GameBetAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效投注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'GameValidBet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏盈亏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'GameMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'PromoMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Domains', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'域名类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Domains', @level2type=N'COLUMN',@level2name=N'DoType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'域名名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Domains', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Domains', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否https' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Domains', @level2type=N'COLUMN',@level2name=N'IsHttps'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Domains', @level2type=N'COLUMN',@level2name=N'Marks'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameApiTimestamps', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'保存到秒的时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameApiTimestamps', @level2type=N'COLUMN',@level2name=N'Timestamps'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameApiTimestamps', @level2type=N'COLUMN',@level2name=N'Mark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'Date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'玩家名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'PlayerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'BetAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效投注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'ValidBet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'盈亏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'Date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'玩家名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'PlayerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'BetAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效投注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'ValidBet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'盈亏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameDic'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'禁用, 启用, 维护' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameInfo', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏类型  真人 体育  ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameInfo', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏状态  开启  维护  删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameInfo', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认费率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameInfo', @level2type=N'COLUMN',@level2name=N'DefaultRate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是免转钱包 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameInfo', @level2type=N'COLUMN',@level2name=N'IsTransferWallet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'API日志时区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameInfo', @level2type=N'COLUMN',@level2name=N'ApiTimeZone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏登录/API基础参数配置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameInfo', @level2type=N'COLUMN',@level2name=N'Config'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogs', @level2type=N'COLUMN',@level2name=N'BetAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效投注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogs', @level2type=N'COLUMN',@level2name=N'ValidBet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'盈亏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogs', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogs', @level2type=N'COLUMN',@level2name=N'SourceOrderCreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单派奖时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogs', @level2type=N'COLUMN',@level2name=N'SourceOrderAwardTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'GameCategory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'GameTypeStr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'PlayerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'SourceId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件 篮球 足球等类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'Event'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注比赛ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'MatchName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注项' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'BetItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'BetContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开奖结果' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'Results'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'BetAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效投注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'ValidBet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'盈亏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'派奖金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'AwardAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'SourceOrderCreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单派奖时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'SourceOrderAwardTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.早盘 2.滚球 3.其它' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'Stage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'欧赔' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'DEOdds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单在平台创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'包网平台结算时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsEsport', @level2type=N'COLUMN',@level2name=N'SettlementTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'GameCategory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'GameTypeStr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'PlayerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'SourceId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注比赛ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'MatchName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注项' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'BetItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'BetContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开奖结果' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'Results'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'BetAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效投注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'ValidBet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'盈亏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'派奖金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'AwardAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'SourceOrderCreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单派奖时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'SourceOrderAwardTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.早盘 2.滚球 3.其它' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'Stage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'欧赔' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'DEOdds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单在平台创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'包网平台结算时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsFinance', @level2type=N'COLUMN',@level2name=N'SettlementTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsHunt', @level2type=N'COLUMN',@level2name=N'SourceOrderCreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单派奖时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsHunt', @level2type=N'COLUMN',@level2name=N'SourceOrderAwardTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注设备' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsHunt', @level2type=N'COLUMN',@level2name=N'Device'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsHunt', @level2type=N'COLUMN',@level2name=N'Ip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'房间号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsHunt', @level2type=N'COLUMN',@level2name=N'RoomName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsLive', @level2type=N'COLUMN',@level2name=N'SourceOrderCreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单派奖时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsLive', @level2type=N'COLUMN',@level2name=N'SourceOrderAwardTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注设备' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsLive', @level2type=N'COLUMN',@level2name=N'Device'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsLive', @level2type=N'COLUMN',@level2name=N'Ip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算前额度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsLive', @level2type=N'COLUMN',@level2name=N'CreditBefore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算后额度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsLive', @level2type=N'COLUMN',@level2name=N'CreditAfter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'局号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsLive', @level2type=N'COLUMN',@level2name=N'GameCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSlot', @level2type=N'COLUMN',@level2name=N'SourceOrderCreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单派奖时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSlot', @level2type=N'COLUMN',@level2name=N'SourceOrderAwardTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注设备' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSlot', @level2type=N'COLUMN',@level2name=N'Device'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSlot', @level2type=N'COLUMN',@level2name=N'Ip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算前额度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSlot', @level2type=N'COLUMN',@level2name=N'CreditBefore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算后额度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSlot', @level2type=N'COLUMN',@level2name=N'CreditAfter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否串关' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSport', @level2type=N'COLUMN',@level2name=N'IsSeries'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下注金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSport', @level2type=N'COLUMN',@level2name=N'BetAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效投注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSport', @level2type=N'COLUMN',@level2name=N'ValidBet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'奖金' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSport', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'派奖金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSport', @level2type=N'COLUMN',@level2name=N'AwardAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSport', @level2type=N'COLUMN',@level2name=N'SourceOrderCreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单派奖时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSport', @level2type=N'COLUMN',@level2name=N'SourceOrderAwardTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSport', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.早盘 2.滚球 3.其它' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSport', @level2type=N'COLUMN',@level2name=N'Stage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'欧赔' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSport', @level2type=N'COLUMN',@level2name=N'DEOdds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单在平台创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSport', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameLogsSport', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameMerchant', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameMerchant', @level2type=N'COLUMN',@level2name=N'TypeDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用：true(1)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameMerchant', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'链接字符串配置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameMerchant', @level2type=N'COLUMN',@level2name=N'Config'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'费率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameMerchant', @level2type=N'COLUMN',@level2name=N'Rate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameTransferLogs', @level2type=N'COLUMN',@level2name=N'TypeStr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转账状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameTransferLogs', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转账时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameTransferLogs', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转账金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameTransferLogs', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转入/转出' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameTransferLogs', @level2type=N'COLUMN',@level2name=N'TransType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转账数据' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameTransferLogs', @level2type=N'COLUMN',@level2name=N'Raw'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏转账日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameTransferLogs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsers', @level2type=N'COLUMN',@level2name=N'TypeStr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员游戏名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsers', @level2type=N'COLUMN',@level2name=N'PlayerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员游戏密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsers', @level2type=N'COLUMN',@level2name=N'PlayerPsw'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏余额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsers', @level2type=N'COLUMN',@level2name=N'Balance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否锁定禁止登录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsers', @level2type=N'COLUMN',@level2name=N'IsLock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否测试账户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsers', @level2type=N'COLUMN',@level2name=N'IsTest'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注册时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsers', @level2type=N'COLUMN',@level2name=N'RegisterTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsers', @level2type=N'COLUMN',@level2name=N'LastLoginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户游戏信息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'Date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'GameTypeStr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'MemberId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'玩家名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'PlayerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'BetAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效投注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'ValidBet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'盈亏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatistic', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'Date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'GameTypeStr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'MemberId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'玩家名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'PlayerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'投注金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'BetAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效投注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'ValidBet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'盈亏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameUsersDailyReportStatisticByGameRules', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpArea', @level2type=N'COLUMN',@level2name=N'TypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpArea', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpArea', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpArea', @level2type=N'COLUMN',@level2name=N'Tcontent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpArea', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpAreaType', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpAreaType', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否跳转链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpAreaType', @level2type=N'COLUMN',@level2name=N'IsHref'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否开启' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpAreaType', @level2type=N'COLUMN',@level2name=N'IsOpen'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跳转链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpAreaType', @level2type=N'COLUMN',@level2name=N'Href'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpAreaType', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Merchant', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏分数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Merchant', @level2type=N'COLUMN',@level2name=N'GameCredit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Merchant', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Merchant', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登陆域名设置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Merchant', @level2type=N'COLUMN',@level2name=N'Domains'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台加白IP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Merchant', @level2type=N'COLUMN',@level2name=N'IpWhitelist'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeArea', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型 0:通知 1公告' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeArea', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeArea', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeArea', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeArea', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeArea', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeArea', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeInfo', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeInfo', @level2type=N'COLUMN',@level2name=N'MemberId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeInfo', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeInfo', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否已读取' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeInfo', @level2type=N'COLUMN',@level2name=N'IsRead'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeInfo', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台订单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayCallbackLog', @level2type=N'COLUMN',@level2name=N'SysOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'第三方平台返回的订单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayCallbackLog', @level2type=N'COLUMN',@level2name=N'PlatformOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayCallbackLog', @level2type=N'COLUMN',@level2name=N'Contents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类名字段，支付类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayCategory', @level2type=N'COLUMN',@level2name=N'PayType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付类的描述字段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayCategory', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回调加白IP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayCategory', @level2type=N'COLUMN',@level2name=N'IpWhiteList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayMerchant', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付类别Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayMerchant', @level2type=N'COLUMN',@level2name=N'PayCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类名字段，支付类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayMerchant', @level2type=N'COLUMN',@level2name=N'PayCategory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayMerchant', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayMerchant', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置字符串' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayMerchant', @level2type=N'COLUMN',@level2name=N'ConfigStr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayOrder', @level2type=N'COLUMN',@level2name=N'MemberId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付渠道Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayOrder', @level2type=N'COLUMN',@level2name=N'PayMerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求充值金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayOrder', @level2type=N'COLUMN',@level2name=N'ReqDepositAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际充值金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayOrder', @level2type=N'COLUMN',@level2name=N'DepositAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否完成订单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayOrder', @level2type=N'COLUMN',@level2name=N'IsFinish'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayOrder', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认订单时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayOrder', @level2type=N'COLUMN',@level2name=N'ConfirmTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户返回数据Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayOrder', @level2type=N'COLUMN',@level2name=N'PayMerchantOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台订单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayRequestLog', @level2type=N'COLUMN',@level2name=N'SysOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'第三方平台返回的订单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayRequestLog', @level2type=N'COLUMN',@level2name=N'PlatformOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayRequestLog', @level2type=N'COLUMN',@level2name=N'Contents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别图片地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayTypeCategory', @level2type=N'COLUMN',@level2name=N'PicUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否虚拟地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PayTypeCategory', @level2type=N'COLUMN',@level2name=N'IsVirtualCurrency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'TagId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'AType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动规则' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'Config'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否开启' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否前台显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'Visible'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PC端图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'PcCover'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'首页图片显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'IndexPageCover'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsConfig', @level2type=N'COLUMN',@level2name=N'SortNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsTag', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsTag', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsTag', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PromotionsTag', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返水名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesConfig', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesConfig', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户分组ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesConfig', @level2type=N'COLUMN',@level2name=N'GoupId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数配置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesConfig', @level2type=N'COLUMN',@level2name=N'Configs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否手动发放' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesConfig', @level2type=N'COLUMN',@level2name=N'IsManual'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否开启' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesConfig', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesConfig', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesOrder', @level2type=N'COLUMN',@level2name=N'ConfigId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesOrder', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesOrder', @level2type=N'COLUMN',@level2name=N'MemberId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发放金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesOrder', @level2type=N'COLUMN',@level2name=N'Amount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生成记录ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesOrder', @level2type=N'COLUMN',@level2name=N'SourceId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RebatesOrder', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'局部ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SectionDetail', @level2type=N'COLUMN',@level2name=N'SectionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SectionDetail', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'别名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SectionDetail', @level2type=N'COLUMN',@level2name=N'Alias'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开启/禁用  ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SectionDetail', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片链接地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SectionDetail', @level2type=N'COLUMN',@level2name=N'PcImgUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面链接地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SectionDetail', @level2type=N'COLUMN',@level2name=N'PageUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否存在子菜单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SectionDetail', @level2type=N'COLUMN',@level2name=N'HasSubMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Key类型 手机/PC' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SectionKey', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'子类型的类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SectionKey', @level2type=N'COLUMN',@level2name=N'DetailType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Key类型 手机/PC' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempletKey', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'子类型的类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempletKey', @level2type=N'COLUMN',@level2name=N'DetailType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户层级关系' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserHierarchy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'AccountName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'真实姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'IdName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Pasw'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资金密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'FPasw'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级代理' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'AgentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户类型  会员/代理' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户基本信息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersBank', @level2type=N'COLUMN',@level2name=N'AccountName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersBank', @level2type=N'COLUMN',@level2name=N'AccountNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户银行卡绑定' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersBank'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户金额, 包含锁定' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFunds', @level2type=N'COLUMN',@level2name=N'TotalFunds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'苏定资金' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFunds', @level2type=N'COLUMN',@level2name=N'LockFunds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总充值金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFunds', @level2type=N'COLUMN',@level2name=N'TotalRechargedFunds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总充值笔数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFunds', @level2type=N'COLUMN',@level2name=N'TotalRechargedFundsCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户总提现笔数目' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFunds', @level2type=N'COLUMN',@level2name=N'TotalWithdrawalCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总投注金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFunds', @level2type=N'COLUMN',@level2name=N'TotalBetFunds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户总盈亏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFunds', @level2type=N'COLUMN',@level2name=N'TotalProfitAndLoss'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动奖金' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFunds', @level2type=N'COLUMN',@level2name=N'PromotionsFunds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'包含  佣金/反水/活动/饭店/奖励' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFunds', @level2type=N'COLUMN',@level2name=N'OtherFunds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户资金' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFunds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账变金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFundsLog', @level2type=N'COLUMN',@level2name=N'Amount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'锁定金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFundsLog', @level2type=N'COLUMN',@level2name=N'LockedAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'余额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFundsLog', @level2type=N'COLUMN',@level2name=N'Balance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账变类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFundsLog', @level2type=N'COLUMN',@level2name=N'FundsType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账变类型之子类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFundsLog', @level2type=N'COLUMN',@level2name=N'SubFundsType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转入/转出' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFundsLog', @level2type=N'COLUMN',@level2name=N'TransType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFundsLog', @level2type=N'COLUMN',@level2name=N'SourceId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户资金日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersFundsLog'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersLoginLog', @level2type=N'COLUMN',@level2name=N'LoginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersLoginLog', @level2type=N'COLUMN',@level2name=N'IP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP中文地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersLoginLog', @level2type=N'COLUMN',@level2name=N'IPCn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否最后登录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersLoginLog', @level2type=N'COLUMN',@level2name=N'IsLastLogin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否成功登录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersLoginLog', @level2type=N'COLUMN',@level2name=N'Sucess'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户登录日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersLoginLog'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否管理元手动协助签到' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersSignInLog', @level2type=N'COLUMN',@level2name=N'IsAssist'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户签到日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersSignInLog'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否开启， true开启， false未开启' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VipGroups', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否默认, true默认, false 非默认' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VipGroups', @level2type=N'COLUMN',@level2name=N'IsDefault'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值配置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VipGroups', @level2type=N'COLUMN',@level2name=N'PaySetting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提现设定' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VipGroups', @level2type=N'COLUMN',@level2name=N'WithdrawalSetting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分组满足条件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VipGroups', @level2type=N'COLUMN',@level2name=N'GroupSetting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'积分设置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VipGroups', @level2type=N'COLUMN',@level2name=N'PointSetting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分组描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VipGroups', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VipGroups', @level2type=N'COLUMN',@level2name=N'SortNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WashOrder', @level2type=N'COLUMN',@level2name=N'MemberId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资金类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WashOrder', @level2type=N'COLUMN',@level2name=N'FundsType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账变金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WashOrder', @level2type=N'COLUMN',@level2name=N'Amount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账变对应的流水金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WashOrder', @level2type=N'COLUMN',@level2name=N'WashAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WashOrder', @level2type=N'COLUMN',@level2name=N'Mark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效投注金额 账变金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WashOrderDetail', @level2type=N'COLUMN',@level2name=N'Amount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'未完成的总流水' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WashOrderDetail', @level2type=N'COLUMN',@level2name=N'Balance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WashOrderDetail', @level2type=N'COLUMN',@level2name=N'Mark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打码数据的Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WashOrderDetail', @level2type=N'COLUMN',@level2name=N'SourceOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WithdrawOrder', @level2type=N'COLUMN',@level2name=N'MemberId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求提现金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WithdrawOrder', @level2type=N'COLUMN',@level2name=N'ReqWithdrawAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际提现金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WithdrawOrder', @level2type=N'COLUMN',@level2name=N'WithdrawAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否完成订单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WithdrawOrder', @level2type=N'COLUMN',@level2name=N'IsFinish'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WithdrawOrder', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认订单时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WithdrawOrder', @level2type=N'COLUMN',@level2name=N'ConfirmTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'第三方系统订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WithdrawOrder', @level2type=N'COLUMN',@level2name=N'WithdrawMerchantOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WithdrawOrder', @level2type=N'COLUMN',@level2name=N'Marks'
GO









/****** Object:  Table [dbo].[SysAccount]    Script Date: 2024/5/30 13:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Name] [varchar](32) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[Avatar] [varchar](256) NOT NULL,
	[NickName] [varchar](32) NOT NULL,
	[Mobile] [varchar](16) NOT NULL,
	[Email] [varchar](128) NOT NULL,
	[LastLoginIp] [varchar](64) NOT NULL,
	[LastLoginTime] [datetime] NOT NULL,
	[IsLock] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[Remark] [nvarchar](128) NOT NULL,
	[CreatedId] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[SysStr] [varchar](18) NOT NULL,
 CONSTRAINT [PK_SysAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysAccountSession]    Script Date: 2024/5/30 13:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysAccountSession](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[Session] [varchar](32) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SysAccountSession] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysMenu]    Script Date: 2024/5/30 13:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[DisplayName] [nvarchar](32) NOT NULL,
	[IconClass] [varchar](128) NOT NULL,
	[LinkUrl] [varchar](128) NOT NULL,
	[Sort] [int] NOT NULL,
	[Permission] [varchar](2048) NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[IsSystem] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[ModifyId] [int] NOT NULL,
	[ModifyTime] [datetime] NOT NULL,
	[CreatedId] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[SysStr] [varchar](18) NOT NULL,
 CONSTRAINT [PK_SysMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysMenuAction]    Script Date: 2024/5/30 13:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysMenuAction](
	[Id] [int] IDENTITY(10000,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[Code] [varchar](16) NOT NULL,
	[Url] [varchar](64) NOT NULL,
	[ActionType] [tinyint] NOT NULL,
	[DataType] [tinyint] NOT NULL,
 CONSTRAINT [PK_SysMenuAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysRole]    Script Date: 2024/5/30 13:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MerchantId] [int] NOT NULL,
	[RoleName] [nvarchar](32) NOT NULL,
	[RoleType] [int] NOT NULL,
	[IsSystem] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[Remark] [nvarchar](128) NOT NULL,
	[CreatedId] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[SysStr] [varchar](18) NOT NULL,
 CONSTRAINT [PK_SysRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysRolePermission]    Script Date: 2024/5/30 13:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRolePermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[MenuActionId] [int] NOT NULL,
 CONSTRAINT [PK_SysRolePermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SysAccount] ON 
GO
INSERT [dbo].[SysAccount] ([Id], [MerchantId], [RoleId], [Name], [Password], [Avatar], [NickName], [Mobile], [Email], [LastLoginIp], [LastLoginTime], [IsLock], [IsDelete], [Remark], [CreatedId], [CreatedTime], [SysStr]) VALUES (1, 0, 1, N'KING', N'bviWoTT/WJmrWyMzoaeRGw==', N'1', N'KING', N'18888888888', N'18888888888@QQ.COM', N'1', CAST(N'2021-11-30T11:36:26.480' AS DateTime), 0, 0, N'KING', 1, CAST(N'2021-11-30T11:36:26.480' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysAccount] ([Id], [MerchantId], [RoleId], [Name], [Password], [Avatar], [NickName], [Mobile], [Email], [LastLoginIp], [LastLoginTime], [IsLock], [IsDelete], [Remark], [CreatedId], [CreatedTime], [SysStr]) VALUES (2, 1000, 2, N'KING', N'bviWoTT/WJmrWyMzoaeRGw==', N'1', N'KING', N'18888888888', N'18888888888@QQ.COM', N'1', CAST(N'2021-11-30T11:36:26.480' AS DateTime), 0, 0, N'KING', 1, CAST(N'2021-11-30T11:36:26.480' AS DateTime), N'luckymerchant')
GO
INSERT [dbo].[SysAccount] ([Id], [MerchantId], [RoleId], [Name], [Password], [Avatar], [NickName], [Mobile], [Email], [LastLoginIp], [LastLoginTime], [IsLock], [IsDelete], [Remark], [CreatedId], [CreatedTime], [SysStr]) VALUES (3, 1000, 3, N'sys1000', N'bviWoTT/WJmrWyMzoaeRGw==', N'1', N'NickName', N'Mobile', N'Email@Email.com', N'1', CAST(N'2020-08-26T13:17:21.843' AS DateTime), 0, 0, N'Remark', 1, CAST(N'2020-08-26T13:17:21.843' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysAccount] ([Id], [MerchantId], [RoleId], [Name], [Password], [Avatar], [NickName], [Mobile], [Email], [LastLoginIp], [LastLoginTime], [IsLock], [IsDelete], [Remark], [CreatedId], [CreatedTime], [SysStr]) VALUES (4, 0, 4, N'sysAdmin', N'Bo89bIK8+ja3CUwhTssenw==', N'1', N'NickName', N'Mobile', N'Email@Email.com', N'1', CAST(N'2020-08-26T13:17:21.843' AS DateTime), 0, 0, N'Remark', 1, CAST(N'2020-08-26T13:17:21.843' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysAccount] ([Id], [MerchantId], [RoleId], [Name], [Password], [Avatar], [NickName], [Mobile], [Email], [LastLoginIp], [LastLoginTime], [IsLock], [IsDelete], [Remark], [CreatedId], [CreatedTime], [SysStr]) VALUES (5, 1001, 3, N'boniusys', N'bviWoTT/WJmrWyMzoaeRGw==', N'1', N'', N'', N'', N'', CAST(N'2023-01-03T00:00:00.000' AS DateTime), 0, 0, N'', 1, CAST(N'2023-01-03T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysAccount] ([Id], [MerchantId], [RoleId], [Name], [Password], [Avatar], [NickName], [Mobile], [Email], [LastLoginIp], [LastLoginTime], [IsLock], [IsDelete], [Remark], [CreatedId], [CreatedTime], [SysStr]) VALUES (6, 1002, 3, N'fanbosys', N'bviWoTT/WJmrWyMzoaeRGw==', N'1', N'', N'', N'', N'', CAST(N'2023-01-03T00:00:00.000' AS DateTime), 0, 0, N'', 1, CAST(N'2023-01-03T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysAccount] ([Id], [MerchantId], [RoleId], [Name], [Password], [Avatar], [NickName], [Mobile], [Email], [LastLoginIp], [LastLoginTime], [IsLock], [IsDelete], [Remark], [CreatedId], [CreatedTime], [SysStr]) VALUES (7, 1003, 3, N'sys1001', N'bviWoTT/WJmrWyMzoaeRGw==', N'1', N'', N'', N'', N'', CAST(N'2023-01-03T00:00:00.000' AS DateTime), 0, 0, N'', 1, CAST(N'2023-01-03T00:00:00.000' AS DateTime), N'bwMerchant')
GO
SET IDENTITY_INSERT [dbo].[SysAccount] OFF
GO
SET IDENTITY_INSERT [dbo].[SysAccountSession] ON 
GO
INSERT [dbo].[SysAccountSession] ([Id], [AccountId], [Session], [UpdateTime]) VALUES (1, 4, N'33d1525aba4444f88fa2f969fb33d291', CAST(N'2023-12-06T00:36:08.327' AS DateTime))
GO
INSERT [dbo].[SysAccountSession] ([Id], [AccountId], [Session], [UpdateTime]) VALUES (2, 4, N'b1c612f4057e4530aac894d35e3b878f', CAST(N'2023-12-06T00:36:59.743' AS DateTime))
GO
INSERT [dbo].[SysAccountSession] ([Id], [AccountId], [Session], [UpdateTime]) VALUES (3, 4, N'615b6707fe2b4ea0a948a18973ef2e73', CAST(N'2023-12-06T00:38:10.853' AS DateTime))
GO
INSERT [dbo].[SysAccountSession] ([Id], [AccountId], [Session], [UpdateTime]) VALUES (4, 4, N'3f66e7fadb7b496ab36c99d9f31a1e9e', CAST(N'2023-12-06T00:42:51.170' AS DateTime))
GO
INSERT [dbo].[SysAccountSession] ([Id], [AccountId], [Session], [UpdateTime]) VALUES (5, 4, N'78766987daba4a998ec8597a12b95763', CAST(N'2023-12-10T01:32:04.903' AS DateTime))
GO
INSERT [dbo].[SysAccountSession] ([Id], [AccountId], [Session], [UpdateTime]) VALUES (6, 4, N'd7a0aeb3ef554b1ea8ed6e644c849d2a', CAST(N'2023-12-10T03:37:12.180' AS DateTime))
GO
INSERT [dbo].[SysAccountSession] ([Id], [AccountId], [Session], [UpdateTime]) VALUES (7, 3, N'428159d913884f74b6854035e4cd6a93', CAST(N'2023-12-10T04:10:26.487' AS DateTime))
GO
INSERT [dbo].[SysAccountSession] ([Id], [AccountId], [Session], [UpdateTime]) VALUES (8, 3, N'ba3bb31cdd974007a3abf176097b1228', CAST(N'2024-05-29T11:02:19.557' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[SysAccountSession] OFF
GO
SET IDENTITY_INSERT [dbo].[SysMenu] ON 
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (1, 0, N'games', N'游戏管理', N'&#xe653;', N'LinkUrl', 0, N'3bc5962b7c6542879ebd685ceb2ccc34', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (2, 0, N'orders', N'订单管理', N'&#xe65e;', N'LinkUrl', 0, N'ff448a21a9fc4332aea1b5427c6f50d5', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:24.450' AS DateTime), 0, CAST(N'2020-08-28T14:22:24.450' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (3, 0, N'users', N'会员管理', N'&#xe65e;', N'LinkUrl', 0, N'f2448a21a9fc4332aea1b5427c6f50d5', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:24.450' AS DateTime), 0, CAST(N'2020-08-28T14:22:24.450' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (4, 0, N'statistic', N'统计管理', N'&#xe60a;', N'LinkUrl', 0, N'0986bc9e0a7a47178cd18beb8e637012', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (5, 0, N'set', N'系统设置', N'&#xe631;', N'LinkUrl', 0, N'7dab00d0a149467895a76a61ee06ac89', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (6, 5, N'sys_role', N'角色管理', N'&#xe770;', N'sysaccount/rolelist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (7, 5, N'sys_account', N'管理员管理', N'&#xe770;', N'sysaccount/accountlist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (8, 1, N'games_list', N'游戏列表', N'&#xe653;', N'games/gamelist.html', 0, N'3bc5962b7c6542879ebd685ceb2ccc34', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (9, 1, N'games_result_list', N'待录入赛果列表', N'&#xe653;', N'games/gameresultlist.html', 0, N'3bc5962b7c6542879ebd685ceb2ccc34', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (10, 2, N'orders_list', N'订单列表', N'&#xe653;', N'orders/orderlist.html', 0, N'3bc5962b7c6542879ebd685ceb2ccc34', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (11, 2, N'orders_check_list', N'审核列表', N'&#xe653;', N'orders/orderchecklist.html', 0, N'3bc5962b7c6542879ebd685ceb2ccc34', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (12, 4, N'statistic_game', N'按比赛统计', N'&#xe60a;', N'statistic/game.html', 0, N'0986bc9e0a7a47178cd18beb8e637012', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (13, 4, N'statistic_day', N'按日期统计', N'&#xe60a;', N'statistic/day.html', 0, N'0986bc9e0a7a47178cd18beb8e637012', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (14, 4, N'statistic_month', N'按月统计', N'&#xe60a;', N'statistic/month.html', 0, N'0986bc9e0a7a47178cd18beb8e637012', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (15, 4, N'statistic_site', N'按站点统计', N'&#xe60a;', N'statistic/site.html', 0, N'0986bc9e0a7a47178cd18beb8e637012', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (16, 0, N'set', N'系统设置', N'&#xe631;', N'LinkUrl', 0, N'7dab00d0a149467895a76a61ee06ac89', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), N'luckymerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (17, 16, N'sys_role', N'角色管理', N'&#xe770;', N'sysaccount/rolelist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), N'luckymerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (18, 16, N'sys_account', N'管理员管理', N'&#xe770;', N'sysaccount/accountlist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), N'luckymerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (19, 0, N'users', N'会员管理', N'&#xe631;', N'LinkUrl', 0, N'7dab00d0a149467895a76a61ee06ac89', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), N'luckymerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (20, 19, N'users_list', N'会员列表', N'&#xe631;', N'user/list.html', 0, N'7dab00d0a149467895a76a61ee06ac89', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), N'luckymerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (21, 19, N'users_profit', N'转账列表', N'&#xe631;', N'user/trans.html', 0, N'7dab00d0a149467895a76a61ee06ac89', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), N'luckymerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (22, 0, N'orders', N'订单查询', N'&#xe631;', N'order/list.html', 0, N'7dab00d0a149467895a76a61ee06ac89', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), N'luckymerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (23, 0, N'statistic', N'报表管理', N'&#xe631;', N'LinkUrl', 0, N'7dab00d0a149467895a76a61ee06ac89', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), N'luckymerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (24, 23, N'statistic_day', N'日报表管理', N'&#xe631;', N'statistic/daily.html', 0, N'7dab00d0a149467895a76a61ee06ac89', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), N'luckymerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (25, 23, N'statistic_month', N'月报表管理', N'&#xe631;', N'statistic/monthly.html', 0, N'7dab00d0a149467895a76a61ee06ac89', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), N'luckymerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (26, 3, N'user_slist', N'会员列表', N'&#xe65e;', N'members/userlist.html', 0, N'f2448a21a9fc4332aea1b5427c6f50d5', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:24.450' AS DateTime), 0, CAST(N'2020-08-28T14:22:24.450' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (27, 1, N'minutesgames_list', N'分分彩游戏列表', N'&#xe653;', N'minutes/games/gamelist.html', 0, N'3bc5962b7c6542879ebd685ceb2ccc34', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (28, 1, N'minutesgames_result_list', N'分分彩赛果录入列表', N'&#xe653;', N'minutes/games/gameresultsetlist.html', 0, N'3bc5962b7c6542879ebd685ceb2ccc34', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (29, 1, N'games_list', N'奖池盘口游戏列表', N'&#xe653;', N'games/gamejackpotlist.html', 0, N'3bc5962b7c6542879ebd685ceb2ccc34', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (30, 1, N'games_list', N'冠军盘口游戏列表', N'&#xe653;', N'games/gamechampionlist.html', 0, N'3bc5962b7c6542879ebd685ceb2ccc34', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (31, 5, N'set_multi', N'多语言管理', N'&#xe653;', N'set/languagelist.html', 0, N'3bc5962b7c6542879ebd685ceb2ccc34', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (32, 0, N'members', N'会员管理', N'&#xe770;', N'', 0, N'cd2473dd5d71414580ca8c7902021b13', 1, 0, 0, 0, CAST(N'2020-08-28T00:00:43.553' AS DateTime), 0, CAST(N'2020-08-28T00:00:43.553' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (33, 0, N'资金管理', N'资金管理', N'&#xe65e;', N'LinkUrl', 0, N'05c6bc4edaa34f48a98f32c16aef6861', 1, 0, 0, 0, CAST(N'2020-08-28T00:01:13.647' AS DateTime), 0, CAST(N'2020-08-28T00:01:13.647' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (34, 0, N'游戏管理', N'游戏管理', N'&#xe653;', N'LinkUrl', 0, N'12bf62798cf34acf97864706a9bffb07', 1, 0, 0, 0, CAST(N'2020-08-28T00:01:29.787' AS DateTime), 0, CAST(N'2020-08-28T00:01:29.787' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (35, 0, N'活动管理', N'活动管理', N'&#xe627;', N'LinkUrl', 0, N'16cb14f8928f482183f3b4aefdfbf023', 1, 0, 0, 0, CAST(N'2020-08-28T00:01:38.817' AS DateTime), 0, CAST(N'2020-08-28T00:01:38.817' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (36, 0, N'内容管理', N'内容管理', N'&#xe6b2;', N'LinkUrl', 0, N'44e6881c198847b0b2b4fa4e8f84317c', 1, 0, 0, 0, CAST(N'2020-08-28T00:01:53.267' AS DateTime), 0, CAST(N'2020-08-28T00:01:53.267' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (37, 0, N'统计管理', N'报表管理', N'&#xe60a;', N'LinkUrl', 0, N'd07f824bc9fa4dcea44a9dc4bfd0d7ff', 1, 0, 0, 0, CAST(N'2020-08-28T00:02:13.103' AS DateTime), 0, CAST(N'2020-08-28T00:02:13.103' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (38, 0, N'系统管理', N'系统管理', N'&#xe631;', N'LinkUrl', 0, N'1b973a68f5a14129bded367aaffdddf1', 1, 0, 0, 0, CAST(N'2020-08-28T00:02:28.383' AS DateTime), 0, CAST(N'2020-08-28T00:02:28.383' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (39, 0, N'分类等待', N'分类等待', N'IconUrl', N'LinkUrl', 0, N'323da7507da64c99a03401e9716cdd6a', 0, 0, 0, 0, CAST(N'2020-08-28T00:41:07.277' AS DateTime), 0, CAST(N'2020-08-28T00:41:07.277' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (40, 0, N'分类等待', N'分类等待', N'IconUrl', N'LinkUrl', 0, N'9a5c9d0b182344488d4049ebdb8aed28', 0, 0, 0, 0, CAST(N'2020-08-28T00:41:08.120' AS DateTime), 0, CAST(N'2020-08-28T00:41:08.120' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (41, 0, N'分类等待', N'分类等待', N'IconUrl', N'LinkUrl', 0, N'ad81982c16664ca8a5229a72f58a017d', 0, 0, 0, 0, CAST(N'2020-08-28T00:41:08.897' AS DateTime), 0, CAST(N'2020-08-28T00:41:08.897' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (42, 32, N'members_manager', N'会员信息', N'&#xe770;', N'', 0, N'cd2473dd5d71414580ca8c7902021b14', 0, 0, 0, 0, CAST(N'2020-08-28T00:00:43.553' AS DateTime), 0, CAST(N'2020-08-28T00:00:43.553' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (43, 32, N'member_info', N'会员列表', N'&#xe770;', N'members/memberlist.html', 0, N'cd2473dd5d71414580ca8c7902021b15', 1, 0, 0, 0, CAST(N'2020-08-28T00:00:43.553' AS DateTime), 0, CAST(N'2020-08-28T00:00:43.553' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (44, 32, N'agent_manager', N'代理列表', N'&#xe770;', N'agent/list.html', 0, N'cd2473dd5d71414580ca8c7902021b16', 1, 0, 0, 0, CAST(N'2020-08-28T00:00:43.553' AS DateTime), 0, CAST(N'2020-08-28T00:00:43.553' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (45, 32, N'member_welfare', N'分组管理', N'&#xe770;', N'members/viplist.html', 0, N'cd2473dd5d71414580ca8c7902021b17', 1, 0, 0, 0, CAST(N'2020-08-28T00:00:43.553' AS DateTime), 0, CAST(N'2020-08-28T00:00:43.553' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (46, 35, N'promotion_list', N'活动列表', N'&#xe770;', N'promotions/prolist.html', 0, N'asdasdfasdfasd', 1, 0, 0, 1, CAST(N'2020-08-08T00:00:00.000' AS DateTime), 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (47, 35, N'promotion_orders', N'活动订单', N'&#xe770;', N'promotions/orderlist.html', 0, N'asdasdfasdfasd', 1, 0, 0, 1, CAST(N'2020-08-08T00:00:00.000' AS DateTime), 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (48, 33, N'recharge_list', N'充值订单', N'&#xe770;', N'pay/orderlist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime), 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (49, 33, N'withdraw_list', N'出款订单', N'&#xe770;', N'pay/withdraw-orderlist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime), 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (50, 33, N'funds_manual', N'款项管理', N'&#xe770;', N'pay/funds-page.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime), 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (51, 33, N'recharge_channe_llist', N'支付渠道', N'&#xe770;', N'pay/paylist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime), 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (52, 34, N'game_list', N'游戏管理', N'&#xe770;', N'game/gamelist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2021-01-22T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-22T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (53, 34, N'game_orderlist', N'游戏订单', N'&#xe770;', N'game/orderlist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2021-01-24T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-24T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (54, 36, N'help_center', N'帮助区域', N'&#xe770;', N'area/helplist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2021-03-03T00:00:00.000' AS DateTime), 0, CAST(N'2021-03-03T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (55, 36, N'section_center', N'页面配置', N'&#xe770;', N'area/sectionlist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2021-03-03T00:00:00.000' AS DateTime), 0, CAST(N'2021-03-03T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (56, 33, N'withdraw_channe_llist', N'代付渠道', N'&#xe770;', N'pay/withdrawallist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime), 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (57, 38, N'sys_role', N'角色管理', N'&#xe770;', N'sysaccount/rolelist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (58, 38, N'sys_account', N'管理员管理', N'&#xe770;', N'sysaccount/accountlist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (59, 36, N'msg_center', N'通知公告', N'&#xe770;', N'area/noticelist.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2021-05-09T00:00:00.000' AS DateTime), 0, CAST(N'2021-03-03T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (60, 38, N'sys_siteconfig', N'站点配置', N'&#xe770;', N'config/site.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (61, 0, N'merchant', N'站点管理', N'&#xe7ae;', N'LinkUrl', 0, N'ce73fe42835447a1abda1163a6139b11', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:01.450' AS DateTime), 0, CAST(N'2020-08-28T14:22:01.450' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (62, 0, N'game', N'游戏管理', N'&#xe653;', N'LinkUrl', 0, N'3bc5962b7c6542879ebd685ceb2ccc34', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), 0, CAST(N'2020-08-28T14:22:13.693' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (63, 0, N'pay', N'支付管理', N'&#xe65e;', N'LinkUrl', 0, N'ff448a21a9fc4332aea1b5427c6f50d5', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:24.450' AS DateTime), 0, CAST(N'2020-08-28T14:22:24.450' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (64, 0, N'statistic', N'统计管理', N'&#xe60a;', N'LinkUrl', 0, N'0986bc9e0a7a47178cd18beb8e637012', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (65, 0, N'set', N'系统设置', N'&#xe631;', N'LinkUrl', 0, N'7dab00d0a149467895a76a61ee06ac89', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.053' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (66, 0, N'templet', N'模板管理', N'&#xe7ae;', N'LinkUrl', 0, N'd6173df57de44e0fa73ff7dfb5497228', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:42.313' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.313' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (67, 0, N'log', N'日志管理', N'&#xe7ae;', N'LinkUrl', 0, N'cf0c1b2552c64492b5370a6989c626f7', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:42.567' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.567' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (68, 0, N'待定', N'系统设置', N'&#xe7ae;', N'LinkUrl', 0, N'f5b04172c32843978923ea9b20cf1379', 0, 0, 0, 0, CAST(N'2020-08-28T14:23:42.913' AS DateTime), 0, CAST(N'2020-08-28T14:23:42.913' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (69, 0, N'待定', N'系统设置', N'&#xe7ae;', N'LinkUrl', 0, N'890102d48b5648f9b58187a95ce52c12', 0, 0, 0, 0, CAST(N'2020-08-28T14:23:43.357' AS DateTime), 0, CAST(N'2020-08-28T14:23:43.357' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (70, 61, N'merchant_set', N'站点设定', N'&#xe7ae;', N'/views/merchant/merchlist.html', 0, N'2222d89ea4114891b5e047129f4d78ec', 1, 0, 0, 0, CAST(N'2020-08-28T14:54:38.303' AS DateTime), 0, CAST(N'2020-08-28T14:54:38.307' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (71, 61, N'merchant_member', N'会员信息', N'&#xe7ae;', N'LinkUrl', 0, N'878976d6b6b7418dabf2dae00ce05b63', 1, 0, 0, 0, CAST(N'2020-08-28T14:55:37.113' AS DateTime), 0, CAST(N'2020-08-28T14:55:37.113' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (72, 61, N'merchant_domain', N'域名配置', N'&#xe7ae;', N'/views/merchant/domain/list.html', 0, N'878976d6b6b7418dabf2dae00ce05b63', 1, 0, 0, 0, CAST(N'2020-08-28T14:55:37.113' AS DateTime), 0, CAST(N'2020-08-28T14:55:37.113' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (73, 62, N'game_info', N'游戏信息', N'&#xe7ae;', N'/views/game/vendor/venderlist.html', 0, N'878976d6b6b7418dabf2dae00ce05b63', 1, 0, 0, 0, CAST(N'2020-08-28T14:55:37.113' AS DateTime), 0, CAST(N'2020-08-28T14:55:37.113' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (74, 63, N'pay_list', N'支付列表', N'&#xe65e;', N'/views/pay/paylist.html', 0, N'ff448a21a9fc4332aea1b5427c6f50d5', 1, 0, 0, 0, CAST(N'2020-08-28T14:22:24.450' AS DateTime), 0, CAST(N'2020-08-28T14:22:24.450' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (75, 65, N'set_cache', N'缓存管理', N'&#xe65e;', N'/views/set/cache.html', 0, N'0', 1, 0, 0, 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (76, 65, N'set_merchant', N'站点设置', N'&#xe65e;', N'/views/set/merchant.html', 0, N'0', 1, 0, 0, 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (77, 66, N'templet_list', N'模板管理', N'&#xe65e;', N'/views/templet/tmplist.html', 0, N'0', 1, 0, 0, 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (78, 67, N'log_error', N'错误日志', N'&#xe65e;', N'/views/logs/errorlist.html', 0, N'0', 1, 0, 0, 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (79, 66, N'templet_key', N'PC模板配置', N'&#xe65e;', N'/views/templet/keylist.html', 0, N'0', 1, 0, 0, 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (80, 66, N'game_templet', N'游戏模板配置', N'&#xe65e;', N'/views/templet/gamelist.html', 0, N'0', 1, 0, 0, 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (81, 0, N'statistic_site', N'商户管理', N'&#xe60a;', N'LinkUrl', 0, N'0986bc9e0a7a47178cd18beb8e637012', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (82, 81, N'statistic_site', N'商户列表', N'&#xe60a;', N'merchants/list.html', 0, N'0986bc9e0a7a47178cd18beb8e637012', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (83, 0, N'content_site', N'内容管理', N'&#xe60a;', N'LinkUrl', 0, N'0986bc9e0a7a47178cd18beb8e637012', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (84, 83, N'content_site', N'通知管理', N'&#xe60a;', N'content/noticelist.html', 0, N'0986bc9e0a7a47178cd18beb8e637012', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (85, 5, N'set_game_category', N'参数配置', N'&#xe60a;', N'set/gamecatelist.html', 0, N'0986bc9e0a7a47178cd18beb8e637012', 1, 0, 0, 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), 0, CAST(N'2020-08-28T14:23:28.950' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (86, 37, N'sys_siteconfig', N'代理报表', N'&#xe770;', N'data/agent.html', 0, N'aaa', 1, 0, 0, 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), 0, CAST(N'2020-01-25T00:00:00.000' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (87, 65, N'set_tasks', N'任务管理', N'&#xe65e;', N'/views/set/tasks.html', 0, N'0', 1, 0, 0, 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), N'bwSys')
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Name], [DisplayName], [IconClass], [LinkUrl], [Sort], [Permission], [IsDisplay], [IsSystem], [IsDelete], [ModifyId], [ModifyTime], [CreatedId], [CreatedTime], [SysStr]) VALUES (88, 65, N'set_tasks', N'任务管理', N'&#xe65e;', N'/views/set/tasks.html', 0, N'0', 1, 0, 0, 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-26T00:00:00.000' AS DateTime), N'bwSys')
GO
SET IDENTITY_INSERT [dbo].[SysMenu] OFF
GO
SET IDENTITY_INSERT [dbo].[SysMenuAction] ON 
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10000, 56, N'[代付渠道表格]', N'af312efb7bb14e5c', N'/withdrawals/loadmerchants', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10001, 43, N'[会员表格]', N'a3e9c5236ab4b9b9', N'/member/load', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10002, 0, N'会员分组字典', N'abfb04af19792bca', N'/vips/groupdic', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10003, 45, N'[分组管理][分组表格]', N'bf312efb7bb14e5b', N'/vips/load', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10004, 49, N'出款订单列表', N'bb3555aa1d2f996f', N'/withdrawals/withdrawalsorderload', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10005, 51, N'支付渠道列表', N'af2940b542137f1a', N'/pay/load', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10006, 52, N'[游戏管理表格]', N'afbc9d86f365994f', N'/game/merchantsconfig', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10007, 53, N'[游戏订单][游戏订单表格]', N'a73ea08836a67519', N'/game/gameorders', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10008, 0, N'游戏类型', N'c650e8d50627f1aa', N'/game/merchantgametype', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10009, 46, N'活动管理列表', N'a514b03617d65891', N'/promotions/load', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10010, 47, N'[活动订单表格]', N'b9d28b56932e7748', N'/promotions/getorderlist', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10011, 54, N'[帮助区域表格]', N'afc22a5e174f4ec6', N'/areas/helparealist', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10012, 55, N'[页面配置表格]', N'e950675213c7928b', N'/areas/section', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10013, 57, N'[角色管理][角色管理表格]', N'a61a59c5a04a9ada', N'/account/role/load', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10014, 58, N'管理员管理列表', N'ae641c03bcc1f883', N'/account/manager/load', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10015, 43, N'[添加会员]', N'a41ce472055ed63a', N'', 1, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10016, 43, N'[会员表格][编辑]', N'a35ca39fd8e1068b', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10017, 43, N'[会员表格][代理配置]', N'c37c4fe299a1fcc4', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10018, 43, N'[会员表格][账变日志]', N'abd8b4c126ecdbf5', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10019, 43, N'[会员表格][登陆日志]', N'a51ba100c1cfa329', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10020, 43, N'[添加会员][保存提交]', N'd6e54b35b7e6548a', N'/member/add', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10021, 43, N'[会员表格][编辑][保存提交]', N'ad7dc3080e8cf581', N'/member/updateinfo', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10022, 43, N'[会员表格][代理配置][保存提交]', N'ef3ea37a5de2d4ed', N'/member/setagent', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10023, 45, N'[分组管理][新增分组]', N'b474006639fb81b2', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10024, 45, N'[分组管理][新增分组][保存提交]', N'aba6390ce2a86dad', N'/vips/add', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10025, 45, N'[分组表格][编辑]', N'af829007a99bf40a', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10026, 45, N'[分组表格][分组设定]', N'a1db673a93a64c88', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10027, 45, N'[分组表格][配置支付]', N'd1f43defc52b0a64', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10028, 45, N'[分组表格][积分设定]', N'a0b2741e4aaf9acf', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10029, 45, N'[分组表格][编辑][保存提交]', N'a69e245d2d4f3bac', N'/vips/add', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10030, 45, N'[分组表格][分组设定][保存提交]', N'ae5962a9d9a7fd06', N'/vips/setgroup', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10031, 45, N'[分组表格][配置支付][保存提交]', N'ac47794628de51e1', N'/vips/setpay', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10032, 50, N'[款项管理][手动加款]', N'a0e68ba989dc3b56', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10033, 50, N'[款项管理][手动减款]', N'a346400b924df4e9', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10034, 50, N'[手动减款][保存提交]', N'a50586451a2d7f09', N'/pay/manualfunds', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10035, 50, N'[手动加款][保存提交]', N'd9559edc0a858cdc', N'/pay/manualfunds', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10036, 51, N'[支付渠道][添加支付渠道]', N'a2843e8bedef69', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10037, 51, N'[支付渠道][支付类型管理]', N'a42276f025046a53', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10038, 51, N'[支付渠道表格][编辑]', N'a38272817684aa57', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10039, 51, N'[支付渠道表格][订单汇总]', N'f81529eea56e6c81', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10040, 51, N'[添加支付渠道][保存提交]', N'a024c16dbc5d1354', N'/pay/savemerchantconfig', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10041, 0, N'支付商户字典', N'af424033a7713188', N'/pay/paycategory', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10042, 51, N'[支付类型管理][保存提交]', N'a73bd1d40766a79c', N'/pay/saveconfig', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10043, 56, N'[代付渠道][添加代付渠道]', N'a3ca3614d5ba59fb', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10044, 56, N'[添加代付渠道][保存提交]', N'e787d6bab7a8bb15', N'/withdrawals/savemerchantconfig', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10045, 56, N'[代付渠道表格][编辑]', N'a7f1ca972ee28b29', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10046, 56, N'[代付渠道表格][订单汇总]', N'ab3114e3189fec06', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10047, 56, N'[代付渠道列表][编辑][保存提交]', N'e2bb2bf21e563e4d', N'/withdrawals/savemerchantconfig', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10048, 52, N'[游戏管理表格][编辑]', N'a05fcaf378ce26c1', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10049, 52, N'[游戏管理表格][订单汇总]', N'a69c8bfec0e48c5', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10050, 52, N'[游戏管理表格][转账汇总]', N'a501463ba08c34da', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10051, 52, N'[游戏管理表格][编辑][保存提交]', N'ab4984ad09b4abd6', N'/game/UpdateMerchantGameStatus', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10052, 0, N'开启的游戏字典', N'bf2690fa74e06dcc', N'/game/merchantgametype', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10053, 53, N'[游戏订单表格][投注明细]', N'a1d0c4867192fb45', N'/game/gameordersdetails', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10054, 46, N'[活动列表][创建活动]', N'a4c2a7f675e753d6', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10055, 46, N'[活动列表][创建活动][保存提交]', N'fbf6ab2072822790', N'/promotions/savepro', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10056, 46, N'[活动列表表格][编辑活动]', N'a978b86011cd3bf7', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10057, 46, N'[活动列表表格][编辑活动][保存提交]', N'd56b23ddf4043feb', N'/promotions/savepro', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10058, 46, N'[活动列表表格][编辑活动规则]', N'c8f2649535b2ed8c', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10059, 46, N'[活动列表表格][订单汇总]', N'aa858dc9fec81079', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10060, 46, N'[活动列表表格][编辑活动规则][保存提交]', N'afcbc1d58dc7d773', N'/promotions/savepromoconfig', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10061, 47, N'[活动订单][创建活动返水订单]', N'df09d7cf1d0f42a0', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10062, 47, N'[活动订单][创建活动返水订单][保存提交]', N'af108eb81718c25f', N'/promotions/saveorder', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10063, 47, N'[活动订单表格][确认订单]', N'f19aace677c8b482', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10064, 47, N'[活动订单表格][返水明细]', N'b15717bddf5df4c6', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10065, 54, N'[帮助区域][添加区域内容]', N'a4af1f550ca77e14', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10066, 54, N'[帮助区域][帮助类别管理]', N'c6b0e0cd16f8104', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10067, 0, N'帮助类别字典', N'a2a0c761da7863cd', N'/areas/helptypedic', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10068, 54, N'[添加区域内容][保存提交]', N'10b069d79712918e', N'/areas/savehelpcontent', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10069, 54, N'[帮助类别管理][类别列表]', N'ab08cedd2687ce0a', N'/areas/helptypearealist', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10070, 57, N'[角色管理][添加角色]', N'f9b20f9e04731e60', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10071, 57, N'[角色管理表格][编辑]', N'a1084b565c20dcbd', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10072, 57, N'[角色管理][编辑][保存提交]', N'efc9e8c09def2553', N'/account/role/addormodify', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10073, 58, N'[管理员管理][添加账户]是否显示', N'a6b6913d6484289c', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10074, 58, N'[管理员管理][添加账户][保存提交]', N'a628641bb08f2be7', N'/account/role/addormodify', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10075, 57, N'[角色管理][添加角色]角色Tree信息', N'c8c437f14254385d', N'/account/menu/loaddatawithparentroleid', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10076, 0, N'支付商户词典', N'abac7e1aca504e1a', N'/pay/paymerchantdic', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10077, 0, N'支付类型词典', N'aff71ba29e0ce596', N'/pay/paytypecatedic', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10078, 0, N'主界面菜单请求数据', N'ab447bc6ff3ffcf5', N'/account/home/menus', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10079, 0, N'主界面页面缓存加载数据', N'c5fe169533946ef4', N'/dcache/init', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10080, 43, N'[帮助类别管理][保存提交]', N'd660822b20618823', N'/areas/savehelptype', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10081, 48, N'[充值订单][充值表格]', N'892ee87964eaa32a', N'/pay/orderload', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10082, 0, N'请求登陆', N'debd4b60d67c5514', N'/account/signin', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10083, 0, N'[页面配置][编辑]', N'a326ba935de7db73', N'/areas/sectiondetails', 1, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10084, 55, N'[页面配置][编辑][保存提交]', N'd223e9df5f66d623', N'/areas/sectionsave', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10085, 0, N'kindeditor图片上传', N'd145888565ac3c25', N'/upload/kindeditor', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10086, 0, N'图片上传', N'14077aebe1306478', N'/upload/image', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10087, 46, N'活动标签管理', N'ac43f079541bd8ab', N'', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10088, 46, N'[活动列表][活动标签列表]', N'a6775e66ff9f12b', N'/promotions/tagload', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10089, 46, N'[活动列表][活动标签列表][保存提交]', N'e442b562a087e909', N'/promotions/savetag', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10090, 46, N'[活动列表][活动标签列表][删除]', N'18fed25ad0bcc0dd', N'/promotions/deletetag', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10091, 46, N'[活动列表][创建活动][活动标签]', N'de50afc192ef17ee', N'/promotions/tagdic', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10092, 59, N'[通知公告][通知公告列表]', N'b5ddf1094329e58d', N'/areas/noticeload', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10093, 59, N'[通知公告][添加内容]', N'aaaf7bc0d32eb910', N'', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10094, 59, N'[通知公告][添加内容][保存提交]', N'a0310553133dc2a0', N'/areas/noticesave', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10095, 59, N'[通知公告列表][编辑]', N'cd22ecb8c073278f', N'/areas/noticesave', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10096, 59, N'[通知公告列表][删除]', N'adc964e7107929f6', N'/areas/noticedel', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10097, 51, N'[支付渠道][支付类型管理][支付类型列表]', N'5a619370b3e42b8d', N'/pay/loadPayType', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10098, 51, N'[支付渠道][支付类型管理][支付类型列表][是否启用]', N'fe873e702a8d57e5', N'/pay/updatecatetorytypestatus', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10099, 51, N'[支付渠道][支付类型管理][支付类型列表][保存提交]', N'bb9d4f805e2035e5', N'/pay/savepaytypecat', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10100, 58, N'[管理员管理][编辑角色][角色字典]', N'a834e35110cdbc4c', N'/role/roledic', 2, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10101, 60, N'[站点配置][查看站点配置信息]', N'9becc3bfb2db5924', N'/merchant/getcsconfig', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10102, 60, N'[站点配置][保存站点配置信息]', N'452aa37a5ca314fe', N'/merchant/savecsconfig', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10103, 0, N'保存幻灯片(slider)', N'805eede882342e85', N'/areas/savebanner', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10104, 54, N'[内容管理][帮助区域]获取帮助内容', N'e0dbbc8aec7e98d', N'/areas/gethelpcontent', 2, 3)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10105, 44, N'[代理列表][代理列表]', N'b7881d97cdd997ac', N'/member/agentload', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10106, 54, N'[内容管理][帮助区域][删除]', N'cb4de4370202e9e7', N'/areas/delhelpcontent', 2, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10107, 0, N'获取', N'ed74fe38c8521f66', N'/areas/sectiondic', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10108, 0, N'[资金管理][支付渠道][配置支付渠道]获取支付类别列表', N'b76a45369e4c8ff5', N'/pay/paytypelist', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10109, 0, N'获取代理信息', N'9e8b7149c69face', N'/member/getagents', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10110, 0, N'获取代理返点活动数据', N'd74b48248a365c20', N'/promotions/getagentpromodic', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10111, 0, N'[会员列表]重置会员资金密码', N'75a4958c8c978337', N'/member/resetfpsw', 1, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10112, 32, N'[会员管理][会员列表][会员账变]列表', N'5f5ae4d7c97de786', N'/member/getfundslog', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10113, 33, N'[资金管理][充值订单]确认', N'd45c1d1030741883', N'/pay/confirmpayorder', 1, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10114, 37, N'[报表管理][代理报表]列表', N'215dd8e70fe4f84b', N'/ds/agentd', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10115, 33, N'支付列表', N'1fe27b10d142056d', N'/pay/orderload', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10116, 33, N'[资金管理][充值订单][取消订单]', N'c0ae17bd79691bc8', N'/pay/cannelpayorders', 1, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10117, 32, N'设置默认代理按钮', N'840377acc1fc169f', N'/member/setdefaultagent', 1, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10118, 32, N'取消默认代理按钮', N'bf3c6eb75566d536', N'/member/canneldefaultagent', 1, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10119, 34, N'[游戏管理][游戏订单]洗码', N'95d713c4d438f984', N'/game/pushordertowash', 1, 2)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10120, 35, N'[活动管理][活动列表][返水订单]', N'a1b30c63db239efd', N'/promotions/getrebateorderlist', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10121, 32, N'代理列表', N'6bf9045e08839b80', N'/merchant/member/agentload', 2, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10122, 45, N'分组字典', N'd638f84cf61c199f', N'/merchant/vips/groupdic', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10123, 45, N'会员列表', N'fbd914448b6a95aa', N'/merchant/member/load', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10124, 45, N'分组管理列表', N'ae4372de380fbf47', N'/merchant/vips/load', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10125, 48, N'充值订单字典', N'54b33e86b43aacbd', N'/merchant/pay/paytypecatedic', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10126, 49, N'出款订单列表', N'7aebac191e1dfe9', N'/merchant/withdrawals/withdrawalsorderload', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10127, 51, N'支付渠道列表', N'22baf640c76b89cc', N'/merchant/pay/load', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10128, 56, N'代付渠道列表', N'1601898e4c62be5c', N'/merchant/withdrawals/loadmerchants', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10129, 52, N'游戏管理列表', N'fe0cf55a335d46b4', N'/merchant/game/merchantsconfig', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10130, 53, N'游戏类别', N'6d3d416732d647e0', N'/merchant/game/merchantgametype', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10131, 53, N'游戏订单列表', N'3370abc97317f656', N'/merchant/game/gameorders', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10132, 46, N'活动列表', N'a76aa28fc6ad9f8', N'/merchant/promotions/load', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10133, 47, N'活动订单', N'72b6ee8151342362', N'/merchant/promotions/getorderlist', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10134, 54, N'[内容管理][帮助区域][列表]', N'74c41ed1ede5e4fb', N'/merchant/areas/helparealist', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10135, 54, N'[内容管理][帮助区域][添加区域内容][帮助类型字典]', N'c8f3a9b1ef2fa28c', N'/merchant/areas/helptypedic', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10136, 54, N'[内容管理][帮助区域][帮助类别管理][列表]', N'35f17e77cb458aba', N'/merchant/areas/helptypearealist', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10137, 55, N'[内容管理][页面配置][列表]', N'14126f732b2d0a19', N'/merchant/areas/section', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10138, 55, N'[内容管理][页面配置][添加Key子菜单][菜单字典]', N'123a40483f735ac2', N'/merchant/areas/sectiondic', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10139, 59, N'[内容管理][通知公告][列表]', N'71112b6e4bb9abe7', N'/merchant/areas/noticeload', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10140, 59, N'[报表管理][代理报表][列表]', N'14dec43468d85a54', N'/merchant/ds/agentd', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10141, 58, N'[系统管理][管理员管理][添加账户][角色类型字典]', N'120d60f5094788e', N'/merchant/account/role/roledic', 1, 1)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10142, 60, N'[系统管理][管理员管理][站点配置][站点配置信息]', N'f4f64ec326dbf742', N'/merchant/merchant/getcsconfig', 1, 0)
GO
INSERT [dbo].[SysMenuAction] ([Id], [ParentId], [Name], [Code], [Url], [ActionType], [DataType]) VALUES (10143, 32, N'站点基础数据初始化', N'b7e8419aca02c084', N'/merchant/dcache/init', 1, 0)
GO
SET IDENTITY_INSERT [dbo].[SysMenuAction] OFF
GO
SET IDENTITY_INSERT [dbo].[SysRole] ON 
GO
INSERT [dbo].[SysRole] ([Id], [MerchantId], [RoleName], [RoleType], [IsSystem], [IsDelete], [Remark], [CreatedId], [CreatedTime], [SysStr]) VALUES (1, 1000, N'kING', 1, 1, 0, N'总管', 0, CAST(N'2021-11-30T11:34:24.827' AS DateTime), N'lukcysys')
GO
INSERT [dbo].[SysRole] ([Id], [MerchantId], [RoleName], [RoleType], [IsSystem], [IsDelete], [Remark], [CreatedId], [CreatedTime], [SysStr]) VALUES (2, 1000, N'kING', 1, 1, 0, N'总管', 0, CAST(N'2021-11-30T11:34:24.827' AS DateTime), N'luckymerchant')
GO
INSERT [dbo].[SysRole] ([Id], [MerchantId], [RoleName], [RoleType], [IsSystem], [IsDelete], [Remark], [CreatedId], [CreatedTime], [SysStr]) VALUES (3, 1000, N'系统管理', 1, 0, 0, N'Remark', 0, CAST(N'2020-08-28T09:14:28.567' AS DateTime), N'bwMerchant')
GO
INSERT [dbo].[SysRole] ([Id], [MerchantId], [RoleName], [RoleType], [IsSystem], [IsDelete], [Remark], [CreatedId], [CreatedTime], [SysStr]) VALUES (4, 1000, N'系统管理', 1, 0, 0, N'0', 0, CAST(N'2022-07-20T00:00:00.000' AS DateTime), N'bwSys')
GO
SET IDENTITY_INSERT [dbo].[SysRole] OFF
GO
SET IDENTITY_INSERT [dbo].[SysRolePermission] ON 
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (1, 1, 1, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (2, 1, 2, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (3, 1, 3, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (4, 1, 4, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (5, 1, 5, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (6, 1, 6, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (7, 1, 7, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (8, 1, 8, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (9, 1, 9, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (10, 1, 10, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (11, 1, 11, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (12, 1, 12, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (13, 1, 13, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (14, 1, 14, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (15, 1, 15, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (16, 2, 16, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (17, 2, 17, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (18, 2, 18, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (19, 2, 19, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (20, 2, 20, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (21, 2, 21, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (22, 2, 22, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (23, 2, 23, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (24, 2, 24, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (25, 2, 25, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (26, 1, 26, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (27, 1, 27, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (28, 1, 28, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (29, 1, 29, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (30, 1, 30, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (31, 1, 31, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (32, 4, 61, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (33, 4, 62, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (34, 4, 63, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (35, 4, 64, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (36, 4, 65, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (37, 4, 66, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (38, 4, 67, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (39, 4, 68, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (40, 4, 69, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (41, 4, 70, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (42, 4, 71, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (43, 4, 72, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (44, 4, 73, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (45, 4, 74, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (46, 4, 75, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (47, 4, 76, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (48, 4, 77, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (49, 4, 78, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (50, 4, 79, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (51, 4, 80, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (52, 1, 81, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (53, 1, 82, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (54, 1, 83, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (55, 1, 84, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (56, 6, 32, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (57, 6, 42, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (58, 6, 43, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (59, 6, 44, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (60, 6, 45, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (61, 6, 33, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (62, 6, 48, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (63, 6, 49, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (64, 6, 50, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (65, 6, 51, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (66, 6, 56, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (67, 6, 34, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (68, 6, 52, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (69, 6, 53, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (70, 6, 35, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (71, 6, 46, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (72, 6, 47, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (73, 6, 36, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (74, 6, 54, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (75, 6, 55, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (76, 6, 59, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (77, 6, 37, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (78, 6, 38, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (79, 6, 57, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (80, 6, 58, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (81, 6, 60, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (82, 6, 0, 10001)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (83, 6, 0, 10015)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (84, 6, 0, 10016)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (85, 6, 0, 10017)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (86, 6, 0, 10018)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (87, 6, 0, 10019)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (88, 6, 0, 10020)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (89, 6, 0, 10021)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (90, 6, 0, 10022)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (91, 6, 0, 10080)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (92, 6, 0, 10003)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (93, 6, 0, 10023)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (94, 6, 0, 10024)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (95, 6, 0, 10025)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (96, 6, 0, 10026)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (97, 6, 0, 10027)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (98, 6, 0, 10028)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (99, 6, 0, 10029)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (100, 6, 0, 10030)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (101, 6, 0, 10031)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (102, 6, 0, 10081)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (103, 6, 0, 10004)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (104, 6, 0, 10032)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (105, 6, 0, 10033)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (106, 6, 0, 10034)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (107, 6, 0, 10035)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (108, 6, 0, 10005)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (109, 6, 0, 10036)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (110, 6, 0, 10037)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (111, 6, 0, 10038)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (112, 6, 0, 10039)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (113, 6, 0, 10040)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (114, 6, 0, 10042)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (115, 6, 0, 10097)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (116, 6, 0, 10098)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (117, 6, 0, 10099)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (118, 6, 0, 10043)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (119, 6, 0, 10044)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (120, 6, 0, 10045)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (121, 6, 0, 10046)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (122, 6, 0, 10047)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (123, 6, 0, 10006)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (124, 6, 0, 10048)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (125, 6, 0, 10049)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (126, 6, 0, 10050)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (127, 6, 0, 10051)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (128, 6, 0, 10007)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (129, 6, 0, 10053)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (130, 6, 0, 10009)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (131, 6, 0, 10054)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (132, 6, 0, 10055)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (133, 6, 0, 10056)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (134, 6, 0, 10057)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (135, 6, 0, 10058)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (136, 6, 0, 10059)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (137, 6, 0, 10060)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (138, 6, 0, 10087)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (139, 6, 0, 10088)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (140, 6, 0, 10089)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (141, 6, 0, 10090)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (142, 6, 0, 10010)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (143, 6, 0, 10061)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (144, 6, 0, 10062)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (145, 6, 0, 10063)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (146, 6, 0, 10064)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (147, 6, 0, 10011)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (148, 6, 0, 10065)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (149, 6, 0, 10066)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (150, 6, 0, 10068)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (151, 6, 0, 10069)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (152, 6, 0, 10012)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (153, 6, 0, 10084)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (154, 6, 0, 10092)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (155, 6, 0, 10093)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (156, 6, 0, 10094)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (157, 6, 0, 10095)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (158, 6, 0, 10096)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (159, 6, 0, 10013)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (160, 6, 0, 10070)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (161, 6, 0, 10071)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (162, 6, 0, 10072)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (163, 6, 0, 10014)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (164, 6, 0, 10073)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (165, 6, 0, 10074)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (166, 6, 0, 10102)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (167, 6, 0, 10083)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (168, 6, 0, 10103)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (169, 6, 0, 10104)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (170, 6, 0, 10105)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (171, 3, 32, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (172, 3, 42, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (173, 3, 43, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (174, 3, 44, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (175, 3, 45, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (176, 3, 33, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (177, 3, 48, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (178, 3, 49, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (179, 3, 50, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (180, 3, 51, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (181, 3, 56, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (182, 3, 34, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (183, 3, 52, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (184, 3, 53, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (185, 3, 35, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (186, 3, 46, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (187, 3, 47, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (188, 3, 36, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (189, 3, 54, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (190, 3, 55, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (191, 3, 59, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (192, 3, 37, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (193, 3, 38, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (194, 3, 57, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (195, 3, 58, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (196, 3, 60, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (197, 3, 0, 10001)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (198, 3, 0, 10015)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (199, 3, 0, 10016)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (200, 3, 0, 10017)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (201, 3, 0, 10018)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (202, 3, 0, 10019)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (203, 3, 0, 10020)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (204, 3, 0, 10021)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (205, 3, 0, 10022)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (206, 3, 0, 10080)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (207, 3, 0, 10105)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (208, 3, 0, 10003)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (209, 3, 0, 10023)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (210, 3, 0, 10024)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (211, 3, 0, 10025)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (212, 3, 0, 10026)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (213, 3, 0, 10027)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (214, 3, 0, 10028)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (215, 3, 0, 10029)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (216, 3, 0, 10030)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (217, 3, 0, 10031)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (218, 3, 0, 10081)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (219, 3, 0, 10004)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (220, 3, 0, 10032)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (221, 3, 0, 10033)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (222, 3, 0, 10034)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (223, 3, 0, 10035)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (224, 3, 0, 10005)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (225, 3, 0, 10036)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (226, 3, 0, 10037)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (227, 3, 0, 10038)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (228, 3, 0, 10039)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (229, 3, 0, 10040)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (230, 3, 0, 10042)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (231, 3, 0, 10097)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (232, 3, 0, 10098)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (233, 3, 0, 10099)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (234, 3, 0, 10043)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (235, 3, 0, 10044)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (236, 3, 0, 10045)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (237, 3, 0, 10046)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (238, 3, 0, 10047)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (239, 3, 0, 10006)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (240, 3, 0, 10048)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (241, 3, 0, 10049)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (242, 3, 0, 10050)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (243, 3, 0, 10051)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (244, 3, 0, 10007)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (245, 3, 0, 10053)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (246, 3, 0, 10009)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (247, 3, 0, 10054)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (248, 3, 0, 10055)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (249, 3, 0, 10056)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (250, 3, 0, 10057)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (251, 3, 0, 10058)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (252, 3, 0, 10059)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (253, 3, 0, 10060)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (254, 3, 0, 10087)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (255, 3, 0, 10088)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (256, 3, 0, 10089)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (257, 3, 0, 10090)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (258, 3, 0, 10010)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (259, 3, 0, 10061)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (260, 3, 0, 10062)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (261, 3, 0, 10063)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (262, 3, 0, 10064)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (263, 3, 0, 10011)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (264, 3, 0, 10065)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (265, 3, 0, 10066)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (266, 3, 0, 10068)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (267, 3, 0, 10069)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (268, 3, 0, 10104)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (269, 3, 0, 10106)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (270, 3, 0, 10012)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (271, 3, 0, 10084)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (272, 3, 0, 10092)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (273, 3, 0, 10093)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (274, 3, 0, 10094)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (275, 3, 0, 10095)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (276, 3, 0, 10096)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (277, 3, 0, 10013)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (278, 3, 0, 10070)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (279, 3, 0, 10071)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (280, 3, 0, 10072)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (281, 3, 0, 10014)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (282, 3, 0, 10073)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (283, 3, 0, 10074)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (284, 3, 0, 10102)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (285, 3, 0, 10083)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (286, 3, 0, 10103)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (287, 1, 85, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (288, 3, 86, 0)
GO
INSERT [dbo].[SysRolePermission] ([Id], [RoleId], [MenuId], [MenuActionId]) VALUES (289, 4, 87, 0)
GO
SET IDENTITY_INSERT [dbo].[SysRolePermission] OFF
GO
ALTER TABLE [dbo].[SysAccount] ADD  CONSTRAINT [DF_SysAccount_IsLock]  DEFAULT ((0)) FOR [IsLock]
GO
ALTER TABLE [dbo].[SysAccount] ADD  CONSTRAINT [DF_SysAccount_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysMenu] ADD  CONSTRAINT [DF_SysMenu_SysStr]  DEFAULT ('') FOR [SysStr]
GO
ALTER TABLE [dbo].[SysRole] ADD  CONSTRAINT [DF_SysRole_RoleType]  DEFAULT ((1)) FOR [RoleType]
GO
ALTER TABLE [dbo].[SysRole] ADD  CONSTRAINT [DF_SysRole_IsSystem]  DEFAULT ((0)) FOR [IsSystem]
GO
ALTER TABLE [dbo].[SysRole] ADD  CONSTRAINT [DF_SysRole_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysRole] ADD  CONSTRAINT [DF_SysRole_SysStr]  DEFAULT ('') FOR [SysStr]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'头像' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'Avatar'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'NickName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次登录IP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'LastLoginIp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'LastLoginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否锁定' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'IsLock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建该账户的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'CreatedId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'不同系统使用的区分字符串' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccount', @level2type=N'COLUMN',@level2name=N'SysStr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccountSession', @level2type=N'COLUMN',@level2name=N'AccountId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录Session' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccountSession', @level2type=N'COLUMN',@level2name=N'Session'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysAccountSession', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'DisplayName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图标地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'IconClass'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'链接地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'LinkUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序数字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作权限（按钮权限时使用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Permission'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否系统默认' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'IsSystem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'不同系统使用的区分字符串' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'SysStr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenuAction', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenuAction', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码，GUID 16位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenuAction', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenuAction', @level2type=N'COLUMN',@level2name=N'Url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'MerchantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:超管, 1:普通管理员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'RoleType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否系统默认' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'IsSystem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建该角色的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'CreatedId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'不同系统使用的区分字符串' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'SysStr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面操作权限按钮' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRolePermission', @level2type=N'COLUMN',@level2name=N'MenuActionId'
GO

















