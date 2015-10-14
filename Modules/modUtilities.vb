' -------------------------------------------------------------------------
' Module: modUtilities
' Abstract: Log messages to disk
'
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
Imports System.Text.RegularExpressions


Public Module modUtilities

    ' -------------------------------------------------------------------------
    '  Module constants
    ' -------------------------------------------------------------------------
    ' What log file should we use
    Private Const strLOG_FILE_EXTENSION As String = ".Log"

    ' -------------------------------------------------------------------------
    '  Module variables
    ' -------------------------------------------------------------------------
    Private m_strOldLogFilePath As String           ' Name of the last log file opened
    Private m_fsLogFile As FileStream = Nothing     ' File handle of the last log file opened


#Region "Validation"
    ' -------------------------------------------------------------------------
    ' Name: IsValidZipCode
    ' Abstract: Validate Zip Codes With Regex
    '               Formats:    (5 digit)       ##### 
    '                           (5 + 4 digit)   #####-####
    ' -------------------------------------------------------------------------
    Public Function IsValidZipCode(ByVal strZipCode As String) As Boolean

        Dim blnIsValidZipCode As Boolean = False

        Try

            Dim strStart As String = "^"
            Dim strStop As String = "$"
            Dim strPattern1 As String
            Dim strPattern2 As String

            ' #####
            strPattern1 = strStart & "\d{5}" & strStop

            ' #####-####
            strPattern2 = strStart & "\d{5}" & "\-" & "\d{4}" & strStop

            ' Does it match any of the formats?
            If Regex.IsMatch(strZipCode, strPattern1) = True Or _
               Regex.IsMatch(strZipCode, strPattern2) = True Then

                ' Yes
                blnIsValidZipCode = True

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnIsValidZipCode

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidHomePhoneNumber
    ' Abstract: Validate Home Phone Number With Regex
    ' Formats:  1.  (3 digits, space or dash or dot, 4 digits)   ###-#### 
    '           2.  (3 digits, space or dash or dot, 3 digits, 
    '               space or dash or dot, 4 digits)              ###-###-####
    '           3.  (Left parenthesiss, 3 digits, right parenthesis, 
    '               space or dash or dot, 3 digits, space or dash or dot, 
    '               4 digits)                                   (###) ###-####  
    ' -------------------------------------------------------------------------
    Public Function IsValidHomePhoneNumber(ByVal strHomePhoneNumber As String) As Boolean

        Dim blnIsValidZipCode As Boolean = False

        Try

            Dim strStart As String = "^"
            Dim strStop As String = "$"
            Dim strSpaceOrDashOrDot As String = "[ \-\.]"
            Dim strPattern1 As String
            Dim strPattern2 As String
            Dim strPattern3 As String

            ' ###-####
            strPattern1 = strStart & "\d{3}" & strSpaceOrDashOrDot & "\d{4}" & strStop

            ' ###-###-####
            strPattern2 = strStart & _
                          "\d{3}" & strSpaceOrDashOrDot & _
                          "\d{3}" & strSpaceOrDashOrDot & "\d{4}" & _
                          strStop

            ' (###) ###-####
            strPattern3 = strStart & _
                          "\(\d{3}\)" & strSpaceOrDashOrDot & _
                          "\d{3}" & strSpaceOrDashOrDot & _
                          "\d{4}" & _
                          strStop

            ' Does it match any of the formats?
            If Regex.IsMatch(strHomePhoneNumber, strPattern1) = True Or _
               Regex.IsMatch(strHomePhoneNumber, strPattern2) = True Or _
               Regex.IsMatch(strHomePhoneNumber, strPattern3) Then

                ' Yes
                blnIsValidZipCode = True

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnIsValidZipCode

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidSalary
    ' Abstract: Validate Salary With Regex
    '               Formats:    $###,###.##      or     ###,###.##
    '                           $######.##       or     ######.##
    '                           $###,###         or     ###,###
    '                           $#####           or     #####
    ' -------------------------------------------------------------------------
    Public Function IsValidSalary(ByVal strSalary As String) As Boolean

        Dim blnIsValidSalary As Boolean = False

        Try

            Dim strStart As String = "^"
            Dim strOptionalDollarSign As String = "\$?"
            Dim strOptionalDecimal = "(\.\d{2})?"
            Dim strStop As String = "$"
            Dim strPattern1 As String
            Dim strPattern2 As String

            ' No commas
            strPattern1 = strStart & _
                          strOptionalDollarSign & _
                          "\d+" & _
                          strOptionalDecimal & _
                          strStop

            ' With commas
            strPattern2 = strStart & _
                          strOptionalDollarSign & _
                          "\d{1,3}(,\d{3})*" & _
                          strOptionalDecimal & _
                          strStop

            ' Does it match any of the formats?
            If Regex.IsMatch(strSalary, strPattern1) = True Or _
               Regex.IsMatch(strSalary, strPattern2) = True Then

                ' Yes
                blnIsValidSalary = True

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnIsValidSalary

    End Function



    ' -------------------------------------------------------------------------
    ' Name: ConvertSalaryStringToDecimal
    ' Abstract: Remove commas and $ from salary to put in database
    ' -------------------------------------------------------------------------
    Public Function ConvertSalaryStringToDecimal(ByVal strSalary As String) As Decimal

        Dim decSalary As Decimal = 0

        Try

            ' Remove dollar signs and commas
            strSalary = strSalary.Replace("$", "")
            strSalary = strSalary.Replace(",", "")

            decSalary = Val(strSalary)

        Catch excError As Exception

            ' Log and display errors
            WriteLog(excError)

        End Try

        Return decSalary

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidDateOfBirth
    ' Abstract: Validate DateOfBirth With Regex
    '               Formats:    yyyy/mm/dd   or  yyyy-mm-dd
    ' -------------------------------------------------------------------------
    Public Function IsValidDate(ByVal strDate As String) As Boolean

        Dim blnIsValidDate As Boolean = False

        Try

            Dim strStart As String = "^"
            Dim strYear As String = "\d{4}"
            Dim strSlashOrDash As String = "[\-|\/]"
            Dim strMonth As String = "(0[1-9]|[1-9]|1[012])"
            Dim strDay As String = "(0[1-9]|[1-9]|[12][0-9]|3[01])"
            Dim strStop As String = "$"
            Dim strPattern1 As String

            strPattern1 = strStart & _
                          strYear & _
                          strSlashOrDash & _
                          strMonth & _
                          strSlashOrDash & _
                          strDay & _
                          strStop

            ' Does it match any of the formats?
            If Regex.IsMatch(strDate, strPattern1) = True Then

                ' Yes
                blnIsValidDate = True

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnIsValidDate

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsValidEmailAddress
    ' Abstract: Validate Email Address With Regex
    ' -------------------------------------------------------------------------
    Public Function IsValidEmailAddress(ByVal strEmailAddress As String) As Boolean

        Dim blnIsValidEmailAddress As Boolean = False

        Try

            Dim strStart As String = "^"
            Dim strStop As String = "$"
            Dim strPattern1 As String

            ' 1: letter, N: letters, numbers, dots or dashs
            ' @
            ' 1: letter, N: letters, numbers, dots or dashs, 1: dot, 2-6: letters
            strPattern1 = strStart & _
                          "[a-zA-Z][a-zA-Z0-9\.\-]*" & _
                          "@" & _
                          "[a-zA-Z][a-zA-Z0-9\.\-]*\.[a-zA-Z]{2,6}" & _
                          strStop

            ' Does it match any of the formats?
            If Regex.IsMatch(strEmailAddress, strPattern1) = True Then

                ' Yes
                blnIsValidEmailAddress = True

            End If

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return blnIsValidEmailAddress

    End Function

