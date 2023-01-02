using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;




namespace SudokuSolver
{
    public class Graph
    {
        
        private readonly Vertex[,] _vertices;
        private readonly string _source;
        private readonly List<Vertex> _nve;
        private readonly List<Vertex> _graphAsList;
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

            _vertices = new Vertex[length, length];
            _nve = new List<Vertex>();
            _graphAsList = new List<Vertex>();
            Initialize();
        }

        private void Initialize()
        {
            int length = (int) Math.Sqrt(_source.Length);
            int c = 0;
            int connectionsLength = (int) (3 * (length - 1) - 2 * (Math.Sqrt(length)-1));
            //convert input to an NxN matrix of vertices
            for (int i = 0; i < length; i++)
            {
                for (int x = 0; x < length; x++)
                {
                    _vertices[i, x] = new Vertex(_source[c] - '0');
                    _vertices[i, x].Connections = new Vertex[connectionsLength];
                    c++;
                }
            }

            //add connections to each vertex
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Vertex currentVertex = _vertices[i, j];
                    int conIndex = 0;

                    //add vertices in the same row and column as the current vertex
                    for (int k = 0; k < length; k++)
                    {
                        if (k != i)
                        {
                            currentVertex.Connections[conIndex] = _vertices[k, j];
                            conIndex++;
                        }

                        if (k != j)
                        {
                            currentVertex.Connections[conIndex] = _vertices[i, k];
                            conIndex++;
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
                            currentVertex.Connections[conIndex] = _vertices[k, l];
                            conIndex++;
                        }
                    }
                }
            }
            Convert(_vertices);
        }

        private void Convert(Vertex[,] v)
        {
            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    _graphAsList.Add(v[i,j]);
                    if(v[i,j].color == 0)
                        _nve.Add(v[i,j]);
                }
            }
        }
        
        private string ConvertToString()
        {
            string sudoku = "";
            foreach (var vertex in _graphAsList)
            {
                sudoku += vertex.color;
            }

            return sudoku;
        }
        public void ColorGraph(Graph graph)
        {
            int numColors = (int) Math.Sqrt(graph._source.Length);
            //check if the coloring was successful
            Console.WriteLine(ColorGraph(numColors, 0) ? "success" : "fail");
            Program.PrintSudoku(ConvertToString());
        }
        
        private bool ColorGraph(int numColors, int vertex)
        {
            //all vertices have been colored
            if (vertex == _nve.Count)
            {
                return true;
            }
        
            //try coloring the vertex with the first available color
            for (int color = 1; color <= numColors; color++)
            {
                if (!CanColor(_nve[vertex], color)) continue;
                //color the vertex
                _nve[vertex].color = color;
        
                //try coloring the next vertex
                if (ColorGraph(numColors, vertex + 1))
                {
                    return true;
                }
        
                //if the coloring failed, backtrack to the previous vertex and reset it's color
                _nve[vertex].color = 0;
            }
            //no valid color was found for the current vertex
            return false;
        }
        
        private static bool CanColor(Vertex v, int color)
        {
            //check if any connected vertex has a matching color
            foreach (var neighbor in v.Connections)
            {
                if (neighbor.color == color)
                    return false;
            }

            return true;
        }
    }
}