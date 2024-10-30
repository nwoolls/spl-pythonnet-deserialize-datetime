using System.Globalization;
using Python.Runtime;

PythonEngine.Initialize();
try
{
    using (Py.GIL())
    {
        const string sourceCode = """
                                  import datetime
                                  today = datetime.datetime.now()
                                  """;
        var scope = Py.CreateScope();
        scope.Exec(sourceCode);
        var today = scope.Get("today");
        Console.WriteLine($"Type: {today.GetPythonType().Name}");

        try
        {
            Console.WriteLine(today.ToDateTime(CultureInfo.InvariantCulture));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        try
        {
            Console.WriteLine(today.As<DateTime>());
        }
        catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
finally
{
    RuntimeData.FormatterType = typeof(NoopFormatter);
    PythonEngine.Shutdown();
}