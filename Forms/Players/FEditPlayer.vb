' -------------------------------------------------------------------------
' Form: FEditPlayer
' Abstract: Edit a player in the database
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On


' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------



Public Class FEditPlayer

    ' -------------------------------------------------------------------------
    ' Form Variables
    ' -------------------------------------------------------------------------
    Private f_intPlayerID As Integer
    Private f_blnResult As Boolean



    ' -------------------------------------------------------------------------
    ' Name: SetPlayerID
    ' Abstract: What player are we going to edit
    '           Called after instance is created but before shown
    ' -------------------------------------------------------------------------
    Public Sub SetPlayerID(ByVal intPlayerID As Integer)

        Try

            ' Player ID
            f_intPlayerID = intPlayerID

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: FEditPlayer_Shown
    ' Abstract: Load the form with values from the database
    ' -------------------------------------------------------------------------
    Private Sub FEditPlayer_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Dim udtPlayer As udtPlayerType
            Dim udtState As udtStateType

            ' Make a suitcase instance
            udtPlayer = New udtPlayerType
            udtState = New udtStateType

            ' We are busy
            SetBusyCursor(Me, True)

            ' Load States Combobox
            LoadComboBoxFromDatabase("TStates", "intStateID", "strState", cmbStates)

            ' Set the Player ID
            udtPlayer.intPlayerID = f_intPlayerID

            ' Is the data OK (pass in the empty suitcase by ref so it can be filled up)?
            If GetPlayerInformationFromDatabase(udtPlayer) = True Then

                ' Set the State ID
                udtState.intStateID = udtPlayer.intStateID

                ' Is the data OK (pass in the empty suitcase by ref so it can be filled up)?
                If GetStateInformationFromDatabase(udtState) = True Then

                    GetValuesFromForm(udtPlayer, udtState)

                End If
                
            Else

                ' Somthing went wrong. Warn the user ...
                MessageBox.Show(Me, "Error: Unable to load the information for player to edit." & vbNewLine & _
                                    "The form will now close.", "Edit Player Error", _
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                ' ... and close the form
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
    ' Name: GetValuesFromForm
    ' Abstract: Get the values from the fields on the form
    ' -------------------------------------------------------------------------
    Private Sub GetValuesFromForm(ByRef udtPlayer As udtPlayerType, ByRef udtState As udtStateType)

        Try

            txtFirstName.Text = udtPlayer.strFirstName
            txtMiddleName.Text = udtPlayer.strMiddleName
            txtLastName.Text = udtPlayer.strLastName
            txtStreetAddress.Text = udtPlayer.strStreetAddress
            txtCity.Text = udtPlayer.strCity
            cmbStates.Text = udtState.strState
            txtZipCode.Text = udtPlayer.strZipCode
            txtHomePhoneNumber.Text = udtPlayer.strHomePhoneNumber
            txtSalary.Text = FormatCurrency(udtPlayer.decSalary)
            txtDateOfBirth.Text = udtPlayer.dtmDateOfBirth.ToString("yyyy/MM/dd")
            txtEmailAddress.Text = udtPlayer.strEmailAddress

            ' Sex radio buttons
            If udtPlayer.intSexID = 1 Then

                ' If female
                radSexFemale.Checked = True

            Else

                ' If male
                radSexMale.Checked = True

            End If

            ' Most Valuable Player
            If udtPlayer.blnMostValuablePlayer = True Then

                chkMostValuablePlayer.Checked = True

            Else

                chkMostValuablePlayer.Checked = False

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

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
    ' Abstract: Check that the Player and Mascot data is valid
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

            ' Was a home phone number entered?
            If txtDateOfBirth.Text <> "" Then

                ' Yes - Is the home phone number in the proper format?
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
                    strErrorMessage &= "-Email Address must be in proper format."

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
            Dim udtPlayer As New udtPlayerType
            Dim intSelectedStateID As Integer
            Dim liSelectedState As CListItem

            ' Yes, get the player to edit ID
            liSelectedState = cmbStates.SelectedItem
            intSelectedStateID = liSelectedState.GetID

            ' Load it with data from form
            udtPlayer.intPlayerID = f_intPlayerID
            udtPlayer.strFirstName = txtFirstName.Text
            udtPlayer.strMiddleName = txtMiddleName.Text
            udtPlayer.strLastName = txtLastName.Text
            udtPlayer.strStreetAddress = txtStreetAddress.Text
            udtPlayer.strCity = txtCity.Text
            udtPlayer.intStateID = intSelectedStateID
            udtPlayer.strZipCode = txtZipCode.Text
            udtPlayer.strHomePhoneNumber = txtHomePhoneNumber.Text
            udtPlayer.decSalary = ConvertSalaryStringToDecimal(txtSalary.Text)

            ' Saving the date = 1800/01/01 if blank
            ' Was a date of birth entered?
            If txtDateOfBirth.Text <> "" Then

                udtPlayer.dtmDateOfBirth = txtDateOfBirth.Text

            Else

                udtPlayer.dtmDateOfBirth = "01/01/1800"

            End If

            ' Sex Radio Buttons
            ' Female
            If radSexFemale.Checked = True Then

                udtPlayer.intSexID = 1

                ' Male
            ElseIf radSexMale.Checked = True Then

                udtPlayer.intSexID = 2

            End If

            ' Most Valuable Player
            If chkMostValuablePlayer.Checked = True Then

                udtPlayer.blnMostValuablePlayer = True

            Else

                udtPlayer.blnMostValuablePlayer = False

            End If

            ' Email Address
            udtPlayer.strEmailAddress = txtEmailAddress.Text

            ' We are busy
            SetBusyCursor(Me, True)

            f_blnResult = EditPlayerInDatabase2(udtPlayer)

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

            clsPlayer = New CListItem(f_intPlayerID, strFullName)

        Catch excError As Exception

            ' Log and Display Error Message
            WriteLog(excError)

        End Try

        Return clsPlayer

    End Function

End Class