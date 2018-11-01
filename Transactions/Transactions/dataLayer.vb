Imports System.Data
Imports System.Data.SqlClient

Public Class dataLayer
    Private Function getConnection() As String
        Return My.Settings.bankConnectionString

    End Function
    Public Function MoneyTransfer(ByVal fromPerson As Integer, ByVal toTransfer As Double) As Boolean
        Dim flag As Boolean = False
        Dim sql1, sql2 As String
        Dim conn As SqlConnection
        Dim cmd1 As SqlCommand
        Dim cmd2 As SqlCommand
        Dim cmd3 As SqlCommand
        Dim cust_id1 As New SqlParameter("@cust_id", SqlDbType.Int)
        cust_id1.Direction = ParameterDirection.Input
        Dim Amount1 As New SqlParameter("@amount", SqlDbType.VarChar)
        Amount1.Direction = ParameterDirection.Input
        cust_id1.Value = fromPerson
        Amount1.Value = toTransfer * -1
        sql1 = "insert into checking(cust_id, amount)values(@cust_id, @amount)"
        Dim cust_id2 As New SqlParameter("@cust_id", SqlDbType.Int)
        Dim Amount2 As New SqlParameter("@amount", SqlDbType.VarChar)
        cust_id2.Value = fromPerson
        Amount2.Value = toTransfer
        sql2 = "insert into savings(cust_id, amount)values(@cust_id, @amount)"
        conn = New SqlConnection(getConnection())
        conn.Open()
        cmd1 = New SqlCommand(sql1, conn)
        cmd2 = New SqlCommand(sql2, conn)
        cmd1.Parameters.Add(cust_id1)
        cmd1.Parameters.Add(Amount1)
        cmd2.Parameters.Add(cust_id2)
        cmd2.Parameters.Add(Amount2)
        Dim tr As SqlTransaction
        tr = conn.BeginTransaction()
        cmd1.Transaction = tr
        cmd2.Transaction = tr

        cmd1.ExecuteNonQuery()
        cmd2.ExecuteNonQuery()

        cmd3 = New SqlCommand()
        cmd3.Connection = conn
        Dim cust_id3 As New SqlParameter("@cust_id", SqlDbType.Int)
        cust_id3.Value = fromPerson
        cmd3.CommandText = "select sum(amount) from checking where cust_id=@cust_id"
        cmd3.Parameters.Add(cust_id3)
        cmd3.Transaction = tr
        Dim total As Double = cmd3.ExecuteScalar()
        If total < 0 Then
            tr.Rollback()
            MessageBox.Show("Insufficient Funds")
        Else
            tr.Commit()
            MessageBox.Show("Transfer Successful")
        End If
        conn.Close()
        Return flag
    End Function

End Class
