' -------------------------------------------------------------------------
' Form: FAddPlayer
' Abstract: Add a player to the database
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On



' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------



Public Class FAddPlayer



    ' -------------------------------------------------------------------------
    ' Form Variables
    ' -------------------------------------------------------------------------
    Private f_intNewPlayerID As Integer = 0
    Private f_blnResult As Boolean



    ' --------------------------------------------------------------------------------
    ' Name: FAddPlayer_Shown
    ' Abstract: Load ComboBox when form is shown
    ' --------------------------------------------------------------------------------
    Private Sub FAddPlayer_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Try

            ' We are busy
            SetBusyCursor(Me, True)

            If LoadComboBoxFromDatabase("TStates", "intStateID", "strState", cmbStates) = False Then

                MessageBox.Show("Unable to load the states list" & vbNewLine & _
                                "The form will now close.", _
                                Me.Text & " Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Me.Close()

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        Finally

            ' We are not busy
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

                ' Add player to database
                If SaveData() = True Then

                    Me.Close()

                End If

            End If

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

    End Sub



#Region "Player Validation"

    ' -------------------------------------------------------------------------
    ' Name: IsValidData
    ' Abstract: Check that the Player data is valid
    ' -------------------------------------------------------------------------
    Private Function IsValidData() As Boolean

        Dim blnIsValidData As Boolean = True

        Try

            Dim strErrorMessage As String = "Please correct the following error(s):" & vbNewLine

            ' First Name
            blnIsValidData = blnIsValidData And IsValidPlayerFirstName(strErrorMessage)

            ' Last Name
            blnIsValidData = blnIsValidData And IsValidPlayerLastName(strErrorMessage)

            ' Zip Code
            blnIsValidData = blnIsValidData And IsValidPlayerZipCode(strErrorMessage)

            ' Home Phone Number
            blnIsValidData = blnIsValidData And IsValidPlayerHomePhoneNumber(strErrorMessage)

            ' Salary
            blnIsValidData = blnIsValidData And IsValidPlayerSalary(strErrorMessage)

            ' Date of Birth
            blnIsValidData = blnIsValidData And IsValidPlayerDateOfBirth(strErrorMessage)

            ' Email Address
            blnIsValidData = blnIsValidData And IsValidPlayerEmailAddress(strErrorMessage)

            ' Bad Data?
            If blnIsValidData = False Then

                ' Yes, warn the user
                MessageBox.Show(strErrorMessage, Me.Text & "Add TPlayers Record Error(s)", _
                                MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

        Return blnIsValidData

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidPlayerFirstName
    ' Abstract: Check that the Player data is valid
    ' -------------------------------------------------------------------------
    Public Function IsValidPlayerFirstName(ByRef strErrorMessage As String) As Boolean

        Dim blnIsValidPlayerFirstName As Boolean = True

        Try

            ' Is first name blank?
            If txtFirstName.Text = "" Then

                ' Yes
                strErrorMessage &= "-First Name cannot be blank" & vbNewLine
                blnIsValidPlayerFirstName = False

            End If

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

        Return blnIsValidPlayerFirstName

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidPlayerLastName
    ' Abstract: Check that the Player data is valid
    ' -------------------------------------------------------------------------
    Public Function IsValidPlayerLastName(ByRef strErrorMessage As String) As Boolean

        Dim blnIsValidPlayerLastName As Boolean = True

        Try

            ' Is last name blank?
            If txtLastName.Text = "" Then

                ' Yes
                strErrorMessage &= "-Last Name cannot be blank" & vbNewLine
                blnIsValidPlayerLastName = False

            End If

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

        Return blnIsValidPlayerLastName

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidPlayerZipCode
    ' Abstract: Check that the Zip Code is valid
    ' -------------------------------------------------------------------------
    Public Function IsValidPlayerZipCode(ByRef strErrorMessage As String) As Boolean

        Dim blnIsValidPlayerZipCode As Boolean = True

        Try

            ' Was a zip code entered?
            If txtZipCode.Text <> "" Then

                ' Yes - Is the zipcode in the proper format?
                If IsValidZipCode(txtZipCode.Text) = False Then

                    ' No - Display error message
                    strErrorMessage &= "-Zip Code must be in proper format." & vbNewLine & _
                                       "       (##### or #####-####)" & vbNewLine
                    blnIsValidPlayerZipCode = False

                End If

            End If

        Catch excError As Exception

            ' Display and Log Error Message
            WriteLog(excError)

        End Try

        Return blnIsValidPlayerZipCode

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidPlayerHomePhoneNumber
    ' Abstract: Check that the Home Phone Number is valid
    ' -------------------------------------------------------------------------
    Public Function IsValidPlayerHomePhoneNumber(ByRef strErrorMessage As String) As Boolean

        Dim blnIsValidPlayerHomePhoneNumber As Boolean = True

        Try

            ' Was a home phone number entered?
            If txtHomePhoneNumber.Text <> "" Then

                ' Yes - Is the home phone number in the proper format?
                If IsValidHomePhoneNumber(txtHomePhoneNumber.Text) = False Then

                    ' No - Display error message
                    strErrorMessage &= "-Home Phone Number must be in proper format." & vbNewLine

                    blnIsValidPlayerHomePhoneNumber = False

                End If

            End If

        Catch excError As Exception

            ' Display and Log Error Message
            WriteLog(excError)

        End Try

        Return blnIsValidPlayerHomePhoneNumber

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidPlayerSalary
    ' Abstract: Check that the Salary is valid
    ' -------------------------------------------------------------------------
    Public Function IsValidPlayerSalary(ByRef strErrorMessage As String) As Boolean

        Dim blnIsValidPlayerSalary As Boolean = True

        Try

            ' Was a home phone number entered?
            If txtSalary.Text <> "" Then

                ' Yes - Is the home phone number in the proper format?
                If IsValidSalary(txtSalary.Text) = False Then

                    ' No - Display error message
                    strErrorMessage &= "-Salary must be in proper format." & vbNewLine

                    blnIsValidPlayerSalary = False

                End If

            End If

        Catch excError As Exception

            ' Display and Log Error Message
            WriteLog(excError)

        End Try

        Return blnIsValidPlayerSalary

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidPlayerDateOfBirth
    ' Abstract: Check that the Date of Birth is valid
    ' -------------------------------------------------------------------------
    Public Function IsValidPlayerDateOfBirth(ByRef strErrorMessage As String) As Boolean

        Dim blnIsValidPlayerDateOfBirth As Boolean = True

        Try

            ' Was a date of birth entered?
            If txtDateOfBirth.Text <> "" Then

                ' Yes - Is the date of birth in the proper format?
                If IsValidDate(txtDateOfBirth.Text) = False Then

                    ' No - Display error message
                    strErrorMessage &= "-Date of Birth must be in proper format." & vbNewLine

                    blnIsValidPlayerDateOfBirth = False

                End If

            End If

        Catch excError As Exception

            ' Display and Log Error Message
            WriteLog(excError)

        End Try

        Return blnIsValidPlayerDateOfBirth

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidPlayerEmailAddress
    ' Abstract: Check that the Date of Birth is valid
    ' -------------------------------------------------------------------------
    Public Function IsValidPlayerEmailAddress(ByRef strErrorMessage As String) As Boolean

        Dim blnIsValidPlayerEmailAddress As Boolean = True

        Try

            ' Was a date of birth entered?
            If txtEmailAddress.Text <> "" Then

                ' Yes - Is the home phone number in the proper format?
                If IsValidEmailAddress(txtEmailAddress.Text) = False Then

                    ' No - Display error message
                    strErrorMessage &= "-Email Address must be in proper format." & vbNewLine & _
                                       "       (example@example.com)" & vbNewLine

                    blnIsValidPlayerEmailAddress = False

                End If

            End If

        Catch excError As Exception

            ' Display and Log Error Message
            WriteLog(excError)

        End Try

        Return blnIsValidPlayerEmailAddress

    End Function

#End Region

    ' -------------------------------------------------------------------------
    ' Name: SaveData
    ' Abstract:Saving the data
    ' -------------------------------------------------------------------------
    Private Function SaveData() As Boolean

        Try

            ' The suitcase
            Dim udtNewPlayer As New udtPlayerType
            Dim intSelectedStateID As Integer
            Dim liSelectedState As CListItem

            ' get the player ID
            liSelectedState = cmbStates.SelectedItem
            intSelectedStateID = liSelectedState.GetID

            ' Load it with data from form
            udtNewPlayer.intPlayerID = 0  ' don't know it yet so 0
            udtNewPlayer.strFirstName = txtFirstName.Text
            udtNewPlayer.strMiddleName = txtMiddleName.Text
            udtNewPlayer.strLastName = txtLastName.Text
            udtNewPlayer.strStreetAddress = txtStreetAddress.Text
            udtNewPlayer.strCity = txtCity.Text
            udtNewPlayer.intStateID = intSelectedStateID
            udtNewPlayer.strZipCode = txtZipCode.Text
            udtNewPlayer.strHomePhoneNumber = txtHomePhoneNumber.Text
            udtNewPlayer.decSalary = ConvertSalaryStringToDecimal(txtSalary.Text)

            ' Saving the date = 1800/01/01 if blank
            ' Was a date of birth entered?
            If txtDateOfBirth.Text <> "" Then

                udtNewPlayer.dtmDateOfBirth = txtDateOfBirth.Text

            Else

                udtNewPlayer.dtmDateOfBirth = "1800/01/01"

            End If

            ' Sex Radio Buttons
            If radSexFemale.Checked = True Then udtNewPlayer.intSexID = 1
            If radSexMale.Checked = True Then udtNewPlayer.intSexID = 2

            ' Most Valuable Player
            udtNewPlayer.blnMostValuablePlayer = chkMostValuablePlayer.Checked

            ' Email Address
            udtNewPlayer.strEmailAddress = txtEmailAddress.Text

            ' We are busy
            SetBusyCursor(Me, True)

            f_blnResult = AddPlayerToDatabase2(udtNewPlayer)

            ' Did it work?
            If f_blnResult = True Then

                ' Yes, save the new player ID
                f_intNewPlayerID = udtNewPlayer.intPlayerID

            End If

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        Finally

            ' We are not busy
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
    ' Name: GetNewPlayerInformation
    ' Abstract: Get the new player information
    ' -------------------------------------------------------------------------
    Public Function GetNewPlayerInformation() As CListItem

        Dim clsPlayer As CListItem = Nothing

        Try

            Dim strFullName As String = txtLastName.Text + ", " + txtFirstName.Text

            clsPlayer = New CListItem(f_intNewPlayerID, strFullName)

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

        Return clsPlayer

    End Function

End Class