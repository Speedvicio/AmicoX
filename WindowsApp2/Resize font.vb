Module Resize_font

    Public Sub AdjustText(lblQueue As Label)
        Try
            If AmicoX.ToolStripComboBox1.Text = "Default" Then Exit Sub
            Dim Fit As Boolean = False
            Dim CurSize As Single
            Dim SizeStep As Single = 1
            Dim pfc As New Drawing.Text.PrivateFontCollection()
            pfc.AddFontFile(IO.Path.Combine(Application.StartupPath, "Font\" & AmicoX.ToolStripComboBox1.Text & ".ttf"))
            Do Until Fit
                CurSize += SizeStep
                Dim Fnt As Font = New Font(pfc.Families(0), CurSize)
                Dim textSize As Size = TextRenderer.MeasureText(lblQueue.Text, Fnt)
                If textSize.Height >= lblQueue.Height Or textSize.Width >= lblQueue.Width Or lblQueue.Height = 0 Or lblQueue.Width = 0 Then
                    Fit = True
                    If textSize.Width > lblQueue.Width Then
                        CurSize -= SizeStep
                    End If
                    If textSize.Height > lblQueue.Height Then
                        CurSize -= SizeStep
                    End If
                End If
            Loop

            If CurSize >= 0 Then
                lblQueue.Font = New Font(pfc.Families(0), CurSize)
                'Application.DoEvents()
            End If
        Catch
        End Try
    End Sub

End Module