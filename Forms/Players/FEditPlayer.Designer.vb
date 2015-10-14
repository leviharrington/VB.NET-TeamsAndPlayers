<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FEditPlayer
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
        Me.lblSalaryFormat = New System.Windows.Forms.Label()
        Me.txtEmailAddress = New System.Windows.Forms.TextBox()
        Me.lblEmailAddress = New System.Windows.Forms.Label()
        Me.chkMostValuablePlayer = New System.Windows.Forms.CheckBox()
        Me.lblMostValuablePlayer = New System.Windows.Forms.Label()
        Me.radSexMale = New System.Windows.Forms.RadioButton()
        Me.radSexFemale = New System.Windows.Forms.RadioButton()
        Me.lblDateOfBirthFormat = New System.Windows.Forms.Label()
        Me.txtDateOfBirth = New System.Windows.Forms.TextBox()
        Me.lblDateOfBirth = New System.Windows.Forms.Label()
        Me.cmbStates = New System.Windows.Forms.ComboBox()
        Me.txtSalary = New System.Windows.Forms.TextBox()
        Me.lblSalary = New System.Windows.Forms.Label()
        Me.lblHomePhoneNumberFormat = New System.Windows.Forms.Label()
        Me.txtHomePhoneNumber = New System.Windows.Forms.TextBox()
        Me.lblHomePhoneNumber = New System.Windows.Forms.Label()
        Me.txtZipCode = New System.Windows.Forms.TextBox()
        Me.lblZipCode = New System.Windows.Forms.Label()
        Me.lblState = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtStreetAddress = New System.Windows.Forms.TextBox()
        Me.lblStreetAddress = New System.Windows.Forms.Label()
        Me.txtMiddleName = New System.Windows.Forms.TextBox()
        Me.lblMiddleName = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.lblRequiredField = New System.Windows.Forms.Label()
        Me.lblMascot = New System.Windows.Forms.Label()
        Me.lblTeam = New System.Windows.Forms.Label()
        Me.lblSex = New System.Windows.Forms.Label()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblSalaryFormat
        '
        Me.lblSalaryFormat.AutoSize = True
        Me.lblSalaryFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalaryFormat.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblSalaryFormat.Location = New System.Drawing.Point(19, 343)
        Me.lblSalaryFormat.Name = "lblSalaryFormat"
        Me.lblSalaryFormat.Size = New System.Drawing.Size(38, 12)
        Me.lblSalaryFormat.TabIndex = 19
        Me.lblSalaryFormat.Text = "$12,345"
        '
        'txtEmailAddress
        '
        Me.txtEmailAddress.Location = New System.Drawing.Point(144, 477)
        Me.txtEmailAddress.MaxLength = 50
        Me.txtEmailAddress.Name = "txtEmailAddress"
        Me.txtEmailAddress.Size = New System.Drawing.Size(188, 20)
        Me.txtEmailAddress.TabIndex = 29
        '
        'lblEmailAddress
        '
        Me.lblEmailAddress.AutoSize = True
        Me.lblEmailAddress.Location = New System.Drawing.Point(18, 480)
        Me.lblEmailAddress.Name = "lblEmailAddress"
        Me.lblEmailAddress.Size = New System.Drawing.Size(76, 13)
        Me.lblEmailAddress.TabIndex = 28
        Me.lblEmailAddress.Text = "Email Address:"
        '
        'chkMostValuablePlayer
        '
        Me.chkMostValuablePlayer.AutoSize = True
        Me.chkMostValuablePlayer.Location = New System.Drawing.Point(144, 442)
        Me.chkMostValuablePlayer.Name = "chkMostValuablePlayer"
        Me.chkMostValuablePlayer.Size = New System.Drawing.Size(44, 17)
        Me.chkMostValuablePlayer.TabIndex = 27
        Me.chkMostValuablePlayer.Text = "Yes"
        Me.chkMostValuablePlayer.UseVisualStyleBackColor = True
        '
        'lblMostValuablePlayer
        '
        Me.lblMostValuablePlayer.AutoSize = True
        Me.lblMostValuablePlayer.Location = New System.Drawing.Point(18, 443)
        Me.lblMostValuablePlayer.Name = "lblMostValuablePlayer"
        Me.lblMostValuablePlayer.Size = New System.Drawing.Size(109, 13)
        Me.lblMostValuablePlayer.TabIndex = 26
        Me.lblMostValuablePlayer.Text = "Most Valuable Player:"
        '
        'radSexMale
        '
        Me.radSexMale.AutoSize = True
        Me.radSexMale.Location = New System.Drawing.Point(249, 405)
        Me.radSexMale.Name = "radSexMale"
        Me.radSexMale.Size = New System.Drawing.Size(48, 17)
        Me.radSexMale.TabIndex = 25
        Me.radSexMale.TabStop = True
        Me.radSexMale.Text = "Male"
        Me.radSexMale.UseVisualStyleBackColor = True
        '
        'radSexFemale
        '
        Me.radSexFemale.AutoSize = True
        Me.radSexFemale.Checked = True
        Me.radSexFemale.Location = New System.Drawing.Point(167, 405)
        Me.radSexFemale.Name = "radSexFemale"
        Me.radSexFemale.Size = New System.Drawing.Size(59, 17)
        Me.radSexFemale.TabIndex = 24
        Me.radSexFemale.TabStop = True
        Me.radSexFemale.Text = "Female"
        Me.radSexFemale.UseVisualStyleBackColor = True
        '
        'lblDateOfBirthFormat
        '
        Me.lblDateOfBirthFormat.AutoSize = True
        Me.lblDateOfBirthFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateOfBirthFormat.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblDateOfBirthFormat.Location = New System.Drawing.Point(19, 380)
        Me.lblDateOfBirthFormat.Name = "lblDateOfBirthFormat"
        Me.lblDateOfBirthFormat.Size = New System.Drawing.Size(57, 12)
        Me.lblDateOfBirthFormat.TabIndex = 22
        Me.lblDateOfBirthFormat.Text = "yyyy/mm/dd"
        '
        'txtDateOfBirth
        '
        Me.txtDateOfBirth.Location = New System.Drawing.Point(144, 364)
        Me.txtDateOfBirth.MaxLength = 50
        Me.txtDateOfBirth.Name = "txtDateOfBirth"
        Me.txtDateOfBirth.Size = New System.Drawing.Size(188, 20)
        Me.txtDateOfBirth.TabIndex = 21
        '
        'lblDateOfBirth
        '
        Me.lblDateOfBirth.AutoSize = True
        Me.lblDateOfBirth.Location = New System.Drawing.Point(18, 367)
        Me.lblDateOfBirth.Name = "lblDateOfBirth"
        Me.lblDateOfBirth.Size = New System.Drawing.Size(69, 13)
        Me.lblDateOfBirth.TabIndex = 20
        Me.lblDateOfBirth.Text = "Date of Birth:"
        '
        'cmbStates
        '
        Me.cmbStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStates.FormattingEnabled = True
        Me.cmbStates.Location = New System.Drawing.Point(144, 210)
        Me.cmbStates.Name = "cmbStates"
        Me.cmbStates.Size = New System.Drawing.Size(188, 21)
        Me.cmbStates.TabIndex = 11
        '
        'txtSalary
        '
        Me.txtSalary.Location = New System.Drawing.Point(144, 327)
        Me.txtSalary.MaxLength = 50
        Me.txtSalary.Name = "txtSalary"
        Me.txtSalary.Size = New System.Drawing.Size(188, 20)
        Me.txtSalary.TabIndex = 18
        Me.txtSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSalary
        '
        Me.lblSalary.AutoSize = True
        Me.lblSalary.Location = New System.Drawing.Point(18, 330)
        Me.lblSalary.Name = "lblSalary"
        Me.lblSalary.Size = New System.Drawing.Size(39, 13)
        Me.lblSalary.TabIndex = 17
        Me.lblSalary.Text = "Salary:"
        '
        'lblHomePhoneNumberFormat
        '
        Me.lblHomePhoneNumberFormat.AutoSize = True
        Me.lblHomePhoneNumberFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHomePhoneNumberFormat.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblHomePhoneNumberFormat.Location = New System.Drawing.Point(19, 303)
        Me.lblHomePhoneNumberFormat.Name = "lblHomePhoneNumberFormat"
        Me.lblHomePhoneNumberFormat.Size = New System.Drawing.Size(111, 12)
        Me.lblHomePhoneNumberFormat.TabIndex = 16
        Me.lblHomePhoneNumberFormat.Text = "###-#### or ###-###-####"
        '
        'txtHomePhoneNumber
        '
        Me.txtHomePhoneNumber.Location = New System.Drawing.Point(144, 287)
        Me.txtHomePhoneNumber.MaxLength = 50
        Me.txtHomePhoneNumber.Name = "txtHomePhoneNumber"
        Me.txtHomePhoneNumber.Size = New System.Drawing.Size(188, 20)
        Me.txtHomePhoneNumber.TabIndex = 15
        '
        'lblHomePhoneNumber
        '
        Me.lblHomePhoneNumber.AutoSize = True
        Me.lblHomePhoneNumber.Location = New System.Drawing.Point(18, 290)
        Me.lblHomePhoneNumber.Name = "lblHomePhoneNumber"
        Me.lblHomePhoneNumber.Size = New System.Drawing.Size(112, 13)
        Me.lblHomePhoneNumber.TabIndex = 14
        Me.lblHomePhoneNumber.Text = "Home Phone Number:"
        '
        'txtZipCode
        '
        Me.txtZipCode.Location = New System.Drawing.Point(144, 248)
        Me.txtZipCode.MaxLength = 50
        Me.txtZipCode.Name = "txtZipCode"
        Me.txtZipCode.Size = New System.Drawing.Size(188, 20)
        Me.txtZipCode.TabIndex = 13
        '
        'lblZipCode
        '
        Me.lblZipCode.AutoSize = True
        Me.lblZipCode.Location = New System.Drawing.Point(18, 251)
        Me.lblZipCode.Name = "lblZipCode"
        Me.lblZipCode.Size = New System.Drawing.Size(53, 13)
        Me.lblZipCode.TabIndex = 12
        Me.lblZipCode.Text = "Zip Code:"
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.Location = New System.Drawing.Point(18, 213)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(35, 13)
        Me.lblState.TabIndex = 10
        Me.lblState.Text = "State:"
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(144, 172)
        Me.txtCity.MaxLength = 50
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(188, 20)
        Me.txtCity.TabIndex = 9
        '
        'txtStreetAddress
        '
        Me.txtStreetAddress.Location = New System.Drawing.Point(144, 134)
        Me.txtStreetAddress.MaxLength = 50
        Me.txtStreetAddress.Name = "txtStreetAddress"
        Me.txtStreetAddress.Size = New System.Drawing.Size(188, 20)
        Me.txtStreetAddress.TabIndex = 7
        '
        'lblStreetAddress
        '
        Me.lblStreetAddress.AutoSize = True
        Me.lblStreetAddress.Location = New System.Drawing.Point(18, 137)
        Me.lblStreetAddress.Name = "lblStreetAddress"
        Me.lblStreetAddress.Size = New System.Drawing.Size(79, 13)
        Me.lblStreetAddress.TabIndex = 6
        Me.lblStreetAddress.Text = "Street Address:"
        '
        'txtMiddleName
        '
        Me.txtMiddleName.Location = New System.Drawing.Point(144, 58)
        Me.txtMiddleName.MaxLength = 50
        Me.txtMiddleName.Name = "txtMiddleName"
        Me.txtMiddleName.Size = New System.Drawing.Size(188, 20)
        Me.txtMiddleName.TabIndex = 3
        '
        'lblMiddleName
        '
        Me.lblMiddleName.AutoSize = True
        Me.lblMiddleName.Location = New System.Drawing.Point(18, 61)
        Me.lblMiddleName.Name = "lblMiddleName"
        Me.lblMiddleName.Size = New System.Drawing.Size(72, 13)
        Me.lblMiddleName.TabIndex = 2
        Me.lblMiddleName.Text = "Middle Name:"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(188, 542)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(95, 31)
        Me.btnCancel.TabIndex = 32
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(71, 542)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(95, 31)
        Me.btnOK.TabIndex = 31
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(144, 96)
        Me.txtLastName.MaxLength = 50
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(188, 20)
        Me.txtLastName.TabIndex = 5
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(144, 20)
        Me.txtFirstName.MaxLength = 50
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(188, 20)
        Me.txtFirstName.TabIndex = 1
        '
        'lblRequiredField
        '
        Me.lblRequiredField.AutoSize = True
        Me.lblRequiredField.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRequiredField.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblRequiredField.Location = New System.Drawing.Point(139, 516)
        Me.lblRequiredField.Name = "lblRequiredField"
        Me.lblRequiredField.Size = New System.Drawing.Size(77, 12)
        Me.lblRequiredField.TabIndex = 30
        Me.lblRequiredField.Text = "* = Required Field"
        '
        'lblMascot
        '
        Me.lblMascot.AutoSize = True
        Me.lblMascot.Location = New System.Drawing.Point(18, 99)
        Me.lblMascot.Name = "lblMascot"
        Me.lblMascot.Size = New System.Drawing.Size(65, 13)
        Me.lblMascot.TabIndex = 4
        Me.lblMascot.Text = "Last Name:*"
        '
        'lblTeam
        '
        Me.lblTeam.AutoSize = True
        Me.lblTeam.Location = New System.Drawing.Point(18, 23)
        Me.lblTeam.Name = "lblTeam"
        Me.lblTeam.Size = New System.Drawing.Size(64, 13)
        Me.lblTeam.TabIndex = 0
        Me.lblTeam.Text = "First Name:*"
        '
        'lblSex
        '
        Me.lblSex.AutoSize = True
        Me.lblSex.Location = New System.Drawing.Point(16, 407)
        Me.lblSex.Name = "lblSex"
        Me.lblSex.Size = New System.Drawing.Size(28, 13)
        Me.lblSex.TabIndex = 23
        Me.lblSex.Text = "Sex:"
        '
        'lblCity
        '
        Me.lblCity.AutoSize = True
        Me.lblCity.Location = New System.Drawing.Point(16, 177)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(27, 13)
        Me.lblCity.TabIndex = 8
        Me.lblCity.Text = "City:"
        '
        'FEditPlayer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(351, 592)
        Me.Controls.Add(Me.lblCity)
        Me.Controls.Add(Me.lblSex)
        Me.Controls.Add(Me.lblSalaryFormat)
        Me.Controls.Add(Me.txtEmailAddress)
        Me.Controls.Add(Me.lblEmailAddress)
        Me.Controls.Add(Me.chkMostValuablePlayer)
        Me.Controls.Add(Me.lblMostValuablePlayer)
        Me.Controls.Add(Me.radSexMale)
        Me.Controls.Add(Me.radSexFemale)
        Me.Controls.Add(Me.lblDateOfBirthFormat)
        Me.Controls.Add(Me.txtDateOfBirth)
        Me.Controls.Add(Me.lblDateOfBirth)
        Me.Controls.Add(Me.cmbStates)
        Me.Controls.Add(Me.txtSalary)
        Me.Controls.Add(Me.lblSalary)
        Me.Controls.Add(Me.lblHomePhoneNumberFormat)
        Me.Controls.Add(Me.txtHomePhoneNumber)
        Me.Controls.Add(Me.lblHomePhoneNumber)
        Me.Controls.Add(Me.txtZipCode)
        Me.Controls.Add(Me.lblZipCode)
        Me.Controls.Add(Me.lblState)
        Me.Controls.Add(Me.txtCity)
        Me.Controls.Add(Me.txtStreetAddress)
        Me.Controls.Add(Me.lblStreetAddress)
        Me.Controls.Add(Me.txtMiddleName)
        Me.Controls.Add(Me.lblMiddleName)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtLastName)
        Me.Controls.Add(Me.txtFirstName)
        Me.Controls.Add(Me.lblRequiredField)
        Me.Controls.Add(Me.lblMascot)
        Me.Controls.Add(Me.lblTeam)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FEditPlayer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Player"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSalaryFormat As System.Windows.Forms.Label
    Friend WithEvents txtEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblEmailAddress As System.Windows.Forms.Label
    Friend WithEvents chkMostValuablePlayer As System.Windows.Forms.CheckBox
    Friend WithEvents lblMostValuablePlayer As System.Windows.Forms.Label
    Friend WithEvents radSexMale As System.Windows.Forms.RadioButton
    Friend WithEvents radSexFemale As System.Windows.Forms.RadioButton
    Friend WithEvents lblDateOfBirthFormat As System.Windows.Forms.Label
    Friend WithEvents txtDateOfBirth As System.Windows.Forms.TextBox
    Friend WithEvents lblDateOfBirth As System.Windows.Forms.Label
    Friend WithEvents cmbStates As System.Windows.Forms.ComboBox
    Friend WithEvents txtSalary As System.Windows.Forms.TextBox
    Friend WithEvents lblSalary As System.Windows.Forms.Label
    Friend WithEvents lblHomePhoneNumberFormat As System.Windows.Forms.Label
    Friend WithEvents txtHomePhoneNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblHomePhoneNumber As System.Windows.Forms.Label
    Friend WithEvents txtZipCode As System.Windows.Forms.TextBox
    Friend WithEvents lblZipCode As System.Windows.Forms.Label
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtStreetAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblStreetAddress As System.Windows.Forms.Label
    Friend WithEvents txtMiddleName As System.Windows.Forms.TextBox
    Friend WithEvents lblMiddleName As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents lblRequiredField As System.Windows.Forms.Label
    Friend WithEvents lblMascot As System.Windows.Forms.Label
    Friend WithEvents lblTeam As System.Windows.Forms.Label
    Friend WithEvents lblSex As System.Windows.Forms.Label
    Friend WithEvents lblCity As System.Windows.Forms.Label
End Class
