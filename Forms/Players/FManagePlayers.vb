' -------------------------------------------------------------------------
' Form: FManagePlayers
' Abstract: Manage Players in database
' -------------------------------------------------------------------------

' --------------------------------------------------------------------------------
' Options
' --------------------------------------------------------------------------------
Option Explicit On


Public Class FManagePlayers

    ' --------------------------------------------------------------------------------
    ' Constants
    ' --------------------------------------------------------------------------------

    ' --------------------------------------------------------------------------------
    ' Form variables
    ' --------------------------------------------------------------------------------


    ' --------------------------------------------------------------------------------
    ' Name: FManagePlayers_Shown
    ' Abstract: Load List Box when form is shown
    ' --------------------------------------------------------------------------------
    Private Sub FManagePlayers_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Try

            Dim blnResult As Boolean = False

            ' Load the teams list
            blnResult = LoadPlayersList()

            ' Did it work?
            If blnResult = False Then

                ' No, warn the user
                MessageBox.Show("Unable to load the players list" & vbNewLine & _
                                "The form will now close.", _
                                Me.Text & " Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' and close the form
                Me.Close()

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: LoadPlayersList
    ' Abstract: Load the Player list
    ' --------------------------------------------------------------------------------
    Private Function LoadPlayersList() As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strSourceTable As String = ""

            If chkShowDeleted.Checked = False Then

                strSourceTable = "VActivePlayers"

            Else

                strSourceTable = "VInactivePlayers"

            End If

            ' We are busy
            SetBusyCursor(Me, True)

            blnResult = LoadListBoxFromDatabase(strSourceTable, "intPlayerID", "strLastName + ', ' + strFirstName", lstPlayers)

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        Finally

            ' We are not busy
            SetBusyCursor(Me, False)

        End Try

        Return blnResult

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: btnAdd_Click
    ' Abstract: Close the database connection when form is closed
    ' --------------------------------------------------------------------------------
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Try

            Dim liNewPlayerInformation As CListItem
            Dim frmAddPlayer As FAddPlayer
            Dim intIndex As Integer

            ' Make an instance
            frmAddPlayer = New FAddPlayer

            ' Show modally
            frmAddPlayer.ShowDialog()

            ' Was the Add successful?
            If frmAddPlayer.GetResult() = True Then

                ' Get the new Player values
                liNewPlayerInformation = frmAddPlayer.GetNewPlayerInformation

                ' Add Item returns index of newly added item ...
                intIndex = lstPlayers.Items.Add(liNewPlayerInformation)

                ' ... which we can use to select it
                lstPlayers.SelectedIndex = intIndex

            End If

        Catch excError As Exception

            ' Log and display error
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: btnEdit_Click
    ' Abstract: Delete the currently selected player
    ' --------------------------------------------------------------------------------
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        Try

            Dim intSelectedPlayerID As Integer
            Dim liSelectedPlayer As CListItem
            Dim frmEditPlayer As FEditPlayer
            Dim liNewPlayerInformation As CListItem
            Dim intIndex As Integer

            ' Is a player selected?
            If lstPlayers.SelectedIndex < 0 Then

                ' No, warn the user
                MessageBox.Show("You must select a player to edit.", Me.Text & " Error", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else

                ' Yes, get the player to edit ID
                liSelectedPlayer = lstPlayers.SelectedItem
                intSelectedPlayerID = liSelectedPlayer.GetID

                ' Create instance
                frmEditPlayer = New FEditPlayer

                ' Set the form values
                frmEditPlayer.SetPlayerID(intSelectedPlayerID)

                ' Show it modally
                frmEditPlayer.ShowDialog(Me)

                ' Was the Edit successful?
                If frmEditPlayer.GetResult() = True Then

                    ' Get the new player values
                    liNewPlayerInformation = frmEditPlayer.GetNewPlayerInformation

                    ' Yes, remove and re-add from list so it gets sorted correctly
                    lstPlayers.Items.RemoveAt(lstPlayers.SelectedIndex)
                    ' Add Item returns index of newly added item ...
                    intIndex = lstPlayers.Items.Add(liNewPlayerInformation)
                    ' ... which we can use to select it
                    lstPlayers.SelectedIndex = intIndex

                End If

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: btnDelete_Click
    ' Abstract: Delete the currently selected player
    ' --------------------------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Try

            ' Delete?
            If chkShowDeleted.Checked = False Then

                ' Yes
                DeletePlayer()

            Else

                ' No, undelete
                UndeletePlayer()

            End If

        Catch ex As Exception

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: btnClose_Click
    ' Abstract: Close the database connection when form is closed
    ' --------------------------------------------------------------------------------
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        Try

            Me.Close()

        Catch excError As Exception

            ' Log and display error
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: DeletePlayer
    ' Abstract: Delete the currently selected player
    ' --------------------------------------------------------------------------------
    Private Sub DeletePlayer()

        Try

            Dim intSelectedPlayerID As Integer
            Dim liSelectedPlayer As CListItem
            Dim strSelectedPlayerName As String
            Dim intSelectedPlayerIndex As Integer
            Dim drConfirm As DialogResult
            Dim blnResult As Boolean

            ' Is a player selected?
            If lstPlayers.SelectedIndex < 0 Then

                ' No, warn the user
                MessageBox.Show("You must select a player to delete.", Me.Text & " Error", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else

                ' Get selected index so we can select the next closest item after delete
                intSelectedPlayerIndex = lstPlayers.SelectedIndex

                ' Get the player ID and name
                liSelectedPlayer = lstPlayers.SelectedItem
                intSelectedPlayerID = liSelectedPlayer.GetID
                strSelectedPlayerName = liSelectedPlayer.GetName

                ' Yes, confirm they want to delete (use name for user confirmation)
                drConfirm = MessageBox.Show("Are you sure?", "Delete Player: " & strSelectedPlayerName, _
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                ' Yes?
                If drConfirm = Windows.Forms.DialogResult.Yes Then

                    ' We are busy
                    SetBusyCursor(Me, True)

                    ' Yes, delete the player (use ID for database command)
                    blnResult = DeletePlayerFromDatabase(intSelectedPlayerID)

                    ' Was the delete successful?
                    If blnResult = True Then

                        ' Yes, remove the player from the list
                        lstPlayers.Items.RemoveAt(intSelectedPlayerIndex)

                        ' Select the next player in the list
                        HighLightNextItemInList(lstPlayers, intSelectedPlayerIndex)

                    End If

                End If

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        Finally

            ' We are not busy
            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: UndeletePlayer
    ' Abstract: Lazarus, come out?
    ' --------------------------------------------------------------------------------
    Private Sub UndeletePlayer()

        Try

            Dim liSelectedPlayer As CListItem
            Dim intSelectedPlayerID As Integer
            Dim intSelectedPlayerIndex As Integer
            Dim blnResult As Boolean

            ' Is a team selected?
            If lstPlayers.SelectedIndex < 0 Then

                ' No, warn the user
                MessageBox.Show("You must select a player to delete.", Me.Text & " Error", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else

                ' Get the team ID and list index
                liSelectedPlayer = lstPlayers.SelectedItem
                intSelectedPlayerID = liSelectedPlayer.GetID
                intSelectedPlayerIndex = lstPlayers.SelectedIndex

                ' We are busy
                SetBusyCursor(Me, True)

                ' Yes, undelete the team
                blnResult = UndeletePlayerFromDatabase(intSelectedPlayerID)

                ' Was the delete successful?
                If blnResult = True Then

                    ' Yes, remove the team from the list
                    lstPlayers.Items.RemoveAt(intSelectedPlayerIndex)

                    ' Is there an item to highlight?
                    If lstPlayers.Items.Count > 0 Then

                        ' Yes, select the next team in the list
                        HighLightNextItemInList(lstPlayers, intSelectedPlayerIndex)

                    End If

                End If

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        Finally

            ' We are not busy
            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: chkShowDeleted_CheckedChanged
    ' Abstract: Toggle between active and inactive teams
    ' --------------------------------------------------------------------------------
    Private Sub chkShowDeleted_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowDeleted.CheckedChanged

        Try

            If chkShowDeleted.Checked = False Then

                btnAdd.Enabled = True
                btnEdit.Enabled = True
                btnDelete.Text = "&Delete"

            Else

                btnAdd.Enabled = False
                btnEdit.Enabled = False
                btnDelete.Text = "&Undelete"

            End If

            LoadPlayersList()

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub

End Class