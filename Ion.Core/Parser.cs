using System;
using System.Data;
using Ion.Core.SyntaxTree;

namespace Ion.Core;

public class Parser
{
    Lexer lexer;
    Token current_token;

    public Parser(Lexer lexer)
    {
        this.lexer = lexer;
        current_token = lexer.GetNextToken();
    }

    private void Eat(TokenType token_type)
    {
        if (current_token != null)
        {
            if (current_token.type == token_type)
            {
                current_token = lexer.GetNextToken();
            }
            else
            {
                throw new InterpreterException($"Token type mismatch, excepted {token_type}, got {current_token.type}.");
            }
        }
        else
        {
            throw new InterpreterException("Null token");
        }
    }

    private object Compound_Statement()
    {

        Eat(TokenType.LCURLYBRACKET);
        object[] nodes = Statement_List();
        Eat(TokenType.RCURLYBRACKET);

        CompoundNode root = new CompoundNode();
        foreach (object node in nodes)
        {
            root.children_nodes.Append(node);
        }

        return root;
    }

    private object[] Statement_List()
    {
        object node = Statement();
        object[] results = { node };

        while (current_token.type == TokenType.NEXT)
        {
            Eat(TokenType.NEXT);
            results.Append(Statement());
        }

        if (current_token.type == TokenType.ID)
        {
            throw new ParserException();
        }

        return results;
    }

    private object Statement()
    {
        object node;
        if (current_token.type == TokenType.LCURLYBRACKET)
        {
            node = Compound_Statement();
        }
        else if (current_token.type == TokenType.ID)
        {
            node = Assign_Statement();
        }
        else
        {
            node = Ignore();
        }
        return node;
    }

    private object Assign_Statement()
    {
        Token token = current_token;
        Eat(TokenType.ASSIGN);
        object node = new AssignNode(Variable(), token, Expression());
        return node;
    }

    private object Variable()
    {
        object node = new VariableNode(current_token);
        Eat(TokenType.ID);
        return node;
    }

    private object Ignore()
    {
        object node = new IgnoreNode();
        return node;
    }

    private object Term()
    {
        object node = Factor();

        while (current_token.type is TokenType.MULTIPLY or TokenType.DIVIDE)
        {
            Token token = current_token;
            if (current_token.type is TokenType.MULTIPLY)
            {
                Eat(TokenType.MULTIPLY);
            }
            else if (current_token.type is TokenType.DIVIDE)
            {
                Eat(TokenType.DIVIDE);
            }
            node = new BinaryOperatorNode(node, token, Factor());
        }

        return node;
    }

    private object Factor()
    {
        Token token = current_token;
        if (current_token.type == TokenType.PLUS)
        {
            Eat(TokenType.PLUS);
            object node = new UnaryOperatorNode(token, Factor());
            return node;
        }
        else if (current_token.type == TokenType.MINUS)
        {
            Eat(TokenType.MINUS);
            object node = new UnaryOperatorNode(token, Factor());
            return node;
        }
        else if (current_token.type == TokenType.INTEGER)
        {
            Eat(TokenType.INTEGER);
            return new NumberNode(token);
        }
        else if (current_token.value.ToString() == "(")
        {
            Eat(TokenType.LPARENTHESIS);
            object node = Expression();
            Eat(TokenType.RPARENTHESIS);
            return node;
        }
        else
        {
            object node = Variable();
            return node;
        }
        throw new ParserException();
    }

    private object Expression()
    {
        object node = Term();

        while (current_token.type is TokenType.PLUS or TokenType.MINUS)
        {
            Token token = current_token;
            if (current_token.type is TokenType.PLUS)
            {
                Eat(TokenType.PLUS);
            }
            else if (current_token.type is TokenType.MINUS)
            {
                Eat(TokenType.MINUS);
            }
            node = new BinaryOperatorNode(node, token, Term());
        }

        return node;
    }

    public object Parse() { return Statement_List(); }
}
