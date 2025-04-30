using System;

namespace Ion.Core.SyntaxTree;

public class VariableNode : AbstractSyntaxTree
{
    public VariableNode(Token token)
    {
        this.token = token;
    }
}
