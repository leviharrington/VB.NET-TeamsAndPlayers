<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnManageTeams = New System.Windows.Forms.Button()
        Me.btnManagePlayers = New System.Windows.Forms.Button()
        Me.btnManageTeamPlayers = New System.Windows.Forms.Button()
        Me.mnuMainMenu = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsManage = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsManageTeams = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsManagePlayers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsAssign = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsAssignTeamPlayers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelpAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.grpManageAssign = New System.Windows.Forms.GroupBox()
        Me.mnuMainMenu.SuspendLayout()
        Me.grpManageAssign.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnManageTeams
        '
        Me.btnManageTeams.Location = New System.Drawing.Point(19, 32)
        Me.btnManageTeams.Name = "btnManageTeams"
        Me.btnManageTeams.Size = New System.Drawing.Size(221, 40)
        Me.btnManageTeams.TabIndex = 0
        Me.btnManageTeams.Text = "Teams"
        Me.btnManageTeams.UseVisualStyleBackColor = True
        '
        'btnManagePlayers
        '
        Me.btnManagePlayers.Location = New System.Drawing.Point(19, 152)
        Me.btnManagePlayers.Name = "btnManagePlayers"
        Me.btnManagePlayers.Size = New System.Drawing.Size(221, 40)
        Me.btnManagePlayers.TabIndex = 1
        Me.btnManagePlayers.Text = "Players"
        Me.btnManagePlayers.UseVisualStyleBackColor = True
        '
        'btnManageTeamPlayers
        '
        Me.btnManageTeamPlayers.Location = New System.Drawing.Point(19, 93)
        Me.btnManageTeamPlayers.Name = "btnManageTeamPlayers"
        Me.btnManageTeamPlayers.Size = New System.Drawing.Size(221, 40)
        Me.btnManageTeamPlayers.TabIndex = 2
        Me.btnManageTeamPlayers.Text = "Team Players"
        Me.btnManageTeamPlayers.UseVisualStyleBackColor = True
        '
        'mnuMainMenu
        '
        Me.mnuMainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuTools, Me.mnuHelp})
        Me.mnuMainMenu.Location = New System.Drawing.Point(0, 0)
        Me.mnuMainMenu.Name = "mnuMainMenu"
        Me.mnuMainMenu.Size = New System.Drawing.Size(293, 24)
        Me.mnuMainMenu.TabIndex = 3
        Me.mnuMainMenu.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFileExit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "File"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Name = "mnuFileExit"
        Me.mnuFileExit.Size = New System.Drawing.Size(152, 22)
        Me.mnuFileExit.Text = "Exit"
        '
        'mnuTools
        '
        Me.mnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuToolsManage, Me.mnuToolsAssign})
        Me.mnuTools.Name = "mnuTools"
        Me.mnuTools.Size = New System.Drawing.Size(48, 20)
        Me.mnuTools.Text = "Tools"
        '
        'mnuToolsManage
        '
        Me.mnuToolsManage.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuToolsManageTeams, Me.mnuToolsManagePlayers})
        Me.mnuToolsManage.Name = "mnuToolsManage"
        Me.mnuToolsManage.Size = New System.Drawing.Size(152, 22)
        Me.mnuToolsManage.Text = "Manage"
        '
        'mnuToolsManageTeams
        '
        Me.mnuToolsManageTeams.Name = "mnuToolsManageTeams"
        Me.mnuToolsManageTeams.Size = New System.Drawing.Size(152, 22)
        Me.mnuToolsManageTeams.Text = "Teams"
        '
        'mnuToolsManagePlayers
        '
        Me.mnuToolsManagePlayers.Name = "mnuToolsManagePlayers"
        Me.mnuToolsManagePlayers.Size = New System.Drawing.Size(152, 22)
        Me.mnuToolsManagePlayers.Text = "Players"
        '
        'mnuToolsAssign
        '
        Me.mnuToolsAssign.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuToolsAssignTeamPlayers})
        Me.mnuToolsAssign.Name = "mnuToolsAssign"
        Me.mnuToolsAssign.Size = New System.Drawing.Size(152, 22)
        Me.mnuToolsAssign.Text = "Assign"
        '
        'mnuToolsAssignTeamPlayers
        '
        Me.mnuToolsAssignTeamPlayers.Name = "mnuToolsAssignTeamPlayers"
        Me.mnuToolsAssignTeamPlayers.Size = New System.Drawing.Size(152, 22)
        Me.mnuToolsAssignTeamPlayers.Text = "Team Players"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHelpAbout})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(44, 20)
        Me.mnuHelp.Text = "Help"
        '
        'mnuHelpAbout
        '
        Me.mnuHelpAbout.Name = "mnuHelpAbout"
        Me.mnuHelpAbout.Size = New System.Drawing.Size(152, 22)
        Me.mnuHelpAbout.Text = "About"
        '
        'grpManageAssign
        '
        Me.grpManageAssign.Controls.Add(Me.btnManageTeamPlayers)
        Me.grpManageAssign.Controls.Add(Me.btnManageTeams)
        Me.grpManageAssign.Controls.Add(Me.btnManagePlayers)
        Me.grpManageAssign.Location = New System.Drawing.Point(17, 39)
        Me.grpManageAssign.Name = "grpManageAssign"
        Me.grpManageAssign.Size = New System.Drawing.Size(258, 213)
        Me.grpManageAssign.TabIndex = 4
        Me.grpManageAssign.TabStop = False
        Me.grpManageAssign.Text = "Manage / Assign"
        '
        'FMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(293, 273)
        Me.Controls.Add(Me.grpManageAssign)
        Me.Controls.Add(Me.mnuMainMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MainMenuStrip = Me.mnuMainMenu
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Homework 15 - SQL Server"
        Me.mnuMainMenu.ResumeLayout(False)
        Me.mnuMainMenu.PerformLayout()
        Me.grpManageAssign.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnManageTeams As System.Windows.Forms.Button
    Friend WithEvents btnManagePlayers As System.Windows.Forms.Button
    Friend WithEvents btnManageTeamPlayers As System.Windows.Forms.Button
    Friend WithEvents mnuMainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsManage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsManageTeams As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsManagePlayers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsAssign As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsAssignTeamPlayers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHelpAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grpManageAssign As System.Windows.Forms.GroupBox
End Class
