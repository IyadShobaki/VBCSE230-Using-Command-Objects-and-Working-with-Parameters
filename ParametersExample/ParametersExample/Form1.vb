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
        sql = "select * from customers where country = @country"
        conn = New SqlConnection(getConnection())
        conn.Open()
        cmd = New SqlCommand(sql, conn)
        cmd.Parameters.Add(pCountry)
        da = New SqlDataAdapter(cmd)
        da.Fill(dt)
        conn.Close()
        Return dt
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dt As New DataTable
        dt = getCustomers(TextBox1.Text)
        DataGridView1.DataSource = dt
    End Sub
End Class
