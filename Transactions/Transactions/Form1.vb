Imports System.Data
Imports System.Data.SqlClient

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Dsbank.Customer' table. You can move, or remove it, as needed.
        Me.CustomerTableAdapter.Fill(Me.Dsbank.Customer)
        Dim dl As New dataLayer()
        dl.loadListbox()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim dl As New dataLayer()
            Dim CustId As Integer = CType(ComboBox1.SelectedValue, Integer)
            dl.MoneyTransfer(CustId, CType(txtAmount.Text, Double))
        Catch ex As Exception
            MessageBox.Show("Please enter a number for amount")
            txtAmount.Focus()

        End Try
    End Sub


End Class
