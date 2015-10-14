-- --------------------------------------------------------------------------------
-- Name: Levi Harrington
-- Abstract: Teams and Players
-- --------------------------------------------------------------------------------


-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
USE dbTeamsAndPlayers		-- Get out of the dbSQL1 database
SET NOCOUNT ON			-- Report only errors



-- --------------------------------------------------------------------------------
-- Drops
-- --------------------------------------------------------------------------------
IF OBJECT_ID( 'TTeamPlayers' )							IS NOT NULL DROP TABLE TTeamPlayers
IF OBJECT_ID( 'TTeams' )							IS NOT NULL DROP TABLE TTeams
IF OBJECT_ID( 'TPlayers' )							IS NOT NULL DROP TABLE TPlayers
IF OBJECT_ID( 'TTeamStatuses' )							IS NOT NULL DROP TABLE TTeamStatuses
IF OBJECT_ID( 'TPlayerStatuses' )						IS NOT NULL DROP TABLE TPlayerStatuses
IF OBJECT_ID( 'TSexes' )							IS NOT NULL DROP TABLE TSexes
IF OBJECT_ID( 'TStates' )							IS NOT NULL DROP TABLE TStates

IF OBJECT_ID( 'VActiveTeams' )							IS NOT NULL DROP VIEW VActiveTeams
IF OBJECT_ID( 'VActivePlayers' )						IS NOT NULL DROP VIEW VActivePlayers
IF OBJECT_ID( 'VInActiveTeams' )						IS NOT NULL DROP VIEW VInActiveTeams
IF OBJECT_ID( 'VInActivePlayers' )						IS NOT NULL DROP VIEW VInActivePlayers

IF OBJECT_ID( 'uspSetTeamStatus' )						IS NOT NULL DROP PROCEDURE uspSetTeamStatus
IF OBJECT_ID( 'uspAddTeam' )							IS NOT NULL DROP PROCEDURE uspAddTeam
IF OBJECT_ID( 'uspEditTeam' )							IS NOT NULL DROP PROCEDURE uspEditTeam
IF OBJECT_ID( 'uspSetPlayerStatus' )						IS NOT NULL DROP PROCEDURE uspSetPlayerStatus
IF OBJECT_ID( 'uspAddPlayer' )							IS NOT NULL DROP PROCEDURE uspAddPlayer
IF OBJECT_ID( 'uspEditPlayer' )							IS NOT NULL DROP PROCEDURE uspEditPlayer
IF OBJECT_ID( 'uspAddTeamPlayer' )						IS NOT NULL DROP PROCEDURE uspAddTeamPlayer
IF OBJECT_ID( 'uspRemoveTeamPlayer' )						IS NOT NULL DROP PROCEDURE uspRemoveTeamPlayer



-- --------------------------------------------------------------------------------
-- Create Tables
-- --------------------------------------------------------------------------------
CREATE TABLE TTeams
(
	 intTeamID					INTEGER			NOT NULL
	,strTeam					VARCHAR(50)		NOT NULL
	,strMascot					VARCHAR(50)		NOT NULL
	,intTeamStatusID				INTEGER			NOT NULL
	,CONSTRAINT TTeams_PK PRIMARY KEY ( intTeamID )
)

CREATE TABLE TPlayers
(
	 intPlayerID					INTEGER			NOT NULL
	,strFirstName					VARCHAR(50)		NOT NULL
	,strMiddleName					VARCHAR(50)		NOT NULL
	,strLastName					VARCHAR(50)		NOT NULL
	,strStreetAddress				VARCHAR(50)		NOT NULL
	,strCity					VARCHAR(50)		NOT NULL
	,intStateID					INTEGER			NOT NULL
	,strZipCode					VARCHAR(50)		NOT NULL
	,strHomePhoneNumber				VARCHAR(50)		NOT NULL
	,monSalary					MONEY			NOT NULL
	,dtmDateOfBirth					DATETIME		NOT NULL
	,intSexID					INTEGER			NOT NULL
	,blnMostValuablePlayer				BIT			NOT NULL
	,strEmailAddress				VARCHAR(50)		NOT NULL
	,intPlayerStatusID				INTEGER			NOT NULL
	,CONSTRAINT TPlayers_PK PRIMARY KEY ( intPlayerID )	
)

