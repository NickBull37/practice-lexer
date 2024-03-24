namespace PracticeLexer
{
    /// <summary>Takes source code as input and breaks it down into tokens.</summary>
    public class Lexer
    {
        public static IEnumerable<(Tokens, object? value)> Tokenize(string input)
        {
            var iterator = 0;

            while (iterator < input.Length)
            {
                var c = input[iterator];

                // Handle whitespace
                if (char.IsWhiteSpace(c))
                {
                    iterator++;
                    continue;
                }

                // Handle letters
                if (char.IsLetter(c))
                {
                    var startPosition = iterator;

                    while (iterator < input.Length && char.IsLetterOrDigit(input[iterator]))
                    {
                        iterator++;
                    }

                    var value = input[startPosition..iterator];

                    var token = value switch
                    {
                        "bool" => Tokens.Boolean,
                        "false" => Tokens.False,
                        "if" => Tokens.IfStatement,
                        "int" => Tokens.Integer,
                        "public" => Tokens.PublicModifier,
                        "return" => Tokens.Return,
                        "static" => Tokens.StaticModifier,
                        "string" => Tokens.String,
                        "true" => Tokens.True,
                        "var" => Tokens.Variable,
                        _ => Tokens.Identifier
                    };

                    yield return (token, value);
                    continue;
                }

                // Hanlde digits
                if (char.IsDigit(c))
                {
                    var start = iterator;

                    while (iterator < input.Length && char.IsDigit(input[iterator]))
                    {
                        iterator++;
                    }

                    var value = input[start..iterator];

                    yield return (Tokens.Integer, int.Parse(value));
                    continue;
                }

                // Handle double quotes
                if (c == '\"')
                {
                    var start = iterator + 1; // Skip opening quote
                    iterator++;

                    while (iterator < input.Length && input[iterator] != '\"')
                    {
                        iterator++;
                    }

                    var value = input[start..iterator];

                    yield return (Tokens.String, value);

                    iterator++;

                    continue;
                }

                // Handle single char tokens
                yield return c switch
                {
                    '=' => (Tokens.Equal, '='),
                    '+' => (Tokens.Plus, '+'),
                    ',' => (Tokens.Comma, ','),
                    '.' => (Tokens.Period, '.'),
                    ';' => (Tokens.Semicolon, ';'),
                    '(' => (Tokens.OpenParen, '('),
                    ')' => (Tokens.CloseParen, ')'),
                    '{' => (Tokens.OpenCurly, '{'),
                    '}' => (Tokens.CloseCurly, '}'),
                    '\n' => (Tokens.NewLine, '\n'),
                    '\r' => (Tokens.CarriageReturn, '\r'),
                    _ => (Tokens.Illegal, c),
                };

                iterator++;
            }

            yield return (Tokens.Eof, null);
        }
    }
}
