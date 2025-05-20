using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CodePrufer
{

    internal class Program
    {
        static void Main()
        {
            var graph = new Dictionary<int, List<int>>();  // вершина и смежные 

            // Чтение данных из файла
            string[] lines = File.ReadAllLines("input.txt");

            foreach (string input in lines)
            {
                if (input.Trim() == "bb") break;

                var nodes = input.Split().Select(int.Parse).ToArray();
                int a = nodes[0], b = nodes[1];

                if (!graph.ContainsKey(a)) graph[a] = new List<int>();
                if (!graph.ContainsKey(b)) graph[b] = new List<int>();

                graph[a].Add(b);
                graph[b].Add(a);
            }

            // код прюфера, степени, листья
            var pruferCode = new List<int>();
            var degree = new Dictionary<int, int>();
            var leaves = new SortedSet<int>();

            foreach (var node in graph.Keys)
            {
                degree[node] = graph[node].Count; // ^ = число соседей
                if (degree[node] == 1)
                    leaves.Add(node);
            }

            while (graph.Count > 2)
            {
                int leaf = leaves.Min;
                leaves.Remove(leaf);

                int neighbor = graph[leaf].First();
                pruferCode.Add(neighbor);

                degree[neighbor]--;
                if (degree[neighbor] == 1)
                    leaves.Add(neighbor);

                graph[neighbor].Remove(leaf);
                graph.Remove(leaf);
            }

            Console.WriteLine("Код Прюфера: " + string.Join(" ", pruferCode));
            File.WriteAllText("output.txt", string.Join(" ", pruferCode));
        }
    }
}