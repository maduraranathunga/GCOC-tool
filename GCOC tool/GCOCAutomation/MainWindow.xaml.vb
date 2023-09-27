Imports Microsoft.Windows.Controls.Ribbon
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports Microsoft.Office.Interop
Imports System.IO
Imports iTextSharp.text.pdf
Imports System.Xml
Imports System.Windows.Threading
Imports System.Windows.Forms.Integration
Imports PDF = PDFCreator

Class MainWindow

#Region "Declaration"

    Dim AxWebBrowser As Object
    Dim xlApp As Excel.Application
    Dim XlWorkbook As Excel.Workbook
    Dim xlSheet1 As Excel.Worksheet
    Dim XlRange As Excel.Range
    Dim XlFormulaRange As Excel.Range
    Dim XlFormulaRange1 As Excel.Range
    Dim XlFormulaRange2 As Excel.Range
    Dim XlFormulaRange3 As Excel.Range
    Dim XlFormulaRangeSum As Excel.Range
    Private Cancel As Boolean = False
    Private Property pageready As Boolean = False
    Dim DefaultPrinter As Object
    Dim PDFCr As PDF.clsPDFCreator
    Private WithEvents pdf As PDFCreator.clsPDFCreator

#End Region

#Region "Page Loading Functions"


    Private Sub WaitForPageLoad()
        AddHandler WebBrowser.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        While Not pageready
            DoEvents()
        End While

        pageready = False
    End Sub

    Private Sub PageWaiter(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
        If WebBrowser.ReadyState = WebBrowserReadyState.Complete Then
            pageready = True
            RemoveHandler WebBrowser.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        End If
    End Sub

    Public Sub DoEvents()
        Dim frame As DispatcherFrame = New DispatcherFrame
        Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, New DispatcherOperationCallback(AddressOf ExitFrame), frame)
        Dispatcher.PushFrame(frame)
    End Sub

    Public Function ExitFrame(ByVal f As Object) As Object
        CType(f, DispatcherFrame).Continue = False
        Return Nothing
    End Function

