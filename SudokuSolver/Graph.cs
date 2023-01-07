using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;




namespace SudokuSolver
{
    public class Graph
    {
        
        private readonly int[,] _vertices;
        private readonly string _source;
        private int[] Colors;
        private readonly int[] colorsOriginal;
        private readonly int[][] connections;
        public Graph(string source)
        {
            _source = source;
            var sqrt = Math.Sqrt(source.Length);
            //checks if input is correct
            if (sqrt % 1 != 0)
            {
                if (Math.Sqrt(sqrt) % 1 != 0)
                {
                    Console.WriteLine("input sudoku doesn't have a perfect square side length");
                }
                Console.WriteLine("input doesn't represent an NxN grid");
                return;
            }

            int length = (int) sqrt;
            int connectionsLength = (int) (3 * (length - 1) - 2 * (Math.Sqrt(length)-1));
            connections = new int[source.Length][];
            for (int i = 0; i < connections.Length; i++)
            {
                connections[i] = new int[connectionsLength];
            }
            _vertices = new int[length, length];
            Colors = new int[source.Length];
            colorsOriginal = new int[source.Length];
            Initialize();
        }

        private void Initialize()
        {
            int length = (int) Math.Sqrt(_source.Length);
            int c = 0;
            //convert input to an NxN matrix of vertices
            for (int i = 0; i < _source.Length; i++)
            {
                Colors[i] = _source[c] - '0';
                colorsOriginal[i] = _source[c++] - '0';
            }

            //add connections to each vertex
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    int currentVertex = length * i + j;
                    int conIndex = 0;

                    //add vertices in the same row and column as the current vertex
                    for (int k = 0; k < length; k++)
                    {
                        if (k != i)
                        {
                            connections[currentVertex][conIndex++] = k * length + j;
                        }

                        if (k != j)
                        {
                            connections[currentVertex][conIndex++] = i * length + k;
                        }
                    }

                    //add vertices in the same nxn box as the current vertex
                    var boxLength = (int) Math.Sqrt(length);
                    
                    int startRow = i - i % boxLength;
                    int startCol = j - j % boxLength;
                    int endRow = startRow + boxLength;
                    int endCol = startCol + boxLength;

                    for (int k = startRow; k < endRow; k++)
                    {
                        for (int l = startCol; l < endCol; l++)
                        {
                            if (k == i || l == j) continue;
                            connections[currentVertex][conIndex++] = k * length + l;
                        }
                    }
                }
            }
        }
        
        private string ConvertToString()
        {
            string sudoku = "";
            int x = 0;
            foreach (var vertex in Colors)
            {
                sudoku += vertex == 0 ? System.Convert.ToChar(Colors[x++] + 48) : System.Convert.ToChar(vertex + 48);
            }

            return sudoku;
        }
        public void ColorGraph()
        {
            int numColors = (int) Math.Sqrt(_source.Length);
            //check if the coloring was successful
            Console.WriteLine(ColorGraph(Colors, numColors, 0) ? "success" : "fail");
            Program.PrintSudoku(ConvertToString());
        }
        
        private bool ColorGraph(int[] colors, int numColors, int vertex)
        {
            //all vertices have been colored
            if (vertex == colors.Length)
            {
                Colors = colors;
                return true;
            }

            if (colors[vertex] > 0 && colors[vertex] == colorsOriginal[vertex])
                return ColorGraph(colors, numColors, vertex + 1);
            
            //try coloring the vertex with the first available color
            for (int color = 1; color <= numColors; color++)
            {
                if (!CanColor(colors,vertex, color)) continue;
                //color the vertex
                colors[vertex] = color;
        
                //try coloring the next vertex
                if (ColorGraph(colors, numColors, vertex + 1))
                {
                    return true;
                }
        
                //if the coloring failed, backtrack to the previous vertex and reset it's color
               colors[vertex] = 0;
            }
            //no valid color was found for the current vertex
            return false;
        }
        
        private bool CanColor(int[] colors, int v, int color)
        {
            //check if any connected vertex has a matching color
            for (int i = 0; i < connections[v].Length; i++)
            {
                
                if (colors[connections[v][i]] == color)
                    return false;
            }

            return true;
        }
    }
}