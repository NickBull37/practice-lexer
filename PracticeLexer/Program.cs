using PracticeLexer;

string testInput = @"
var five = 5;
var ten = 10;
public static int AddNumbers(int x, int y)
{
    return x + y;
}
var sum = AddNumbers(five, ten);
";

string testInput2 = @"
var name = ""Danny Ocean"";
public static bool IsNameEmpty(string name)
{
    if (string.IsNullOrEmpty(name)) {
        return true;
    }
    return false;
}
var isNameEmpty = IsNameEmpty(name);
";

var tokens = Lexer.Tokenize(testInput2);

foreach (var token in tokens)
{
    Console.WriteLine(token);
}