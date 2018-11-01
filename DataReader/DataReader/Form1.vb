Imports System.Data
Imports System.Data.SqlClient

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadListbox()
    End Sub
    Public Sub loadListbox()
        ListBox1.Items.Clear()
        Dim connStr As String = My.Settings.NORTHWNDConnectionString
        Dim sql As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        sql = "select RegionID, RegionDescription from region"
        conn = New SqlConnection(connStr)
        conn.Open()
        cmd = New SqlCommand(sql, conn)
        Dim reader As SqlDataReader = cmd.ExecuteReader
        Dim moreresults As Boolean = True
        Do While moreresults
            While reader.Read
                ListBox1.Items.Add(reader(1))
            End While
            moreresults = reader.NextResult()

        Loop
        conn.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim connStr As String = My.Settings.NORTHWNDConnectionString
        Dim sql As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        conn = New SqlConnection(connStr)
        conn.Open()
        cmd = New SqlCommand()
        cmd.Connection = conn

        Dim Rid As New SqlParameter("@regionid", SqlDbType.Int)
        Rid.Direction = ParameterDirection.Input

        Dim RDiscription As New SqlParameter("@RegionDescription", SqlDbType.NChar)
        RDiscription.Direction = ParameterDirection.Input
        Rid.Value = CType(txtID.Text, Integer)
        RDiscription.Value = txtDesciption.Text
        cmd.Parameters.Add(Rid)
        cmd.Parameters.Add(RDiscription)
        cmd.CommandText = "insert into region(regionid, regiondescription)values(@regionid, @regiondescription)"
        Dim result As Integer = cmd.ExecuteNonQuery()
        conn.Close()
        If result > 0 Then
            MessageBox.Show("Success")
        Else
            MessageBox.Show("Failure")
        End If
        loadListbox()

    End Sub
End Class
