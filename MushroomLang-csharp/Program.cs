﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

Bootstrap(/*"../../../../examples/pyramid.umm");//*/args[0]);

void Bootstrap(string filePath)
{
    try
    {
        if (File.Exists(filePath))
            Run(File.ReadAllText(filePath));
    }
    catch (IOException)
    {
        throw new Exception($"{filePath}가 어떻게 파일이름이냐ㅋㅋ");
    }
};

void Run (string content) {
    var proceed = content.TrimEnd().Split("\n").Select(x => x.Trim()).ToArray();
    if (!(proceed[0] == "어떻게" && proceed[^1] == "이 균이냐ㅋㅋ"))
        throw new Exception("어떻게 이 코드가 버섯랭이냐ㅋㅋ");
    var variables = new Dictionary<int, int>();
    if (content.Contains("~")) proceed = content.Split("~");
    var pointer = 1;

    while (proceed[pointer] != "이 균이냐ㅋㅋ")
    {
        var operation = proceed[pointer++];
        var evaludated = ParseOperation(operation);
        if (evaludated != null)
            Environment.Exit(evaludated.Value);
    }

    int? ParseOperation(string operation) 
    {
        if (operation.Contains("포자") && operation.Contains("?"))
        {
            var condition = Numberify(
                SubStringJs(operation, 2, operation.LastIndexOf("?") + 1)
            );
            if (condition == 0)
                return ParseOperation(operation.Substring(operation.LastIndexOf("?") + 1));
            return null;
        }
        if (operation.Contains('버'))
        {
            var a = operation.Split('버');
            var variablePointer = operation.Split('버')[0].Count(x => x == '어');
            var setteeValue = Numberify(operation.Split('버')[1]);
            if (variables.ContainsKey(variablePointer))
                variables[variablePointer] = setteeValue;
            else
                variables.Add(variablePointer, setteeValue);
        }
        if (operation.Contains('균') && operation[^1] == '!')
            Console.Write(Numberify(operation[1..^1]));
        if (operation.Contains('균') && operation[^1] == 'ㅋ')
        {
            if (operation == "균ㅋ") Console.WriteLine();
            Console.Write((char)Numberify(operation[1..^1]));
        }
        if (operation.Contains('섯'))
        {
            pointer = Numberify(operation.Split('섯')[1]) - 1;
        }
        if (operation.IndexOf("브섯!") == 0)
            return Numberify(operation.Split("브섯!")[1]);
        return null;

        int Numberify(string a)
        {
            var numbered = 0;
            if (a.Contains(" "))
                return a.Split(" ").Select(x => Numberify(x)).Aggregate((a, b) => a * b);
            if (a.Contains("균?"))
                a = a.Replace("균?", string.Concat(Enumerable.Repeat(".", int.Parse(Console.ReadLine()))));
            
            var index = a.Count(x => x == '어') - 1;
            if (variables.ContainsKey(index))
                if (a.Contains('어')) numbered += variables[index];
            if (a.Contains('.')) numbered += a.Count(x => x == '.');
            if (a.Contains(',')) numbered -= a.Count(x => x == ',');
            return numbered;
        }

        string SubStringJs(string str, int from, int to)
        {
            if (from == to) return string.Empty;
            if (to < from)
            {
                var t = from;
                from = to;
                to = t;
            }
            return str.Substring(from, to - from);
        }
    }
}
