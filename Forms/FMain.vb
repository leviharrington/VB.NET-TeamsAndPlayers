' --------------------------------------------------------------------------------
' Name: Levi Harrington
' Abstract: Teams and Players
' --------------------------------------------------------------------------------



Public Class FMain

    ' --------------------------------------------------------------------------------
    ' Name: FMain_Shown
    ' Abstract: Connect to the database when form opens
    ' --------------------------------------------------------------------------------
    Private Sub FMain_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Try

            ' We are busy
            SetBusyCursor(Me, True)

            ' Connect to the database. End if connection fails
            If OpenDatabaseConnectionSQLServer() = False Then

                MessageBox.Show("Unable to connect to database." & vbNewLine & _
                                "Application will now close.", _
                                Me.Text & " Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Application.Exit()

            End If


        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

            ' End program
            Application.Exit()

        Finally

            ' We are not busy
            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: mnuFileExit_Click
    ' Abstract: Exit application
    ' --------------------------------------------------------------------------------
    Private Sub mnuFileExit_Click(sender As Object, e As EventArgs) Handles mnuFileExit.Click

        Try

            Me.Close()

        Catch excError As Exception

            ' Display error message
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: TeamsToolStripMenuItem_Click
    ' Abstract: Menu strip - open FManageTeams
    ' --------------------------------------------------------------------------------
    Private Sub TeamsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuToolsManageTeams.Click

        Try

            Dim frmManageTeams As New FManageTeams

            frmManageTeams.ShowDialog()

        Catch excError As Exception

            ' Display error message
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: PlayersToolStripMenuItem_Click
    ' Abstract: Menu strip - open FManagePlayers
    ' --------------------------------------------------------------------------------
    Private Sub PlayersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuToolsManagePlayers.Click

        Try

            Dim frmManagePlayers As New FManagePlayers

            frmManagePlayers.ShowDialog()

        Catch excError As Exception

            ' Display error message
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: TeamPlayersToolStripMenuItem_Click
    ' Abstract: Menu strip - open FAssignTeamPlayers
    ' --------------------------------------------------------------------------------
    Private Sub TeamPlayersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuToolsAssignTeamPlayers.Click

        Try

            Dim frmAssignTeamPlayers As New FAssignTeamPlayers

            frmAssignTeamPlayers.ShowDialog()

        Catch excError As Exception

            ' Display error message
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    '  Name:  btnManageTeams_Click
    '  Abstract:  Modally show an instance of the FManageTeams form
    ' --------------------------------------------------------------------------------
    Private Sub btnManageTeams_Click(sender As Object, e As EventArgs) Handles btnManageTeams.Click

        Try

            Dim frmManageTeams As New FManageTeams

            frmManageTeams.ShowDialog()

        Catch excError As Exception

            ' Display error message
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    '  Name:  btnManageTeamPlayers_Click
    '  Abstract:  Modally show an instance of the FManageTeamPlayers form
    ' --------------------------------------------------------------------------------
    Private Sub btnManageTeamPlayers_Click(sender As Object, e As EventArgs) Handles btnManageTeamPlayers.Click

        Try

            Dim frmAssignTeamPlayers As New FAssignTeamPlayers

            frmAssignTeamPlayers.ShowDialog()

        Catch excError As Exception

            ' Display error message
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    '  Name:  btnManagePlayers_Click
    '  Abstract:  Modally show an instance of the FManagePlayers form
    ' --------------------------------------------------------------------------------
    Private Sub btnManagePlayers_Click(sender As Object, e As EventArgs) Handles btnManagePlayers.Click

        Try

            Dim frmManagePlayers As New FManagePlayers

            frmManagePlayers.ShowDialog()

        Catch excError As Exception

            ' Display error message
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: FMain_Close
    ' Abstract: Close the database connection when form is closed
    ' --------------------------------------------------------------------------------
    Private Sub FMain_Closing(sender As Object, e As EventArgs) Handles MyBase.FormClosing

        Try

            CloseDatabaseConnection()

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

    End Sub


    
End Class