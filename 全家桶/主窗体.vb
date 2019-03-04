﻿Public Class 主窗体

    Private 工具列表 As Dictionary(Of HLGroupItem, HLForm), RightClose As Boolean

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = 标题
        Icon = 图标
        With 消息图标
            .Visible = True
            .Icon = Icon
            .Text = Text
            .ContextMenuStrip = NotifMenu
            AddHandler .DoubleClick, Sub()
                                         打开主页ToolStripMenuItem_Click(Nothing, Nothing)
                                     End Sub
        End With
        工具列表 = New Dictionary(Of HLGroupItem, HLForm)
        Dim a As String = "bilibili"
        添加工具(a, "图床", B站图床)
        a = "生活"
        添加工具(a, 中小学生学习水平估测)
        添加工具(a, 每月提醒我, True)
        a = "系统"
        添加工具(a, 文件图标提取)
        ListTools.SortAll()
        RightClose = False
        随机一句话()
    End Sub

    Private Sub 添加工具(组 As String, 窗体 As HLForm, Optional 预加载 As Boolean = False)
        添加工具(组, 窗体.Name, 窗体, 预加载)
    End Sub

    Private Sub 添加工具(组 As String, 名字 As String, 窗体 As HLForm, Optional 预加载 As Boolean = False)
        Dim g As HLGroup = ListTools.GetGroup(组)
        If 为空(g) Then
            g = New HLGroup(组)
            ListTools.Groups.Add(g)
        End If
        Dim t As New HLGroupItem(名字, 窗体.Icon)
        窗体.KeyPreview = True
        AddHandler 窗体.FormClosing, AddressOf 主窗体_FormClosing
        AddHandler 窗体.KeyDown, AddressOf 主窗体_KeyDown
        g.Items.Add(t)
        工具列表.Add(t, 窗体)
        With 窗体
            If 预加载 Then
                .ShowInTaskbar = False
                .Show()
                .Hide()
                .ShowInTaskbar = True
            End If
        End With
    End Sub

    Private Sub ListTools_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListTools.MouseDoubleClick
        Dim i As HLGroupItem = ListTools.SelectedItem
        If 为空(i) OrElse 工具列表.ContainsKey(i) = False Then Exit Sub
        ShowUp(Me, 工具列表.Item(i))
    End Sub

    Private Sub ShowUp(a As HLForm, m As HLForm)
        With m
            .Show()
            .Left = a.Left
            .Top = a.Top
            .WindowState = FormWindowState.Normal
            .BringToFront()
        End With
    End Sub

    Private Sub 主窗体_FormClosing(sender As HLForm, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Dim m As Integer = 0
        For Each i As Form In My.Application.OpenForms
            If i.Visible Then m += 1
        Next
        If Text <> sender.Text AndAlso Me.Visible = False AndAlso m < 2 Then
            ShowUp(sender, Me)
            With Me
                .Left = sender.Right - .Width
                .Top = sender.Top
            End With
        ElseIf Text = sender.Text Then
            If RightClose Then
                退出ToolStripMenuItem.PerformClick()
            End If
        End If
        sender.Hide()
    End Sub

    Private Sub 打开主页ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 打开主页ToolStripMenuItem.Click
        ShowUp(Me, Me)
    End Sub

    Private Sub 退出ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 退出ToolStripMenuItem.Click
        My.MyApplication.正常退出()
    End Sub

    Private Sub 主窗体_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.W Then
            sender.Close()
        End If
    End Sub

    Private Sub 主窗体_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = MouseButtons.Right Then RightClose = True
    End Sub

    Private Sub 主窗体_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        RightClose = False
    End Sub

    Private Sub 随机一句话()
        LabFun.Text = 随机.当中一个("reviewed LOVE - Recommended
本质上就是款单纯的跳台游戏，就是类似 I WANNA 那样的游戏。用来打发时间还不错，但要一直玩，怕会崩溃。
Reviewer received this product for free
Read the full review").ToString
    End Sub

End Class
