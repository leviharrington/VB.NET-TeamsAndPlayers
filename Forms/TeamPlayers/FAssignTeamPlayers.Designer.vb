<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FAssignTeamPlayers
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
        Me.lblTeam = New System.Windows.Forms.Label()
        Me.cmbTeams = New System.Windows.Forms.ComboBox()
        Me.grpPlayers = New System.Windows.Forms.GroupBox()
        Me.btnNone = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnAll = New System.Windows.Forms.Button()
        Me.lstAvailablePlayers = New System.Windows.Forms.ListBox()
        Me.lblAvailable = New System.Windows.Forms.Label()
        Me.lstSelectedPlayers = New System.Windows.Forms.ListBox()
        Me.lblSelected = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.grpPlayers.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTeam
        '
        Me.lblTeam.AutoSize = True
        Me.lblTeam.Location = New System.Drawing.Point(14, 24)
        Me.lblTeam.Name = "lblTeam"
        Me.lblTeam.Size = New System.Drawing.Size(37, 13)
        Me.lblTeam.TabIndex = 0
        Me.lblTeam.Text = "Team:"
        '
        'cmbTeams
        '
        Me.cmbTeams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTeams.FormattingEnabled = True
        Me.cmbTeams.Location = New System.Drawing.Point(67, 21)
        Me.cmbTeams.Name = "cmbTeams"
        Me.cmbTeams.Size = New System.Drawing.Size(179, 21)
        Me.cmbTeams.TabIndex = 1
        '
        'grpPlayers
        '
        Me.grpPlayers.Controls.Add(Me.btnNone)
        Me.grpPlayers.Controls.Add(Me.btnRemove)
        Me.grpPlayers.Controls.Add(Me.btnAdd)
        Me.grpPlayers.Controls.Add(Me.btnAll)
        Me.grpPlayers.Controls.Add(Me.lstAvailablePlayers)
        Me.grpPlayers.Controls.Add(Me.lblAvailable)
        Me.grpPlayers.Controls.Add(Me.lstSelectedPlayers)
        Me.grpPlayers.Controls.Add(Me.lblSelected)
        Me.grpPlayers.Location = New System.Drawing.Point(18, 58)
        Me.grpPlayers.Name = "grpPlayers"
        Me.grpPlayers.Size = New System.Drawing.Size(607, 351)
        Me.grpPlayers.TabIndex = 2
        Me.grpPlayers.TabStop = False
        Me.grpPlayers.Text = "Players"
        '
        'btnNone
        '
        Me.btnNone.Location = New System.Drawing.Point(246, 242)
        Me.btnNone.Name = "btnNone"
        Me.btnNone.Size = New System.Drawing.Size(115, 33)
        Me.btnNone.TabIndex = 7
        Me.btnNone.Text = "None >>"
        Me.btnNone.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(246, 187)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(115, 33)
        Me.btnRemove.TabIndex = 6
        Me.btnRemove.Text = "Remove >"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(246, 132)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(115, 33)
        Me.btnAdd.TabIndex = 5
        Me.btnAdd.Text = "< Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnAll
        '
        Me.btnAll.Location = New System.Drawing.Point(246, 77)
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Size = New System.Drawing.Size(115, 33)
        Me.btnAll.TabIndex = 4
        Me.btnAll.Text = "<< All"
        Me.btnAll.UseVisualStyleBackColor = True
        '
        'lstAvailablePlayers
        '
        Me.lstAvailablePlayers.FormattingEnabled = True
        Me.lstAvailablePlayers.Location = New System.Drawing.Point(385, 45)
        Me.lstAvailablePlayers.Name = "lstAvailablePlayers"
        Me.lstAvailablePlayers.Size = New System.Drawing.Size(202, 277)
        Me.lstAvailablePlayers.Sorted = True
        Me.lstAvailablePlayers.TabIndex = 3
        '
        'lblAvailable
        '
        Me.lblAvailable.AutoSize = True
        Me.lblAvailable.Location = New System.Drawing.Point(382, 29)
        Me.lblAvailable.Name = "lblAvailable"
        Me.lblAvailable.Size = New System.Drawing.Size(53, 13)
        Me.lblAvailable.TabIndex = 2
        Me.lblAvailable.Text = "Available:"
        '
        'lstSelectedPlayers
        '
        Me.lstSelectedPlayers.FormattingEnabled = True
        Me.lstSelectedPlayers.Location = New System.Drawing.Point(20, 45)
        Me.lstSelectedPlayers.Name = "lstSelectedPlayers"
        Me.lstSelectedPlayers.Size = New System.Drawing.Size(202, 277)
        Me.lstSelectedPlayers.Sorted = True
        Me.lstSelectedPlayers.TabIndex = 1
        '
        'lblSelected
        '
        Me.lblSelected.AutoSize = True
        Me.lblSelected.Location = New System.Drawing.Point(17, 29)
        Me.lblSelected.Name = "lblSelected"
        Me.lblSelected.Size = New System.Drawing.Size(52, 13)
        Me.lblSelected.TabIndex = 0
        Me.lblSelected.Text = "Selected:"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(207, 426)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(228, 43)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'FAssignTeamPlayers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 488)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grpPlayers)
        Me.Controls.Add(Me.cmbTeams)
        Me.Controls.Add(Me.lblTeam)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FAssignTeamPlayers"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Assign Team Players"
        Me.grpPlayers.ResumeLayout(False)
        Me.grpPlayers.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTeam As System.Windows.Forms.Label
    Friend WithEvents cmbTeams As System.Windows.Forms.ComboBox
    Friend WithEvents grpPlayers As System.Windows.Forms.GroupBox
    Friend WithEvents btnNone As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnAll As System.Windows.Forms.Button
    Friend WithEvents lstAvailablePlayers As System.Windows.Forms.ListBox
    Friend WithEvents lblAvailable As System.Windows.Forms.Label
    Friend WithEvents lstSelectedPlayers As System.Windows.Forms.ListBox
    Friend WithEvents lblSelected As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
