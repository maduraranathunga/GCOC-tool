Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Public Class SettingReset

    Dim EncryptionKey As String = "WasanthaK"
    Dim cipherText, clearText, UserIdText As String

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnReset.Click
        UserIdText = tbUserId.Text.ToLower
        If UserIdText = "gcoc\administrator" & Now().ToString("hhmm") Then

            clearText = Now().ToString("yyyy-MM-dd")
            Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
                 &H65, &H64, &H76, &H65, &H64, &H65, _
                 &H76})
                encryptor.KeySize = 128
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using ms As New MemoryStream()
                    Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                        cs.Write(clearBytes, 0, clearBytes.Length)
                        cs.Close()
                    End Using
                    clearText = Convert.ToBase64String(ms.ToArray())
                End Using
            End Using

            TextBox1.Text = clearText
            TextBox1.Visibility = Windows.Visibility.Visible

        ElseIf tbUserId.Text = "GCOCAutomation" Then

            If tbPassword.Text = "" Then

                MsgBox("Application Value Empty", MsgBoxStyle.OkOnly, "Application Value Empty")

            Else

                cipherText = tbPassword.Text
                cipherText = cipherText.Replace(" ", "+")
                Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
                Using encryptor As Aes = Aes.Create()
                    Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
                     &H65, &H64, &H76, &H65, &H64, &H65, _
                     &H76})
                    encryptor.KeySize = 128
                    encryptor.Key = pdb.GetBytes(32)
                    encryptor.IV = pdb.GetBytes(16)
                    Using ms As New MemoryStream()
                        Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                            cs.Write(cipherBytes, 0, cipherBytes.Length)
                            cs.Close()
                        End Using
                        cipherText = Encoding.Unicode.GetString(ms.ToArray())
                    End Using
                End Using

                If cipherText = Now().ToString("yyyy-MM-dd") Then
                    My.Settings.FirstRun = Now.ToString
                    My.Settings.Save()
                    MsgBox("Setting Reset Successfully !", MsgBoxStyle.OkOnly, "Reset Success")
                    Me.Close()
                Else
                    MsgBox("Invalid Application Value", MsgBoxStyle.OkOnly, "Invalid Application Value")
                End If

            End If

        Else

            MsgBox("Invalid Application Name", MsgBoxStyle.OkOnly, "Invalid Application Name")

        End If

    End Sub


    

    Private Sub SettingReset_Initialized(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Initialized
        If tbUserId.Text = "gcoc\administrator" & Now().ToString("hhmm") Then
            TextBox1.Visibility = Windows.Visibility.Visible
        Else
            TextBox1.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

End Class
