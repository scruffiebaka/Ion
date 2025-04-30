using System;

namespace Ion.Core;

public class NumberNode : AbstractSyntaxTree
{
    public NumberNode(Token token)
    {
        this.token = token;
    }
}
