' -------------------------------------------------------------------------
' Form: FManageTeams
' Abstract: Manage Teams in database
' -------------------------------------------------------------------------

' --------------------------------------------------------------------------------
' Options
' --------------------------------------------------------------------------------
Option Explicit On


Public Class FManageTeams

    ' --------------------------------------------------------------------------------
    ' Constants
    ' --------------------------------------------------------------------------------

    ' --------------------------------------------------------------------------------
    ' Form variables
    ' --------------------------------------------------------------------------------


    ' --------------------------------------------------------------------------------
    ' Name: FManageTeams_Shown
    ' Abstract: Load List Box when form is shown
    ' --------------------------------------------------------------------------------
    Private Sub FManageTeams_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Try

            Dim blnResult As Boolean = False

            ' Load the teams list
            blnResult = LoadTeamsList()

            ' Did it work?
            If blnResult = False Then

                ' No, warn the user
                MessageBox.Show("Unable to load the teams list" & vbNewLine & _
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
    ' Name: LoadTeamsList
    ' Abstract: Load the Team list
    ' --------------------------------------------------------------------------------
    Private Function LoadTeamsList() As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim strSourceTable As String = ""

            If chkShowDeleted.Checked = False Then

                strSourceTable = "VActiveTeams"

            Else

                strSourceTable = "VInactiveTeams"

            End If

            ' We are busy
            SetBusyCursor(Me, True)

            blnResult = LoadListBoxFromDatabase(strSourceTable, _
                                                "intTeamID", _
                                                "strTeam", _
                                                lstTeams)

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

            Dim liNewTeamInfo As CListItem
            Dim frmAddTeam As FAddTeam
            Dim intIndex As Integer

            ' Make an instance
            frmAddTeam = New FAddTeam

            ' Show modally
            frmAddTeam.ShowDialog()

            ' Was the Add successful?
            If frmAddTeam.GetResult() = True Then

                ' Get the new team values
                liNewTeamInfo = frmAddTeam.GetNewTeamInformation

                ' Add new record to the listbox
                intIndex = lstTeams.Items.Add(liNewTeamInfo)

                ' ... which we can use to select it
                lstTeams.SelectedIndex = intIndex

            End If

        Catch excError As Exception

            ' Log and display error
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: btnEdit_Click
    ' Abstract: Delete the currently selected team
    ' --------------------------------------------------------------------------------
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        Try

            Dim intSelectedTeamID As Integer
            Dim liSelectedTeam As CListItem
            Dim frmEditTeam As FEditTeam
            Dim liNewTeamInformation As CListItem
            Dim intIndex As Integer

            ' Is a team selected?
            If lstTeams.SelectedIndex < 0 Then

                ' No, warn the user
                MessageBox.Show("You must select a team to edit.", Me.Text & " Error", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else

                ' Yes, get the team to edit ID
                liSelectedTeam = lstTeams.SelectedItem
                intSelectedTeamID = liSelectedTeam.GetID

                ' Create instance
                frmEditTeam = New FEditTeam

                ' Set the form values
                frmEditTeam.SetTeamID(intSelectedTeamID)

                ' Show it modally
                frmEditTeam.ShowDialog(Me)

                ' Was the Edit successful?
                If frmEditTeam.GetResult() = True Then

                    ' Get the new team values
                    liNewTeamInformation = frmEditTeam.GetNewTeamInformation

                    ' Yes, remove and re-add from list so it gets sorted correctly
                    lstTeams.Items.RemoveAt(lstTeams.SelectedIndex)

                    ' Add Item returns index of newly added item ...
                    intIndex = lstTeams.Items.Add(liNewTeamInformation)

                    ' ... which we can use to select it
                    lstTeams.SelectedIndex = intIndex

                End If

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: btnDelete_Click
    ' Abstract: Delete the currently selected team
    ' --------------------------------------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Try

            ' Delete?
            If chkShowDeleted.Checked = True = False Then

                ' Yes
                DeleteTeam()

            Else

                ' No, undelete
                UndeleteTeam()

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

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
    ' Name: DeleteTeam
    ' Abstract: Delete the currently selected team
    ' --------------------------------------------------------------------------------
    Private Sub DeleteTeam()

        Try

            Dim intSelectedTeamID As Integer
            Dim liSelectedTeam As CListItem
            Dim strSelectedTeamName As String
            Dim intSelectedTeamIndex As Integer
            Dim drConfirm As DialogResult
            Dim blnResult As Boolean

            ' Is a team selected?
            If lstTeams.SelectedIndex < 0 Then

                ' No, warn the user
                MessageBox.Show("You must select a team to delete.", Me.Text & " Error", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else

                ' Get selected index so we can select the next closest item after delete
                intSelectedTeamIndex = lstTeams.SelectedIndex

                ' Get the team ID and name
                liSelectedTeam = lstTeams.SelectedItem
                intSelectedTeamID = liSelectedTeam.GetID
                strSelectedTeamName = liSelectedTeam.GetName

                ' Yes, confirm they want to delete (use name for user confirmation)
                drConfirm = MessageBox.Show("Are you sure?", "Delete Team: " & strSelectedTeamName, _
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                ' Yes?
                If drConfirm = Windows.Forms.DialogResult.Yes Then

                    ' We are busy
                    SetBusyCursor(Me, True)

                    ' Yes, delete the team (use ID for database command)
                    blnResult = DeleteTeamFromDatabase(intSelectedTeamID)

                    ' Was the delete successful?
                    If blnResult = True Then

                        ' Yes, remove the team from the list
                        lstTeams.Items.RemoveAt(intSelectedTeamIndex)

                        ' Select the next team in the list
                        HighLightNextItemInList(lstTeams, intSelectedTeamIndex)

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
    ' Name: UndeleteTeam
    ' Abstract: Lazarus, come out?
    ' --------------------------------------------------------------------------------
    Private Sub UndeleteTeam()

        Try

            Dim liSelectedTeam As CListItem
            Dim intSelectedTeamID As Integer
            Dim intSelectedTeamIndex As Integer
            Dim blnResult As Boolean

            ' Is a team selected?
            If lstTeams.SelectedIndex < 0 Then

                ' No, warn the user
                MessageBox.Show("You must select a team to delete.", Me.Text & " Error", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else

                ' Get the team ID and list index
                liSelectedTeam = lstTeams.SelectedItem
                intSelectedTeamID = liSelectedTeam.GetID
                intSelectedTeamIndex = lstTeams.SelectedIndex


                ' We are busy
                SetBusyCursor(Me, True)

                ' Yes, undelete the team
                blnResult = UndeleteTeamFromDatabase(intSelectedTeamID)

                ' Was the delete successful?
                If blnResult = True Then

                    ' Yes, remove the team from the list
                    lstTeams.Items.RemoveAt(intSelectedTeamIndex)

                    ' Is there an item to highlight?
                    If lstTeams.Items.Count > 0 Then

                        ' Yes, select the next team in the list
                        HighLightNextItemInList(lstTeams, intSelectedTeamIndex)

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

            LoadTeamsList()

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub

End Class