#End Region


    Private Sub Release(ByVal Obj As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(Obj) > 0)
            End While
        Catch
        Finally
            Obj = Nothing
        End Try
    End Sub


    Private Sub tglSriLanka_Checked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles tglSriLanka.Checked

        tglSriLanka.SmallImageSource = CType(FindResource("ON"), ImageSource)

    End Sub


    Private Sub tglSriLanka_Unchecked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles tglSriLanka.Unchecked

        tglSriLanka.SmallImageSource = CType(FindResource("OFF"), ImageSource)

    End Sub


    Private Sub tglIndia_Checked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles tglIndia.Checked

        tglIndia.SmallImageSource = CType(FindResource("ON"), ImageSource)


    End Sub


    Private Sub tglIndia_Unchecked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles tglIndia.Unchecked

        tglIndia.SmallImageSource = CType(FindResource("OFF"), ImageSource)

    End Sub


    Private Sub tglStart_Checked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles tglStart.Checked

        tglStart.LargeImageSource = CType(FindResource("Start"), ImageSource)
        tglStart.Label = "Stop"
        Cancel = False


        Dim Con As New System.Data.OleDb.OleDbConnection
        Cancel = False
        AxWebBrowser = WebBrowser.ActiveXInstance
        Dim Count As Integer
        Dim Countch As Integer
        Dim StartLn As Integer
        Dim Item As Integer = 0
        Dim Complete As Integer = 0
        Dim UnComplete As Integer = 0
        Dim xmlPath As String = My.Settings.Folder.ToString & "\GCOC Folder\"
        Dim omApp As New Outlook.Application
        Dim omNamespace As Outlook.NameSpace = omApp.GetNamespace("MAPI")
        Dim omDrafts As Outlook.MAPIFolder = omNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderDrafts)
        Dim omMailItem As Outlook.MailItem

        '// Check for validate period

        'If (1360 - DateDiff(DateInterval.Day, Convert.ToDateTime(My.Settings.FirstRun), Now)) < 0 Then

        '    MsgBox("GCOC Automation was unable to upload the data. Please contact the apllication developer.", vbCritical, "Application Error")
        '    tglStart.IsChecked = False
        '    Exit Sub

        'End If

        If tglIndia.IsChecked Then
            If My.Settings.InCom = "" Or My.Settings.InUId = "" Or My.Settings.InPw = "" Then

                MsgBox("Fill Ec Vision Credentials To Proceed.", MsgBoxStyle.OkOnly + vbCritical, "Set Creadentials")
                tglStart.IsChecked = False
                Exit Sub

            End If

        ElseIf tglSriLanka.IsChecked Then
            If My.Settings.SLCom = "" Or My.Settings.SLUId = "" Or My.Settings.SLPw = "" Then

                MsgBox("Fill Ec Vision Credentials To Proceed.", MsgBoxStyle.OkOnly + vbCritical, "Set Creadentials")
                tglStart.IsChecked = False
                Exit Sub

            End If

        End If

        If My.Settings.FtyLocation = "" Or My.Settings.FtyName = "" Or My.Settings.FtyAdd = "" _
            Or My.Settings.FtyEmail = "" Or My.Settings.FtyTp = "" Or My.Settings.FtyCirtifier = "" Then

            MsgBox("Fill Ec Vision Plant Data To Proceed.", MsgBoxStyle.OkOnly + vbCritical, "Set Plant Data")
            tglStart.IsChecked = False
            Exit Sub

        End If

        '// Check for Excel File

        For Each Me.XlWorkbook In xlApp.Workbooks

            If XlWorkbook.Name = "VPO List.xlsx" Then
                XlWorkbook.Save()
                xlSheet1 = XlWorkbook.Sheets("VPO")
            End If

        Next

        '// Action on excel not find.

        If xlSheet1 Is Nothing Then
            MsgBox("M3CDU was unable to find the 'VPO List' file. Please close the application and try again.", MsgBoxStyle.Critical, "Excel File Not Found")
            tglStart.IsChecked = False
            Exit Sub
        End If


        XlRange = xlSheet1.Range("A2:A1048576")
        Countch = xlApp.WorksheetFunction.CountA(XlRange) + 1

        If Countch = 1 Then
            MsgBox("No Data to upload in VPO coloumn", vbInformation, "No Data")
            tglStart.IsChecked = False
            Exit Sub
        End If

        Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName("AcroRd32")

        If pProcess.Length = 0 Then

            MsgBox("Please Open Any PDF File to Proceed", vbInformation, "Open A PDF")
            tglStart.IsChecked = False
            Exit Sub

        End If

        XlFormulaRange = xlSheet1.Range(xlSheet1.Cells(2, 8), xlSheet1.Cells(Countch, 8))
        XlFormulaRange.Value = "=WORKDAY(C2,-10)"

        XlFormulaRange1 = xlSheet1.Range(xlSheet1.Cells(2, 9), xlSheet1.Cells(Countch, 9))
        XlFormulaRange1.Value = "=VLOOKUP(F2,LabData!$A$1:$D$3,2,FALSE)"

        XlFormulaRange2 = xlSheet1.Range(xlSheet1.Cells(2, 10), xlSheet1.Cells(Countch, 10))
        XlFormulaRange2.Value = "=VLOOKUP(F2,LabData!$A$1:$D$3,3,FALSE)"

        XlFormulaRange3 = xlSheet1.Range(xlSheet1.Cells(2, 11), xlSheet1.Cells(Countch, 11))
        XlFormulaRange3.Value = "=VLOOKUP(F2,LabData!$A$1:$D$3,4,FALSE)"

        XlFormulaRangeSum = xlApp.Union(XlFormulaRange, XlFormulaRange1, XlFormulaRange2, XlFormulaRange3)
        XlFormulaRangeSum.Copy()
        XlFormulaRangeSum.PasteSpecial(Excel.XlPasteType.xlPasteValues)
        xlApp.CutCopyMode = False

        Dim LabSheet As Excel.Worksheet = XlWorkbook.Sheets("LabData")

        If LabSheet.Range("F1").Value > 0 Then

            MsgBox("Format Error in Excel Data or Invalid Data Has Enterd.", vbInformation, "Excel Data Error")
            tglStart.IsChecked = False
            Exit Sub

        End If


        ImageFront.Visibility = Windows.Visibility.Hidden
        winHost.Visibility = Windows.Visibility.Visible

        With PDFCr
            .cOption("UseAutosave") = 1
            .cOption("UseAutosaveDirectory") = 1
            .cOption("AutosaveFormat") = 0
            .cOption("OpenOutputFile") = 0
            .cClearCache()
            .cDefaultPrinter = "PDFCreator"
        End With

        WebBrowser.Navigate("https://asiaportal.ecvision.com/mast/jsp/profile/Login.jsp?type=session")
        WaitForPageLoad()

        If tglIndia.IsChecked Then

            AxWebBrowser.Document.GetElementsByName("companyName").Item(0).value = My.Settings.InCom
            AxWebBrowser.Document.GetElementsByName("userName").Item(0).value = My.Settings.InUId
            AxWebBrowser.Document.GetElementsByName("password").Item(0).value = My.Settings.InPw
            SendKeys.SendWait("{ENTER}")
            WaitForPageLoad()

        ElseIf tglSriLanka.IsChecked Then

            AxWebBrowser.Document.GetElementsByName("companyName").Item(0).value = My.Settings.SLCom
            AxWebBrowser.Document.GetElementsByName("userName").Item(0).value = My.Settings.SLUId
            AxWebBrowser.Document.GetElementsByName("password").Item(0).value = My.Settings.SLPw
            SendKeys.SendWait("{ENTER}")
            WaitForPageLoad()



            AxWebBrowser.Document.GetElementsByName("defaultRoleID").Item(0).Checked = True

            AxWebBrowser.Document.GetElementsByName("loginButton").Item(0).Click()
            WaitForPageLoad()

        End If


        Con.ConnectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties= 'Excel 12.0 Xml;HDR=YES';", My.Application.Info.DirectoryPath.ToString & "\VPO List.xlsx")

        Try

            Con.Open()
            Dim com As OleDbCommand = New OleDbCommand("Select [VPO],[Invoice No],[Test Report Number],[Test Report Date(Need to Be In Date Format)],[Composition],[Production Date],[Test Lab Name],[Test Lab Add],[Test Lab Phone] FROM [VPO$]", Con)

            Dim rdr As OleDbDataReader = com.ExecuteReader()

            While rdr.Read

                If Cancel <> True Then


                    Dim FolderPath As String = My.Settings.Folder.ToString & "\GCOC Folder\" & rdr.Item(1).ToString
                    Directory.CreateDirectory(FolderPath)

                    WebBrowser.Navigate("https://asiaportal.ecvision.com/mast/CommonSearchAction.do?pageid=CS_SEARCH_MPO_IN_TAB&operation=submenuchange")
                    WaitForPageLoad()

                    Item = Item + 1
                    stsitemStatus.Content = "Item " & Item & " of " & Countch - 1

                    AxWebBrowser.Document.GetElementsByName("mpoNo").Item(0).value = rdr.GetValue(0).ToString
                    AxWebBrowser.Document.GetElementsByName("ommonSearchSearchBtn").Item(0).Click()
                    WaitForPageLoad()

                    Dim Row As Integer = AxWebBrowser.Document.GetElementsByName("totalrows").Item(0).value

                    If Row > 0 Then

                        AxWebBrowser.Document.Links(Row - 1).Click()
                        WaitForPageLoad()

                        Dim GCOCPrint As String = FolderPath & "\" & rdr.GetValue(0).ToString & ".pdf"
                        ' Dim GCOC As String = xmlPath & "GeneralCertificateOfConformity_MAST_Edited.pdf"
                        Dim GCOC As String = FolderPath & "\" & "GeneralCertificateOfConformity_MAST_Edited.pdf"
                        Dim pdfrdr As PdfReader = New PdfReader(My.Application.Info.DirectoryPath + "\GeneralCertificateOfConformity_MAST_Automation_New.pdf")
                        Dim stm As PdfStamper = New PdfStamper(pdfrdr, New FileStream(GCOC, FileMode.Create))
                        Dim fields As AcroFields = stm.AcroFields

                        fields.SetField("VPONumber", AxWebBrowser.Document.GetElementsByTagName("table").Item(7).Cells(1).InnerText)
                        fields.SetField("GenericAtricleNum", AxWebBrowser.Document.GetElementsByTagName("table").Item(7).Cells(9).InnerText)
                        fields.SetField("CPO", AxWebBrowser.Document.GetElementsByTagName("table").Item(7).Cells(13).InnerText)
                        fields.SetField("Description", AxWebBrowser.Document.GetElementsByTagName("table").Item(7).Cells(11).InnerText & " " & rdr.GetString(4).ToString)
                        fields.SetField("Style", AxWebBrowser.Document.GetElementsByTagName("table").Item(7).Cells(15).InnerText)
                        fields.SetField("DateManufactured", rdr.GetDateTime(5).ToString("MM/dd/yyyy"))

                        fields.SetField("ActualFactoryLocation", My.Settings.FtyLocation)
                        fields.SetField("FactoryName", My.Settings.FtyName)
                        fields.SetField("FactoryAddress1", My.Settings.FtyAdd)
                        fields.SetField("FactoryEmail", My.Settings.FtyEmail)
                        fields.SetField("FactoryPhone", My.Settings.FtyTp)
                        fields.SetField("FactoryPerson", My.Settings.FtyCirtifier)

                        fields.SetField("TestReportDate5", rdr.GetDateTime(3).ToString("MM/dd/yyyy"))
                        fields.SetField("TestReportNumber5", rdr.Item(2).ToString)
                        fields.SetField("CertID", AxWebBrowser.Document.GetElementsByTagName("table").Item(7).Cells(1).InnerText & AxWebBrowser.Document.GetElementsByTagName("table").Item(7).Cells(9).InnerText & Now.ToString("ddMMyyyyHHmmss"))
                        fields.SetField("TestingLabName", rdr.Item(6).ToString)
                        fields.SetField("TestingLabAddress1", rdr.Item(7).ToString)
                        fields.SetField("TestingLabPhone", rdr.Item(8).ToString)

                        If AxWebBrowser.Document.GetElementsByTagName("table").Item(7).Cells(5).InnerText = "VSS" Then
                            fields.SetField("LabelerName", "Victoria's Secret Stores")
                        ElseIf AxWebBrowser.Document.GetElementsByTagName("table").Item(7).Cells(5).InnerText = "VSD" Then
                            fields.SetField("LabelerName", "Victoria's Secret Direct")
                        End If

                        stm.FormFlattening = False
                        stm.Close()

                        PDFCr.cOption("AutosaveDirectory") = FolderPath
                        PDFCr.cOption("AutosaveFilename") = rdr.GetValue(0).ToString
                        PDFCr.cPrintFile(GCOC)

                        'Do Until PDFCr.cCountOfPrintjobs > 0

                        'Loop

                        'Do Until PDFCr.cCountOfPrintjobs = 0

                        'Loop

                        PDFCr.cPrinterStop = False


                        '---xml----

                        Dim reader As PdfReader = New PdfReader(GCOC)

                        Dim dataSetsNode As XmlNode = fields.Xfa.DatasetsNode

                        Dim settings As XmlWriterSettings = New XmlWriterSettings()
                        settings.Indent = True
                        Dim writer As XmlWriter = XmlWriter.Create(xmlPath + "GeneralCertificateOfConformity_MAST_data.xml", settings)
                        dataSetsNode.WriteTo(writer)
                        writer.Flush()
                        writer.Close()


                        omMailItem = CType(omDrafts.Items.Add, Outlook.MailItem)
                        With omMailItem
                            .To = "mast@gcoc.limitedbrands.com"
                            .Subject = "v04012012 (" & rdr.GetValue(0).ToString & ")"
                            .Body = "The attached file contains data that was entered into a form. It is not the form itself." & Environment.NewLine & Environment.NewLine _
                                    & "The recipient of this data file should save it locally with a unique name. Adobe Acrobat Professional 7 or later can process this data by importing it back into the blank form or creating a spreadsheet from several data files. See Help in Adobe Acrobat Professional 7 or later for more details."
                            .Attachments.Add(xmlPath + "GeneralCertificateOfConformity_MAST_data.xml", , , "GeneralCertificateOfConformity_MAST_data.xml")
                            .Save()

                        End With

                        Complete = Complete + 1
                    Else

                        xlSheet1.Cells(Item + 1, 1).Interior.Color = 255
                        UnComplete = UnComplete + 1

                    End If

                Else

                    stsitemStatus.Content = "Canceled at " + Convert.ToString(Item) + " of " + Convert.ToString(Countch - 1)
                    tglStart.IsChecked = False
                    Exit While

                End If
            End While

            WebBrowser.Navigate("https://asiaportal.ecvision.com/mast/CommonSearchAction.do?pageid=CS_SEARCH_MPO_IN_TAB&operation=submenuchange")

            rdr.Close()
            Con.Close()

            omApp = Nothing
            omNamespace = Nothing
            omDrafts = Nothing
            omMailItem = Nothing
            tglStart.IsChecked = False

            If Me.Cancel <> True Then
                stsitemStatus.Content = "Completed"
            End If

            MsgBox("GCOC completed for " + Convert.ToString(Item) + " VPO Number(s)" + vbCrLf + Convert.ToString(Complete) + " GCOCS(s) Completed successfuly" + vbCrLf + Convert.ToString(UnComplete) + " Error(s) Occured.", vbInformation, "Creation Completed")
            tglStart.IsChecked = False

            ImageFront.Visibility = Windows.Visibility.Visible
            winHost.Visibility = Windows.Visibility.Hidden

        Catch ex As Exception

            MsgBox(ex.Message.ToString, vbCritical + vbOKOnly, "Appliation Error")
            tglStart.IsChecked = False

            ImageFront.Visibility = Windows.Visibility.Visible
            winHost.Visibility = Windows.Visibility.Hidden

        End Try

    End Sub


    Private Sub tglStart_Unchecked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles tglStart.Unchecked

        tglStart.LargeImageSource = CType(FindResource("Stop"), ImageSource)
        tglStart.Label = "Start"
        Cancel = True

    End Sub


    Private Sub btnCredential_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnCredential.Click

        Dim winCredentials As Credentials = New Credentials
        winCredentials.Show()

    End Sub


    Private Sub btnFolder_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnFolder.Click

        Dim FolderSelect As FolderBrowserDialog = New FolderBrowserDialog

        If FolderSelect.ShowDialog = System.Windows.Forms.DialogResult.OK Then

            My.Settings.Folder = FolderSelect.SelectedPath.ToString
            My.Settings.Save()
            btnFolder.ToolTipDescription = "Current GCOC Folder - : " & Environment.NewLine & My.Settings.Folder

        End If


    End Sub


    Private Sub btnFolder_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnFolder.Loaded

        If My.Settings.Folder = "" Then

            btnFolder.ToolTipDescription = "GCOC Folder Not Set."

        Else

            btnFolder.ToolTipDescription = "Current GCOC Folder - " & Environment.NewLine & My.Settings.Folder

        End If

    End Sub


    Private Sub MainWindow_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing

        Dim M3CDUExit As MessageBoxResult
        M3CDUExit = MessageBox.Show("'VPO List' File will be closed automaticaly. You will loose any unsaved data." + vbCrLf + "Are You Rearly Want to Exit?.", "GCOC Automation Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

        Select Case M3CDUExit

            Case MsgBoxResult.Yes

                PDFCr.cDefaultPrinter = DefaultPrinter
                PDFCr.cClose()
                pdf = Nothing
                PDFCr = Nothing
                Release(PDFCr)

                On Error GoTo NextLN


                xlApp.Workbooks("VPO List.xlsx").Close(SaveChanges:=False)

NextLN:         xlSheet1 = Nothing
                XlWorkbook = Nothing
                Release(xlApp)
                e.Cancel = False

            Case Else

                e.Cancel = True

        End Select

    End Sub


    Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded


        System.Windows.Forms.Application.EnableVisualStyles()
        WebBrowser.ScriptErrorsSuppressed = True
        Me.Title = "GCOC Automation - Version " + Convert.ToString(My.Application.Info.Version)

        PDFCr = CreateObject("PDFCreator.clsPDFCreator")


        With PDFCr
            .cStart(, ForceInitialize:=True)
            DefaultPrinter = .cDefaultPrinter
        End With

        WebBrowser.Navigate("https://asiaportal.ecvision.com/mast/EndUserLoginAction.do")

        If My.Settings.FirstRun = "" Then
            My.Settings.FirstRun = Now.ToString
            My.Settings.Save()
        End If

        If My.Settings.Folder = "" Then
            My.Settings.Folder = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString
            My.Settings.Save()
            btnFolder.ToolTipDescription = "Current GCOC Folder - : " & Environment.NewLine & My.Settings.Folder
        End If

        System.IO.File.WriteAllBytes(My.Application.Info.DirectoryPath + "\VPO List.xlsx", My.Resources.VPO_List)
        System.IO.File.WriteAllBytes(My.Application.Info.DirectoryPath + "\GeneralCertificateOfConformity_MAST_Automation_New.pdf", My.Resources.GeneralCertificateOfConformity_MAST_Automation)


        On Error Resume Next

        xlApp = GetObject(, "Excel.Application")

        If xlApp Is Nothing Then

            xlApp = CreateObject("Excel.Application")
            XlWorkbook = xlApp.Workbooks.Open(My.Application.Info.DirectoryPath + "\VPO List.xlsx")

            If XlWorkbook Is Nothing Then

                MsgBox("Cannot Find the 'VPO List' File in the Application Folder." + vbCrLf + "The application is not functional without the 'VPO List' File", vbOKOnly + vbInformation, "File Open Error")
                xlApp = Nothing
                Exit Sub

            End If

            xlApp.Visible = True

        Else

            For Each Me.XlWorkbook In xlApp.Workbooks

                If XlWorkbook.Name = "VPO List.xlsx" Then
                    xlSheet1 = XlWorkbook.Sheets("VPO")
                End If

            Next

            If xlSheet1 Is Nothing Then

                XlWorkbook = xlApp.Workbooks.Open(My.Application.Info.DirectoryPath + "\VPO List.xlsx")

                If XlWorkbook Is Nothing Then

                    MsgBox("Cannot Find the 'VPO List' File in the Application Folder." + vbCrLf + "The application is not functional without the 'VPO List' File", vbOKOnly + vbInformation, "File Open Error")
                    xlApp = Nothing
                    Exit Sub

                End If

            End If

        End If

    End Sub


    Private Sub WebBrowser_StatusTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles WebBrowser.StatusTextChanged
        stsitemBrowser.Content = WebBrowser.StatusText
    End Sub


    Private Sub WebBrowser_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser.DocumentCompleted
        pgrsbarBrowser.Visibility = False
    End Sub


    Private Sub WebBrowser_ProgressChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserProgressChangedEventArgs) Handles WebBrowser.ProgressChanged

        Dim messageBoxVB As New System.Text.StringBuilder()

        If e.CurrentProgress >= 0 Then
            pgrsbarBrowser.Visibility = True
            pgrsbarBrowser.Maximum = e.MaximumProgress
            If e.MaximumProgress < e.CurrentProgress Then Exit Sub
            pgrsbarBrowser.Value = e.CurrentProgress

        End If

    End Sub

   
    Private Sub btnAbout_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAbout.Click

        Dim About As About = New About
        About.Show()

    End Sub




    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click



    '    Dim GCOc As String = "C:\Users\WasanthaK\Desktop\new2gcoc.pdf"
    '    Dim pdfrdr As PdfReader = New PdfReader(My.Application.Info.DirectoryPath + "\GeneralCertificateOfConformity_MAST_Automation_New.pdf")
    '    Dim stm As PdfStamper = New PdfStamper(pdfrdr, New FileStream(GCOc, FileMode.Create))
    '    Dim fields As AcroFields = stm.AcroFields
    '    fields.SetField("ImporterName", "Mast Industries,Inc")
    '    fields.SetField("ImporterAddress1", "Mast Industries,Inc,C/o Rick Paul,2 Limited Parkway,Columbus,OH 43230,USA")
    '    fields.SetField("ImporterPhone", "614-415-2423")


    '    fields.SetField("LabelerAddress1", "Victoria's Secret Brand Management,4 Limited Parkway Reynoldsburg,OH 4306")
    '    fields.SetField("LabelerPhone", "614-577-2404")


    '    fields.SetField("ExemptFlammable", 0)
    '    fields.SetField("CFR1610", 0)

    '    stm.FormFlattening = False

    '    stm.Close()
    'End Sub

    Private Sub btnSettingReset_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnSettingReset.Click

        Dim settingReset As SettingReset = New SettingReset
        settingReset.Show()

    End Sub

    Private Sub RibbonMy_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles RibbonMy.SelectionChanged

    End Sub
End Class





