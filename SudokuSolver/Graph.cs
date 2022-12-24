using System;
using System.Collections.Generic;
using System.Linq;
using NumSharp;
using NumSharp.Generic;
using NumSharp.Utilities;

namespace SudokuSolver
{
    public class Graph
    {
        public Vertex[,] vertices;
        private string source;
        public Graph(string source)
        {
            this.source = source;
            double sqrt = Math.Sqrt(source.Length);
            //checks if input is correct
            if(sqrt % 1 != 0)
            {
                Console.WriteLine("input doesn't represent an NxN grid");
                return;
            }
            int length = (int)sqrt;
            
            vertices = new Vertex[length, length];
            Initialize();
        }

        private void Initialize()
        {
            
            //converts input to an NxN matrix of vertices
            int length = (int) Math.Sqrt(source.Length);
            int c = 0;
            for (int i = 0; i < length; i++)
            {
                for (int x = 0; x < length; x++)
                {
                    vertices[i,x] = new Vertex(c, source[i]);
                    c++;
                }
            }
            
            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                for (int j = 0; j < vertices.GetLength(1); j++)
                {
                    int conIndex = 0;
                    //adds all vertices in the same column to connections
                    for (int k = 0; k < vertices.GetLength(0); k++)
                    {
                        if(!vertices[k,j].Equals(vertices[i,j]))
                        {
                            //check if this checks out
                            vertices[i, j].Connections[conIndex] = vertices[k, j];
                            conIndex++;
                        }
                    }
                    //adds all vertices in the same row to connections
                    for (int k = 0; k < vertices.GetLength(1); k++)
                    {
                        if (!vertices[j, k].Equals(vertices[i, j]))
                        {
                            vertices[i, j].Connections[conIndex] = vertices[j, k];
                            conIndex++;
                        }
                    }

                    var boxMatrix = Box(i, j, vertices);
                    var width = boxMatrix[0].size;
                    for (int k = 0; k < boxMatrix.size/width; k++)
                    {
                        var row = boxMatrix[k];
                        int l = 0;
                        foreach (var vertex in row)
                        {
                            vertices[k, l] = vertex as Vertex;
                        }
                    }
                }
            }
        }

        public NDArray Box(int i, int j, Vertex[,] mat)
        {
            NDArray a = np.array(mat);
            switch (i)
            {
                case >= 0 and < 3:
                    switch (j)
                    {
                        case >= 0 and < 3:
                            return a[":3,:3"];
                        case > 2 and < 6:
                            return a[":3,4:6"];
                        case > 5 and < 9:
                            return a[":3,7:9"];
                    }
                    break;
                case > 2 and < 6:
                    switch (j)
                    {
                        case >= 0 and < 3:
                            return a["4:6,:3"];
                        case > 2 and < 6:
                            return a["4:6,4:6"];
                        case > 5 and < 9:
                            return a["4:6,7:9"];
                    }
                    break;
                case > 5 and < 9:
                    switch (j)
                    {
                        case >= 0 and < 3:
                            return a["7:9,:3"];
                        case > 2 and < 6:
                            return a["7:9,4:6"];
                        case > 5 and < 9:
                            return a["7:9,7:9"];
                    }
                    break;
            }
        
            throw new Exception("you're an idiot");
        }
    }
}