
Imports System.Runtime.InteropServices
Imports System.Threading
Imports vxlapi_NET
Module GlobalVariables
    Public WarnCanId1 As UInt32 = 0
    Public WarnCanId2 As UInt32 = 0
    Public WarnByte1 As Byte = 0
    Public WarnByte2 As Byte = 0
    Public WarnMask1 As Byte = 0
    Public WarnMask2 As Byte = 0
End Module
Public Class Form1
    <DllImport("kernel32.dll", SetLastError:=True)>
    Shared Function WaitForSingleObject(ByVal handle As Integer, ByVal timeOut As Integer) As Integer
    End Function


    Shared appName As String = "VXL_CAN_NET"
    Shared CANDemo As XLDriver = New XLDriver
    Shared driverConfig As XLClass.xl_driver_config = New XLClass.xl_driver_config
    Shared hwType As UInt32 = 57
    Shared hwIndex As UInt32
    Shared hwChannel As UInt32 = 0        '0 =1 1 =2 2 =3...
    Shared busTypeCAN As XLDefine.XL_BusTypes = XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN
    Shared flags As UInt32
    Shared portHandle As Int32
    Shared eventHandle As Int32
    Shared accessMask As UInt64
    Shared txMask As UInt64
    Shared permissionMask As UInt64
    Shared channelIndex As Int32
    Shared rx_inable_flag As Boolean = False
    Shared receivedEvent As XLClass.xl_event = New XLClass.xl_event
    Shared RX_Thread As Thread
    Shared rxdata As String
    Shared txdata As String
    Shared MaskCanId1 As UInt32 = 0
    Shared MaskCanId2 As UInt32 = 0
    Shared MaskCanId3 As UInt32 = 0
    Shared xlStatus As XLDefine.XL_Status

    Private Sub hw_init(sender As Object, e As EventArgs) Handles Button1.Click
        'MsgBox(WarnCanId2 & WarnByte2 & WarnMask2)
        'MsgBox(WarnCanId1 & WarnByte1 & WarnMask1)

        ToolStripStatusLabel1.Text = String.Empty

        If ComboBox1.Text = "VN1610" Then
            hwType = 55
        ElseIf ComboBox1.Text = "VN1630" Then
            hwType = 57
        ElseIf ComboBox1.Text = "VIRTUAL" Then
            hwType = 1
        ElseIf ComboBox1.Text = "CANCASE" Then
            hwType = 21
        End If

        hwChannel = Convert.ToUInt32(ComboBox2.Text) - 1

        ' Open XL Driver
        xlStatus = CANDemo.XL_OpenDriver
        If xlStatus <> 0 Then
            ToolStripStatusLabel1.Text = "open driver error:" & xlStatus
            Return
        End If

        ' Get the complete XL Driver configuration, stored in driverConfig object
        xlStatus = CANDemo.XL_GetDriverConfig(driverConfig)
        If xlStatus <> 0 Then
            ToolStripStatusLabel1.Text = "get config error:" & xlStatus
            Return
        End If

        'create the item with 2 channels
        xlStatus = CANDemo.XL_SetApplConfig(appName, Convert.ToUInt32(0), hwType, hwIndex, hwChannel, busTypeCAN)
        If xlStatus <> 0 Then
            ToolStripStatusLabel1.Text = "set config error:" & xlStatus
            Return
        End If

        ' Read setting of CAN1)
        accessMask = CANDemo.XL_GetChannelMask(hwType, hwIndex, hwChannel)
        txMask = accessMask

        permissionMask = Convert.ToUInt64(Convert.ToInt32(accessMask) Or Convert.ToInt32(accessMask))

        ' Open port
        xlStatus = CANDemo.XL_OpenPort(portHandle, appName, accessMask, permissionMask, Convert.ToUInt32(1024), XLDefine.XL_InterfaceVersion.XL_INTERFACE_VERSION, busTypeCAN)
        If xlStatus <> 0 Then
            ToolStripStatusLabel1.Text = "open port error:" & xlStatus
            Return
        End If

        ' Activate channel
        xlStatus = CANDemo.XL_ActivateChannel(portHandle, accessMask, busTypeCAN, flags)
        If xlStatus <> 0 Then
            ToolStripStatusLabel1.Text = "actviate ch error:" & xlStatus
            Return
        End If

        ' Get RX event handle
        xlStatus = CANDemo.XL_SetNotification(portHandle, eventHandle, 1)
        If xlStatus <> 0 Then
            ToolStripStatusLabel1.Text = "set notfication error:" & xlStatus
            Return
        End If

        ' Reset time stamp clock
        xlStatus = CANDemo.XL_ResetClock(portHandle)
        If xlStatus <> 0 Then
            ToolStripStatusLabel1.Text = "reset clk error:" & xlStatus
            Return
        End If

        Button2.Enabled = True
        Button4.Enabled = True
        Button1.BackColor = Color.LightGreen
        Button4.BackColor = Color.Coral
        ToolStripStatusLabel1.Text = "Setting OK"
    End Sub

    Private Sub close_hw(sender As Object, e As EventArgs) Handles Button4.Click
        ToolStripStatusLabel1.Text = String.Empty
        xlStatus = CANDemo.XL_ClosePort(portHandle)
        If xlStatus <> 0 Then
            ToolStripStatusLabel1.Text = "close port error:" & xlStatus
        End If

        xlStatus = CANDemo.XL_CloseDriver()
        If xlStatus <> 0 Then
            ToolStripStatusLabel1.Text = "close driver error:" & xlStatus
        End If

        Button2.Enabled = False
        Button4.Enabled = False
        Button1.BackColor = Color.DarkSeaGreen
        Button4.BackColor = Color.RosyBrown
        ToolStripStatusLabel1.Text = "Finished"
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        CANDemo.XL_ClosePort(portHandle)
        CANDemo.XL_CloseDriver()
        Application.Exit()
        'If RX_Thread IsNot Nothing AndAlso RX_Thread.IsAlive Then
        '    RX_Thread.Join()
        'End If
    End Sub


    Private Sub start_monitor(sender As Object, e As EventArgs) Handles Button2.Click
        rx_inable_flag = True
        RX_Thread = New Thread(AddressOf RX_ThreadFunc)
        CANDemo.XL_FlushReceiveQueue(portHandle)
        RX_Thread.Start()

        Button3.Enabled = True
        Button6.Enabled = True
        Button2.BackColor = Color.LightGreen
        Button3.BackColor = Color.Coral
    End Sub

    Private Sub stop_monitor(sender As Object, e As EventArgs) Handles Button3.Click
        rx_inable_flag = False

        Button3.Enabled = False
        Button6.Enabled = False
        Button2.BackColor = Color.DarkSeaGreen
        Button3.BackColor = Color.RosyBrown
    End Sub


    Private Sub send_can(sender As Object, e As EventArgs) Handles Button6.Click
        ToolStripStatusLabel1.Text = String.Empty
        Dim transmitEvent As XLClass.xl_event = New XLClass.xl_event
        Dim xlEventCollection As XLClass.xl_event_collection = New XLClass.xl_event_collection(Convert.ToUInt32(1))

        xlEventCollection.xlEvent(0).tag = XLDefine.XL_EventTags.XL_TRANSMIT_MSG

        If Convert.ToUInt32(TextBox3.Text, 16) > &H7FF Then
            xlEventCollection.xlEvent(0).tagData.can_Msg.id = Convert.ToUInt32(TextBox3.Text, 16) Or &H80000000UI
        Else
            xlEventCollection.xlEvent(0).tagData.can_Msg.id = Convert.ToUInt32(TextBox3.Text, 16)
        End If

        xlEventCollection.xlEvent(0).tagData.can_Msg.dlc = Convert.ToUInt16(TextBox4.Text, 16)

        xlEventCollection.xlEvent(0).tagData.can_Msg.data(0) = Convert.ToByte(TextBox5.Text, 16)
        xlEventCollection.xlEvent(0).tagData.can_Msg.data(1) = Convert.ToByte(TextBox6.Text, 16)
        xlEventCollection.xlEvent(0).tagData.can_Msg.data(2) = Convert.ToByte(TextBox7.Text, 16)
        xlEventCollection.xlEvent(0).tagData.can_Msg.data(3) = Convert.ToByte(TextBox8.Text, 16)
        xlEventCollection.xlEvent(0).tagData.can_Msg.data(4) = Convert.ToByte(TextBox9.Text, 16)
        xlEventCollection.xlEvent(0).tagData.can_Msg.data(5) = Convert.ToByte(TextBox10.Text, 16)
        xlEventCollection.xlEvent(0).tagData.can_Msg.data(6) = Convert.ToByte(TextBox11.Text, 16)
        xlEventCollection.xlEvent(0).tagData.can_Msg.data(7) = Convert.ToByte(TextBox12.Text, 16)

        xlStatus = CANDemo.XL_CanTransmit(portHandle, txMask, xlEventCollection)
        If xlStatus <> 0 Then
            ToolStripStatusLabel1.Text = "send error:" & xlStatus
            Return
        End If


        txdata = "Ch" & transmitEvent.chanIndex + 1 _
        & "[Tx] " _
        & Hex(xlEventCollection.xlEvent(0).tagData.can_Msg.id And &H7FFFFFFFUI).PadLeft(8, " "c) _
        & "  (" _
        & Hex(xlEventCollection.xlEvent(0).tagData.can_Msg.dlc).PadLeft(2, " "c) _
        & ")  "

        For i = 0 To xlEventCollection.xlEvent(0).tagData.can_Msg.dlc - 1

            txdata = txdata & Hex(xlEventCollection.xlEvent(0).tagData.can_Msg.data(i)).PadLeft(2, "0"c) & " "
        Next
        txdata = txdata & vbCrLf
        ToolStripStatusLabel1.Text = "send succeed"

        print_text(txdata)
    End Sub

    Private Sub RX_ThreadFunc(token As CancellationToken)
        While rx_inable_flag = True
            Dim xlStatus As XLDefine.XL_Status = XLDefine.XL_Status.XL_SUCCESS
            Dim waitResult As XLDefine.WaitResults = New XLDefine.WaitResults
            waitResult = WaitForSingleObject(eventHandle, 1000)
            If (waitResult <> XLDefine.WaitResults.WAIT_TIMEOUT) Then
                xlStatus = XLDefine.XL_Status.XL_SUCCESS
                While (xlStatus <> XLDefine.XL_Status.XL_ERR_QUEUE_IS_EMPTY)
                    xlStatus = CANDemo.XL_Receive(portHandle, receivedEvent)
                    '  If receive succeed....
                    If (xlStatus = XLDefine.XL_Status.XL_SUCCESS) Then
                        If receivedEvent.tag = XLDefine.XL_EventTags.XL_RECEIVE_MSG Then

                            rxdata = "Ch" & receivedEvent.chanIndex + 1 _
                                 & " Rx  " _
                                 & Hex(receivedEvent.tagData.can_Msg.id And &H7FFFFFFFUI).PadLeft(8, " "c) _
                                 & "  (" _
                                 & Hex(receivedEvent.tagData.can_Msg.dlc).PadLeft(2, " "c) _
                                 & ")  "
                            For i = 0 To receivedEvent.tagData.can_Msg.dlc - 1
                                rxdata = rxdata & Hex(receivedEvent.tagData.can_Msg.data(i)).PadLeft(2, "0"c) & " "
                            Next
                            rxdata = rxdata & vbCrLf

                            If MaskCanId1 <> 0 Then

                                'for filter id
                                If (receivedEvent.tagData.can_Msg.id = MaskCanId1) Or
                                    (receivedEvent.tagData.can_Msg.id = MaskCanId2) Or
                                     (receivedEvent.tagData.can_Msg.id = MaskCanId3) Then
                                    print_text(rxdata)

                                    'for sys warn 
                                ElseIf receivedEvent.tagData.can_Msg.id = WarnCanId1 Then
                                    If (receivedEvent.tagData.can_Msg.data(WarnByte1) And WarnMask1) = 1 Then
                                        Button8.BackColor = Color.Red
                                    Else
                                        Button8.BackColor = Color.Maroon
                                    End If

                                    'for brake warn
                                ElseIf receivedEvent.tagData.can_Msg.id = WarnCanId2 Then
                                    If (receivedEvent.tagData.can_Msg.data(WarnByte2) And WarnMask2) = 1 Then
                                        Button9.BackColor = Color.Red
                                    Else
                                        Button9.BackColor = Color.Maroon
                                    End If

                                End If
                            Else
                                'print_text(rxdata)
                            End If

                        End If
                    End If
                End While
            End If
        End While
    End Sub

    Private Sub print_text(v As String)
        If TextBox2.InvokeRequired Then
            TextBox2.Invoke(New MethodInvoker(Sub()
                                                  TextBox2.AppendText(v)
                                              End Sub))
        Else
            TextBox2.AppendText(v)
        End If
        Return
    End Sub

    Private Sub clear_textbox(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox2.Clear()
        ToolStripStatusLabel1.Text = String.Empty
    End Sub

    Private Sub setFilterBTN_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If TextBox14.Text = String.Empty Then
            MaskCanId1 = 0
        ElseIf Convert.ToUInt32(TextBox14.Text, 16) > &H7FF Then
            MaskCanId1 = Convert.ToUInt32(TextBox14.Text, 16) Or &H80000000UI
        Else
            MaskCanId1 = Convert.ToUInt32(TextBox14.Text, 16)
        End If

        If TextBox1.Text = String.Empty Then
            MaskCanId2 = 0
        ElseIf Convert.ToUInt32(TextBox1.Text, 16) > &H7FF Then
            MaskCanId2 = Convert.ToUInt32(TextBox1.Text, 16) Or &H80000000UI
        Else
            MaskCanId2 = Convert.ToUInt32(TextBox1.Text, 16)
        End If

        If TextBox13.Text = String.Empty Then
            MaskCanId3 = 0
        ElseIf Convert.ToUInt32(TextBox13.Text, 16) > &H7FF Then
            MaskCanId3 = Convert.ToUInt32(TextBox13.Text, 16) Or &H80000000UI
        Else
            MaskCanId3 = Convert.ToUInt32(TextBox13.Text, 16)
        End If


        If (MaskCanId1 = 0) And (MaskCanId2 = 0) And (MaskCanId3 = 0) Then
            Button7.BackColor = Color.DarkCyan
        Else
            Button7.BackColor = Color.Cyan
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        CANDemo.XL_ClosePort(portHandle)
        CANDemo.XL_CloseDriver()
        Application.Exit()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MaskCanId1 = Convert.ToUInt32(TextBox14.Text, 16) Or &H80000000UI
    End Sub

    Private Sub InputIDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputIDToolStripMenuItem.Click
        subForm.Show()
    End Sub
End Class