#End Region

    ' -------------------------------------------------------------------------
    ' Name: TrimAllFormTextBoxes
    ' Abstract: Trim all text boxes on form
    '
    '   Example Call:  TrimAllFormTextBoxes( Me )
    '
    ' -------------------------------------------------------------------------
    Public Sub TrimAllFormTextBoxes(ByRef frmTarget As Form)

        Try

            TrimAllFormTextBoxes(frmTarget.Controls)

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: TrimAllFormTextBoxes
    ' Abstract: Trim all text boxes on form
    ' -------------------------------------------------------------------------
    Private Sub TrimAllFormTextBoxes(ByVal ccTarget As Control.ControlCollection)

        Try

            Dim intIndex As Integer
            Dim ctlCurrentControl As Control

            ' Trim all the top level controls
            For intIndex = 0 To ccTarget.Count - 1

                ctlCurrentControl = ccTarget.Item(intIndex)

                ' Is it a textbox?
                If TypeOf ctlCurrentControl Is TextBox Then

                    ' Yes - trim it
                    ctlCurrentControl.Text = ctlCurrentControl.Text.Trim

                End If

                ' Container (e.g. groupbox, panel, etc)?
                If ctlCurrentControl.HasChildren = True Then

                    ' Yes, recurse and get child controls
                    TrimAllFormTextBoxes(ctlCurrentControl)

                End If

            Next

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: HighlightNextItemInList
    ' Abstract: Highlight the next item in listbox after a delete
    ' -------------------------------------------------------------------------
    Public Sub HighLightNextItemInList(ByRef lstTarget As ListBox, ByVal intTargetIndex As Integer)

        Try

            ' Passed the end of the list?
            If intTargetIndex > lstTarget.Items.Count - 1 Then

                ' Yes, set it to the end
                intTargetIndex = lstTarget.Items.Count - 1

            End If

            ' Select it
            lstTarget.SelectedIndex = intTargetIndex

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: SetBusyCursor
    ' Abstract: Enable/Disable the form and set the cursor to normal or busy
    ' -------------------------------------------------------------------------
    Public Sub SetBusyCursor(ByRef frmForm As Form, ByVal blnBusy As Boolean)

        Try

            ' Busy?
            If blnBusy = True Then

                ' Yes
                frmForm.Cursor = Cursors.WaitCursor
                frmForm.Enabled = False

            Else

                ' No
                frmForm.Cursor = Cursors.Default
                frmForm.Enabled = True

            End If

        Catch excError As Exception

            ' Display error message
            WriteLog(excError)

        End Try

    End Sub