CREATE TABLE TTeamPlayers
(
	 intTeamID					INTEGER			NOT NULL
	,intPlayerID					INTEGER			NOT NULL
	,CONSTRAINT TTeamPlayers_PK PRIMARY KEY ( intTeamID, intPlayerID )
)

CREATE TABLE TStates
(
	 intStateID					INTEGER			NOT NULL
	,strState					VARCHAR(50)		NOT NULL
	,strStateAbbreviation				VARCHAR(50)		NOT NULL
	,CONSTRAINT TStates_PK PRIMARY KEY ( intStateID )
)

CREATE TABLE TSexes
(
	 intSexID					INTEGER			NOT NULL
	,strSex						VARCHAR(50)		NOT NULL
	,CONSTRAINT TSexes_PK PRIMARY KEY ( intSexID )
)

CREATE TABLE TTeamStatuses
(
	 intTeamStatusID				INTEGER			NOT NULL
	,strTeamStatus					VARCHAR(50)		NOT NULL
	,CONSTRAINT TTeamStatuses_PK PRIMARY KEY ( intTeamStatusID )
)

CREATE TABLE TPlayerStatuses
(
	 intPlayerStatusID				INTEGER			NOT NULL
	,strPlayerStatus				VARCHAR(50)		NOT NULL
	,CONSTRAINT TPlayerStatuses_PK PRIMARY KEY ( intPlayerStatusID )
)



-- --------------------------------------------------------------------------------
-- Identify and Create Foreign Keys
-- --------------------------------------------------------------------------------
--#		Child						Parent					Column(s)
--		------						-------					---------
--1		TTeams						TTeamStatuses				intTeamStatusID
--2		TPlayers					TStates					intStateID
--3		TPlayers					TSexes					intSexID
--4		TPlayers					TPlayerStatuses				intPlayerStatusID
--5		TTeamPlayers					TTeams					intTeamID
--6		TTeamPlayers					TPlayers				intPlayerID

--1
ALTER TABLE TTeams ADD CONSTRAINT TTeams_TTeamStatuses_FK
	FOREIGN KEY (intTeamStatusID) REFERENCES TTeamStatuses (intTeamStatusID)

--2
ALTER TABLE TPlayers ADD CONSTRAINT TPlayers_TStates_FK
	FOREIGN KEY (intStateID) REFERENCES TStates (intStateID)

--3
ALTER TABLE TPlayers ADD CONSTRAINT TPlayers_TSexes_FK
	FOREIGN KEY (intSexID) REFERENCES TSexes (intSexID)

--4
ALTER TABLE TPlayers ADD CONSTRAINT TPlayers_TPlayerStatuses_FK
	FOREIGN KEY (intPlayerStatusID) REFERENCES TPlayerStatuses (intPlayerStatusID)

--5
ALTER TABLE TTeamPlayers ADD CONSTRAINT TTeamPlayers_TTeams_FK
	FOREIGN KEY (intTeamID) REFERENCES TTeams (intTeamID)

--6
ALTER TABLE TTeamPlayers ADD CONSTRAINT TTeamPlayers_TPlayers_FK
	FOREIGN KEY (intPlayerID) REFERENCES TPlayers (intPlayerID)



-- --------------------------------------------------------------------------------
-- Add records to each table
-- --------------------------------------------------------------------------------
INSERT INTO TTeamStatuses (intTeamStatusID, strTeamStatus)
VALUES   ( 1, 'Active')
	,( 2, 'Inactive')

INSERT INTO TPlayerStatuses (intPlayerStatusID, strPlayerStatus)
VALUES	 ( 1, 'Active')
	,( 2, 'Inactive')

INSERT INTO TSexes (intSexID, strSex)
VALUES	 ( 1, 'Female')
	,( 2, 'Male')

INSERT INTO TTeams (intTeamID, strTeam, strMascot, intTeamStatusID)
VALUES	 ( 1, 'Cincy', 'Walking Carpets', 1 )
	,( 2, 'Columbus', 'Nerf Herders', 1 )
	,( 3, 'Cleveland', 'Banthas', 1 )
	,( 4, 'Detroit', 'Droids', 2 )

