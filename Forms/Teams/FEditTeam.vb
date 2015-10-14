' -------------------------------------------------------------------------
' Form: FEditTeam
' Abstract: Edit a team in the database
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On


' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------



Public Class FEditTeam

    ' -------------------------------------------------------------------------
    ' Form Variables
    ' -------------------------------------------------------------------------
    Private f_intTeamID As Integer
    Private f_blnResult As Boolean


    ' -------------------------------------------------------------------------
    ' Name: SetTeamID
    ' Abstract: What team are we going to edit
    '           Called after instance is created but before shown
    ' -------------------------------------------------------------------------
    Public Sub SetTeamID(ByVal intTeamID As Integer)

        Try

            ' Team ID
            f_intTeamID = intTeamID

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: FEditTeam_Shown
    ' Abstract: Load the form with values from the database
    ' -------------------------------------------------------------------------
    Private Sub FEditTeam_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Dim udtTeam As udtTeamType

            ' Make a suitcase instance
            udtTeam = New udtTeamType

            ' Set the ID
            udtTeam.intTeamID = f_intTeamID

            ' We are busy
            SetBusyCursor(Me, True)

            ' Is the data OK (pass in the empty suitcase by ref so it can be filled up)?
            If GetTeamInformationFromDatabase(udtTeam) = True Then

                txtTeam.Text = udtTeam.strTeam
                txtMascot.Text = udtTeam.strMascot

            Else

                ' Somthing went wrong. Warn the user ...
                MessageBox.Show(Me, "Error: Unable to load the information for team to edit." & vbNewLine & _
                                    "The form will now close.", "Edit Team Error", _
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                ' ... and close the form
                Me.Close()

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        Finally

            ' We are not buys
            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnOK_Click
    ' Abstract: OK button event
    ' -------------------------------------------------------------------------
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        Try

            ' Trim all form text boxes
            TrimAllFormTextBoxes(Me)

            ' Is Valid Data?
            If IsValidData() = True Then

                ' Add team to database
                If SaveData() = True Then

                    Me.Close()

                End If

            End If

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: IsValidData
    ' Abstract: Check that the Team and Mascot data is valid
    ' -------------------------------------------------------------------------
    Private Function IsValidData() As Boolean

        Dim blnIsValidData As Boolean = True

        Try

            Dim strErrorMessage As String = "Please correct the following error(s):" & vbNewLine

            ' Team
            blnIsValidData = blnIsValidData And IsValidTeamName(strErrorMessage)

            ' Mascot
            blnIsValidData = blnIsValidData And IsValidMascotName(strErrorMessage)

            ' Bad Data?
            If blnIsValidData = False Then

                ' Yes, warn the user
                MessageBox.Show(strErrorMessage, Me.Text & "Add TTeams Record Error(s)", _
                                MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

        Return blnIsValidData

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidTeamName
    ' Abstract: Check that the Team data is valid
    ' -------------------------------------------------------------------------
    Public Function IsValidTeamName(ByRef strErrorMessage As String) As Boolean

        Dim blnIsValidTeamName As Boolean = True

        Try

            ' Is team blank?
            If txtTeam.Text = "" Then

                ' Yes
                strErrorMessage &= "-Team Name cannot be blank" & vbNewLine
                blnIsValidTeamName = False

            End If

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

        Return blnIsValidTeamName

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidMascotName
    ' Abstract: Check that the Team data is valid
    ' -------------------------------------------------------------------------
    Public Function IsValidMascotName(ByRef strErrorMessage As String) As Boolean

        Dim blnIsValidMascotName As Boolean = True

        Try

            ' Is mascot blank?
            If txtMascot.Text = "" Then

                ' Yes
                strErrorMessage &= "-Mascot Name cannot be blank" & vbNewLine
                blnIsValidMascotName = False

            End If

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

        Return blnIsValidMascotName

    End Function



    ' -------------------------------------------------------------------------
    ' Name: SaveData
    ' Abstract:Saving the data
    ' -------------------------------------------------------------------------
    Private Function SaveData() As Boolean

        Try

            ' The suitcase
            Dim udtTeam As New udtTeamType

            ' Load it with data from form
            udtTeam.strTeam = txtTeam.Text
            udtTeam.strMascot = txtMascot.Text
            udtTeam.intTeamID = f_intTeamID

            ' We are busy
            SetBusyCursor(Me, True)

            f_blnResult = EditTeamInDatabase2(udtTeam)

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        Finally

            SetBusyCursor(Me, False)

        End Try

        Return f_blnResult

    End Function



    ' -------------------------------------------------------------------------
    ' Name: btnCancel_Click
    ' Abstract: OK button event
    ' -------------------------------------------------------------------------
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Try

            Me.Close()

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: GetResult
    ' Abstract: Was the add/edit successful?
    ' -------------------------------------------------------------------------
    Public Function GetResult() As Boolean

        Try

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

        Return f_blnResult

    End Function



    ' -------------------------------------------------------------------------
    ' Name: GetNewTeamInformation
    ' Abstract: Get the new team information
    ' -------------------------------------------------------------------------
    Public Function GetNewTeamInformation() As CListItem

        Dim clsTeam As CListItem = Nothing

        Try

            clsTeam = New CListItem(f_intTeamID, txtTeam.Text)

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

        Return clsTeam

    End Function

End Class