Imports System.Data
Imports System.Data.SqlClient

Public Class Form1
    Private Function getConnection() As String
        Return My.Settings.PeopleConnectionString
    End Function
    Private Function getPeople() As DataTable
        Dim sql As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        sql = "select * from People"
        conn = New SqlConnection(getConnection())
        conn.Open()
        cmd = New SqlCommand(sql, conn)
        da = New SqlDataAdapter(cmd)
        da.Fill(dt)
        conn.Close()
        Return dt
    End Function
    Private Sub AddPeople(fname As String, lname As String)
        Dim sql As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        sql = "insert into People(firstname, lastname)values(@fname, @lname)"
        conn = New SqlConnection(getConnection())
        conn.Open()
        cmd = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@fname", fname)
        cmd.Parameters.AddWithValue("@lname", lname)
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Dim dt As New DataTable
        dt = getPeople()
        DataGridView1.DataSource = dt
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            AddPeople(txtFirstName.Text, txtLastName.Text)
            MessageBox.Show("Added")
        Catch ex As Exception
            MessageBox.Show("Error" & ex.Message)
        End Try
        btnView.PerformClick()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim dr As DataGridViewRow = DataGridView1.SelectedRows(0)
            lblPeopleID.Text = dr.Cells(0).Value.ToString
            txtFirstName.Text = dr.Cells(1).Value.ToString
            txtLastName.Text = dr.Cells(2).Value.ToString
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim sql As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        sql = "update People set firstname=@fname, lastname=@lname where people_id=@id"
        conn = New SqlConnection(getConnection())
        conn.Open()
        cmd = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@fname", txtFirstName.Text)
        cmd.Parameters.AddWithValue("@lname", txtLastName.Text)
        cmd.Parameters.AddWithValue("@id", CInt(lblPeopleID.Text))
        cmd.ExecuteNonQuery()
        conn.Close()
        btnView.PerformClick()

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim sql As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        sql = "delete from People where people_id=@id"
        conn = New SqlConnection(getConnection())
        conn.Open()
        cmd = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@id", CInt(lblPeopleID.Text))
        cmd.ExecuteNonQuery()
        conn.Close()
        btnView.PerformClick()

    End Sub

    Private Sub FileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileToolStripMenuItem.Click
        Application.Exit()
    End Sub
End Class
