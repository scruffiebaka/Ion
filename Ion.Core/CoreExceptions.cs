using System;

namespace Ion.Core;

[Serializable]
public class ParserException : Exception
{
    public ParserException() : base() { }
    public ParserException(string message) : base(message) { }
    public ParserException(string message, Exception inner) : base(message, inner) { }
}

public class InterpreterException : Exception
{
    public InterpreterException() : base() { }
    public InterpreterException(string message) : base(message) { }
    public InterpreterException(string message, Exception inner) : base(message, inner) { }
}

public class LexerException : Exception
{
    public LexerException() : base() { }
    public LexerException(string message) : base(message) { }
    public LexerException(string message, Exception inner) : base(message, inner) { }
}
