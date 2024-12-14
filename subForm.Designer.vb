<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class subForm
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        TextBox2 = New TextBox()
        TextBox3 = New TextBox()
        TextBox6 = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        TextBox1 = New TextBox()
        TextBox4 = New TextBox()
        TextBox5 = New TextBox()
        Label5 = New Label()
        Button1 = New Button()
        SuspendLayout()
        ' 
        ' TextBox2
        ' 
        TextBox2.Font = New Font("Yu Gothic UI", 12F)
        TextBox2.Location = New Point(43, 30)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(118, 29)
        TextBox2.TabIndex = 0
        TextBox2.Text = "183"
        TextBox2.TextAlign = HorizontalAlignment.Right
        ' 
        ' TextBox3
        ' 
        TextBox3.Font = New Font("Yu Gothic UI", 12F)
        TextBox3.Location = New Point(167, 30)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(74, 29)
        TextBox3.TabIndex = 0
        TextBox3.Text = "8"
        TextBox3.TextAlign = HorizontalAlignment.Right
        ' 
        ' TextBox6
        ' 
        TextBox6.Font = New Font("Yu Gothic UI", 12F)
        TextBox6.Location = New Point(247, 30)
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New Size(72, 29)
        TextBox6.TabIndex = 0
        TextBox6.Text = "40"
        TextBox6.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Yu Gothic UI", 12F)
        Label1.Location = New Point(43, 6)
        Label1.Name = "Label1"
        Label1.Size = New Size(115, 21)
        Label1.TabIndex = 1
        Label1.Text = "ID(0-1FFFFFFF)"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Yu Gothic UI", 12F)
        Label2.Location = New Point(167, 6)
        Label2.Name = "Label2"
        Label2.Size = New Size(74, 21)
        Label2.TabIndex = 1
        Label2.Text = "Byte(0-7)"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Yu Gothic UI", 12F)
        Label3.Location = New Point(247, 6)
        Label3.Name = "Label3"
        Label3.Size = New Size(97, 21)
        Label3.TabIndex = 1
        Label3.Text = "Mask(00-FF)"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Yu Gothic UI", 12F)
        Label4.Location = New Point(12, 38)
        Label4.Name = "Label4"
        Label4.Size = New Size(19, 21)
        Label4.TabIndex = 1
        Label4.Text = "1"
        ' 
        ' TextBox1
        ' 
        TextBox1.Font = New Font("Yu Gothic UI", 12F)
        TextBox1.Location = New Point(43, 75)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(118, 29)
        TextBox1.TabIndex = 0
        TextBox1.Text = "184"
        TextBox1.TextAlign = HorizontalAlignment.Right
        ' 
        ' TextBox4
        ' 
        TextBox4.Font = New Font("Yu Gothic UI", 12F)
        TextBox4.Location = New Point(167, 75)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(74, 29)
        TextBox4.TabIndex = 0
        TextBox4.Text = "8"
        TextBox4.TextAlign = HorizontalAlignment.Right
        ' 
        ' TextBox5
        ' 
        TextBox5.Font = New Font("Yu Gothic UI", 12F)
        TextBox5.Location = New Point(247, 75)
        TextBox5.Name = "TextBox5"
        TextBox5.Size = New Size(72, 29)
        TextBox5.TabIndex = 0
        TextBox5.Text = "80"
        TextBox5.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Yu Gothic UI", 12F)
        Label5.Location = New Point(12, 83)
        Label5.Name = "Label5"
        Label5.Size = New Size(19, 21)
        Label5.TabIndex = 1
        Label5.Text = "2"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(332, 66)
        Button1.Name = "Button1"
        Button1.Size = New Size(79, 38)
        Button1.TabIndex = 2
        Button1.Text = "Set ID"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' subForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(423, 126)
        Controls.Add(Button1)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label1)
        Controls.Add(TextBox5)
        Controls.Add(TextBox6)
        Controls.Add(TextBox4)
        Controls.Add(TextBox3)
        Controls.Add(TextBox1)
        Controls.Add(TextBox2)
        Name = "subForm"
        Text = "WARN LED ID Setting"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Button1 As Button
End Class
