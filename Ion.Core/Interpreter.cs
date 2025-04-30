using System;
using Ion.Core.SyntaxTree;

namespace Ion.Core;

public class Interpreter : NodeVisitor
{
    Parser parser;

    public Dictionary<object, object> symbol_table = new Dictionary<object, object>();

    public Interpreter(Parser parser)
    {
        this.parser = parser;
    }

    public object Interpret()
    {
        object tree = parser.Parse();
        return Visit(tree);
    }

    object visit_UnaryOperatorNode(UnaryOperatorNode node)
    {
        if (node._operator.type == TokenType.PLUS)
        {
            return int.Parse(Visit(node._expression).ToString());
        }
        else if (node._operator.type == TokenType.MINUS)
        {
            return -1 * int.Parse(Visit(node._expression).ToString());
        }
        else
        {
            throw new InterpreterException();
        }

    }

    object visit_BinaryOperatorNode(BinaryOperatorNode node)
    {
        if (node._operator.type == TokenType.PLUS)
        {
            return int.Parse(Visit(node.left_token).ToString()) + int.Parse(Visit(node.right_token).ToString());
        }
        else if (node._operator.type == TokenType.MINUS)
        {
            return int.Parse(Visit(node.left_token).ToString()) - int.Parse(Visit(node.right_token).ToString());
        }
        else if (node._operator.type == TokenType.MULTIPLY)
        {
            return int.Parse(Visit(node.left_token).ToString()) * int.Parse(Visit(node.right_token).ToString());
        }
        else if (node._operator.type == TokenType.DIVIDE)
        {
            return int.Parse(Visit(node.left_token).ToString()) / int.Parse(Visit(node.right_token).ToString());
        }
        else
        {
            throw new InterpreterException();
        }
    }

    object visit_NumberNode(NumberNode node)
    {
        return node.token.value;
    }

    void visit_CompoundNode(CompoundNode node)
    {
        foreach (object child in node.children_nodes)
        {
            Visit(child);
        }
    }

    void visit_AssignNode(AssignNode node)
    {
        symbol_table[node.left_token] = Visit(node.right_token);
    }

    object visit_VariableNode(VariableNode node){
        if(symbol_table.TryGetValue(node, out object? val)){
            if(val == null){
                throw new InterpreterException();
            }else{
                return val;
            }
        }
        throw new InterpreterException();
    }

    void visit_IgnoreNode(IgnoreNode node) { }

}
