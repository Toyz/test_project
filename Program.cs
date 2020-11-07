using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using test_project.tasks;
using test_project.attributes;

namespace test_project
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = LoadTasks();
            if(args.Length == 0) {
                Console.Error.WriteLine($"TODO: List all args");
                return;
            }

            var typ = items.GetValueOrDefault(args[0].ToLower());
            if(typ == null) {
                Console.Error.WriteLine($"{args[0]} is not a valid command");
                return;
            }

            var task = (ITask)Activator.CreateInstance(typ);

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            task.Run(args.Skip(1).ToArray());

            stopWatch.Stop();
            Console.WriteLine($"{typ.Name} took {stopWatch.ElapsedMilliseconds}ms to process");
        }

        private static Dictionary<string, Type> LoadTasks() {
            var tasks = Assembly.GetExecutingAssembly().GetTypes().Where(typ => typ.GetInterfaces().Contains(typeof(ITask)) && !typ.IsAbstract && typ.GetCustomAttributes(typeof(TaskAttribute), true).Length > 0).ToList();

            var results = new Dictionary<string, Type>();
            foreach(var task in tasks) {
                if (task.GetCustomAttributes(typeof(TaskAttribute), true).First() is TaskAttribute attr)
                {
                    var name = attr.Name;
                    if(string.IsNullOrEmpty(name)) name = task.Name.ToLower();
                    results.Add(name, task);
                }
            } 

            return results;
        }
    }
}
