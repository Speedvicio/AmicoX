Public Class Audio

    Public Enum FMUSIC_TYPES
        FMUSIC_TYPE_NONE
        FMUSIC_TYPE_MOD        'Protracker / Fasttracker
        FMUSIC_TYPE_S3M        'ScreamTracker 3
        FMUSIC_TYPE_XM         'FastTracker 2
        FMUSIC_TYPE_IT         'Impulse Tracker.
        FMUSIC_TYPE_MIDI       'MIDI file
    End Enum

    Public Enum FMOD_ERRORS
        FMOD_ERR_NONE   '/* No errors */
        FMOD_ERR_BUSY   '/* Cannot Call this command after FSOUND_Init. Call FSOUND_Close first. */
        FMOD_ERR_UNINITIALIZED  '/* This command failed because FSOUND_Init Or FSOUND_SetOutput was Not called */
        FMOD_ERR_INIT   '/* Error initializing output device. */
        FMOD_ERR_ALLOCATED  '/* Error initializing output device, but more specifically, the output device Is already In use And cannot be reused. */
        FMOD_ERR_PLAY   '/* Playing the sound failed. */
        FMOD_ERR_OUTPUT_FORMAT  '/* Soundcard does Not support the features needed For this soundsystem (16bit stereo output) */
        FMOD_ERR_COOPERATIVELEVEL   '/* Error setting cooperative level For hardware. */
        FMOD_ERR_CREATEBUFFER   '/* Error creating hardware sound buffer. */
        FMOD_ERR_FILE_NOTFOUND  '/* File Not found */
        FMOD_ERR_FILE_FORMAT    '/* Unknown file format */
        FMOD_ERR_FILE_BAD   '/* Error loading file */
        FMOD_ERR_MEMORY '/* Not enough memory Or resources */
        FMOD_ERR_VERSION    '/* The version number Of this file format Is Not supported */
        FMOD_ERR_INVALID_PARAM  '/* An invalid parameter was passed To this Function */
        FMOD_ERR_NO_EAX '/* Tried To use an EAX command On a non EAX enabled channel Or output. */
        FMOD_ERR_CHANNEL_ALLOC  '/* Failed To allocate a New channel */
        FMOD_ERR_RECORD '/* Recording Is Not supported On this machine */
        FMOD_ERR_MEDIAPLAYER    '/* Windows Media Player Not installed so cannot play wma Or use internet streaming. */
        FMOD_ERR_CDDEVICE   '/* An Error occured trying To open the specified CD device */
    End Enum

    Public Enum FSOUND_OUTPUTTYPES
        FSOUND_OUTPUT_NOSOUND    ' NoSound driver, all calls to this succeed but do nothing.
        FSOUND_OUTPUT_WINMM      ' Windows Multimedia driver.
        FSOUND_OUTPUT_DSOUND     ' DirectSound driver.  You need this to get EAX2 or EAX3 support, or FX api support.
        FSOUND_OUTPUT_ASIO       ' Low latency ASIO driver
    End Enum

    Public Enum FSOUND_SPEAKERMODES
        FSOUND_SPEAKERMODE_DOLBYDIGITAL  ' The audio is played through a speaker arrangement of surround speakers with a subwoofer.
        FSOUND_SPEAKERMODE_HEADPHONE     ' The speakers are headphones.
        FSOUND_SPEAKERMODE_MONO          ' The speakers are monaural.
        FSOUND_SPEAKERMODE_QUAD          ' The speakers are quadraphonic.
        FSOUND_SPEAKERMODE_STEREO        ' The speakers are stereo (default value).
        FSOUND_SPEAKERMODE_SURROUND      ' The speakers are surround sound.
    End Enum

    Public Enum FSOUND_MODES
        FSOUND_LOOP_OFF = 1            ' For non looping samples.
        FSOUND_LOOP_NORMAL = 2         ' For forward looping samples.
        FSOUND_LOOP_BIDI = 4           ' For bidirectional looping samples.  (no effect if in hardware).
        FSOUND_8BITS = 8               ' For 8 bit samples.
        FSOUND_16BITS = 16             ' For 16 bit samples.
        FSOUND_MONO = 32               ' For mono samples.
        FSOUND_STEREO = 64             ' For stereo samples.
        FSOUND_UNSIGNED = 128          ' For source data containing unsigned samples.
        FSOUND_SIGNED = 256            ' For source data containing signed data.
        FSOUND_DELTA = 512             ' For source data stored as delta values.
        FSOUND_IT214 = 1024            ' For source data stored using IT214 compression.
        FSOUND_IT215 = 2048            ' For source data stored using IT215 compression.
        FSOUND_HW3D = 4096             ' Attempts to make samples use 3d hardware acceleration. (if the card supports it)
        FSOUND_2D = 8192               ' Ignores any 3d processing.  overrides FSOUND_HW3D.  Located in software.
        FSOUND_STREAMABLE = 16384      ' For realtime streamable samples.  If you dont supply this sound may come out corrupted.
        FSOUND_LOADMEMORY = 32768      ' For FSOUND_Sample_Load - name will be interpreted as a pointer to data
        FSOUND_LOADRAW = 65536         ' For FSOUND_Sample_Load/FSOUND_Stream_Open - will ignore file format and treat as raw pcm.
        FSOUND_MPEGACCURATE = 131072   ' For FSOUND_Stream_Open - scans MP2/MP3 (VBR also) for accurate FSOUND_Stream_GetLengthMs/FSOUND_Stream_SetTime.
        FSOUND_FORCEMONO = 262144      ' For forcing stereo streams and samples to be mono - needed with FSOUND_HW3D - incurs speed hit
        FSOUND_HW2D = 524288           ' 2d hardware sounds.  allows hardware specific effects
        FSOUND_ENABLEFX = 1048576      ' Allows DX8 FX to be played back on a sound.  Requires DirectX 8 - Note these sounds cant be played more than once, or have a changing frequency
        FSOUND_MPEGHALFRATE = 2097152  ' For FMODCE only - decodes mpeg streams using a lower quality decode, but faster execution
        FSOUND_XADPCM = 4194304        ' For XBOX only - Describes a user sample that its contents are compressed as XADPCM
        FSOUND_VAG = 8388608           ' For PS2 only - Describes a user sample that its contents are compressed as Sony VAG format.
        FSOUND_NONBLOCKING = 16777216  ' For FSOUND_Stream_OpenFile - Causes stream to open in the background and not block the foreground app - stream plays only when ready.

        FSOUND_NORMAL = FSOUND_16BITS Or FSOUND_SIGNED Or FSOUND_MONO
    End Enum

    Public Enum FSOUND_FX_MODES
        FSOUND_FX_CHORUS
        FSOUND_FX_COMPRESSOR
        FSOUND_FX_DISTORTION
        FSOUND_FX_ECHO
        FSOUND_FX_FLANGER
        FSOUND_FX_GARGLE
        FSOUND_FX_I3DL2REVERB
        FSOUND_FX_PARAMEQ
        FSOUND_FX_WAVES_REVERB
    End Enum

    Public Enum FSOUND_CHANNELSAMPLEMODE
        FSOUND_FREE = -1                 ' definition for dynamically allocated channel or sample
        FSOUND_UNMANAGED = -2            ' definition for allocating a sample that is NOT managed by fsound
        FSOUND_ALL = -3                  ' for a channel index or sample index, this flag affects ALL channels or samples available!  Not supported by all functions.
        FSOUND_STEREOPAN = -1            ' definition for full middle stereo volume on both channels
        FSOUND_SYSTEMCHANNEL = -1000     ' special channel ID for channel based functions that want to alter the global FSOUND software mixing output channel
        FSOUND_SYSTEMSAMPLE = -1000      ' special sample ID for all sample based functions that want to alter the global FSOUND software mixing output sample
    End Enum

    Private booAbortMusic As Boolean
    Private XMPointer As Integer = 0
    Public SOUNDDIR As String
    Public IsPlaying As Boolean
    Public BackOrder, ThisOrder As Integer
    Public StartOrder As Integer
    Dim Pause As Boolean

    Public Const FSOUND_INIT_ACCURATEVULEVELS = &H8
    Public Const FSOUND_INIT_ENABLEOUTPUTFX = &H4

    Public Declare Function FSOUND_GetError Lib “fmod.dll” Alias “_FSOUND_GetError@0” () As FMOD_ERRORS
    Public Declare Function FSOUND_Close Lib "fmod.dll" Alias "_FSOUND_Close@0" () As Integer
    Public Declare Function FSOUND_SetBufferSize Lib "fmod.dll" Alias "_FSOUND_SetBufferSize@4" (ByVal lenms As Integer) As Byte
    Public Declare Function FSOUND_Init Lib "fmod.dll" Alias "_FSOUND_Init@12" (ByVal mixrate As Integer, ByVal maxchannels As Integer, ByVal flags As Integer) As Byte
    Public Declare Function FSOUND_GetOutput Lib "fmod.dll" Alias "_FSOUND_GetOutput@0" () As FSOUND_OUTPUTTYPES
    Public Declare Function FSOUND_SetOutput Lib "fmod.dll" Alias "_FSOUND_SetOutput@4" (ByVal outputtype As FSOUND_OUTPUTTYPES) As Byte
    Public Declare Function FSOUND_SetSpeakerMode Lib "fmod.dll" Alias "_FSOUND_SetSpeakerMode@4" (ByVal speakermode As FSOUND_SPEAKERMODES) As Integer
    Public Declare Function FSOUND_SetPaused Lib "fmod.dll" Alias "_FSOUND_SetPaused@8" (ByVal xmodule As Integer, ByVal Paused As Byte) As Byte

    Public Declare Function FSOUND_FX_Enable Lib "fmod.dll" Alias "_FSOUND_FX_Enable@8" (ByVal xmodule As Integer, ByVal fx As FSOUND_FX_MODES) As Integer
    Public Declare Function FSOUND_FX_Disable Lib "fmod.dll" Alias "_FSOUND_FX_Disable@4" (ByVal xmodule As Integer) As Byte
    Public Declare Function FSOUND_FX_SetParamEQ Lib "fmod.dll" Alias "_FSOUND_FX_SetParamEQ@16" (ByVal fxid As Integer, ByVal Center As Single, ByVal Bandwidth As Single, ByVal Gain As Single) As Byte

    Public Declare Function FMUSIC_LoadSong Lib "fmod.dll" Alias "_FMUSIC_LoadSong@4" (ByVal name As String) As Integer
    Public Declare Function FMUSIC_PlaySong Lib "fmod.dll" Alias "_FMUSIC_PlaySong@4" (ByVal xmodule As Integer) As Byte
    Public Declare Function FMUSIC_SetPaused Lib "fmod.dll" Alias "_FMUSIC_SetPaused@8" (ByVal xmodule As Integer, ByVal Pause As Byte) As Byte
    Public Declare Function FMUSIC_StopSong Lib "fmod.dll" Alias "_FMUSIC_StopSong@4" (ByVal xmodule As Integer) As Byte
    Public Declare Function FMUSIC_StopAllSongs Lib "fmod.dll" Alias "_FMUSIC_StopAllSongs@0" () As Integer
    Public Declare Function FMUSIC_OptimizeChannels Lib "fmod.dll" Alias "_FMUSIC_OptimizeChannels@12" (ByVal xmodule As Integer, ByVal maxchannels As Integer, ByVal minvolume As Integer) As Byte
    Public Declare Function FMUSIC_SetLooping Lib "fmod.dll" Alias "_FMUSIC_SetLooping@8" (ByVal xmodule As Integer, ByVal looping As Byte) As Byte
    Public Declare Function FMUSIC_SetPanSeperation Lib "fmod.dll" Alias "_FMUSIC_SetPanSeperation@8" (ByVal xmodule As Integer, ByVal pansep As Single) As Byte
    Public Declare Function FMUSIC_IsPlaying Lib "fmod.dll" Alias "_FMUSIC_IsPlaying@4" (ByVal xmodule As Integer) As Byte
    Public Declare Function FMUSIC_FreeSong Lib "fmod.dll" Alias "_FMUSIC_FreeSong@4" (ByVal xmodule As Integer) As Byte
    Public Declare Function FMUSIC_GetOrder Lib "fmod.dll" Alias "_FMUSIC_GetOrder@4" (ByVal xmodule As Integer) As Integer
    Public Declare Function FMUSIC_GetPattern Lib "fmod.dll" Alias "_FMUSIC_GetPattern@4" (ByVal xmodule As Integer) As Byte
    Public Declare Function FMUSIC_GetSpeed Lib "fmod.dll" Alias "_FMUSIC_GetSpeed@4" (ByVal xmodule As Integer) As Byte
    Public Declare Function FMUSIC_GetBPM Lib "fmod.dll" Alias "_FMUSIC_GetBPM@4" (ByVal xmodule As Integer) As Byte
    Public Declare Function FMUSIC_GetRow Lib "fmod.dll" Alias "_FMUSIC_GetRow@4" (ByVal xmodule As Integer) As Integer
    Public Declare Function FMUSIC_GetTime Lib "fmod.dll" Alias "_FMUSIC_GetTime@4" (ByVal xmodule As Integer) As Integer
    Public Declare Function FMUSIC_GetNumOrders Lib "fmod.dll" Alias "_FMUSIC_GetNumOrders@4" (ByVal xmodule As Integer) As Integer
    Public Declare Function FMUSIC_GetNumPatterns Lib "fmod.dll" Alias "_FMUSIC_GetNumPatterns@4" (ByVal xxmodule As Integer) As Integer
    Public Declare Function FMUSIC_GetNumChannels Lib "fmod.dll" Alias "_FMUSIC_GetNumChannels@4" (ByVal xmodule As Integer) As Integer
    Public Declare Function FMUSIC_GetPatternLength Lib "fmod.dll" Alias "_FMUSIC_GetPatternLength@8" (ByVal xmodule As Integer, ByVal orderno As Integer) As Byte
    Public Declare Function FMUSIC_GetName Lib "fmod.dll" Alias "_FMUSIC_GetName@4" (ByVal xmodule As Integer) As Integer
    Public Declare Function FMUSIC_GetType Lib "fmod.dll" Alias "_FMUSIC_GetType@4" (ByVal xmodule As Integer) As FMUSIC_TYPES
    Public Declare Function FMUSIC_SetOrder Lib "fmod.dll" Alias "_FMUSIC_SetOrder@8" (ByVal xmodule As Integer, ByVal order As Integer) As Byte
    Public Declare Function FMUSIC_GetGlobalVolume Lib "fmod.dll" Alias "_FMUSIC_GetGlobalVolume@4" (ByVal xxmodule As Integer) As Byte
    Public Declare Function FMUSIC_GetMasterVolume Lib "fmod.dll" Alias "_FMUSIC_GetMasterVolume@4" (ByVal xxmodule As Integer) As Integer
    Public Declare Function FMUSIC_SetMasterVolume Lib "fmod.dll" Alias "_FMUSIC_SetMasterVolume@8" (ByVal xxmodule As Integer, ByVal volume As Integer) As Byte
    Public Declare Function ConvCStringToVBString Lib "kernel32" Alias "lstrcpyA" (ByVal lpsz As String, ByVal pt As Integer) As Integer
    Private Declare Function FSOUND_DSP_GetSpectrum Lib “fmod.dll” Alias “_FSOUND_DSP_GetSpectrum@0” () As Integer
    Private Declare Function FSOUND_DSP_GetFFTUnit Lib “fmod.dll” Alias “_FSOUND_DSP_GetFFTUnit@0” () As Integer
    Private Declare Function FSOUND_DSP_SetActive Lib “fmod.dll” Alias “_FSOUND_DSP_SetActive@8” (ByVal unit As Integer, ByVal active As Integer) As Integer

    Public Function GetStringFromPointer(ByVal lpString As Integer) As String
        Dim NullCharPos As Integer
        Dim szBuffer As New String(" ", 255)

        ConvCStringToVBString(szBuffer, lpString)
        NullCharPos = InStr(szBuffer, vbNullChar)
        GetStringFromPointer = Left(szBuffer, NullCharPos - 1)
    End Function

    Public Function GetNameFromChip()
        Return (GetStringFromPointer(FMUSIC_GetName(XMPointer)))
    End Function

    Public Sub StartFmod()
        FSOUND_SetBufferSize(100)
        FSOUND_SetOutput(2)
        FSOUND_SetSpeakerMode(4)
        If FSOUND_Init(44100, 32, FSOUND_INIT_ACCURATEVULEVELS + FSOUND_INIT_ENABLEOUTPUTFX) = 0 Then
            MsgBox(FSOUND_GetError.ToString)
        End If
        'FSOUND_DSP_GetFFTUnit
        'FSOUND_DSP_GetSpectrum
    End Sub

    Public Sub New()
        StartFmod()
    End Sub

    Public Sub SetPan()
        FMUSIC_SetPanSeperation(XMPointer, AmicoX.NumericUpDown1.Value / 10)
    End Sub

    Public Sub PlaySound()
        If booAbortMusic = False Then
            XMPointer = FMUSIC_LoadSong(SOUNDDIR) ' path to the xm file in the temp folder
            SetLoop()
            FMUSIC_OptimizeChannels(XMPointer, 256, 10)
            FMUSIC_PlaySong(XMPointer) ' play the music
            MasterVolume(AmicoX.VolMaster.Value)
            IsPlaying = FMUSIC_IsPlaying(XMPointer)
            FMUSIC_SetOrder(XMPointer, StartOrder)
            SetPan()
            booAbortMusic = True
        End If
    End Sub

    Public Sub SetLoop()
        If AmicoX.Button6.Tag = "repeatR" Then
            FMUSIC_SetLooping(XMPointer, 1)
        Else
            FMUSIC_SetLooping(XMPointer, 0)
        End If
    End Sub

    Public Sub SetPattern()
        If AmicoX.TypeOrder = "NextOrder" Then
            FMUSIC_SetOrder(XMPointer, FMUSIC_GetOrder(XMPointer) + 1)
            If AmicoX.Label3.Text <> "." And FMUSIC_GetNumOrders(XMPointer) > StartOrder Then StartOrder += 1
        Else
            FMUSIC_SetOrder(XMPointer, FMUSIC_GetOrder(XMPointer) - 1)
            If AmicoX.Label3.Text <> "." And StartOrder > 0 Then StartOrder -= 1
        End If

        Dim repstring(), repstring1() As String
        If IsPlaying = False And AmicoX.Label3.Text <> "." Then
            repstring = Split(AmicoX.Label3.Text, "ORDERS:")
            repstring1 = Split(repstring(1), "/")
            AmicoX.Label3.Text = repstring(0) & "ORDERS:" & (StartOrder + 1).ToString("00") & "/" & Val(repstring1(1)).ToString("00")
        End If
    End Sub

    Public Sub PauseUnPauseMusic()
        If Pause = False Then
            FMUSIC_SetPaused(XMPointer, 1)
            Pause = True
            AmicoX.Button1.BackgroundImage = My.Resources.forward
            AmicoX.ToolTip1.SetToolTip(AmicoX.Button1, "Play")
            If AmicoX.isXP = True Then AmicoX.Timer1.Stop()
        ElseIf Pause = True Then
            FMUSIC_SetPaused(XMPointer, 0)
            Pause = False
            AmicoX.Button1.BackgroundImage = My.Resources.pause
            AmicoX.ToolTip1.SetToolTip(AmicoX.Button1, "Pause")
            If AmicoX.isXP = True Then AmicoX.Timer1.Start()
        End If
    End Sub

    Public Sub StopMusic()
        'Stop the music.
        If booAbortMusic = True Then
            FMUSIC_StopSong(XMPointer)
            FMUSIC_StopAllSongs
            IsPlaying = FMUSIC_IsPlaying(XMPointer)
            FMUSIC_FreeSong(XMPointer)
            AmicoX.Timer1.Stop()
            AmicoX.Label2.Text = "00:00"
            AmicoX.ToolTip1.SetToolTip(AmicoX.Button1, "Play")
            AmicoX.ProgressBar3.Value = 0

            Dim arr() As Integer = New Integer((20) - 1) {}
            Array.Clear(arr, 0, arr.Length)
            If AmicoX.isXP = False Then
                AmicoX.PeakMeterCtrl1.SetData(arr, 0, 20)
                AmicoX.PeakMeterCtrl3.SetData(arr, 0, 20)
            End If
            AmicoX.PeakMeterCtrl2.SetData(arr, 0, 20)

            'FSOUND_Close
            booAbortMusic = False
        End If
    End Sub

    Public Sub FmodClose()
        FSOUND_Close
    End Sub

    Public Sub GetMusicInfo()
        Try
            XMPointer = FMUSIC_LoadSong(SOUNDDIR)
            AmicoX.NumericUpDown1.Enabled = False
            If XMPointer = 0 Then SOUNDDIR = "" : Exit Sub
            Dim ftype As FMUSIC_TYPES
            Dim mtype, orders, channels, rows As String

            ftype = FMUSIC_GetType(XMPointer)

            Select Case ftype.ToString
                Case "FMUSIC_TYPE_MOD"
                    mtype = "MOD"
                    AmicoX.NumericUpDown1.Enabled = True
                Case "FMUSIC_TYPE_IT"
                    mtype = "IT"
                Case "FMUSIC_TYPE_XM"
                    mtype = "XM"
                Case "FMUSIC_TYPE_S3M"
                    mtype = "S3M"
                Case "FMUSIC_TYPE_MIDI"
                    mtype = "MIDI"
            End Select

            AmicoX.AudioOn()
            channels = FMUSIC_GetNumChannels(XMPointer)
            orders = (FMUSIC_GetOrder(XMPointer) + 1).ToString("00") & "/" & FMUSIC_GetNumOrders(XMPointer).ToString("00")
            AmicoX.Label3.Text = "FORMAT: " & mtype & " - CH: " & channels & " - ORDERS: " & orders
            AmicoX.VolMaster.Value = FMUSIC_GetMasterVolume(XMPointer)
            normalise()
            AmicoX.ProgressBar3.Maximum = (FMUSIC_GetNumOrders(XMPointer) * (FMUSIC_GetPatternLength(XMPointer, FMUSIC_GetOrder(XMPointer)) - 1))
        Catch
            MsgBox(FSOUND_GetError.ToString)
        End Try
    End Sub

    Public Sub MasterVolume(MVol As Integer)
        FMUSIC_SetMasterVolume(XMPointer, MVol)
    End Sub

    Public Sub normalise()
        If AmicoX.Button8.Tag = "NOn" Then
            If FMUSIC_GetMasterVolume(XMPointer) < 120 Then
                FMUSIC_SetMasterVolume(XMPointer, 256)
                AmicoX.VolMaster.Value = 256
            End If
        End If
    End Sub

    Public Sub UpdateTimeOrders()

        IsPlaying = FMUSIC_IsPlaying(XMPointer)
        AmicoX.VolMaster.Value = FMUSIC_GetMasterVolume(XMPointer)
        AmicoX.Label2.Text = PhraseTime(Int(FMUSIC_GetTime(XMPointer) / 1000))
        ThisOrder = FMUSIC_GetOrder(XMPointer) + 1

        If AmicoX.Button6.Tag = "repeatG" And ThisOrder <= BackOrder And BackOrder < FMUSIC_GetNumOrders(XMPointer) Then
            If BackOrder + 1 = FMUSIC_GetNumOrders(XMPointer) Then StopMusic()
            FMUSIC_SetOrder(XMPointer, BackOrder + 1)
        End If
        BackOrder = ThisOrder - 1
        Dim Uorders As String = ThisOrder.ToString("00") & "/" & (FMUSIC_GetNumOrders(XMPointer)).ToString("00")
        Dim Sorders() As String = Split(AmicoX.Label3.Text, "ORDERS:")
        AmicoX.Label3.Text = Sorders(0) & "ORDERS:" & Uorders
        Dim increasestep As Integer = FMUSIC_GetOrder(XMPointer) * (FMUSIC_GetPatternLength(XMPointer, FMUSIC_GetOrder(XMPointer)) - 1)

        Try
            AmicoX.ProgressBar3.Value = FMUSIC_GetRow(XMPointer) + increasestep
        Catch
            'MsgBox(FSOUND_GetError.ToString)
        End Try
    End Sub

    Public Function PhraseTime(TimeSec As Integer) As String
        Dim Min As Byte, Sec As String
        Min = Fix(TimeSec / 60)
        Sec = CStr(TimeSec - (Min * 60))
        If Len(Sec) = 1 Then
            Sec = "0" & Sec
        End If
        PhraseTime = Min.ToString("D2") & ":" & Sec
    End Function

End Class