#Region "WriteLog"

    ' -------------------------------------------------------------------------
    ' Name: WriteLog
    ' Abstract: Overload withd blnDisplay set to true
    ' -------------------------------------------------------------------------
    Public Sub WriteLog(ByVal excErrorToLog As Exception, _
               Optional ByVal blnDisplayWarning As Boolean = True)

        Try

            WriteLog(excErrorToLog.ToString(), blnDisplayWarning)

        Catch excError As Exception

            ' Display error message
            MessageBox.Show("Error:" & vbNewLine & excError.ToString(), _
                            Application.ProductName, _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: WriteLog
    ' Abstract: Write a message to the error log.
    ' -------------------------------------------------------------------------
    Public Sub WriteLog(ByVal strMessageToLog As String, _
               Optional ByVal blnDisplayWarning As Boolean = True)

        Try

            Dim fsLogFile As FileStream = Nothing
            Dim encConvertToByteArray As New System.Text.UTF8Encoding

            ' Warn the user?
            If blnDisplayWarning = True Then

                ' Yes( ProductName is set in AssemblyInfo )
                MessageBox.Show(strMessageToLog, Application.ProductName, _
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)

            End If

            ' Append a date/time stamp
            strMessageToLog = (DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss") _
                              & " - " & strMessageToLog & vbNewLine & _
                              vbNewLine

            ' Get a free file handle
            fsLogFile = GetLogFile()

            ' Is the file OK?
            If Not fsLogFile Is Nothing Then

                ' Yes, Log it
                fsLogFile.Write(encConvertToByteArray.GetBytes(strMessageToLog), _
                                0, strMessageToLog.Length)

                ' Flush the buffer so we can immediately see results in file.  Very important.
                ' Otherwise we have to wait for flush which might be when application closes
                ' or we get another error.  Waiting for the application to close may not be
                ' a good idea if the application is in a production environment (e.g. a web
                '  app running on a remote server)
                fsLogFile.Flush()

            End If

        Catch excError As Exception

            ' Display error message
            MessageBox.Show("Error:" & vbNewLine & excError.ToString(), _
                            Application.ProductName, _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: DeleteOldFiles
    ' Abstract: Delete any files older than 10 days.
    ' -------------------------------------------------------------------------
    Private Sub DeleteOldFiles()

        Try

            Dim strLogFilePath As String = ""
            Dim dirLogDirectory As DirectoryInfo = Nothing
            Dim dtmFileCreated As DateTime = Now
            Dim intDaysOld As Integer = 0

            ' Path
            strLogFilePath = Application.StartupPath & "\Log\"

            ' Look for any files
            dirLogDirectory = New DirectoryInfo(strLogFilePath)

            ' Are there any?
            For Each finLogFile As FileInfo _
                In dirLogDirectory.GetFiles("*" & strLOG_FILE_EXTENSION)

                ' When was the file created?
                dtmFileCreated = finLogFile.CreationTime

                ' How old is the file?
                intDaysOld = (dtmFileCreated.Subtract(DateTime.Now)).Days

                ' Is the file older than 10 days?
                If intDaysOld > 10 Then

                    ' Yes.  Delete it.
                    finLogFile.Delete()

                End If

            Next

        Catch excError As Exception

            ' Display error message
            MessageBox.Show("Error:" & vbNewLine & excError.ToString(), _
                            Application.ProductName, _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: GetLogFile
    ' Abstract: Open the log file for writing.  Use today's date as part of
    '           the file name.  Each day a new log file will be created.
    '           Makes debug easier.
    '           Use a filestream object so we can specify file read share
    '           during the open call.
    ' -------------------------------------------------------------------------
    Private Function GetLogFile() As FileStream

        Try
            Dim strToday As String = (DateTime.Now).ToString("yyyyMMdd")
            Dim strLogFilePath As String = ""

            ' Log everything in a log directory off of the current application directory
            strLogFilePath = Application.StartupPath & _
                             "\Log\" & strToday & strLOG_FILE_EXTENSION

            ' Is this a new day?
            If m_strOldLogFilePath <> strLogFilePath Then

                ' Save the log file name
                m_strOldLogFilePath = strLogFilePath

                ' Does the log directory exist?
                If Directory.Exists(Application.StartupPath & "\Log") = False Then

                    ' No, so create it
                    Directory.CreateDirectory(Application.StartupPath & "\Log")

                End If

                ' Close old log file( if there is one )
                If Not m_fsLogFile Is Nothing Then m_fsLogFile.Close()

                ' Delete old log files
                DeleteOldFiles()

                ' Does the file exist?
                If File.Exists(strLogFilePath) = False Then

                    ' No, create with shared read access so it can be read while application has it open
                    m_fsLogFile = New FileStream(strLogFilePath, FileMode.Create, _
                                                 FileAccess.Write, FileShare.Read)

                Else

                    ' Yes, append with shared read access so it can be read while application has it open
                    m_fsLogFile = New FileStream(strLogFilePath, FileMode.Append, _
                                                 FileAccess.Write, FileShare.Read)

                End If

            End If

        Catch excError As Exception

            ' Display error message
            MessageBox.Show("Error:" & vbNewLine & excError.ToString(), _
                            Application.ProductName, _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

        ' Return result
        Return m_fsLogFile

    End Function

#End Region

End Module