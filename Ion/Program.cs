using Ion.Core;

while (true)
{
    string? text = null;

    if (args.Length < 1)
    {

        try
        {
            Console.Write(">> ");
            text = Console.ReadLine();
        }
        catch (EndOfStreamException e)
        {
            Console.WriteLine(e);
        }
        if (text == null || text == "")
        {
            continue;
        }
    }
    else
    {
        string path = args[0];
        if (!File.Exists(path))
        {
            Console.WriteLine("File not found. Terminating.");
            Environment.Exit(1);
        }
        text = File.ReadAllText(path);
    }


    Lexer lexer = new Lexer(text);
    Parser parser = new Parser(lexer);
    Interpreter interpreter = new Interpreter(parser);
    object result = interpreter.Interpret();
    Console.WriteLine(result.ToString());
}