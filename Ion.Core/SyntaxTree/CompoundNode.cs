using System;

namespace Ion.Core;

public class CompoundNode : AbstractSyntaxTree
{
    public AbstractSyntaxTree[] children_nodes;

    public CompoundNode()
    {
        children_nodes = [];
    }
}
