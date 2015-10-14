' -------------------------------------------------------------------------
' Class: CListItem
' Abstract: When adding an item to a list we need to store 2 pieces of information:
'                   -name: text part of item for user
'                   -ID: unique identifier for item for programmer to limit the
'                        effects of DML commands to a single row/record
'
'           The name is useless without the ID and the ID is useless without the name.
'           So we package them up in either a structure or a class.
'           Since we want to display these together in a listbox or combobox 
'           we need to override the ToString method to display the name. 
'           Since we have a procedure and variables we should use a class 
'           instead of a structure.

' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On


' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------

Public Class CListItem


    ' -------------------------------------------------------------------------
    ' Properties
    ' -------------------------------------------------------------------------
    Private m_intID As Integer
    Private m_strName As String



    ' -------------------------------------------------------------------------
    ' Methods
    ' -------------------------------------------------------------------------

    ' -------------------------------------------------------------------------
    ' Name: New
    ' Abstract: Default constructor
    ' -------------------------------------------------------------------------
    Public Sub New()

        Try

            m_intID = 0
            m_strName = ""

        Catch excError As Exception

            WriteLog(excError)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: New
    ' Abstract: Paramaterized constructor
    ' -------------------------------------------------------------------------
    Public Sub New(ByVal intID As Integer, ByVal strName As String)

        Try

            Initialize(intID, strName)

        Catch excError As Exception

            WriteLog(excError)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: GetID
    ' Abstract: Get the id property
    ' -------------------------------------------------------------------------
    Public Function GetID() As Integer

        Try

            ' Nothing to do

        Catch excError As Exception

            WriteLog(excError)

        End Try

        Return m_intID

    End Function



    ' -------------------------------------------------------------------------
    ' Name: SetId
    ' Abstract: Set the id property
    ' -------------------------------------------------------------------------
    Public Sub SetID(ByVal intID As Integer)

        Try

            m_intID = intID

        Catch excError As Exception

            WriteLog(excError)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: Initialize
    ' Abstract: Set ID and Name
    ' -------------------------------------------------------------------------
    Public Sub Initialize(ByVal intID As Integer, ByVal strName As String)

        Try

            SetID(intID)
            SetName(strName)

        Catch excError As Exception

            WriteLog(excError)

        End Try

    End Sub

    ' --------------------------------------------------------------------------------
    ' Name: GetName
    ' Abstract: Get the name property
    ' --------------------------------------------------------------------------------
    Public Function GetName() As String

        Try

            ' Nothing to do

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return m_strName

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: SetName
    ' Abstract: Set the name property
    ' --------------------------------------------------------------------------------
    Public Sub SetName(ByVal strName As String)

        Try

            m_strName = strName

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: ToString
    ' Abstract: Return the name property.  This is what gets displayed in 
    '           a listbox or combobox.  Each instance will have its own string.
    ' -------------------------------------------------------------------------------
    Public Overrides Function ToString() As String

        Dim strStringToDisplayInListBoxOrComboBox As String = ""

        Try

            strStringToDisplayInListBoxOrComboBox = m_strName

        Catch excError As Exception

            ' Log and display error message
            WriteLog(excError)

        End Try

        Return strStringToDisplayInListBoxOrComboBox

    End Function

End Class
