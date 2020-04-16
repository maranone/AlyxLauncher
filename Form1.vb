﻿Imports System
Imports System.IO
Imports System.Windows.Forms

Public Class Form1
    Dim didi As String
    Dim dimap As String
    Dim difolder As String
    Dim config_file As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If FolderBrowserDialog1.ShowDialog() = MyBase.DialogResult.OK Then
            didi = FolderBrowserDialog1.SelectedPath & "\steamapps\common\Half-Life Alyx\game\hlvr\maps"
            difolder = FolderBrowserDialog1.SelectedPath
            Dim di As DirectoryInfo = New DirectoryInfo(didi)
            If di.Exists Then
                Dim finfo As New IO.DirectoryInfo(didi)
                For Each fi In finfo.GetFiles
                    If UCase(fi.Name.Substring(fi.Name.Length - 3, 3)) = "VPK" Then
                        ListBox1.Items.Add(fi.Name)
                    End If
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
        TextBox1.Text = Chr(34) & dimap & Chr(34) & " -console –vconsole +map " & A.Substring(0, A.Length - 4)
        Button2.Enabled = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        AllowDrop = True
        Button2.Enabled = False
        CheckBox1.Checked = True
        CheckBox2.Checked = True
        CheckBox3.Checked = True
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
                    If UCase(fi.Name.Substring(fi.Name.Length - 3, 3)) = "VPK" Then
                        ListBox1.Items.Add(fi.Name)
                    End If
                Next
            End If
        Else
            didi = "C:\Program Files (x86)\Steam"
            Dim di As DirectoryInfo = New DirectoryInfo(didi)
            If di.Exists Then
                Dim finfo As New IO.DirectoryInfo(didi)
                For Each fi In finfo.GetFiles
                    If UCase(fi.Name.Substring(fi.Name.Length - 3, 3)) = "VPK" Then
                        ListBox1.Items.Add(fi.Name)
                    End If
                Next
                config_file = ".\AlyxLauncher.txt"
                Dim objWriter As New System.IO.StreamWriter(config_file, False)
                objWriter.WriteLine(didi)
                objWriter.Close()
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim TexttoRun As String
        TexttoRun = TextBox1.Text
        If CheckBox1.Checked = True Then TexttoRun = TexttoRun & " " & TextBox2.Text
        If CheckBox2.Checked = True Then TexttoRun = TexttoRun & " " & TextBox3.Text
        If CheckBox3.Checked = True Then TexttoRun = TexttoRun & " " & TextBox4.Text
        If CheckBox4.Checked = True Then TexttoRun = TexttoRun & " " & TextBox5.Text
        Dim objbat As New System.IO.StreamWriter(".\AlyxLauncher.bat", False)
        objbat.WriteLine(TexttoRun)
        objbat.Close()
        Shell(".\AlyxLauncher.bat", vbNormalFocus)
    End Sub

    Private Sub Form1_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        Dim pathext As String
        Dim pathsplit As Array
        For Each path In files
            If System.IO.File.Exists(path) = True Then
                pathext = path.Substring(path.Length - 3, 3)
                pathsplit = Split(path, "\")
                If UCase(pathext) = "VPK" Then
                    'MsgBox(pathsplit(UBound(pathsplit)))
                    System.IO.File.Copy(path, didi & "\" & pathsplit(UBound(pathsplit)), True)
                    MessageBox.Show(pathsplit(UBound(pathsplit)) & " File Copied, resetting the file list")
                End If
            End If
        Next
        Me.Controls.Clear() 'removes all the controls on the form
        InitializeComponent() 'load all the controls again
        Form1_Load(e, e)
    End Sub

    Private Sub Form1_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
End Class