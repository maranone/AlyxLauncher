Imports System
Imports System.IO
Public Class Form1
    Dim didi As String
    Dim dimap As String
    Dim difolder As String
    Dim config_file As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            didi = FolderBrowserDialog1.SelectedPath & "\steamapps\common\Half-Life Alyx\game\hlvr\maps"
            difolder = FolderBrowserDialog1.SelectedPath
            Dim di As DirectoryInfo = New DirectoryInfo(didi)
            If di.Exists Then
                Dim finfo As New IO.DirectoryInfo(didi)
                For Each fi In finfo.GetFiles
                    ListBox1.Items.Add(fi.Name)
                Next
                config_file = ".\AlyxLauncher.txt"
                Dim objWriter As New System.IO.StreamWriter(config_file, False)
                objWriter.WriteLine(FolderBrowserDialog1.SelectedPath)
                objWriter.Close()
            End If
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        dimap = difolder & "\steamapps\common\Half-Life Alyx\game\bin\win64\hlvr.exe"
        Dim A = ListBox1.SelectedItem.ToString()
        TextBox1.Text = Chr(34) & dimap & Chr(34) & " +map " & A.Substring(0, A.Length - 4)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        config_file = ".\AlyxLauncher.txt"
        If File.Exists(config_file) Then
            config_file = ".\AlyxLauncher.txt"
            Dim reader As New StreamReader(config_file)
            difolder = reader.ReadLine()
            reader.Close()
            didi = difolder & "\steamapps\common\Half-Life Alyx\game\hlvr\maps"
            Dim di As DirectoryInfo = New DirectoryInfo(didi)
            If di.Exists Then
                Dim finfo As New IO.DirectoryInfo(didi)
                For Each fi In finfo.GetFiles
                    ListBox1.Items.Add(fi.Name)
                Next
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim objbat As New System.IO.StreamWriter(".\AlyxLauncher.bat", False)
        objbat.WriteLine(textbox1.text)
        objbat.Close()
        Shell(".\AlyxLauncher.bat", vbNormalFocus)
    End Sub
End Class