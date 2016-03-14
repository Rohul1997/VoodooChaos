Imports PackageIO

Public Class Form1
    Dim Open As New OpenFileDialog
    Dim filepath As String = Open.FileName
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Open.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            filepath = Open.FileName
            readfile()
        End If
    End Sub
    Public Sub readfile()
        Try
            Dim reader As New PackageIO.Reader(filepath, Endian.Little)
            TextBox2.Enabled = False
            Button2.Enabled = True
            reader.Position = &H2F9C
            TextBox2.Text = reader.ReadHexString(+2)
        Catch ex As Exception
            MessageBox.Show("Invalid .cia")
            TextBox2.Enabled = False
            Button2.Enabled = False
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim Writer As New PackageIO.Writer(filepath, Endian.Little)
            Writer.WriteHexString(TextBox1.Text)
            Writer.Position = &H2F9C
            Writer.WriteHexString(TextBox1.Text)
            MsgBox("File Successfully Saved")
        Catch ex As Exception
            MsgBox("Could Not Be Saved", MsgBoxStyle.OkOnly, "Please Try Again Or Try A Diffrent .cia")
        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Process.Start("http://3dschaos.com/members/7.html")
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Process.Start("http://3dschaos.com/members/7.html")
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Process.Start("http://3dschaos.com/")
    End Sub
End Class