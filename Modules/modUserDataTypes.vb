' -------------------------------------------------------------------------
' Module: Module modUserDataTypes
' Abstract: Our use defined data types
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On


' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------



Public Module modUserDataTypes

    ' Structures are like suitcases
    ' It's a lot easier to carry one suitcase instead of a whole bunch of loose 
    ' items. Same with passing data to a procedure. Instead of having 4+ variables,
    ' just use a structure.

    ' Team
    Public Structure udtTeamType

        Dim intTeamID As Integer
        Dim strTeam As String
        Dim strMascot As String
        Dim intTeamStatusID As Integer

    End Structure

    ' Player
    Public Structure udtPlayerType

        Dim intPlayerID As Integer
        Dim strFirstName As String
        Dim strMiddleName As String
        Dim strLastName As String
        Dim strStreetAddress As String
        Dim strCity As String
        Dim intStateID As Integer
        Dim strZipCode As String
        Dim strHomePhoneNumber As String
        Dim decSalary As Decimal
        Dim dtmDateOfBirth As Date
        Dim intSexID As Integer
        Dim blnMostValuablePlayer As Boolean
        Dim strEmailAddress As String
        Dim intPlayerStatusID As Integer

    End Structure

    ' State
    Public Structure udtStateType

        Dim intStateID As Integer
        Dim strState As String
        Dim strStateAbbreviation As String

    End Structure

    ' Sex
    Public Structure udtSexType

        Dim intSexID As Integer
        Dim strSex As String

    End Structure

End Module