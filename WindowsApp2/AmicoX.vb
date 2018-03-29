Imports System.IO
Imports CoreAudioApi

Public Class AmicoX
    Private Music As New Audio
    Dim NumLEDS As Integer = 20
    Private device As MMDevice
    Public InfoMusic As String
    Public PreVolume As Integer
    Public TypeOrder As String = Nothing
    Public isXP As Boolean = False

    Const WM_NCHITTEST As Integer = &H84
    Const HTCLIENT As Integer = &H1
    Const HTCAPTION As Integer = &H2

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case WM_NCHITTEST
                MyBase.WndProc(m)
                If m.Result = IntPtr.op_Explicit(HTCLIENT) Then m.Result = IntPtr.op_Explicit(HTCAPTION)
            Case Else
                MyBase.WndProc(m)
        End Select
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button6.Tag = "repeatR"
        PeakMeterCtrl2.Start(1000 / 20)

        If Environment.OSVersion.Version.Major >= 6 And IO.File.Exists(Application.StartupPath & "\CoreAudioApi.dll") And IO.File.Exists(Application.StartupPath & "\PeakMeterCtrl.dll") Then
            Dim DevEnum As New MMDeviceEnumerator()
            device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia)
            AddHandler device.AudioEndpointVolume.OnVolumeNotification, AddressOf OnVolumeNotification
        Else
            isXP = True
        End If

        ToolStripComboBox1.Text = My.Settings.nFont
        ChangeFont()
        ParseCommandLineArgs()
        'Progressbar1.Value = CInt(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100)
        'Label1.Text = Progressbar1.Value
        'Label2.Text = device.FriendlyName
    End Sub

    Private Sub ParseCommandLineArgs()
        Dim inputArgument As String
        Dim inputName As String = ""

        For Each s As String In Environment.GetCommandLineArgs
            Select Case True
                Case s.ToLower.Contains("-chipfile=")
                    inputArgument = "-chipfile="
                    inputName = s.Remove(0, inputArgument.Length)
            End Select
        Next

        If inputName.Trim <> "" Then
            CleanChipName(Path.GetFileName(inputName))
            Music.StartOrder = 0
            Timer1.Stop()
            Music.StopMusic()
            Button1.BackgroundImage = My.Resources.forward
            Music.SOUNDDIR = inputName
            Music.GetMusicInfo()
        End If

        Environment.CommandLine.Remove(0, Environment.CommandLine.Length)

    End Sub

    Private Sub ChangeFont()
        Dim dimension As Integer = 0
        If ToolStripComboBox1.Text.Trim = "" Then Exit Sub

        If File.Exists(Application.StartupPath & "\Font\" & ToolStripComboBox1.Text & ".ttf") = False _
            And ToolStripComboBox1.Text.Trim <> "Default" Then
            Exit Sub
        End If

        Select Case ToolStripComboBox1.Text
            Case "LOOPY___"
                dimension = 3
            Case "Famirids"
                dimension = -1
            Case "Pixel LCD-7"
                dimension = 5
            Case "DS-DIGI"
                dimension = -1
            Case "RADIOLAND"
                dimension = -3
            Case "Default"
                dimension = 0
                Label1.Font = New Font("UniDreamLED", 12 - dimension, FontStyle.Regular)
                Label2.Font = New Font("UniDreamLED", 20 - dimension, FontStyle.Bold)
                Label3.Font = New Font("UniDreamLED", 12 - dimension, FontStyle.Regular)
                Exit Sub
            Case Else
                dimension = 0
        End Select

        Try
            AdjustText(Label1)
            AdjustText(Label2)
            AdjustText(Label3)
            AdjustText(Label4)
        Catch
        End Try
        'Application.DoEvents()
    End Sub

    Private Sub OnVolumeNotification(data As CoreAudioApi.AudioVolumeNotificationData)
        If Me.InvokeRequired Then
            Me.Invoke(New AudioEndpointVolumeNotificationDelegate(AddressOf OnVolumeNotification), data)
        Else
            'Progressbar1.Value = CInt(data.MasterVolume * 100)
            'Label1.Text = Progressbar1.Value
        End If
    End Sub

    Private Sub VolMaster_Scroll(sender As Object, e As EventArgs) Handles VolMaster.Scroll
        'device.AudioEndpointVolume.MasterVolumeLevelScalar = Progressbar1.Value / 100.0F
        'Label1.Text = Progressbar1.Value
        Music.MasterVolume(VolMaster.Value)
        If VolMaster.Value > 0 Then AudioOn()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim meters1() As Integer = New Integer((NumLEDS) - 1) {}
        Dim meters2() As Integer = New Integer((NumLEDS) - 1) {}
        Dim meters3() As Integer = New Integer((NumLEDS) - 1) {}
        Dim rand As Random = New Random
        Dim i As Integer = 0
        Do While (i < meters1.Length)
            If isXP = False Then
                meters1(i) = rand.Next(0, Int(device.AudioMeterInformation.MasterPeakValue * 150)) 'rand.Next(0, 100)
                meters2(i) = Int(device.AudioMeterInformation.PeakValues(0) * 150)
                meters3(i) = Int(device.AudioMeterInformation.PeakValues(1) * 150)
            Else
                meters1(i) = rand.Next(0, VolMaster.Value / 1.7)
            End If
            i = (i + 1)
        Loop

        Me.PeakMeterCtrl2.SetData(meters1, 0, meters1.Length)

        If isXP = False Then
            Me.PeakMeterCtrl1.SetData(meters2, 0, meters2.Length)
            Me.PeakMeterCtrl3.SetData(meters3, 0, meters3.Length)
        End If

        Music.UpdateTimeOrders()
        'ChangeFont()
        If Music.IsPlaying = False Then Music.StopMusic() : Button1.BackgroundImage = My.Resources.forward
        'Label2.Refresh()
        'Label3.Refresh()
        'Application.DoEvents()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'Progressbar1.Value = Int(device.AudioMeterInformation.PeakValues(0) * 100)
        'ProgressBar2.Value = Int(device.AudioMeterInformation.PeakValues(1) * 100)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim fdlg As OpenFileDialog = New OpenFileDialog()
        fdlg.Title = "Select Supported File"
        fdlg.Filter = "All supported format (*.xm,*.mod,mod.*,*.s3m,*.it,*.mid,*.fsb,*.ft,*.pax)|*.xm;*.mod;mod.*;*.s3m;*.it;*.mid;*.fsb;*.ft;*.pax|All file (*.*)|*.*"
        fdlg.FilterIndex = 1
        fdlg.RestoreDirectory = True
        If fdlg.ShowDialog() = DialogResult.OK Then

            If Path.GetExtension(fdlg.SafeFileName) = ".pax" Then
                DataGridView1.Rows.Clear()
                Using sr As StreamReader = File.OpenText(fdlg.FileName)
                    Do While sr.Peek() >= 0
                        Dim SplitPax() As String = Split(sr.ReadLine(), "|")
                        DataGridView1.Rows.Add(SplitPax)
                    Loop
                End Using
            Else
                CleanChipName(fdlg.SafeFileName)
                Music.StartOrder = 0
                Timer1.Stop()
                Music.StopMusic()
                Button1.BackgroundImage = My.Resources.forward
                Music.SOUNDDIR = fdlg.FileName
                Music.GetMusicInfo()
            End If
        Else
            Exit Sub
        End If
    End Sub

    Private Function CleanChipName(ChipName As String)

        Select Case Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(ChipName))
            Case "xm", "mod", "s3m", "it", "mid", "fsb", "ft"
                Label1.Text = (Replace(Path.GetExtension(ChipName), ".", ""))
            Case Else
                Label1.Text = Path.GetFileNameWithoutExtension(ChipName)
        End Select
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PlayChipModule()
    End Sub

    Private Sub PlayChipModule()
        If Music.IsPlaying = False Then
            If Music.SOUNDDIR <> "" Then
                Button1.BackgroundImage = My.Resources.pause
                Music.PlaySound()
                ToolTip1.SetToolTip(Button1, "Pause")
                Timer1.Start()
            Else
                Music.StopMusic()
                Timer1.Stop()
            End If
        Else
            Music.PauseUnPauseMusic()
        End If
    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Music.StopMusic()
        Music.FmodClose()
        My.Settings.nFont = ToolStripComboBox1.Text
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Music.StopMusic() 'If Music.IsPlaying = True Then
        Button1.BackgroundImage = My.Resources.forward
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        If Button6.Tag = "repeatR" Then
            Button6.BackgroundImage = (My.Resources.repeatG)
            Button6.Tag = "repeatG"
        Else
            Button6.BackgroundImage = (My.Resources.repeatR)
            Button6.Tag = "repeatR"
        End If
        Music.SetLoop()
    End Sub

    Private Sub Button6_MouseEnter(sender As Object, e As EventArgs) Handles Button6.MouseEnter
        Button6.FlatAppearance.MouseOverBackColor = Color.Transparent
        Button6.FlatAppearance.MouseDownBackColor = Color.Transparent

        If Button6.Tag = "repeatR" Then
            ToolTip1.SetToolTip(Button6, "Loop Enabled")
        Else
            ToolTip1.SetToolTip(Button6, "Loop Disabled")
        End If
    End Sub

    Private Sub Button9_MouseEnter(sender As Object, e As EventArgs) Handles Button9.MouseEnter
        Button9.FlatAppearance.MouseOverBackColor = Color.Transparent
        Button9.FlatAppearance.MouseDownBackColor = Color.Transparent
    End Sub

    Private Sub Button8_MouseEnter(sender As Object, e As EventArgs) Handles Button8.MouseEnter
        Button8.FlatAppearance.MouseOverBackColor = Color.Transparent
        Button8.FlatAppearance.MouseDownBackColor = Color.Transparent

        If Button8.Tag = "NOn" Then
            ToolTip1.SetToolTip(Button8, "Volume Normalize On")
        Else
            ToolTip1.SetToolTip(Button8, "Volume Normalize Off")
        End If
    End Sub

    Private Sub VolMaster_MouseMove(sender As Object, e As MouseEventArgs) Handles VolMaster.MouseMove
        ToolTip1.SetToolTip(VolMaster, "Volume = " & VolMaster.Value)
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

        If Directory.Exists(Application.StartupPath & "\Font") = False Then
            FontToolStripMenuItem.Enabled = False
            Exit Sub
        Else
            FontToolStripMenuItem.Enabled = True
        End If

        ToolStripComboBox1.Items.Clear()
        ToolStripComboBox1.Items.Add("Default")
        Dim Files() As String
        Dim f As FileInfo

        Files = Directory.GetFiles(Application.StartupPath & "\Font")
        For Each sFile As String In Files
            f = New FileInfo(sFile)
            ToolStripComboBox1.Items.Add(Path.GetFileNameWithoutExtension(f.Name))
        Next

    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        ChangeFont()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        If Button7.Tag = "VON" Then
            PreVolume = VolMaster.Value
            VolMaster.Value = 0
            Button7.Tag = "VOFF"
            Button7.BackgroundImage = My.Resources.audioOff
            ToolTip1.SetToolTip(Button7, "Un Mute")
        ElseIf Button7.Tag = "VOFF" Then
            VolMaster.Value = PreVolume
            Button7.Tag = "VON"
            Button7.BackgroundImage = My.Resources.audioOn
            ToolTip1.SetToolTip(Button7, "Mute")
        End If
        Music.MasterVolume(VolMaster.Value)
    End Sub

    Public Sub AudioOn()
        Button7.Tag = "VON"
        Button7.BackgroundImage = My.Resources.audioOn
        ToolTip1.SetToolTip(Button7, "Mute")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TypeOrder = Button4.Tag
        Music.SetPattern()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TypeOrder = Button3.Tag
        Music.SetPattern()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If Button8.Tag = "NOn" Then
            Button8.BackgroundImage = My.Resources.signalG
            Button8.Tag = "NOff"
        Else
            Button8.BackgroundImage = My.Resources.signalR
            Button8.Tag = "NOn"
        End If

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If Button9.Tag = "InfoOff" Then
            Button9.BackgroundImage = My.Resources.informationR
            Button9.Tag = "InfoOn"
        Else
            Button9.BackgroundImage = My.Resources.informationG
            Button9.Tag = "InfoOff"
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If File.Exists(Music.SOUNDDIR) = False Then Exit Sub
        Dim SplitLabel() As String = Split(Label3.Text, "ORDERS:")
        Dim SubTunes() As String = Split(SplitLabel(1), "/")
        Dim Values() As String = {Path.GetFileName(Music.SOUNDDIR), SubTunes(0).Trim, Path.GetDirectoryName(Music.SOUNDDIR)}
        If DataGridView1.Rows.Count = 0 Then DataGridView1.Rows.Add(Values)

        Dim crow As Integer = 0
        For i = 0 To DataGridView1.Rows.Count - 1
            Dim RetRow() As String = {DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value}
            If Values(0) & "_" & Values(1) & "_" & Values(2) = RetRow(0) & "_" & RetRow(1) & "_" & RetRow(2) Then
                crow = 0
                Exit For
            Else
                crow = crow + 1
            End If
        Next

        If crow > 0 Then DataGridView1.Rows.Add(Values)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        PlayByGrid()
    End Sub

    Private Sub PlayByGrid()
        CleanChipName(DataGridView1.CurrentRow.Cells(0).Value)

        Music.StartOrder = 0
        Timer1.Stop()
        Music.StopMusic()
        Button1.BackgroundImage = My.Resources.forward
        Music.SOUNDDIR = Path.Combine(DataGridView1.CurrentRow.Cells(2).Value, DataGridView1.CurrentRow.Cells(0).Value)
        Music.GetMusicInfo()

        Music.StartOrder = Val(DataGridView1.CurrentRow.Cells(1).Value - 1)
        PlayChipModule()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(row)
        Next
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        If DataGridView1.Rows.Count = 0 Then Exit Sub
        SaveFileDialog1.Filter = "Playlist Files (*.pax*)|*.pax"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
      Then
            Using sw As StreamWriter = File.CreateText(SaveFileDialog1.FileName)
                For i = 0 To DataGridView1.Rows.Count - 1
                    sw.WriteLine(DataGridView1.Rows(i).Cells(0).Value & "|" & DataGridView1.Rows(i).Cells(1).Value & "|" & DataGridView1.Rows(i).Cells(2).Value)
                Next
            End Using
        End If
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.Close()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Label4_SizeChanged(sender As Object, e As EventArgs) Handles Label4.SizeChanged
        'Label4.Parent = Me
        'Label4.Left = (Label4.Parent.Width \ 2) - (Label4.Width \ 2)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        About.ShowDialog()
    End Sub

    Private Sub Label1_MouseEnter(sender As Object, e As EventArgs) Handles Label1.MouseEnter
        If Music.SOUNDDIR <> Nothing Then ToolTip1.SetToolTip(Label1, Music.GetNameFromChip)
    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        If DataGridView1.Rows.Count <= 0 Then Exit Sub
        Dim lastRowNum As Integer = (DataGridView1.Rows.Count - 1 - 1)

        Dim curDataGridViewRow As DataGridViewRow = DataGridView1.CurrentRow
        Dim curRowNum As Integer = curDataGridViewRow.Index
        If (curRowNum > lastRowNum) Then
            DataGridView1.CurrentCell = DataGridView1.Item(0, 0)
        Else
            Dim nextRow As DataGridViewRow = DataGridView1.Rows(curRowNum + 1)
            DataGridView1.CurrentCell = nextRow.Cells(0)
            nextRow.Selected = True
        End If
        PlayByGrid()
    End Sub

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        If DataGridView1.Rows.Count <= 0 Then Exit Sub
        Dim lastRowNum As Integer = (DataGridView1.Rows.Count - 1 - 1)

        Dim curDataGridViewRow As DataGridViewRow = DataGridView1.CurrentRow
        Dim curRowNum As Integer = curDataGridViewRow.Index
        If (curRowNum = 0) Then
            DataGridView1.CurrentCell = DataGridView1.Item(0, lastRowNum + 1)
        Else
            Dim nextRow As DataGridViewRow = DataGridView1.Rows(curRowNum - 1)
            DataGridView1.CurrentCell = nextRow.Cells(0)
            nextRow.Selected = True
        End If
        PlayByGrid()
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If Music.SOUNDDIR <> "" Then Music.SetPan()
    End Sub

End Class