INSERT INTO TStates (intStateID, strState, strStateAbbreviation)
VALUES   ( 1,	'Alabama',		'AR' )
	,( 2,	'Alaska',		'AK' )
	,( 3,	'Arizona',		'AZ' )
	,( 4,	'Arkansas',		'AR' )
	,( 5,	'California',		'CA' )
	,( 6,	'Colorado',		'CO' )
	,( 7,	'Connecticut',		'CT' )
	,( 8,	'Delaware',		'DE' )
	,( 9,	'Florida',		'FL' )
	,( 10,	'Georgia',		'GA' )
	,( 11,	'Hawaii',		'HI' )
	,( 12,	'Idaho',		'ID' )
	,( 13,	'Illinois',		'IL' )
	,( 14,	'Indiana',		'IN' )
	,( 15,	'Iowa',			'IA' )
	,( 16,	'Kansas',		'KS' )
	,( 17,	'Kentucky',		'KY' )
	,( 18,	'Louisiana',		'LA' )
	,( 19,	'Maine',		'ME' )
	,( 20,	'Maryland',		'MD' )
	,( 21,	'Massachusetts',	'MA' )
	,( 22,	'Michigan',		'MI' )
	,( 23,	'Minnesota',		'MN' )
	,( 24,	'Mississippi',		'MS' )
	,( 25,	'Missouri',		'MO' )
	,( 26,	'Montana',		'MT' )
	,( 27,	'Nebraska',		'NE' )
	,( 28,	'Nevada',		'NV' )
	,( 29,	'New Hampshire',	'NH' )
	,( 30,	'New Jersey',		'NJ' )
	,( 31,	'New Mexico',		'NM' )
	,( 32,	'New York',		'NY' )
	,( 33,	'North Carolina',	'NC' )
	,( 34,	'North Dakota',		'ND' )
	,( 35,	'Ohio',			'OH' )
	,( 36,	'Oklahoma',		'OK' )
	,( 37,	'Oregon',		'OR' )
	,( 38,	'Pennsylvania',		'PA' )
	,( 39,	'Rhode Island',		'RI' )
	,( 40,	'South Carolina',	'SC' )
	,( 41,	'South Dakota',		'SD' )
	,( 42,	'Tennessee',		'TN' )
	,( 43,	'Texas',		'TX' )
	,( 44,	'Utah',			'UT' )
	,( 45,	'Vermont',		'VT' )
	,( 46,	'Virginia',		'VA' )
	,( 47,	'Washington',		'WA' )
	,( 48,	'West Virginia',	'WV' )
	,( 49,	'Wisconsin',		'WI' )
	,( 50,	'Wyoming',		'WY' )
	,( 51,	'American Samoa',	'AS' )
	,( 52,	'District of Columbia',	'DC' )
	,( 53,	'Federated States of Micronesia',	'FM' )
	,( 54,	'Guam',			'GU' )
	,( 55,	'Marshall Islands',	'MH' )
	,( 56,	'Northern Mariana Islands',		'MP' )
	,( 57,	'Palau',		'PW' )
	,( 58,	'Puerto Rico',		'PR' )
	,( 59,	'Virgin Islands',	'VI' )

INSERT INTO TPlayers (intPlayerID, strFirstName, strMiddleName, strLastName, strStreetAddress, strCity, intStateID, strZipCode, strHomePhoneNumber, monSalary, dtmDateOfBirth, intSexID, blnMostValuablePlayer, strEmailAddress, intPlayerStatusID)
VALUES	 ( 1, 'Han', 'Middle', 'Solo', '123 Somewhere Ave', 'Cbus', 3, '11111', '111-1111', 100000, '1990-01-01', 2, 1, 'han@solo.com', 1 )
	,( 2, 'Chew', 'Something', 'Bacca', '000 Kashyyyk Rd.', 'Cincy', 33, '22222', '222-2222', 70000, '1950-02-02', 2, 0, 'chew@bacca.com', 1 )
	,( 3, 'Mara', '', 'Jade', '1 Emperor Way', 'Cleveland', 23, '33333', '333-3333', 123456, '1970-03-03', 1, 1, 'mara@jade.com', 1 )
	,( 4, 'Luke', 'Ben', 'Skywalker', '1 Endor Place', 'Detroit', 55, '44444', '444-4444', 444444, '1980-04-04', 2, 0, 'sky@walker.com', 2 )

INSERT INTO TTeamPlayers (intTeamID, intPlayerID)
VALUES	 ( 1, 1 )
	,( 1, 2 )
	,( 1, 3 )
	,( 2, 1 )
	,( 2, 3 )



