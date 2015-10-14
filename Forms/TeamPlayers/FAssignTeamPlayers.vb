' -------------------------------------------------------------------------
' Form: FAssignTeamPlayers
' Abstract: Assign Teams and Players in database
' -------------------------------------------------------------------------

' --------------------------------------------------------------------------------
' Options
' --------------------------------------------------------------------------------
Option Explicit On



Public Class FAssignTeamPlayers

    ' --------------------------------------------------------------------------------
    ' Name: FAssignTeamPlayers_Shown
    ' Abstract: Connect to the database when form opens
    ' --------------------------------------------------------------------------------
    Private Sub FAssignTeamPlayers_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Try

            ' We are busy
            SetBusyCursor(Me, True)

            ' Load Teams in combo box
            LoadComboBoxFromDatabase("VActiveTeams", "intTeamID", "strTeam", cmbTeams)

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

            ' End program
            ' Application.Exit()

        Finally

            ' We are not busy
            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    '  Name:  cmbTeams_SelectedIndexChanged
    '  Abstract:  Load the selected and available player lists for the current team
    ' --------------------------------------------------------------------------------
    Private Sub cmbTeams_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTeams.SelectedIndexChanged

        Try

            LoadTeamPlayers()

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        Finally

            ' We are NOT busy
            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    '  Name:  LoadTeamPlayers
    '  Abstract:  Load the selected and available player lists for the current team
    ' --------------------------------------------------------------------------------
    Private Sub LoadTeamPlayers()

        Try

            Dim liSelectedTeam As CListItem
            Dim intTeamID As Integer

            ' We are busy
            SetBusyCursor(Me, True)

            ' Is a team selected?
            If cmbTeams.SelectedIndex >= 0 Then

                ' Get the selected team ID
                liSelectedTeam = cmbTeams.SelectedItem
                intTeamID = liSelectedTeam.GetID()

                ' Selected Players
                LoadListWithPlayersFromDatabase(intTeamID, lstSelectedPlayers, True)

                ' Available Players
                LoadListWithPlayersFromDatabase(intTeamID, lstAvailablePlayers, False)

                ' Enable/disable add/remove buttons
                EnableButtons()

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        Finally

            ' We are NOT busy
            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    '  Name:  EnableButtons
    '  Abstract:  Enable/disable the OK and add/remove buttons
    ' --------------------------------------------------------------------------------
    Private Sub EnableButtons()

        Try

            ' All
            btnAll.Enabled = False
            ' If lstAvailablePlayers.Items.Count > 0 Then btnAll.Enabled = True

            ' Add
            btnAdd.Enabled = False
            If lstAvailablePlayers.Items.Count > 0 Then btnAdd.Enabled = True

            ' Remove
            btnRemove.Enabled = False
            If lstSelectedPlayers.Items.Count > 0 Then btnRemove.Enabled = True

            ' None
            btnNone.Enabled = False
            ' If lstSelectedPlayers.Items.Count > 0 Then btnNone.Enabled = True

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    '  Name:  btnAdd_Click
    '  Abstract:  Add a player to the team
    ' --------------------------------------------------------------------------------
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Try

            Dim liSelectedItem As CListItem
            Dim intTeamID As Integer
            Dim intPlayerID As Integer
            Dim intIndex As Integer

            ' Is a player selected?
            If lstAvailablePlayers.SelectedIndex >= 0 Then

                ' Yes

                ' We are busy
                SetBusyCursor(Me, True)

                ' Get Team and Player IDs from lists (which are populated with instances of CListItem)
                liSelectedItem = cmbTeams.SelectedItem
                intTeamID = liSelectedItem.GetID
                liSelectedItem = lstAvailablePlayers.SelectedItem
                intPlayerID = liSelectedItem.GetID

                ' Add the player
                If AddPlayerToTeamInDatabase2(intTeamID, intPlayerID) = True Then

                    ' Add to selected players
                    intIndex = lstSelectedPlayers.Items.Add(lstAvailablePlayers.SelectedItem)
                    lstSelectedPlayers.SelectedIndex = intIndex

                    ' Remove from available players
                    intIndex = lstAvailablePlayers.SelectedIndex
                    lstAvailablePlayers.Items.RemoveAt(intIndex)

                    If lstAvailablePlayers.Items.Count > 0 Then

                        ' Highlight next in list
                        HighLightNextItemInList(lstAvailablePlayers, intIndex)

                    End If

                    EnableButtons()

                End If

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        Finally

            ' We are NOT busy
            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    '  Name:  btnRemove_Click
    '  Abstract:  Remove the currently selected player from the team
    ' --------------------------------------------------------------------------------
    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click

        Try

            Dim liSelectedItem As CListItem
            Dim intTeamID As Integer
            Dim intPlayerID As Integer
            Dim intIndex As Integer

            ' Is a player selected?
            If lstSelectedPlayers.SelectedIndex >= 0 Then

                ' Yes

                ' We are busy
                SetBusyCursor(Me, True)

                ' Get Team and Player IDs from lists (which are populated with instances of CListItem)
                liSelectedItem = cmbTeams.SelectedItem
                intTeamID = liSelectedItem.GetID
                liSelectedItem = lstSelectedPlayers.SelectedItem
                intPlayerID = liSelectedItem.GetID

                ' Remove the player
                If RemovePlayerFromTeamInDatabase2(intTeamID, intPlayerID) = True Then

                    ' Add to available players
                    intIndex = lstAvailablePlayers.Items.Add(lstSelectedPlayers.SelectedItem)
                    lstAvailablePlayers.SelectedIndex = intIndex

                    ' Remove from selected players
                    intIndex = lstSelectedPlayers.SelectedIndex
                    lstSelectedPlayers.Items.RemoveAt(intIndex)

                    If lstSelectedPlayers.Items.Count > 0 Then

                        ' Highlight next in list
                        HighLightNextItemInList(lstSelectedPlayers, intIndex)

                    End If

                    EnableButtons()

                End If

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        Finally

            ' We are NOT busy
            SetBusyCursor(Me, False)

        End Try

    End Sub


    ' --------------------------------------------------------------------------------
    '  Name:  btnClose_Click
    '  Abstract:   Close the form
    ' --------------------------------------------------------------------------------
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        Try

            Me.Close()

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub

End Class