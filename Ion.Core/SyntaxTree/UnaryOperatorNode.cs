using System;

namespace Ion.Core.SyntaxTree;

public class UnaryOperatorNode : AbstractSyntaxTree
{
    internal Token _operator;
    internal object _expression;
    
    public UnaryOperatorNode(Token _operator, object _expression)
    {
        this._operator = _operator;
        this._expression = _expression;

        token = _operator;
    }
}
