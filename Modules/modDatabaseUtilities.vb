' -------------------------------------------------------------------------
' Module: modDatabaseUtilites
' Abstract: Database connection and disconnection
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On


' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------
Imports System
Imports System.IO



Public Module modDatabaseUtilities


    ' -------------------------------------------------------------------------
    '  Module variables
    ' -------------------------------------------------------------------------

    ' Access Connection string (notice the use of the relative path)                           
    Private m_strDatabaseConnectionStringMSAccess As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                                                              "Data Source=" & Application.StartupPath & "..\..\..\Database\TeamsAndPlayers3.mdb;" & _
                                                              "User ID=Admin;" & _
                                                              "Password=;"

    ' SQL Server Connection string with integrated login v1
    Private m_strDatabaseConnectionStringSQLServerV1 As String = "Provider=SQLOLEDB;" & _
                                                                 "Server=(Local);" & _
                                                                 "Database=dbTeamsAndPlayers;" & _
                                                                 "Integrated Security=SSPI;"

    ' SQL Server Connection string with integrated login v2
    Private m_strDatabaseConnectionStringSQLServerV2 As String = "Provider=SQLOLEDB;" & _
                                                                 "Server=(Local);" & _
                                                                 "Database=dbTeamsAndPlayers;" & _
                                                                 "Trusted_Connection=True;"


    Private m_conAdministrator As OleDb.OleDbConnection



    ' --------------------------------------------------------------------------------
    ' Name: OpenDatabaseConnectionMSAccess
    ' Abstract: Open a connection to the database.
    '           In a 2-Tier (client server) application we connect once in FMain
    '           and hold the connection open until FMain closes
    '
    '           *********** READ ME ***********
    '           
    '           For MS Access on Windows Vista/7/8 you must set the target CPU to "x86"
    '           under "Project/Properties/Compiler/Advanced Compiler Options/Target CPU"
    ' --------------------------------------------------------------------------------
    Public Function OpenDatabaseConnectionMSAccess() As Boolean

        Dim blnResult As Boolean = False

        Try

            ' Open a connection to the database
            m_conAdministrator = New OleDb.OleDbConnection
            m_conAdministrator.ConnectionString = m_strDatabaseConnectionStringMSAccess
            m_conAdministrator.Open()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

            MsgBox("Unable to connect to the database!" & vbNewLine & _
                    "The application will now close." & vbNewLine & _
                    vbNewLine & _
                    "See modDatabaseUtilities.OpenDatabaseConnection for more details", _
                    MessageBoxButtons.OK, _
                    MessageBoxIcon.Information)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: OpenDatabaseConnectionSQLServer
    ' Abstract: Open a connection to the database.
    '           In 2-tier (client-server) application we connect once in FMain
    '           and hold the connection open until FMain closes
    ' --------------------------------------------------------------------------------
    Public Function OpenDatabaseConnectionSQLServer() As Boolean

        Dim blnResult As Boolean = False

        Try

            ' Open a connection to the database
            m_conAdministrator = New OleDb.OleDbConnection
            m_conAdministrator.ConnectionString = m_strDatabaseConnectionStringSQLServerV1
            m_conAdministrator.Open()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

            MsgBox("Unable to connect to the database!" & vbNewLine & _
                    "The application will now close." & vbNewLine & _
                    vbNewLine & _
                    "See modDatabaseUtilities.OpenDatabaseConnection for more details", _
                    MessageBoxButtons.OK, _
                    MessageBoxIcon.Information)

        End Try

        Return blnResult

    End Function


    ' --------------------------------------------------------------------------------
    ' Name: CloseDatabaseConnection
    ' Abstract: If the database connection is open then close it.
    ' --------------------------------------------------------------------------------
    Public Function CloseDatabaseConnection() As Boolean

        Dim blnResult As Boolean = False

        Try

            ' Anything there?
            If m_conAdministrator IsNot Nothing Then

                ' Open?
                If m_conAdministrator.State <> ConnectionState.Closed Then

                    ' Yes, close it
                    m_conAdministrator.Close()

                End If

                ' Clean up
                m_conAdministrator = Nothing

            End If

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: LoadListBoxFromDatabase
    ' Abstract: Get all the teams from the table
    ' --------------------------------------------------------------------------------
    Public Function LoadListBoxFromDatabase(ByVal strTable As String, _
                                            ByVal strPrimaryKey As String, _
                                            ByVal strNameColumn As String, _
                                            ByRef lstTarget As ListBox, _
                                   Optional ByVal strSortColumn As String = "", _
                                   Optional ByVal strCustomSQL As String = "") As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strSelect As String = ""
            Dim cmdSelect As OleDb.OleDbCommand
            Dim drSourceTable As OleDb.OleDbDataReader
            Dim liNewItem As CListItem
            Dim intID As Integer
            Dim strName As String

            ' Show changes all at once at the end (faster)
            lstTarget.BeginUpdate()

            ' Clear listbox
            lstTarget.Items.Clear()

            ' Build the select statement
            strSelect = BuildSelectStatement(strTable, strPrimaryKey, _
                                             strNameColumn, strSortColumn, _
                                             strCustomSQL)

            ' Wrap a command object around the select statement
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)

            ' Retrieve all the records
            drSourceTable = cmdSelect.ExecuteReader

            ' Loop through records one at a time
            ' Each call to read moves to the next record
            Do While drSourceTable.Read = True

                ' Make a List Item item to hold the information
                intID = drSourceTable.Item(0)    ' Primary Key
                strName = drSourceTable.Item(1)  ' Name Column
                liNewItem = New CListItem(intID, strName)

                ' Add the item to the list
                lstTarget.Items.Add(liNewItem)

            Loop

            ' Select the first item in the list by default
            If lstTarget.Items.Count > 0 Then lstTarget.SelectedIndex = 0

            ' Show any changes
            lstTarget.EndUpdate()

            ' Clean up
            drSourceTable.Close()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: BuildSelectStatement
    ' Abstract: Build a select state for the table, ID, and Name column
    ' --------------------------------------------------------------------------------
    Private Function BuildSelectStatement(ByVal strTable As String, _
                                          ByVal strPrimaryKey As String, _
                                          ByVal strNameColumn As String, _
                                          ByVal strSortColumn As String, _
                                 Optional ByVal strCustomSQL As String = "") As String

        Dim strSelectStatement As String = ""

        Try

            ' Custom select statement?
            If strCustomSQL = "" Then

                ' No, so build one

                ' Sort by name column if nothing provided
                If strSortColumn = "" Then strSortColumn = strNameColumn

                ' Put it all together
                strSelectStatement = "SELECT " & _
                                            strPrimaryKey & ", " & strNameColumn & _
                                     " FROM " & _
                                            strTable & _
                                     " ORDER BY " & strSortColumn

            Else

                ' Yes, use it
                strSelectStatement = strCustomSQL

            End If

        Catch excError As Exception

            WriteLog(excError)

        End Try

        Return strSelectStatement

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: LoadComboBoxFromDatabase
    ' Abstract: Get all the states from TStates
    ' --------------------------------------------------------------------------------
    Public Function LoadComboBoxFromDatabase(ByVal strTableName As String, _
                                            ByVal strPrimaryKey As String, _
                                            ByVal strNameColumn As String, _
                                            ByRef lstTarget As ComboBox) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strSelect As String = ""
            Dim cmdSelect As OleDb.OleDbCommand
            Dim drSourceTable As OleDb.OleDbDataReader
            Dim liNewItem As CListItem
            Dim intID As Integer
            Dim strName As String

            ' Show changes all at once at the end (faster)
            lstTarget.BeginUpdate()

            ' Clear combobox
            lstTarget.Items.Clear()

            ' Build the select statement
            strSelect = "SELECT " & strPrimaryKey & ", " & strNameColumn & _
                        " FROM " & strTableName & _
                        " ORDER BY " & strNameColumn

            ' Retrieve all the records
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            ' Loop through records one at a time
            ' Each call to read moves to the next record
            Do While drSourceTable.Read = True

                ' Make a List Item item to hold the information
                intID = drSourceTable.Item(0)    ' Primary Key
                strName = drSourceTable.Item(1)  ' Name Column
                liNewItem = New CListItem(intID, strName)

                ' Add the item to combobox
                lstTarget.Items.Add(liNewItem)

            Loop

            ' Select the first item in the list by default
            If lstTarget.Items.Count > 0 Then lstTarget.SelectedIndex = 0

            ' Show any changes
            lstTarget.EndUpdate()

            ' Clean up
            drSourceTable.Close()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult
    End Function



    ' --------------------------------------------------------------------------------
    ' Name: GetNextHighestRecordID
    ' Abstract: Get the next highest ID from the table in the database
    '           Danger: Potential Race Condition
    '           Why do we have this? So we can see the mechanics of how everything works.
    ' --------------------------------------------------------------------------------
    Public Function GetNextHighestRecordID(ByVal strPrimaryKey As String, _
                                           ByVal strTable As String, _
                                           ByRef intNextHighestRecordID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strSelect As String
            Dim cmdSelect As OleDb.OleDbCommand
            Dim drSourceTable As OleDb.OleDbDataReader

            strSelect = "SELECT MAX( " & strPrimaryKey & " ) + 1 AS intNextHighestRecordID " & _
                        " FROM " & strTable

            ' Execute command
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            ' Read result (highest ID)
            drSourceTable.Read()

            ' Null? (empty table)
            If drSourceTable.IsDBNull(0) = True Then

                ' Yes, start numbering at 1
                intNextHighestRecordID = 1
            Else

                ' No, get the next highest ID
                intNextHighestRecordID = drSourceTable.Item(0)

            End If

            ' Clean up
            drSourceTable.Close()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: DeleteRecordsFromTable
    ' Abstract: Delete all records from table that match the ID
    ' --------------------------------------------------------------------------------
    Private Function DeleteRecordsFromTable(ByVal intRecordID As Integer, _
                                            ByVal strPrimaryKey As String, _
                                            ByVal strTable As String, _
                                   Optional ByVal strCustomSQL As String = "") As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strDeleteStatement As String
            Dim cmdDelete As OleDb.OleDbCommand
            Dim intRowsAffected As Integer = 0


            If strCustomSQL = "" Then

                ' Build the SQL String
                strDeleteStatement = "DELETE FROM " & strTable & _
                                     " WHERE " & strPrimaryKey & " = " & intRecordID

            Else

                strDeleteStatement = strCustomSQL

            End If

            ' Delete the record(s)
            cmdDelete = New OleDb.OleDbCommand(strDeleteStatement, m_conAdministrator)
            intRowsAffected = cmdDelete.ExecuteNonQuery

            ' Did it work?
            If intRowsAffected > 0 Then

                ' Yes, success
                blnResult = True

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function


#Region "TTeams"

    ' --------------------------------------------------------------------------------
    ' Name: AddTeamToDatabase
    ' Abstract: Add the team to the database
    ' --------------------------------------------------------------------------------
    Public Function AddTeamToDatabase(ByRef udtTeam As udtTeamType) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strInsert As String
            Dim cmdInsert As OleDb.OleDbCommand

            ' Get the next highest team ID
            ' Race condition. Need an atomic action but not possible in access.
            If GetNextHighestRecordID("intTeamID", "TTeams", udtTeam.intTeamID) = True Then

                ' Build the INSERT command. Never build command with raw user input
                ' to prevent SQL injection.
                strInsert = "INSERT INTO TTeams ( intTeamID, strTeam, strMascot, intTeamStatusID )" & _
                            " VALUES ( ?, ?, ?, ? )"

                ' Make the command instance
                cmdInsert = New OleDb.OleDbCommand(strInsert, m_conAdministrator)

                ' Add column values here instead of above to prevent SQL injection
                With cmdInsert.Parameters

                    .AddWithValue("1", udtTeam.intTeamID)
                    .AddWithValue("2", udtTeam.strTeam)
                    .AddWithValue("3", udtTeam.strMascot)
                    .AddWithValue("4", modConstants.intTEAM_STATUS_ACTIVE)

                End With

                ' Insert the row
                cmdInsert.ExecuteNonQuery()

                ' Success
                blnResult = True

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: AddTeamToDatabase2
    ' Abstract: How to add a record using a stored procedure that returns
    '           the record ID. Use this for SQL Server
    '
    '           Advantages :
    '           1) There is only one back and forth from the code to the database.
    '           2) Using a stored procedure takes much of the SQL out of our
    '              code and puts it in the database.
    '           3) Stored procedures are guaranteed to be syntactically correct
    '              once created (unless you are doing dynamic queries)
    '           4) Stored procedures are pre-compiled (after first run and then cached)
    '              so they execute as quickly as possible
    '           5) Once started the stored procedure is guaranteed to finish without
    '              any further input which is good becuase we will never have an
    '              uncommited transaction
    ' --------------------------------------------------------------------------------
    Public Function AddTeamToDatabase2(ByRef udtTeam As udtTeamType) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim cmdStoredProcedure As OleDb.OleDbCommand
            Dim drReturnValues As OleDb.OleDbDataReader

            ' Create sqlcommand object to run our stored procedure
            cmdStoredProcedure = New OleDb.OleDbCommand("uspAddTeam", m_conAdministrator)
            cmdStoredProcedure.CommandType = CommandType.StoredProcedure

            ' Add parameters
            With cmdStoredProcedure.Parameters
                .AddWithValue("1", udtTeam.strTeam)
                .AddWithValue("2", udtTeam.strMascot)
            End With

            ' Execute the stored procedure
            drReturnValues = cmdStoredProcedure.ExecuteReader()

            ' Should be 1 and only 1 record returned
            drReturnValues.Read()

            ' Get the new ID (could also use an output parameter)
            udtTeam.intTeamID = drReturnValues.Item("intTeamID")

            ' Clean up
            drReturnValues.Close()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function


    ' --------------------------------------------------------------------------------
    ' Name: GetTeamInformationFromDatabase
    ' Abstract: Get data for the specified team from the database
    ' --------------------------------------------------------------------------------
    Public Function GetTeamInformationFromDatabase(ByRef udtTeam As udtTeamType) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strSelect As String = ""
            Dim cmdSelect As New OleDb.OleDbCommand
            Dim drTTeams As OleDb.OleDbDataReader

            ' Build the select string
            strSelect = "SELECT *" & _
                        " FROM TTeams" & _
                        " WHERE intTeamID = " & udtTeam.intTeamID

            ' Retrieve the record
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drTTeams = cmdSelect.ExecuteReader

            ' Read (there should be 1 and only 1 row)
            drTTeams.Read()
            With drTTeams
                udtTeam.strTeam = .Item("strTeam")
                udtTeam.strMascot = .Item("strMascot")
            End With

            ' Clean up
            drTTeams.Close()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: EditTeamInDatabase
    ' Abstract: Edit the team in the database
    ' --------------------------------------------------------------------------------
    Public Function EditTeamInDatabase(ByVal udtTeam As udtTeamType) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strUpdate As String
            Dim cmdUpdate As OleDb.OleDbCommand
            Dim intRowsAffected As Integer

            ' Build the UPDATE command. Never build command with raw user input to prevent SQL injection
            strUpdate = "UPDATE TTeams" & _
                        " SET" & _
                        "    strTeam = ?" & _
                        "   ,strMascot = ?" & _
                        " WHERE" & _
                        "   intTeamID = ?"

            ' Make the command instance
            cmdUpdate = New OleDb.OleDbCommand(strUpdate, m_conAdministrator)

            ' Add column values here instead of above to prevent SQL injection attacks
            With cmdUpdate.Parameters

                .AddWithValue("1", udtTeam.strTeam)
                .AddWithValue("2", udtTeam.strMascot)
                .AddWithValue("3", udtTeam.intTeamID)

            End With

            ' Insert the row
            intRowsAffected = cmdUpdate.ExecuteNonQuery()

            ' Success?
            If intRowsAffected = 1 Then blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: EditTeamInDatabase2
    ' Abstract: Edit the team in the database
    ' --------------------------------------------------------------------------------
    Public Function EditTeamInDatabase2(ByVal udtTeam As udtTeamType) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim cmdStoredProcedure As OleDb.OleDbCommand

            ' Create sqlcommand object to run our stored procedure
            cmdStoredProcedure = New OleDb.OleDbCommand("uspEditTeam", m_conAdministrator)
            cmdStoredProcedure.CommandType = CommandType.StoredProcedure

            ' Add column values here instead of above to prevent SQL injection attacks
            With cmdStoredProcedure.Parameters
                .AddWithValue("1", udtTeam.intTeamID)
                .AddWithValue("2", udtTeam.strTeam)
                .AddWithValue("3", udtTeam.strMascot)
            End With

            ' Execute the stored procedure
            cmdStoredProcedure.ExecuteNonQuery()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function


    ' --------------------------------------------------------------------------------
    ' Name: DeleteTeamFromDatabase
    ' Abstract: Mark the team as inactive
    ' --------------------------------------------------------------------------------
    Public Function DeleteTeamFromDatabase(ByVal intTeamID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            blnResult = SetTeamStatusInDatabase2(intTeamID, intTEAM_STATUS_INACTIVE)

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: UndeleteTeamFromDatabase
    ' Abstract: Lazarus, come out?
    ' --------------------------------------------------------------------------------
    Public Function UndeleteTeamFromDatabase(ByVal intTeamID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            blnResult = SetTeamStatusInDatabase2(intTeamID, intTEAM_STATUS_ACTIVE)

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function


    ' --------------------------------------------------------------------------------
    ' Name: SetTeamStatusInDatabase
    ' Abstract: Mark the team as active or inactive
    ' --------------------------------------------------------------------------------
    Public Function SetTeamStatusInDatabase(ByVal intTeamID As Integer, _
                                            ByVal intTeamStatusID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strUpdate As String
            Dim cmdUpdate As OleDb.OleDbCommand
            Dim intRowsAffected As Integer

            ' Build the UPDATE command
            strUpdate = "UPDATE TTeams" & _
                        " SET" & _
                        "    intTeamStatusID = ?" & _
                        " WHERE" & _
                        "    intTeamID = ?"

            ' Make the command instance
            cmdUpdate = New OleDb.OleDbCommand(strUpdate, m_conAdministrator)

            ' Add column value
            With cmdUpdate.Parameters

                .AddWithValue("1", intTeamStatusID)
                .AddWithValue("2", intTeamID)

            End With

            ' Insert the row
            intRowsAffected = cmdUpdate.ExecuteNonQuery()

            ' Success?
            If intRowsAffected = 1 Then blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: SetTeamStatusInDatabase2
    ' Abstract: Mark the team as active or inactive
    ' --------------------------------------------------------------------------------
    Public Function SetTeamStatusInDatabase2(ByVal intTeamID As Integer, _
                                             ByVal intTeamStatusID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim cmdStoredProcedure As OleDb.OleDbCommand

            ' Create sqlcommand object to run our stored procedure
            cmdStoredProcedure = New OleDb.OleDbCommand("uspSetTeamStatus", m_conAdministrator)
            cmdStoredProcedure.CommandType = CommandType.StoredProcedure

            ' Add column values here instead of above to prevent SQL injection attacks
            With cmdStoredProcedure.Parameters
                .AddWithValue("1", intTeamID)
                .AddWithValue("2", intTeamStatusID)
            End With

            ' Execute the stored procedure
            cmdStoredProcedure.ExecuteNonQuery()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



#End Region



#Region "TTeamPlayers"

    ' --------------------------------------------------------------------------------
    ' Name: LoadListWithPlayersFromDatabase
    ' Abstract: Load all the players on/not on the specified team.
    ' --------------------------------------------------------------------------------
    Public Function LoadListWithPlayersFromDatabase(ByVal intTeamID As Integer, _
                                                    ByVal lstTarget As ListBox, _
                                                    ByVal blnPlayersOnTeam As Boolean) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strCustomSQL As String = ""
            Dim strNot As String = "NOT"

            ' Selected players?
            If blnPlayersOnTeam = True Then strNot = ""

            ' Build the Custom SQL statement. Load all the players that are/are not already on the team
            strCustomSQL = "SELECT " & _
                           "    intPlayerID, strLastName + ', ' + strFirstName " & _
                           " FROM " & _
                           "    VActivePlayers " & _
                           " WHERE intPlayerID " & strNot & " IN " & _
                           "    ( " & _
                           "      SELECT intPlayerID " & _
                           "      FROM TTeamPlayers " & _
                           "      WHERE intTeamID = " & intTeamID & _
                           "    ) " & _
                           " ORDER BY " & _
                           "    strLastName, strFirstName"

            blnResult = LoadListBoxFromDatabase("", "", "", lstTarget, "", strCustomSQL)

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: AddPlayerToTeamInDatabase
    ' Abstract: Add the player to the specified team
    ' --------------------------------------------------------------------------------
    Public Function AddPlayerToTeamInDatabase(ByVal intTeamID As Integer, _
                                              ByVal intPlayerID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strInsert As String
            Dim cmdInsert As OleDb.OleDbCommand

            ' Build the INSERT command
            strInsert = "INSERT INTO TTeamPlayers ( intTeamID, intPlayerID )" & _
                        " VALUES ( ?, ? )"

            ' Make the command instance
            cmdInsert = New OleDb.OleDbCommand(strInsert, m_conAdministrator)

            ' Add column values
            With cmdInsert.Parameters

                .AddWithValue("1", intTeamID)
                .AddWithValue("2", intPlayerID)

            End With

            ' Insert the row
            cmdInsert.ExecuteNonQuery()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: AddPlayerToTeamInDatabase2
    ' Abstract: Add the player to the specified team
    ' --------------------------------------------------------------------------------
    Public Function AddPlayerToTeamInDatabase2(ByVal intTeamID As Integer, _
                                              ByVal intPlayerID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim cmdStoredProcedure As OleDb.OleDbCommand

            ' Create sqlcommand object to run our stored procedure
            cmdStoredProcedure = New OleDb.OleDbCommand("uspAddTeamPlayer", m_conAdministrator)
            cmdStoredProcedure.CommandType = CommandType.StoredProcedure

            ' Add parameters
            With cmdStoredProcedure.Parameters
                .Add(New OleDb.OleDbParameter("@intTeamID", intTeamID))
                .Add(New OleDb.OleDbParameter("@intPlayerID", intPlayerID))
            End With

            ' Execute the stored procedure
            cmdStoredProcedure.ExecuteNonQuery()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: RemovePlayerFromTeamInDatabase
    ' Abstract: Remove the player from the specified team
    ' --------------------------------------------------------------------------------
    Public Function RemovePlayerFromTeamInDatabase(ByVal intTeamID As Integer, _
                                                   ByVal intPlayerID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strCustomSQL As String

            strCustomSQL = "DELETE FROM TTeamPlayers" & _
                           " WHERE intTeamID   = " & intTeamID & _
                           " AND   intPlayerID = " & intPlayerID

            blnResult = DeleteRecordsFromTable(0, "", "", strCustomSQL)

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: RemovePlayerFromTeamInDatabase2
    ' Abstract: Remove the player from the specified team
    ' --------------------------------------------------------------------------------
    Public Function RemovePlayerFromTeamInDatabase2(ByVal intTeamID As Integer, _
                                                    ByVal intPlayerID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim cmdStoredProcedure As OleDb.OleDbCommand

            ' Create sqlcommand object to run our stored procedure
            cmdStoredProcedure = New OleDb.OleDbCommand("uspRemoveTeamPlayer", m_conAdministrator)
            cmdStoredProcedure.CommandType = CommandType.StoredProcedure

            ' Add parameters
            With cmdStoredProcedure.Parameters
                .Add(New OleDb.OleDbParameter("@intTeamID", intTeamID))
                .Add(New OleDb.OleDbParameter("@intPlayerID", intPlayerID))
            End With

            ' Execute the stored procedure
            cmdStoredProcedure.ExecuteNonQuery()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function


#End Region



#Region "TPlayers"

    ' --------------------------------------------------------------------------------
    ' Name: AddPlayerToDatabase
    ' Abstract: Add the player to the database
    ' --------------------------------------------------------------------------------
    Public Function AddPlayerToDatabase(ByRef udtPlayer As udtPlayerType) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strInsert As String
            Dim cmdInsert As OleDb.OleDbCommand

            ' Get the next highest player ID
            ' Race condition. Need an atomic action but not possible in access.
            If GetNextHighestRecordID("intPlayerID", "TPlayers", udtPlayer.intPlayerID) = True Then

                ' Build the INSERT command. Never build command with raw user input
                ' to prevent SQL injection.
                strInsert = "INSERT INTO TPlayers ( intPlayerID, strFirstName, strMiddleName, strLastName, strStreetAddress, strCity, intStateID, strZipCode, strHomePhoneNumber, monSalary, dtmDateOfBirth, intSexID, blnMostValuablePlayer, strEmailAddress, intPlayerStatusID )" & _
                            " VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )"

                ' Make the command instance
                cmdInsert = New OleDb.OleDbCommand(strInsert, m_conAdministrator)

                ' Add column values here instead of above to prevent SQL injection
                With cmdInsert.Parameters

                    .AddWithValue("1", udtPlayer.intPlayerID)
                    .AddWithValue("2", udtPlayer.strFirstName)
                    .AddWithValue("3", udtPlayer.strMiddleName)
                    .AddWithValue("4", udtPlayer.strLastName)
                    .AddWithValue("5", udtPlayer.strStreetAddress)
                    .AddWithValue("6", udtPlayer.strCity)
                    .AddWithValue("7", udtPlayer.intStateID)
                    .AddWithValue("8", udtPlayer.strZipCode)
                    .AddWithValue("9", udtPlayer.strHomePhoneNumber)
                    .AddWithValue("10", udtPlayer.decSalary)
                    .AddWithValue("11", udtPlayer.dtmDateOfBirth)
                    .AddWithValue("12", udtPlayer.intSexID)
                    .AddWithValue("13", udtPlayer.blnMostValuablePlayer)
                    .AddWithValue("14", udtPlayer.strEmailAddress)
                    .AddWithValue("15", modConstants.intPLAYER_STATUS_ACTIVE)

                End With

                ' Insert the row
                cmdInsert.ExecuteNonQuery()

                ' Success
                blnResult = True

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: AddPlayerToDatabase2
    ' Abstract: Add the player to the database
    ' --------------------------------------------------------------------------------
    Public Function AddPlayerToDatabase2(ByRef udtPlayer As udtPlayerType) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim cmdStoredProcedure As OleDb.OleDbCommand
            Dim drReturnValues As OleDb.OleDbDataReader

            ' Create sqlcommand object to run our stored procedure
            cmdStoredProcedure = New OleDb.OleDbCommand("uspAddPlayer", m_conAdministrator)
            cmdStoredProcedure.CommandType = CommandType.StoredProcedure

            ' Add parameters
            With cmdStoredProcedure.Parameters
                .AddWithValue("1", udtPlayer.strFirstName)
                .AddWithValue("2", udtPlayer.strMiddleName)
                .AddWithValue("3", udtPlayer.strLastName)
                .AddWithValue("4", udtPlayer.strStreetAddress)
                .AddWithValue("5", udtPlayer.strCity)
                .AddWithValue("6", udtPlayer.intStateID)
                .AddWithValue("7", udtPlayer.strZipCode)
                .AddWithValue("8", udtPlayer.strHomePhoneNumber)
                .AddWithValue("9", udtPlayer.decSalary)
                .AddWithValue("10", udtPlayer.dtmDateOfBirth)
                .AddWithValue("11", udtPlayer.intSexID)
                .AddWithValue("12", udtPlayer.blnMostValuablePlayer)
                .AddWithValue("13", udtPlayer.strEmailAddress)
            End With

            ' Execute the stored procedure
            drReturnValues = cmdStoredProcedure.ExecuteReader()

            ' Should be 1 and only 1 record returned
            drReturnValues.Read()

            ' Get the new ID (could also use an output parameter)
            udtPlayer.intPlayerID = drReturnValues.Item("intPlayerID")

            ' Clean up
            drReturnValues.Close()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: GetPlayerInformationFromDatabase
    ' Abstract: Get data for the specified player from the database
    ' --------------------------------------------------------------------------------
    Public Function GetPlayerInformationFromDatabase(ByRef udtPlayer As udtPlayerType) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strSelect As String = ""
            Dim cmdSelect As New OleDb.OleDbCommand
            Dim drTPlayers As OleDb.OleDbDataReader

            ' Build the select string
            strSelect = "SELECT *" & _
                        " FROM TPlayers" & _
                        " WHERE intPlayerID = " & udtPlayer.intPlayerID

            ' Retrieve the record
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drTPlayers = cmdSelect.ExecuteReader

            ' Read (there should be 1 and only 1 row)
            drTPlayers.Read()
            With drTPlayers
                udtPlayer.strFirstName = .Item("strFirstName")
                udtPlayer.strMiddleName = .Item("strMiddleName")
                udtPlayer.strLastName = .Item("strLastName")
                udtPlayer.strStreetAddress = .Item("strStreetAddress")
                udtPlayer.strCity = .Item("strCity")
                udtPlayer.intStateID = .Item("intStateID")
                udtPlayer.strZipCode = .Item("strZipCode")
                udtPlayer.strHomePhoneNumber = .Item("strHomePhoneNumber")
                udtPlayer.decSalary = .Item("monSalary")
                udtPlayer.dtmDateOfBirth = .Item("dtmDateOfBirth")
                udtPlayer.intSexID = .Item("intSexID")
                udtPlayer.blnMostValuablePlayer = .Item("blnMostValuablePlayer")
                udtPlayer.strEmailAddress = .Item("strEmailAddress")
            End With

            ' Clean up
            drTPlayers.Close()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: EditPlayerInDatabase
    ' Abstract: Edit the player in the database
    ' --------------------------------------------------------------------------------
    Public Function EditPlayerInDatabase(ByVal udtPlayer As udtPlayerType) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strUpdate As String
            Dim cmdUpdate As OleDb.OleDbCommand
            Dim intRowsAffected As Integer

            ' Build the UPDATE command. Never build command with raw user input to prevent SQL injection
            strUpdate = "UPDATE TPlayers" & _
                        " SET" & _
                        "    strFirstName = ?" & _
                        "   ,strMiddleName = ?" & _
                        "   ,strLastName = ?" & _
                        "   ,strStreetAddress = ?" & _
                        "   ,strCity = ?" & _
                        "   ,intStateID = ?" & _
                        "   ,strZipCode = ?" & _
                        "   ,strHomePhoneNumber = ?" & _
                        "   ,monSalary = ?" & _
                        "   ,dtmDateOfBirth = ?" & _
                        "   ,intSexID = ?" & _
                        "   ,blnMostValuablePlayer = ?" & _
                        "   ,strEmailAddress = ?" & _
                        " WHERE" & _
                        "   intPlayerID = ?"

            ' Make the command instance
            cmdUpdate = New OleDb.OleDbCommand(strUpdate, m_conAdministrator)

            ' Add column values here instead of above to prevent SQL injection attacks
            With cmdUpdate.Parameters

                .AddWithValue("1", udtPlayer.strFirstName)
                .AddWithValue("2", udtPlayer.strMiddleName)
                .AddWithValue("3", udtPlayer.strLastName)
                .AddWithValue("4", udtPlayer.strStreetAddress)
                .AddWithValue("5", udtPlayer.strCity)
                .AddWithValue("6", udtPlayer.intStateID)
                .AddWithValue("7", udtPlayer.strZipCode)
                .AddWithValue("8", udtPlayer.strHomePhoneNumber)
                .AddWithValue("9", udtPlayer.decSalary)
                .AddWithValue("10", udtPlayer.dtmDateOfBirth)
                .AddWithValue("11", udtPlayer.intSexID)
                .AddWithValue("12", udtPlayer.blnMostValuablePlayer)
                .AddWithValue("13", udtPlayer.strEmailAddress)
                .AddWithValue("14", udtPlayer.intPlayerID)

            End With

            ' Insert the row
            intRowsAffected = cmdUpdate.ExecuteNonQuery()

            ' Success?
            If intRowsAffected = 1 Then blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: EditPlayerInDatabase2
    ' Abstract: Edit the team in the database
    ' --------------------------------------------------------------------------------
    Public Function EditPlayerInDatabase2(ByVal udtPlayer As udtPlayerType) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim cmdStoredProcedure As OleDb.OleDbCommand

            ' Create sqlcommand object to run our stored procedure
            cmdStoredProcedure = New OleDb.OleDbCommand("uspEditPlayer", m_conAdministrator)
            cmdStoredProcedure.CommandType = CommandType.StoredProcedure

            ' Add column values here instead of above to prevent SQL injection attacks
            With cmdStoredProcedure.Parameters
                .AddWithValue("1", udtPlayer.intPlayerID)
                .AddWithValue("2", udtPlayer.strFirstName)
                .AddWithValue("3", udtPlayer.strMiddleName)
                .AddWithValue("4", udtPlayer.strLastName)
                .AddWithValue("5", udtPlayer.strStreetAddress)
                .AddWithValue("6", udtPlayer.strCity)
                .AddWithValue("7", udtPlayer.intStateID)
                .AddWithValue("8", udtPlayer.strZipCode)
                .AddWithValue("9", udtPlayer.strHomePhoneNumber)
                .AddWithValue("10", udtPlayer.decSalary)
                .AddWithValue("11", udtPlayer.dtmDateOfBirth)
                .AddWithValue("12", udtPlayer.intSexID)
                .AddWithValue("13", udtPlayer.blnMostValuablePlayer)
                .AddWithValue("14", udtPlayer.strEmailAddress)
            End With

            ' Execute the stored procedure
            cmdStoredProcedure.ExecuteNonQuery()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: DeletePlayerFromDatabase
    ' Abstract: Mark Player as inactive
    ' --------------------------------------------------------------------------------
    Public Function DeletePlayerFromDatabase(ByVal intPlayerID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            blnResult = SetPlayerStatusInDatabase2(intPlayerID, intPLAYER_STATUS_INACTIVE)

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: UndeletePlayerFromDatabase
    ' Abstract: Lazarus, come out?
    ' --------------------------------------------------------------------------------
    Public Function UndeletePlayerFromDatabase(ByVal intPlayerID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            blnResult = SetPlayerStatusInDatabase2(intPlayerID, intPLAYER_STATUS_ACTIVE)

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: SetPlayerStatusInDatabase
    ' Abstract: Mark the specified player as active or inactive
    ' --------------------------------------------------------------------------------
    Public Function SetPlayerStatusInDatabase(ByVal intPlayerID As Integer, _
                                              ByVal intPlayerStatusID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strUpdate As String
            Dim cmdUpdate As OleDb.OleDbCommand
            Dim intRowsAffected As Integer

            ' Build the UPDATE command
            strUpdate = "UPDATE TPlayers" & _
                        " SET" & _
                        "    intPlayerStatusID = ?" & _
                        " WHERE" & _
                        "    intPlayerID = ?"

            ' Make the command instance
            cmdUpdate = New OleDb.OleDbCommand(strUpdate, m_conAdministrator)

            ' Add column value
            With cmdUpdate.Parameters

                .AddWithValue("1", intPlayerStatusID)
                .AddWithValue("2", intPlayerID)

            End With

            ' Insert the row
            intRowsAffected = cmdUpdate.ExecuteNonQuery()

            ' Success?
            If intRowsAffected = 1 Then blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: SetPlayerStatusInDatabase2
    ' Abstract: Mark the team as active or inactive
    ' --------------------------------------------------------------------------------
    Public Function SetPlayerStatusInDatabase2(ByVal intPlayerID As Integer, _
                                               ByVal intPlayerStatusID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim cmdStoredProcedure As OleDb.OleDbCommand

            ' Create sqlcommand object to run our stored procedure
            cmdStoredProcedure = New OleDb.OleDbCommand("uspSetPlayerStatus", m_conAdministrator)
            cmdStoredProcedure.CommandType = CommandType.StoredProcedure

            ' Add column values here instead of above to prevent SQL injection attacks
            With cmdStoredProcedure.Parameters
                .AddWithValue("1", intPlayerID)
                .AddWithValue("2", intPlayerStatusID)
            End With

            ' Execute the stored procedure
            cmdStoredProcedure.ExecuteNonQuery()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function



#End Region



    ' --------------------------------------------------------------------------------
    ' Name: GetStateInformationFromDatabase
    ' Abstract: Get data for the selected state from the database
    ' --------------------------------------------------------------------------------
    Public Function GetStateInformationFromDatabase(ByRef udtState As udtStateType) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strSelect As String = ""
            Dim cmdSelect As New OleDb.OleDbCommand
            Dim drTStates As OleDb.OleDbDataReader

            ' Build the select string
            strSelect = "SELECT *" & _
                        " FROM TStates" & _
                        " WHERE intStateID = " & udtState.intStateID

            ' Retrieve the record
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drTStates = cmdSelect.ExecuteReader

            ' Read (there should be 1 and only 1 row)
            drTStates.Read()
            With drTStates
                udtState.strState = .Item("strState")
                udtState.strStateAbbreviation = .Item("strStateAbbreviation")
            End With

            ' Clean up
            drTStates.Close()

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnResult

    End Function

End Module
