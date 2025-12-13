CREATE DATABASE LeaderBoardSystem;
USE LeaderBoardSystem;

CREATE TABLE [User]
(
UserID INT PRIMARY KEY IDENTITY(1,1),
UserName VARCHAR(50),
Email VARCHAR(254),
CONSTRAINT chkEmail CHECK(Email LIKE('___%@_%.__%')),
PhoneNo INT,
UtID VARCHAR(10) REFERENCES UserType(UtID)
);

ALTER TABLE [User] ADD CONSTRAINT chkEmail CHECK(Email LIKE('___%@_%.__%'));

CREATE TABLE UserType
(
UtID VARCHAR(10) PRIMARY KEY,
UserType VARCHAR(15)
);

CREATE TABLE GameDetails
(
Game_ID INT PRIMARY KEY IDENTITY(1,1),
GameHeldDate DATE,
GameStartTime VARCHAR(10),
GameEndTime VARCHAR(10),
GameRoundsHeld INT,
GameWinner INT REFERENCES [User](UserID),
GameMVP INT REFERENCES [User](UserID),
RunnerUp INT REFERENCES [User](UserID),
BestTime VARCHAR(10),
CtID VARCHAR(10) REFERENCES ContestType(CtID),
LtID INT REFERENCES LocationContest(LtID)
);

CREATE TABLE Contest
(
Contest_ID INT PRIMARY KEY IDENTITY(1,1),
CtID VARCHAR(10) REFERENCES ContestType(CtID),
Winner INT REFERENCES [User](UserID),
MVP_OF_Contest INT REFERENCES [User](UserID),
Runner_UP INT REFERENCES [User](UserID),
TotalNumberOfMatches INT,
Best_Time VARCHAR(10),
LtID INT REFERENCES LocationContest(LtID),
ContestStartDate DATE,
ContestEndDate DATE
);

CREATE TABLE ContestType
(
CtID VARCHAR(10) PRIMARY KEY,
ContestType VARCHAR(25)
);

CREATE TABLE LocationContest
(
LtID INT PRIMARY KEY IDENTITY(1,1),
Continent VARCHAR(15),
Country VARCHAR(60),
[State] VARCHAR(100),
City VARCHAR(100),
[Address] VARCHAR(250)
)

CREATE TABLE PlayerScore
(
ScoreID INT PRIMARY KEY IDENTITY(1,1),
PlayerID INT REFERENCES [User](UserID),
Score DECIMAL(10,2),
GameID INT REFERENCES GameDetails(Game_ID),
Rating INT
);

CREATE TABLE GlobalLeaderBoard 
(
GLB_ID INT PRIMARY KEY IDENTITY(1,1),
PlayerID INT REFERENCES [User](UserID),
TotalPoints DECIMAL(10,2),
Rank INT
);

CREATE TABLE ContestLeaderBoard
(
CLB_ID INT PRIMARY KEY IDENTITY(1,1),
PlayerID INT REFERENCES [User](UserID),
ContestID INT REFERENCES Contest(Contest_ID),
TotalPoints DECIMAL(10,2),
Rank INT
);

CREATE TABLE LeaderboardExportHistory
(
ExportID INT PRIMARY KEY IDENTITY(1,1),
GameID INT NULL,
ContestID INT NULL,
FileLocation VARCHAR(500),
ExportedAt DATETIME
);

CREATE TABLE ApplicationLog
(
LogID INT PRIMARY KEY IDENTITY(1,1),
LogType VARCHAR(20),
[Message] VARCHAR(1000),
CreatedAt DATETIME
);








