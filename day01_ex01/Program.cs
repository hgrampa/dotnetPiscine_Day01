using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using day01_ex01;

var process = true;
var tasks = new List<Task>();

while (process)
{
    var input = GetInput().ToLower();
    HandleInput(input, ref process);
    
}

void HandleInput(string input, ref bool process)
{
    switch (input)
    {
        case "add":
        case "a":
            AddTask();
            break;
        case "list":
        case "l":
            ShowTaskList();
            break;
        case "done":
        case "d":
            CompleteTask();
            break;
        case "wontdo":
        case "wd":
            DiscardTask();
            break;
        case "quit":
        case "q":
            process = false;
            break;
        default:
            PrintError("bad input");
            PrintUsage();
            break;
    }
}

#region Commands

// TODO поменять проверки на длину строк на проверки на пустоту (везде)
void AddTask()
{
    var title = GetField("title");
    while (title.Length == 0)
    {
        PrintError("Title cannot be empty");
        title = GetField("title");
    }
    
    var summary = GetField("summary (optional)");
    
    var sDueDate = GetField("due date (optional)");
    DateTime? dueDate = null;
    if (sDueDate.Length > 0 && DateTime.TryParse(sDueDate, out var date))
        dueDate = date;
    else
        dueDate = null;
    
    var type = ParseType(GetField("task type", "[w/work, s/study, p/personal]"));
    while (type == TaskType.Undefined)
    {
        PrintError("Invalid type");
        type = ParseType(GetField("task type", "[w/work, s/study, p/personal]"));
    }

    var priority = ParsePriority(GetField("priority (optional)", "[l/low, n/normal, h/high]"));
    
    tasks.Add(new Task(title, summary, type, dueDate, priority));
    Console.WriteLine($"Task \"{title}\" has been added");
}

void CompleteTask()
{
    var task = FindTask(GetField("title"));
    if (task == null)
    {
        PrintError("No task with this title was found");
        return;
    }
    task.Complete();
    Console.WriteLine($"Task \"{task.Title}\" has been completed");
}

void DiscardTask()
{
    var task = FindTask(GetField("title"));
    if (task == null)
    {
        PrintError("No task with this title was found");
        return;
    }
    task.Discard();
    Console.WriteLine($"Task \"{task.Title}\" has been discarded");
}

void ShowTaskList()
{
    if (tasks.Count == 0)
        Console.WriteLine("Task list is empty");
    else
    {
        foreach (var task in tasks)
        {
            Console.WriteLine($"{task}\n");
        }
    }
}

Task FindTask(string title)
{
    foreach (var task in tasks)
    {
        if (task.Title == title)
            return task;
    }

    return null;
}

#endregion

#region Interface

string GetInput()
{
    var color = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write($"> ");
    Console.ForegroundColor = color;
    var input = Console.ReadLine();
    return input;
}

string GetField(string fieldName, string fieldValues = null)
{
    Console.WriteLine($"Enter: {fieldName}{(fieldValues != null ? $" {fieldValues}" : "")}");
    return GetInput();
}

void PrintUsage()
{
    Console.WriteLine("Usage:\tadd,a - add new task\n\tlist,l - show list of task\n\tdone,d - complete task\n\twontdo,wd - discard task\n\tquit,q - quit program");
}

void PrintError(string description)
{
    var color = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error: {description}");
    Console.ForegroundColor = color;
}

#endregion

TaskPriority ParsePriority(string priority)
{
    var result = TaskPriority.Undefined;
    
    priority = priority.ToLower();
    result = priority.Length switch
    {
        0 => TaskPriority.Normal,
        1 => priority switch
        {
            "l" => TaskPriority.Low,
            "n" => TaskPriority.Normal,
            "h" => TaskPriority.High,
            _ => result
        },
        _ => priority switch
        {
            "low" => TaskPriority.Low,
            "normal" => TaskPriority.Normal,
            "high" => TaskPriority.High,
            _ => result
        }
    };

    return result;
}

TaskType ParseType(string type)
{
    var result = TaskType.Undefined;
    
    type = type.ToLower();
    result = type.Length switch
    {
        0 => TaskType.Undefined,
        1 => type switch
        {
            "w" => TaskType.Work,
            "s" => TaskType.Study,
            "p" => TaskType.Personal,
            _ => result
        },
        _ => type switch
        {
            "work" => TaskType.Work,
            "study" => TaskType.Study,
            "personal" => TaskType.Personal,
            _ => result
        }
    };

    return result;
}

