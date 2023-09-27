Public Class Credentials

    Private Sub Credentials_Deactivated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivated

        Me.Topmost = True
        Me.Activate()

    End Sub

    Private Sub Credentials_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded

        tbINCompany.Text = My.Settings.InCom
        tbINID.Text = My.Settings.InUId
        tbINPW.Password = My.Settings.InPw

        tbSLCompany.Text = My.Settings.SLCom
        tbSLID.Text = My.Settings.SLUId
        tbSLPW.Password = My.Settings.SLPw

        tbPlantLocation.Text = My.Settings.FtyLocation
        tbPlantName.Text = My.Settings.FtyName
        tbPlantAdd.Text = My.Settings.FtyAdd
        tbPlantEmail.Text = My.Settings.FtyEmail
        tbPlantTp.Text = My.Settings.FtyTp
        tbPlantCertifiName.Text = My.Settings.FtyCirtifier

    End Sub

    Private Sub btnSaveCrPlant_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnSave.Click

        My.Settings.InCom = tbINCompany.Text
        My.Settings.InUId = tbINID.Text
        My.Settings.InPw = tbINPW.Password

        My.Settings.SLCom = tbSLCompany.Text
        My.Settings.SLUId = tbSLID.Text
        My.Settings.SLPw = tbSLPW.Password

        My.Settings.FtyLocation = tbPlantLocation.Text
        My.Settings.FtyName = tbPlantName.Text
        My.Settings.FtyAdd = tbPlantAdd.Text
        My.Settings.FtyEmail = tbPlantEmail.Text
        My.Settings.FtyTp = tbPlantTp.Text
        My.Settings.FtyCirtifier = tbPlantCertifiName.Text

        My.Settings.Save()
        Me.Close()
    End Sub
End Class
