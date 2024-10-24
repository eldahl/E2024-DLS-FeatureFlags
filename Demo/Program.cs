// See https://aka.ms/new-console-template for more information

using FeatureHubSDK;

FeatureLogging.DebugLogger += (sender, s) => Console.WriteLine("DEBUG: " + s); 
FeatureLogging.TraceLogger += (sender, s) => Console.WriteLine("TRACE: " + s); 
FeatureLogging.InfoLogger += (sender, s) => Console.WriteLine("INFO: " + s); 
FeatureLogging.ErrorLogger += (sender, s) => Console.WriteLine("ERROR: " + s);

var config = new EdgeFeatureHubConfig("http://featurehub:8085", "4e1c1840-95c7-478e-b6b1-d1d85027c9f5/rn00pmqV7TgqPy7EduiOABwPnKlwOXRe0gJwJ4KD");
var fh = await config.NewContext().Build();
bool greetTimeOfDay = fh["GreetTimeOfDay"].IsEnabled;

await Task.Run(() =>
{
    if (greetTimeOfDay)
    {
        DateTime now = DateTime.Now;
        if (now is { Hour: >= 6 and <= 11, Minute: >= 0 and <= 59, Second: >= 0 and <= 59 })
            Console.WriteLine("Good Morning, World!");
        else if (now is { Hour: >= 12 and <= 17, Minute: >= 00 and <= 59, Second: >= 0 and <= 59 })
            Console.WriteLine("Good Day, World!");
        else if (now is { Minute: >= 0 and <= 59, Second: >= 0 and <= 59, Hour: >= 18 and <= 23 })
            Console.WriteLine("Good Evening, World!");
        else
            Console.WriteLine("Good Night, World!");
        return;
    }
    Console.WriteLine("Hello World");
});

