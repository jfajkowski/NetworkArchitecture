﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NetworkArchitecture.GraphAlgorithms
{
    static class Test
    {
        static private Graph graph;
        static private long averageTimeDijkstra;
        static private long averageTimeFloyd;
        static private long testTime;

        static private void initialize(string path)
        {
            graph = new Graph();
            using (StreamReader streamReader = new StreamReader(path))
            {
                List<string> textFile = new List<string>();

                while (streamReader.EndOfStream == false)
                {
                    string line = streamReader.ReadLine();

                    if (!line.Contains("#"))
                    {
                        textFile.Add(line);
                    }
                }
                graph.load(textFile);
            }
        }

        static private void findShortestPaths(int numberOfTests)
        {
            Stopwatch testTimeStopwatch = new Stopwatch();
            Stopwatch algorithmStopwatch = new Stopwatch();

            testTimeStopwatch.Start();
            for (int i = 0; i < numberOfTests; i++)
            {
                Console.WriteLine("Algorytm Dijkstry:");
                algorithmStopwatch.Restart();
                graph.randomizeEdgesWeights();
                printPaths(Dijkstra.runAlgorithm(graph));
                algorithmStopwatch.Stop();
                averageTimeDijkstra += algorithmStopwatch.ElapsedTicks;


                Console.WriteLine("Algorytm Dijkstry od:");
                algorithmStopwatch.Restart();
                graph.randomizeEdgesWeights();
                printPaths(Dijkstra.runAlgorithm(graph, graph.Vertices[4]));
                algorithmStopwatch.Stop();
                averageTimeDijkstra += algorithmStopwatch.ElapsedTicks;

                Console.WriteLine("Algorytm Dijkstry od do:");
                algorithmStopwatch.Restart();
                graph.randomizeEdgesWeights(); 
                printPaths(Dijkstra.runAlgorithm(graph, graph.Vertices[4], graph.Vertices[3]));
                algorithmStopwatch.Stop();
                averageTimeDijkstra += algorithmStopwatch.ElapsedTicks;


                Console.WriteLine("Algorytm Floyda:");
                algorithmStopwatch.Restart();
                printPaths(Floyd.runAlgorithm(graph));
                algorithmStopwatch.Stop();
                averageTimeFloyd += algorithmStopwatch.ElapsedTicks;

                Console.WriteLine("Algorytm Floyda od:");
                algorithmStopwatch.Restart();
                printPaths(Floyd.runAlgorithm(graph, graph.Vertices[4]));
                algorithmStopwatch.Stop();
                averageTimeFloyd += algorithmStopwatch.ElapsedTicks;

                Console.WriteLine("Algorytm Floyda od do:");
                algorithmStopwatch.Restart();
                printPaths(Floyd.runAlgorithm(graph, graph.Vertices[4],graph.Vertices[3]));
                algorithmStopwatch.Stop();
                averageTimeFloyd += algorithmStopwatch.ElapsedTicks;
            }
            averageTimeDijkstra /= numberOfTests;
            averageTimeFloyd /= numberOfTests;
            testTimeStopwatch.Stop();
            testTime = testTimeStopwatch.ElapsedTicks;
        }
        static private void printPaths(Path[] paths)
        {
            foreach (Path p in paths)
            {
                if(p != null)
                {
                    foreach (Vertex v in p.Vertices)
                    {
                        Console.Write(v.Id + " ");
                    }
                    Console.WriteLine();
                    //if (p.MinWeight == double.MaxValue)
                    //{
                    //    Console.WriteLine(" min: infinity" + " sum: " + p.SumWeight);
                    //}
                    //else
                    //{
                    //    Console.WriteLine(" min: " + p.MinWeight + " sum: " + p.SumWeight);
                    //}
                }
            }
        }

        static private void printPaths(Path[,] paths)
        {
            foreach (Path p in paths)
            {
                if (p != null)
                {
                    foreach (Vertex v in p.Vertices)
                    {
                        Console.Write(v.Id + " ");
                    }
                    Console.WriteLine();
                    //if (p.MinWeight == double.MaxValue)
                    //{
                    //    Console.WriteLine(" min: infinity" + " sum: " + p.SumWeight);
                    //}
                    //else
                    //{
                    //    Console.WriteLine(" min: " + p.MinWeight + " sum: " + p.SumWeight);
                    //}
                }
            }
        }

        static private void printResults()
        {
            Console.WriteLine("Średni czas dla algorytmu Dijkstry: " + new TimeSpan(averageTimeDijkstra));
            Console.WriteLine("Średni czas dla algorytmu Floyda: " + new TimeSpan(averageTimeFloyd));
            Console.WriteLine("Całkowity czas trwania testu: " + new TimeSpan(testTime));
        }

        static public void run(string path, int numberOfTests)
        {
            initialize(path);
            findShortestPaths(numberOfTests);
            printResults();
        }

    }
}
