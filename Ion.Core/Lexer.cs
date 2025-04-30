using System;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;

namespace Ion.Core;

public class Lexer
{
    internal string text;
    internal int text_position;
    internal char current_character;
    internal bool end_of_file = false;

    public Lexer(string text)
    {
        this.text = text;
        text_position = 0;
        current_character = text[text_position];
    }

    Dictionary<string, Token> ReservedKeywords = new Dictionary<string, Token>{
        { "IGNORE", new Token(TokenType.IGNORE, "IGNORE") }
    };

    internal Token GetNextToken()
    {
        while (!end_of_file)
        {
            if (char.IsWhiteSpace(current_character) || current_character == '\t')
            {
                while ((char.IsWhiteSpace(current_character) || current_character == '\t') && !end_of_file)
                {
                    Advance();
                }
                continue;
            }

            if (char.IsDigit(current_character))
            {
                return new Token(TokenType.INTEGER, Integer());
            }

            if (current_character == '+')
            {
                Advance();
                return new Token(TokenType.PLUS, '+');
            }

            if (current_character == '-')
            {
                Advance();
                return new Token(TokenType.MINUS, '-');
            }

            if (current_character == '*')
            {
                Advance();
                return new Token(TokenType.MULTIPLY, '*');
            }

            if (current_character == '/')
            {
                Advance();
                return new Token(TokenType.DIVIDE, '/');
            }

            if (current_character == '(')
            {
                Advance();
                return new Token(TokenType.LPARENTHESIS, '(');
            }

            if (current_character == ')')
            {
                Advance();
                return new Token(TokenType.RPARENTHESIS, ')');
            }

            if (char.IsLetterOrDigit(current_character))
            {
                return ID();
            }

            if (current_character == '\n')
            {
                return new Token(TokenType.NEXT, '\n');
            }

            if (current_character == '=')
            {
                return new Token(TokenType.ASSIGN, '=');
            }

            if (current_character == '{')
            {
                Advance();
                return new Token(TokenType.LCURLYBRACKET, '{');
            }

            if (current_character == '}')
            {
                Advance();
                return new Token(TokenType.RCURLYBRACKET, '}');
            }
        }
        return new Token(TokenType.EOF, '\0');

        throw new LexerException("Error parsing");
    }

    private Token ID()
    {
        string result = "";
        while (char.IsLetterOrDigit(current_character) && !end_of_file)
        {
            result += current_character;
            Advance();
        }
        if (ReservedKeywords.TryGetValue(result, out Token? token))
        {
            return token;
        }
        
        return new Token(TokenType.ID, result);
    }

    private void Advance()
    {
        text_position++;
        if (text_position > (text.Length - 1))
        {
            end_of_file = true;
        }
        else
        {
            current_character = text[text_position];
        }
    }

    private string Integer()
    {
        string integer = "";
        while (char.IsDigit(current_character) && !end_of_file)
        {
            integer += current_character;
            Advance();
        }
        return integer;
    }
}