-- --------------------------------------------------------------------------------
-- 
--                                     VIEWS
--
-- --------------------------------------------------------------------------------

-- --------------------------------------------------------------------------------
-- Name: VActiveTeams
-- Abstract: Show all active teams
-- --------------------------------------------------------------------------------
GO
CREATE VIEW VActiveTeams
AS
SELECT
	 TT.intTeamID
	,TT.strTeam
	,TT.strMascot
FROM
	TTeams		AS TT
WHERE
	intTeamStatusID = 1

GO

-- SELECT * FROM VActiveTeams



-- --------------------------------------------------------------------------------
-- Name: VInActiveTeams
-- Abstract: Show all inactive teams
-- --------------------------------------------------------------------------------
GO
CREATE VIEW VInActiveTeams
AS
SELECT
	 TT.intTeamID
	,TT.strTeam
	,TT.strMascot
FROM
	TTeams		AS TT
WHERE
	intTeamStatusID = 2

GO

-- SELECT * FROM VInActiveTeams



-- --------------------------------------------------------------------------------
-- Name: VActivePlayers
-- Abstract: Show all active players
-- --------------------------------------------------------------------------------
GO
CREATE VIEW VActivePlayers
AS
SELECT
	 TP.intPlayerID
	,TP.strFirstName
	,TP.strMiddleName
	,TP.strLastName
	,TP.strStreetAddress
	,TP.strCity
	,TP.intStateID
	,TP.strZipCode
	,TP.strHomePhoneNumber
	,TP.monSalary
	,TP.dtmDateOfBirth
	,TP.intSexID
	,TP.blnMostValuablePlayer
	,TP.strEmailAddress
	,TP.intPlayerStatusID
FROM
	TPlayers	AS TP
WHERE
	TP.intPlayerStatusID = 1

GO

-- SELECT * FROM VActivePlayers



-- --------------------------------------------------------------------------------
-- Name: VInActivePlayers
-- Abstract: Show all inactive players
-- --------------------------------------------------------------------------------
GO
CREATE VIEW VInActivePlayers
AS
SELECT
	 TP.intPlayerID
	,TP.strFirstName
	,TP.strMiddleName
	,TP.strLastName
	,TP.strStreetAddress
	,TP.strCity
	,TP.intStateID
	,TP.strZipCode
	,TP.strHomePhoneNumber
	,TP.monSalary
	,TP.dtmDateOfBirth
	,TP.intSexID
	,TP.blnMostValuablePlayer
	,TP.strEmailAddress
	,TP.intPlayerStatusID
FROM
	TPlayers	AS TP
WHERE
	TP.intPlayerStatusID = 2

GO

-- SELECT * FROM VInActivePlayers



-- --------------------------------------------------------------------------------
-- 
--                              STORED PROCEDURES
--
-- --------------------------------------------------------------------------------

-- --------------------------------------------------------------------------------
-- Name: uspSetTeamStatus
-- Abstract: Set team status to active or inactive
-- --------------------------------------------------------------------------------
GO

CREATE PROCEDURE uspSetTeamStatus
	 @intTeamID			AS INTEGER
	,@intTeamStatusID	AS INTEGER
AS
SET NOCOUNT ON		-- Report only errors
SET XACT_ABORT ON	-- Terminate and rollback entire transaction on error

UPDATE
	TTeams
SET
	intTeamStatusID = @intTeamStatusID
WHERE
	intTeamID = @intTeamID
	
GO

-- Testing, Testing, 1 2 3

 --uspSetTeamStatus 1, 2

 --GO

 --SELECT * FROM VActiveTeams



-- --------------------------------------------------------------------------------
-- Name: uspAddTeam
-- Abstract: Add a team
-- --------------------------------------------------------------------------------
GO

CREATE PROCEDURE uspAddTeam
	 @strTeam	AS VARCHAR( 50 )
	,@strMascot	AS VARCHAR( 50 )
AS
SET NOCOUNT ON	-- Report only errors
SET XACT_ABORT ON	-- Terminate and rollback entire transaction on error

DECLARE @intTeamID AS INTEGER
DECLARE @blnAlreadyExists AS BIT = 0	 -- False, does not exist 
 
