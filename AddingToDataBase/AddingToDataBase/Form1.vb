Imports System.Data
Imports System.Data.SqlClient

Public Class Form1
    Private Function getConnection() As String
        Return My.Settings.NORTHWNDConnectionString
    End Function
    Private Function getCustomers(country As String) As DataTable
        Dim sql As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim pCountry As New SqlParameter
        pCountry.ParameterName = "@country"
        pCountry.Value = country
        sql = "select * from employees where country = @country"
        conn = New SqlConnection(getConnection())
        conn.Open()
        cmd = New SqlCommand(sql, conn)
        cmd.Parameters.Add(pCountry)
        da = New SqlDataAdapter(cmd)
        da.Fill(dt)
        conn.Close()
        Return dt


    End Function
    Private Sub AddCustomer(fname As String, lname As String, country As String)
        Dim sql As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        sql = "insert into employees(firstname, lastname, country)values("
        sql += "@fname, @lname, @country)"
        conn = New SqlConnection(getConnection())
        conn.Open()
        cmd = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@fname", fname)
        cmd.Parameters.AddWithValue("@lname", lname)
        cmd.Parameters.AddWithValue("@country", country)
        cmd.ExecuteNonQuery()
        conn.Close()

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            AddCustomer(txtFirstName.Text, txtLastName.Text, txtCountry.Text)
            MessageBox.Show("Added")
        Catch ex As Exception
            MessageBox.Show("Error" & ex.Message)

        End Try
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Dim dt As New DataTable
        dt = getCustomers(txtGetCountry.Text)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim dr As DataGridViewRow = DataGridView1.SelectedRows(0)
            lblEmployeeID.Text = dr.Cells(0).Value.ToString()
            txtFirstName.Text = dr.Cells(2).Value.ToString()
            txtLastName.Text = dr.Cells(1).Value.ToString()
            txtCountry.Text = dr.Cells(11).Value.ToString()


        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim sql As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        sql = "update employees set firstname=@fname, lastname=@lname,"
        sql += "country=@country where employeeid=@id"
        conn = New SqlConnection(getConnection())
        conn.Open()
        cmd = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@fname", txtFirstName.Text)
        cmd.Parameters.AddWithValue("@lname", txtLastName.Text)
        cmd.Parameters.AddWithValue("@country", txtCountry.Text)
        cmd.Parameters.AddWithValue("@ID", CInt(lblEmployeeID.Text))
        cmd.ExecuteNonQuery()
        conn.Close()
        btnView.PerformClick()

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim sql As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        sql = "delete from employees where employeeid=@id"
        conn = New SqlConnection(getConnection())
        conn.Open()
        cmd = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@ID", CInt(lblEmployeeID.Text))
        cmd.ExecuteNonQuery()
        conn.Close()
        btnView.PerformClick()

    End Sub
End Class
