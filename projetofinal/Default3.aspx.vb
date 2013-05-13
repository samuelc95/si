﻿
Partial Class Default3
    Inherits System.Web.UI.Page

    Protected Sub btnEnviaArquivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviaArquivo.Click

        Dim tamanho As Integer
        Dim vetor As Byte()

        If FileUpload.PostedFile Is Nothing Then
            FileUpLoad.Text = "Nenhum arquivo definido."
            Exit Sub
        Else

            Dim nomeArquivo As String = FileUpload.PostedFile.FileName
            Dim ext As String = nomeArquivo.Substring(nomeArquivo.LastIndexOf("."))
            ext = ext.ToLower

            Dim imgTipo = FileUpload.PostedFile.ContentType

            If ext = ".jpg" Then
            ElseIf ext = ".bmp" Then
            ElseIf ext = ".gif" Then
            ElseIf ext = "jpg" Then
            ElseIf ext = "bmp" Then
            ElseIf ext = "gif" Then
            Else
                FileUpLoad.Text = "Somente são suportados arquivos nos formatos: gif, bmp, ou jpg."
                Exit Sub
            End If

            tamanho = Convert.ToInt32(FileUpload.PostedFile.InputStream.Length)
            ReDim vetor(tamanho)

            FileUpload.PostedFile.InputStream.Read(vetor, 0, tamanho)

            If salvaImagem(txtTituloImagem.Text.Trim, vetor, tamanho, imgTipo) = True Then
                FileUpLoad.Text = "Imagem enviada com sucesso..."
            Else
                FileUpLoad.Text = "Ocorreu um erro durante o envio da imagem... Tente novamente..."
            End If
        End If
    End Sub
    Protected Function SalvaImagem(ByVal imgTitulo As String, ByVal img As Byte(), ByVal imgTamanho As Integer, ByVal imgTipo As String) As Boolean

        Try
            Dim cnn As Data.SqlClient.SqlConnection
            Dim cmd As Data.SqlClient.SqlCommand
            Dim param As Data.SqlClient.SqlParameter
            Dim strSQL As String

            strSQL = "Insert Into Imagens(imagem,imagemTitulo,imagemTipo,imagemTamanho) Values(@img,@imgTitulo,@imgTipo,@imgTamanho)"

            'defina a string de conexão e cria uma nova conexão
            Dim connString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\ImagensBD.mdf;Integrated Security=True;User Instance=True"
            cnn = New Data.SqlClient.SqlConnection(connString)

            'define o comando a ser executado
            cmd = New Data.SqlClient.SqlCommand(strSQL, cnn)

            'recebe os parâmetros
            param = New Data.SqlClient.SqlParameter("@img", Data.SqlDbType.Image)
            param.Value = img
            cmd.Parameters.Add(param)

            param = New Data.SqlClient.SqlParameter("@imgTitulo", Data.SqlDbType.VarChar)
            param.Value = imgTitulo
            cmd.Parameters.Add(param)

            param = New Data.SqlClient.SqlParameter("@imgTipo", Data.SqlDbType.VarChar)
            param.Value = imgTipo
            cmd.Parameters.Add(param)

            param = New Data.SqlClient.SqlParameter("@imgTamanho", Data.SqlDbType.BigInt)
            param.Value = imgTamanho
            cmd.Parameters.Add(param)

            'abre a conexão
            cnn.Open()
            'executa a instrução SQL se retornar
            cmd.ExecuteNonQuery()
            'fecha a conexao
            cnn.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Private Function txtTituloImagem() As Object
        Throw New NotImplementedException
    End Function

End Class
