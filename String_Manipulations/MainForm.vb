Public Class MainForm
    'https://adonetaccess2003.blogspot.com
    Private Enum Description As Integer
        Asc
        AscW
        Chr
        ChrW
        Filter
        Format
        FormatCurrency
        FormatDateTime
        FormatNumber
        FormatPercent
        InStr
        InStrRev
        Join
        LCase
        Left
        Len
        LSet
        LTrim
        Mid
        Replace
        Right
        RSet
        RTrim
        Space
        Split
        StrComp
        StrConv
        StrDup
        StrReverse
        Trim
        UCase
    End Enum
    Private WithEvents Ioptions As RadioButton
    Private WithEvents ItextBox As TextBox
    Private ThisTextBox(1) As TextBox
    Private Labels(1) As Label
    Dim LabelText As String() = {"Input", "Output"}
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
        Size = New Size(575, 425)
        'Create RadioButtons to Test String Manipulation.
        Dim EnumCount As Integer = 0    'H-Spaces between RadioButtons
        Dim IoptionNm As String() = [Enum].GetNames(GetType(Description))   'Get Enum Values as String List
        For Each Imem As String In IoptionNm
            EnumCount += 20
            Ioptions = New RadioButton
            With Ioptions   'Assign each Enum Member to RadioButton Control
                .Size = New Size(115, 17)
                .Location = New Size(0, EnumCount)
                If EnumCount >= 20 * 19 Then
                    Dim NewEnumCount As Integer = NewEnumCount + 20
                    .Location = New Size(150, NewEnumCount)
                End If
                .Name = Imem
                .Text = .Name
            End With
            AddHandler Ioptions.CheckedChanged, AddressOf OptionOption
            Controls.Add(Ioptions)
        Next
        'Create Two TextBoxes Input and OutPut
        For I As Integer = 0 To 1
            ItextBox = New TextBox
            With ItextBox
                .Multiline = True
                .Size = New Size(250, 80)
                .Location = New Point(300, 100 * (I + 0.25))
                .ScrollBars = ScrollBars.Both
                .Name = "TextBox" & I
                .Text = String.Empty
            End With
            AddHandler ItextBox.TextChanged, AddressOf TextBox_TextChanged
            ThisTextBox(I) = ItextBox
            Controls.Add(ItextBox)
        Next
        'Create Two Labels Input and OutPut
        For I As Integer = 0 To 1
            Dim ILabel = New Label
            With ILabel
                .Size = New Size(100, 20)
                .Location = New Point(ThisTextBox(I).Location.X, ThisTextBox(I).Location.Y - 15)
                .Name = "Label" & I
                .Text = LabelText(I)
            End With
            Labels(I) = ILabel
            Controls.Add(ILabel)
        Next
        ' Dim I As List(Of String) = MySqlConn.ShowDBs
    End Sub
    Private Sub OptionOption(ByVal sender As Object, ByVal e As EventArgs)
        Dim Radios As New RadioButton
        Radios = DirectCast(sender, RadioButton)
        ThisTextBox(0).Text = String.Empty
        ThisTextBox(0).ReadOnly = True
        ThisTextBox(1).Text = String.Empty
        Select Case Radios.Text
            Case Description.Asc.ToString
                ThisTextBox(0).Text =
                    ("Returns an Integer value representing the character code corresponding to a character.")
                For Each N As Char In ThisTextBox(0).Text
                    ThisTextBox(1).Text += Asc(N) & " "
                Next
            Case Description.AscW.ToString
                ThisTextBox(0).Text =
                    ("Returns an Integer value representing the character code corresponding to a character.")
                For Each N As Char In ThisTextBox(0).Text
                    ThisTextBox(1).Text += AscW(N) & " "
                Next
            Case Description.Chr.ToString
                Dim Value1 As New List(Of Integer)
                Dim ThisString As String =
                    ("Returns the character associated with the specified character code.")
                For Each N As Char In ThisString
                    ThisTextBox(0).Text += Asc(N) & " "
                    Value1.Add(Asc(N) & " ")
                Next
                For Each Ioi As Integer In Value1
                    ThisTextBox(1).Text += Chr(Ioi)
                Next
                'Or you can reverse Ascii to String using this method.
                'Dim asciis As Byte() = System.Text.Encoding.ASCII.GetBytes(ThisString)
                'For i As Int32 = 0 To asciis.Length - 1
                ' asciis(i) = asciis(i)
                ' Next
                ' Dim result As String = System.Text.Encoding.ASCII.GetString(asciis)
                'ThisTextBox(1).Text += result
            Case Description.ChrW.ToString
                Dim Value1 As New List(Of Integer)
                Dim ThisString As String =
                    ("Returns the character associated with the specified character code.")
                For Each N As Char In ThisString
                    ThisTextBox(0).Text += Asc(N) & " "
                    Value1.Add(Asc(N) & " ")
                Next
                For Each Ioi As Integer In Value1
                    ThisTextBox(1).Text += ChrW(Ioi)
                Next
            Case Description.Filter.ToString
                Dim ThisString(2) As String
                ThisString(0) = ("Returns a zero-based array containing ")
                ThisString(1) = ("subset of a String array based on ")
                ThisString(2) = ("specified filter criteria.")
                ThisTextBox(0).Text = ThisString(0) & ThisString(1) & ThisString(2)
                Dim OutPutSubString() As String =
                    Strings.Filter(ThisString, "subset of a String array based on ", False, CompareMethod.Text)
                'Result is : Returns a zero-based array containing specified filter criteria. [False:Did not include the filter]
                Dim SubString As String = ("Filter : subset of a String array")
                For I As Integer = 0 To OutPutSubString.Count - 1
                    ThisTextBox(1).Text &= OutPutSubString(I)
                Next
            Case Description.Format.ToString
                ThisTextBox(0).Text =
                    ("Returns a string formatted according to instructions contained in a format String expression. For example : " &
                    " 25000.25 , #07/09/2020 5:04:23 PM#" & vbCrLf)
                Dim ThisNumber As Double = 25000.25
                Dim ThisDate As Date = Now
                Dim ThisTime As TimeSpan = Now.TimeOfDay
                Dim Content(2) As String
                Content(0) = Format(ThisNumber, "##,##0.00") & " , "
                Content(1) = Format(ThisDate, "dddd, MMM d yyyy") & " , "
                Content(2) = Format(ThisDate, "hh:mm:ss tt")
                ThisTextBox(1).Text = ThisTextBox(0).Text & Content(0) & Content(1) & Content(2)
            Case Description.FormatCurrency.ToString
                ThisTextBox(0).Text =
                    ("Returns an expression formatted as a currency value using the currency symbol defined in the system control panel. " &
                    "Example of a negative amount : " & vbCrLf)
                Dim testDebt As Double = -4456.43
                Dim testString As String
                ' Returns "($4,456.43)".
                testString = FormatCurrency(testDebt, , , TriState.True, TriState.True)
                ThisTextBox(1).Text = ThisTextBox(0).Text & testString
            Case Description.FormatDateTime.ToString
                ThisTextBox(0).Text =
                    ("Returns a string expression representing a date/time value. Example of Long Date : " & vbCrLf)
                Dim testDate As DateTime = #3/12/1999#
                ' FormatDateTime returns "Friday, March 12, 1999".
                ' The time information is neutral (00:00:00) and therefore suppressed.
                Dim testString As String = FormatDateTime(testDate, DateFormat.LongDate)
                ThisTextBox(1).Text = ThisTextBox(0).Text & testString
            Case Description.FormatNumber.ToString
                ThisTextBox(0).Text =
                    ("Returns an expression formatted as a number. Example : " & vbCrLf)
                Dim testNumber As Integer = 45600
                ' Returns "45,600.00".
                Dim testString As String = FormatNumber(testNumber, 2, , , TriState.True)
                ThisTextBox(1).Text = ThisTextBox(0).Text & testString
            Case Description.FormatPercent.ToString
                ThisTextBox(0).Text =
                    ("Returns an expression formatted as a percentage (that is, multiplied by 100) with a trailing % character. Example : " & vbCrLf)
                Dim testNumber As Single = 0.76
                ' Returns "76.00%".
                Dim testString As String = FormatPercent(testNumber)
                ThisTextBox(1).Text = ThisTextBox(0).Text & testString
            Case Description.InStr.ToString
                ThisTextBox(0).Text =
                    ("Returns an integer specifying the start position of the first occurrence of one string within another. " &
                    "Example, starting 1st letter, the search of first occurance of letter 'p' result is : ")
                ' Search for "P".
                Dim searchChar As String = "P"
                Dim testPos As Integer
                ' A textual comparison starting at position 1
                testPos = InStr(1, ThisTextBox(0).Text, searchChar, CompareMethod.Text)
                ThisTextBox(1).Text = ThisTextBox(0).Text & testPos.ToString
            Case Description.InStrRev.ToString
                ThisTextBox(0).Text =
                    ("Returns the position of the first occurrence of one string within another, starting from the right side of the string." &
                    "Example, starting the right side, the search of 'fo' result is : ")
                Dim testNumber As Integer
                ' Returns 32.
                testNumber = InStrRev(ThisTextBox(0).Text, "of")
                ThisTextBox(1).Text = ThisTextBox(0).Text & testNumber.ToString
            Case Description.Join.ToString
                ThisTextBox(0).Text =
                    ("Returns a string created by joining a number of substrings contained in an array.")
                Dim anotherString() As String = {"Example of ", "String.Join ", "using ',' delimeter"}
                Dim OutPutstring As String = Join(anotherString, ",")
                ThisTextBox(1).Text = ThisTextBox(0).Text & OutPutstring
            Case Description.LCase.ToString
                ThisTextBox(0).Text =
                    ("Returns a String or Character converted to Lowercase.")
                ThisTextBox(1).Text = LCase(ThisTextBox(0).Text)
            Case Description.Left.ToString
                ThisTextBox(0).Text =
                    ("Returns a string containing a specified number of characters from the left side of a string. " &
                    "Example returns, 5 chars from Left are : ")
                ThisTextBox(1).Text = ThisTextBox(0).Text & Microsoft.VisualBasic.Left(ThisTextBox(0).Text, 5)
            Case Description.Len.ToString
                ThisTextBox(0).Text =
                    ("Returns an integer that contains the number of characters in a string." &
                    "Example returns the Length is : ")
                ThisTextBox(1).Text = ThisTextBox(0).Text & Len(ThisTextBox(0).Text)
            Case Description.LSet.ToString
                ThisTextBox(0).Text =
                    ("Returns a left-aligned string containing the specified string adjusted to the specified length." &
                     "Example returns, 10 chars from Left are : ")
                ThisTextBox(1).Text = ThisTextBox(0).Text & LSet(ThisTextBox(0).Text, 10)
            Case Description.LTrim.ToString
                ThisTextBox(0).Text =
                    ("   Returns a string containing a copy of a specified string with no leading spaces.")
                ThisTextBox(1).Text = LTrim(ThisTextBox(0).Text)
            Case Description.Mid.ToString
                ThisTextBox(0).Text =
                    ("Returns a string containing a specified number of characters from a string." &
                    "Example, returns String starting location '1' for '10' chars long.")
                ThisTextBox(1).Text = ThisTextBox(0).Text & Mid(ThisTextBox(0).Text, 1, 10)
            Case Description.Replace.ToString
                ThisTextBox(0).Text =
                    ("Returns a string in which a specified substring has been replaced with another substring a specified number of times." &
                    "Example, replaces 'R' with space ' '")
                ThisTextBox(1).Text = Replace(ThisTextBox(0).Text, "r", " ", 1, Len(ThisTextBox(0).Text), CompareMethod.Text)
            Case Description.Right.ToString
                ThisTextBox(0).Text =
                    ("Returns a string containing a specified number of characters from the right side of a string. " &
                    "Example returns 6 chars from Right, result is : ")
                ThisTextBox(1).Text = Microsoft.VisualBasic.Right(ThisTextBox(0).Text, 6)
            Case Description.RSet.ToString
                ThisTextBox(0).Text =
                    ("Returns a right-aligned string containing the specified string adjusted to the specified length.")
                ThisTextBox(1).Text = RSet(ThisTextBox(0).Text, 10)
            Case Description.RTrim.ToString
                ThisTextBox(0).Text =
                    ("Returns a string containing a copy of a specified string with no trailing spaces.    ")
                ThisTextBox(1).Text = RTrim(ThisTextBox(0).Text)
            Case Description.Space.ToString
                ThisTextBox(0).Text =
                    ("Returns a string consisting of the specified number of spaces. Example starts with '5' spaces")
                ThisTextBox(1).Text = Space(5) & (ThisTextBox(0).Text)
            Case Description.Split.ToString
                ThisTextBox(0).Text =
                    ("Returns a zero-based, one-dimensional array containing a specified number of substrings. " &
                    "Example returns number of splits of this String, result is : ")
                Dim ThisString() As String = Split(ThisTextBox(0).Text, ",")
                Dim Nlen As Integer = ThisString.Length
                ThisTextBox(1).Text = ThisTextBox(0).Text & Nlen.ToString
            Case Description.StrComp.ToString
                ThisTextBox(0).Text =
                    ("Returns -1, 0, or 1, based on the result of a string comparison.")
                Dim ThisString As String = ("Returns -1, 0, or 1, based on the result of a String Comparison.")
                ThisTextBox(1).Text = ("The result of comparing when changed Upper-Case and Lower Case of the same String using TextCompare is : ") &
                    StrComp(ThisTextBox(0).Text, ThisString, CompareMethod.Text)
            Case Description.StrConv.ToString
                ThisTextBox(0).Text =
                    ("Returns a String Converted as Specified. Example, result is lower-case of this String.")
                ThisTextBox(1).Text = StrConv(ThisTextBox(0).Text, VbStrConv.Lowercase)
            Case Description.StrDup.ToString
                ThisTextBox(0).Text =
                    ("Returns a string or object consisting of the specified character repeated the specified number of times.")
                ThisTextBox(1).Text = StrDup(6, ThisTextBox(0).Text)
            Case Description.StrReverse.ToString
                ThisTextBox(0).Text =
                    ("Returns a string in which the character order of a specified string is reversed.")
                ThisTextBox(1).Text = StrReverse(ThisTextBox(0).Text)
            Case Description.Trim.ToString
                ThisTextBox(0).Text =
                    ("   Returns a string containing a copy of a specified string with no leading or trailing spaces.  ")
                ThisTextBox(1).Text = Trim(ThisTextBox(0).Text)
            Case Description.UCase.ToString
                ThisTextBox(0).Text =
                    ("Returns a string or character containing the specified string converted to uppercase.")
                ThisTextBox(1).Text = UCase(ThisTextBox(0).Text)
        End Select
    End Sub
    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim box As New TextBox
        box = DirectCast(sender, TextBox)
    End Sub
    Private Sub MainForm_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = ChrW(Keys.Escape) Then Close()
    End Sub
End Class