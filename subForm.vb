Public Class subForm

    Private Sub set_id_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'For id set
        If TextBox2.Text = String.Empty Then
            WarnCanId1 = 0
        ElseIf Convert.ToUInt32(TextBox2.Text, 16) > &H7FF Then
            WarnCanId1 = Convert.ToUInt32(TextBox2.Text, 16) Or &H80000000UI
        Else
            WarnCanId1 = Convert.ToUInt32(TextBox2.Text, 16)
        End If

        If TextBox1.Text = String.Empty Then
            WarnCanId2 = 0
        ElseIf Convert.ToUInt32(TextBox1.Text, 16) > &H7FF Then
            WarnCanId2 = Convert.ToUInt32(TextBox1.Text, 16) Or &H80000000UI
        Else
            WarnCanId2 = Convert.ToUInt32(TextBox1.Text, 16)
        End If


        If TextBox3.Text = String.Empty Then
            WarnByte1 = 0
        Else
            WarnByte1 = Convert.ToUInt32(TextBox3.Text, 16)
        End If

        If TextBox4.Text = String.Empty Then
            WarnByte2 = 0
        Else
            WarnByte2 = Convert.ToUInt32(TextBox4.Text, 16)
        End If


        If TextBox6.Text = String.Empty Then
            WarnMask1 = 0
        Else
            WarnMask1 = Convert.ToUInt32(TextBox6.Text, 16)
        End If

        If TextBox5.Text = String.Empty Then
            WarnMask2 = 0
        Else
            WarnMask2 = Convert.ToUInt32(TextBox5.Text, 16)
        End If

        Me.Close()
    End Sub
End Class