using Assel.University.Console;

Console.WriteLine("Hello, HttpClient");
var client = new HttpCustomClient();

var result = await client.GetData();

if (result.IsFailure)
{
    Console.WriteLine("Data receiving failed with error: {0}", result.Error);
}

if (result.IsSuccess)
{
    Console.WriteLine("Data receiving success: ");
    Console.WriteLine("Recieved {0} universities", result.Value.Count());

    foreach (var university in result.Value)
    {
        Console.WriteLine();
        Console.WriteLine("UniversityName: {0}", university.Name);

        if (university.WebPages != null)
        {
            Console.WriteLine("WebPages: {0}", string.Join(",", university.WebPages));
        }
    }
}
