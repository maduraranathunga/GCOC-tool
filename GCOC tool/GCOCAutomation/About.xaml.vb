Public Class About

    Private Sub About_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Label2.Content = "Version               : " & My.Application.Info.Version.ToString
    End Sub

    

    Private Sub ScrollViewer1_ScrollChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.ScrollChangedEventArgs) Handles ScrollViewer1.ScrollChanged
        Label7.Content = "First Run             : " & My.Settings.FirstRun
    End Sub
End Class
