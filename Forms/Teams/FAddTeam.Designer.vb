<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FAddTeam
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.txtMascot = New System.Windows.Forms.TextBox()
        Me.txtTeam = New System.Windows.Forms.TextBox()
        Me.lblRequiredField = New System.Windows.Forms.Label()
        Me.lblMascot = New System.Windows.Forms.Label()
        Me.lblTeam = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(151, 102)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 30)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(36, 102)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(90, 30)
        Me.btnOK.TabIndex = 5
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'txtMascot
        '
        Me.txtMascot.Location = New System.Drawing.Point(66, 51)
        Me.txtMascot.MaxLength = 50
        Me.txtMascot.Name = "txtMascot"
        Me.txtMascot.Size = New System.Drawing.Size(197, 20)
        Me.txtMascot.TabIndex = 3
        '
        'txtTeam
        '
        Me.txtTeam.Location = New System.Drawing.Point(66, 14)
        Me.txtTeam.MaxLength = 50
        Me.txtTeam.Name = "txtTeam"
        Me.txtTeam.Size = New System.Drawing.Size(197, 20)
        Me.txtTeam.TabIndex = 1
        '
        'lblRequiredField
        '
        Me.lblRequiredField.AutoSize = True
        Me.lblRequiredField.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRequiredField.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblRequiredField.Location = New System.Drawing.Point(63, 81)
        Me.lblRequiredField.Name = "lblRequiredField"
        Me.lblRequiredField.Size = New System.Drawing.Size(77, 12)
        Me.lblRequiredField.TabIndex = 4
        Me.lblRequiredField.Text = "* = Required Field"
        '
        'lblMascot
        '
        Me.lblMascot.AutoSize = True
        Me.lblMascot.Location = New System.Drawing.Point(13, 54)
        Me.lblMascot.Name = "lblMascot"
        Me.lblMascot.Size = New System.Drawing.Size(49, 13)
        Me.lblMascot.TabIndex = 2
        Me.lblMascot.Text = "Mascot:*"
        '
        'lblTeam
        '
        Me.lblTeam.AutoSize = True
        Me.lblTeam.Location = New System.Drawing.Point(13, 16)
        Me.lblTeam.Name = "lblTeam"
        Me.lblTeam.Size = New System.Drawing.Size(41, 13)
        Me.lblTeam.TabIndex = 0
        Me.lblTeam.Text = "Team:*"
        '
        'FAddTeam
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(277, 146)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtMascot)
        Me.Controls.Add(Me.txtTeam)
        Me.Controls.Add(Me.lblRequiredField)
        Me.Controls.Add(Me.lblMascot)
        Me.Controls.Add(Me.lblTeam)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FAddTeam"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Team"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents txtMascot As System.Windows.Forms.TextBox
    Friend WithEvents txtTeam As System.Windows.Forms.TextBox
    Friend WithEvents lblRequiredField As System.Windows.Forms.Label
    Friend WithEvents lblMascot As System.Windows.Forms.Label
    Friend WithEvents lblTeam As System.Windows.Forms.Label
End Class