BEGIN TRANSACTION
	
	SELECT
		 @blnAlreadyExists = 1
		,@intTeamID = intTeamID
	FROM
		TTeams (TABLOCKX)	-- Lock table until end of transaction
	WHERE 	
			strTeam = @strTeam
		AND strMascot = @strMascot

	IF @blnAlreadyExists = 0
	BEGIN
		SELECT @intTeamID = MAX( intTeamID ) + 1
		FROM TTeams (TABLOCKX) -- Lock table until end of transaction
 
		-- Default to 1 if table is empty
		SELECT @intTeamID  = COALESCE( @intTeamID , 1 )

		INSERT INTO TTeams( intTeamID, strTeam, strMascot, intTeamStatusID )
		VALUES( @intTeamID , @strTeam, @strMascot, 1 )
 
	END

	IF @blnAlreadyExists = 1
	BEGIN
		EXECUTE uspSetTeamStatus @intTeamID, 1
	END
 
	-- Return ID to caller
	SELECT @intTeamID AS intTeamID
	
COMMIT TRANSACTION


-- Testing, Testing, 1 2 3

 --GO
 --uspAddTeam 'Dayton', 'Dingleberries'
 --GO
 --SELECT * FROM TTeams


-- --------------------------------------------------------------------------------
-- Name: uspEditTeam
-- Abstract: Edit existing team
-- --------------------------------------------------------------------------------
GO

CREATE PROCEDURE uspEditTeam
	 @intTeamID			AS INTEGER
	,@strTeam			AS VARCHAR(50)
	,@strMascot			AS VARCHAR(50)
AS
SET NOCOUNT ON		-- Report only errors
SET XACT_ABORT ON	-- Terminate and rollback entire transaction on error

BEGIN TRANSACTION
	
	-- Update the record
	UPDATE
		TTeams
	SET
		 strTeam	= @strTeam
		,strMascot	= @strMascot
	WHERE
		 intTeamID		= @intTeamID

COMMIT TRANSACTION

-- Testing, Testing, 1 2 3

 --GO
 --uspEditTeam 1, 'Dayton', 'Dingleberries'
 --GO
 --SELECT * FROM TTeams



-- --------------------------------------------------------------------------------
-- Name: uspSetPlayerStatus
-- Abstract: Set player status to active or inactive
-- --------------------------------------------------------------------------------
GO

CREATE PROCEDURE uspSetPlayerStatus
	 @intPlayerID		AS INTEGER
	,@intPlayerStatusID	AS INTEGER
AS
SET NOCOUNT ON		-- Report only errors
SET XACT_ABORT ON	-- Terminate and rollback entire transaction on error

UPDATE
	TPlayers
SET
	intPlayerStatusID = @intPlayerStatusID
WHERE
	intPlayerID = @intPlayerID
	
GO

-- Testing, Testing, 1 2 3

-- uspSetPlayerStatus 1, 2

-- GO

-- SELECT * FROM VActivePlayers



-- --------------------------------------------------------------------------------
-- Name: uspAddPlayer
-- Abstract: Add a player
-- --------------------------------------------------------------------------------
GO

CREATE PROCEDURE uspAddPlayer
	 @strFirstName				VARCHAR(50)
	,@strMiddleName				VARCHAR(50)
	,@strLastName				VARCHAR(50)
	,@strStreetAddress			VARCHAR(50)
	,@strCity				VARCHAR(50)
	,@intStateID				VARCHAR(50)
	,@strZipCode				VARCHAR(50)
	,@strHomePhoneNumber			VARCHAR(50)
	,@monSalary				MONEY
	,@dtmDateOfBirth			DATETIME
	,@intSexID				INTEGER
	,@blnMostValuablePlayer			BIT
	,@strEmailAddress			VARCHAR(50)
AS
SET NOCOUNT ON		-- Report only errors
SET XACT_ABORT ON	-- Terminate and rollback entire transaction on error

DECLARE @intPlayerID AS INTEGER
 
