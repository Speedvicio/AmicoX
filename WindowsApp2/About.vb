Public Class About

    Private Sub About_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Parent = Me
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.Close()
    End Sub

End Class