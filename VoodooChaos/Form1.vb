Imports PackageIO

Public Class Form1
    Dim Open As New OpenFileDialog
    Dim filecia As String = Open.FileName
    Dim filencch As String = Open.FileName
    Dim fileexheader As String = Open.FileName
    Dim fileicon As String = Open.FileName

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        'Open up windows file dialog when open cia file button is clicked and wait for users input of cia file. if successful then store the file'
        Open.Filter = " CIA Files (*.Cia)|*.Cia|All Files (*.*)|*.*"
        If Open.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            filecia = Open.FileName
            readcia()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Open up windows file dialog when open ncch file button is clicked and wait for users input of ncch file. if successful then store the file'
        Open.Filter = " NCCH Files (*.header)|*.header|All Files (*.*)|*.*"
        If Open.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            filencch = Open.FileName
            readncch()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'Open up windows file dialog when open exheader file button is clicked and wait for users input of exheader file. if successful then store the file'
        Open.Filter = " Exheader Files (*.bin)|*.bin|All Files (*.*)|*.*"
        If Open.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            fileexheader = Open.FileName
            readexheader()
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'Open up windows file dialog when open icon file button is clicked and wait for users input of icon file. if successful then store the file'
        Open.Filter = " Icon Files (*.icn)|*.icn|All Files (*.*)|*.*"
        If Open.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            fileicon = Open.FileName
            readicon()
        End If
    End Sub

    Public Sub readcia()
        Try
            Dim reader As New PackageIO.Reader(filecia, Endian.Little)
            'Enable the save cia file button and read the title version value from the cia file and write on to the current title vesion textbox'
            Button2.Enabled = True
            reader.Position = &H2F9C
            TextBox1.Text = reader.ReadHexString(2)

            'if something unexpected happens to the file then prompt the following message and disable the save cia file button'
        Catch ex As Exception
            MessageBox.Show("Invalid .cia file")
            Button2.Enabled = False
        End Try
    End Sub

    Public Sub readncch()
        Try
            Dim reader As New PackageIO.Reader(filencch, Endian.Big)
            'Enable the save ncch file button and read the unique id value from the ncch file and write on to the current unique id textbox'
            Button4.Enabled = True
            reader.Position = &H109
            TextBox4.Text = reader.ReadHexString(3)

            'Enable the save ncch file button and read the title id value from the ncch file and write on to the current title id textbox'
            reader.Position = &H150
            TextBox5.Text = reader.ReadString(10)

            'if something unexpected happens to the file then prompt the following message and disable the save ncch file button'
        Catch ex As Exception
            MessageBox.Show("Invalid .header file")
            Button4.Enabled = False
        End Try
    End Sub

    Public Sub readexheader()
        Try
            Dim reader As New PackageIO.Reader(fileexheader, Endian.Big)
            'Enable the save exheader file button and read the unique id value from the ncch file and write on to the current unique id textbox'
            Button6.Enabled = True
            reader.Position = &H1C9
            TextBox8.Text = reader.ReadHexString(3)

            'if something unexpected happens to the file then prompt the following message and disable the save exheader file button'
        Catch ex As Exception
            MessageBox.Show("Invalid .bin file")
            Button6.Enabled = False
        End Try
    End Sub

    Public Sub readicon()
        Try
            'Enable the Remove Age Rating and Remove Region Lock buttons'
            Button8.Enabled = True
            Button9.Enabled = True

            'if something unexpected happens to the file then prompt the following message and disable the Remove Age Rating and Remove Region Lock buttons'
        Catch ex As Exception
            MessageBox.Show("Invalid .icn file")
            Button8.Enabled = False
            Button9.Enabled = False
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Try
            Dim Writer As New PackageIO.Writer(filecia, Endian.Little)
            'when user clicks on the save cia file button then read the text from the new title version box and write it onto the file'
            Writer.Position = &H2F9C
            Writer.WriteHexString(TextBox2.Text)

            'prompt the user that the file has been successfully saved'
            MsgBox("CIA File Successfully Saved")
            'prompt the user with the following message if the file couldn't be saved'
            Dim reader As New PackageIO.Reader(filecia, Endian.Little)
            reader.Position = &H2F9C
            TextBox1.Text = Reader.ReadHexString(2)
        Catch ex As Exception
            MsgBox("Could Not Be Saved", MsgBoxStyle.OkOnly, "Please Try Again Or Try A Diffrent .cia")
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim Writer As New PackageIO.Writer(filencch, Endian.Big)
            'when user clicks on the save ncch file button then read the text from the new unique id box and write it onto the file'
            Writer.Position = &H109
            Writer.WriteHexString(TextBox3.Text)
            Writer.Position = &H119
            Writer.WriteHexString(TextBox3.Text)

        Catch ex As Exception
            MsgBox("Could Not Be Saved", MsgBoxStyle.OkOnly, "Please Try Again Or Try A Diffrent .header file")
        End Try

        Try
            Dim Writer As New PackageIO.Writer(filencch, Endian.Little)
            'read the text from the new title id box and write it onto the file'
            Writer.Position = &H150
            Writer.WriteString(TextBox6.Text)

            'prompt the user that the file has been successfully saved'
            MsgBox("NCCH File Successfully Saved")
            'prompt the user with the following message if the file couldn't be saved'
        Catch ex As Exception
            MsgBox("Could Not Be Saved", MsgBoxStyle.OkOnly, "Please Try Again Or Try A Diffrent .header file")
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Dim Writer As New PackageIO.Writer(fileexheader, Endian.Big)
            'when user clicks on the save exheader file button then read the text from the new unique id box and write it onto the file'
            Writer.Position = &H1C9
            Writer.WriteHexString(TextBox7.Text)
            Writer.Position = &H201
            Writer.WriteHexString(TextBox7.Text)
            Writer.Position = &H230
            Writer.WriteHexString(TextBox7.Text)

            'prompt the user that the file has been successfully saved'
            MsgBox("Exheader File Successfully Saved")
            'prompt the user with the following message if the file couldn't be saved'
        Catch ex As Exception
            MsgBox("Could Not Be Saved", MsgBoxStyle.OkOnly, "Please Try Again Or Try A Diffrent .bin file")
        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            Dim Writer As New PackageIO.Writer(fileicon, Endian.Little)
            'when user clicks on the Remove Age Rating button then write the following values onto the file'
            Writer.Position = &H2008
            Writer.WriteHexString("00000000000000000000000000000000")

            'prompt the user that the file has been successfully saved'
            MsgBox("Icon File Successfully Saved")
            'prompt the user with the following message if the file couldn't be saved'
        Catch ex As Exception
            MsgBox("Could Not Be Saved", MsgBoxStyle.OkOnly, "Please Try Again Or Try A Diffrent .icn file")
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            Dim Writer As New PackageIO.Writer(fileicon, Endian.Little)
            'when user clicks on the Remove Region Lock button then write the following values onto the file'
            Writer.Position = &H2018
            Writer.WriteHexString("FFFFFF7F")

            'prompt the user that the file has been successfully saved'
            MsgBox("Icon File Successfully Saved")
            'prompt the user with the following message if the file couldn't be saved'
        Catch ex As Exception
            MsgBox("Could Not Be Saved", MsgBoxStyle.OkOnly, "Please Try Again Or Try A Diffrent .icn file")
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