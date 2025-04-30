namespace Ion.Core;

public enum TokenType
{
    INTEGER,
    PLUS,
    MINUS,
    MULTIPLY,
    DIVIDE,
    LPARENTHESIS,
    RPARENTHESIS,
    LCURLYBRACKET,
    RCURLYBRACKET,
    ASSIGN,
    NEXT,
    ID,
    IGNORE,
    EOF
}

public class Token
{
    internal TokenType type;
    internal object value;

    public Token(TokenType type, object value)
    {
        this.type = type;
        this.value = value;
    }
}
