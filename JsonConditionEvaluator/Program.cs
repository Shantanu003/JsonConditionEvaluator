using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using JsonConditionEvaluator;

while (true)
{
    Console.WriteLine("Enter the input in the format: 'condition' 'jsonObject' or type 'exit' to quit:");
    string input = Console.ReadLine();

    if (input.ToLower() == "exit")
    {
        Console.WriteLine("Exiting the program.");
        break;
    }

    int firstQuoteIndex = input.IndexOf('\'');
    int secondQuoteIndex = input.IndexOf('\'', firstQuoteIndex + 1);

    if (firstQuoteIndex == -1 || secondQuoteIndex == -1)
    {
        Console.WriteLine("Invalid input format. Please enter in the format: 'condition' 'jsonObject'.");
        continue;
    }

    string condition = input.Substring(firstQuoteIndex + 1, secondQuoteIndex - firstQuoteIndex - 1);

    string jsonObject = input.Substring(secondQuoteIndex + 1).Trim();

    if (jsonObject.StartsWith("'") && jsonObject.EndsWith("'"))
    {
        jsonObject = jsonObject.Substring(1, jsonObject.Length - 2);
    }

    Dictionary<string, object> jsonData = null;

    try
    {
        jsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonObject);
    }
    catch (JsonReaderException)
    {
        Console.WriteLine("Invalid JSON format. Please re-enter valid JSON.");
        continue;
    }

    ConditionEvaluator evaluator = new ConditionEvaluator();

    bool result = evaluator.EvaluateCondition(condition, jsonData);

    Console.WriteLine("Evaluation Result: " + result);
    Console.WriteLine();
}
