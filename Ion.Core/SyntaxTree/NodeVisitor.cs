using System;
using System.Reflection;

public abstract class NodeVisitor
{
    public object Visit(object node)
    {
        string method_name = "visit_" + node.GetType().Name;
        MethodInfo method = GetType().GetMethod(method_name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        if (method != null)
        {
            return method.Invoke(this, [node]);
        }
        else
        {
            return GenericVisit(node);
        }
    }

    protected virtual object GenericVisit(object node)
    {
        throw new Exception($"No visit_{node.GetType().Name} method");
    }
}