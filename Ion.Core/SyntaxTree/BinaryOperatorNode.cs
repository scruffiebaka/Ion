using System;

namespace Ion.Core;

public class BinaryOperatorNode : AbstractSyntaxTree
{
    internal object left_token;
    internal Token _operator;
    internal object right_token;
    
    public BinaryOperatorNode(object left_token, Token _operator, object right_token)
    {
        this.left_token = left_token;
        this._operator = _operator;
        this.right_token = right_token;

        token = _operator;
    }
}