BEGIN TRANSACTION
	
	SELECT @intPlayerID = MAX( intPlayerID ) + 1
	FROM TPlayers (TABLOCKX) -- Lock table until end of transaction
 
	-- Default to 1 if table is empty
	SELECT @intPlayerID  = COALESCE( @intPlayerID , 1 )

	INSERT INTO TPlayers(intPlayerID, strFirstName, strMiddleName, strLastName, strStreetAddress, strCity, intStateID, strZipCode, strHomePhoneNumber, monSalary, dtmDateOfBirth, intSexID, blnMostValuablePlayer, strEmailAddress, intPlayerStatusID)
	VALUES( @intPlayerID , @strFirstName, @strMiddleName, @strLastName, @strStreetAddress, @strCity, @intStateID, @strZipCode, @strHomePhoneNumber, @monSalary, @dtmDateOfBirth, @intSexID, @blnMostValuablePlayer, @strEmailAddress, 1 )
 
	-- Return ID to caller
	SELECT @intPlayerID AS intPlayerID
	
COMMIT TRANSACTION


-- --------------------------------------------------------------------------------
-- Name: uspEditPlayer
-- Abstract: Edit existing player
-- --------------------------------------------------------------------------------
GO

CREATE PROCEDURE uspEditPlayer
	 @intPlayerID				INTEGER
	,@strFirstName				VARCHAR(50)
	,@strMiddleName				VARCHAR(50)
	,@strLastName				VARCHAR(50)
	,@strStreetAddress			VARCHAR(50)
	,@strCity				VARCHAR(50)
	,@intStateID				VARCHAR(50)
	,@strZipCode				VARCHAR(50)
	,@strHomePhoneNumber			VARCHAR(50)
	,@monSalary				MONEY
	,@dtmDateOfBirth			DATETIME
	,@intSexID				INTEGER
	,@blnMostValuablePlayer			BIT
	,@strEmailAddress			VARCHAR(50)
AS
SET NOCOUNT ON		-- Report only errors
SET XACT_ABORT ON	-- Terminate and rollback entire transaction on error

BEGIN TRANSACTION
	
	-- Update the record
	UPDATE
		TPlayers
	SET
		 strFirstName			= @strFirstName
		,strMiddleName			= @strMiddleName
		,strLastName			= @strLastName
		,strStreetAddress		= @strStreetAddress
		,strCity			= @strCity
		,intStateID			= @intStateID
		,strZipCode			= @strZipCode
		,strHomePhoneNumber		= @strHomePhoneNumber
		,monSalary			= @monSalary
		,dtmDateOfBirth			= @dtmDateOfBirth
		,intSexID			= @intSexID
		,blnMostValuablePlayer		= @blnMostValuablePlayer
		,strEmailAddress		= @strEmailAddress
	WHERE
		 intPlayerID			= @intPlayerID

COMMIT TRANSACTION



-- --------------------------------------------------------------------------------
-- Name: uspAddTeamPlayer
-- Abstract: Assign player to a team
-- --------------------------------------------------------------------------------
GO

CREATE PROCEDURE uspAddTeamPlayer
	 @intTeamID		AS INTEGER
	,@intPlayerID		AS INTEGER
AS
SET NOCOUNT ON		-- Report only errors
SET XACT_ABORT ON	-- Terminate and rollback entire transaction on error

DECLARE @blnAlreadyExists AS BIT = 0	 -- False, does not exist 
 
BEGIN TRANSACTION
	
	SELECT
		 @blnAlreadyExists = 1
	FROM
		TTeamPlayers (TABLOCKX)	-- Lock table until end of transaction
	WHERE 	
		    intTeamID   = @intTeamID
		AND intPlayerID = @intPlayerID

	IF @blnAlreadyExists = 0
	BEGIN

		INSERT INTO TTeamPlayers( intTeamID, intPlayerID )
		VALUES( @intTeamID , @intPlayerID )
 
	END

COMMIT TRANSACTION



-- --------------------------------------------------------------------------------
-- Name: uspRemoveTeamPlayer
-- Abstract: Remove player from team
-- --------------------------------------------------------------------------------
GO

CREATE PROCEDURE uspRemoveTeamPlayer
	 @intTeamID		AS INTEGER
	,@intPlayerID		AS INTEGER
AS
SET NOCOUNT ON		-- Report only errors
SET XACT_ABORT ON	-- Terminate and rollback entire transaction on error

DECLARE @blnAlreadyExists AS BIT = 0	 -- False, does not exist 
 
BEGIN TRANSACTION

	DELETE FROM
		TTeamPlayers
	WHERE
		    intTeamID   = @intTeamID
		AND intPlayerID = @intPlayerID
 	
COMMIT TRANSACTION
