using System;
using System.Collections.Generic;
using System.Linq;


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
                    vertices[i,x] = new Vertex(c, source[c]- '0');
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
                    for (int k = boxMatrix[0]; k < boxMatrix[1]; k++)
                    {
                        for (int l = boxMatrix[2]; l < boxMatrix[3]; l++)
                        {
                            if(conIndex == 22 && j == 1 && i == 0)
                                Console.WriteLine("22");
                            if (!vertices[k, l].Equals(vertices[i, j]))
                            {
                                vertices[i, j].Connections[conIndex] = vertices[k, l];
                                Console.WriteLine(i+","+j);
                                conIndex++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("done");
        }

        public int[] Box(int i, int j, Vertex[,] mat)
        {
             //debug. not adding 24th connection. also remove duplicates using linq later
            switch (i)
            {
                case >= 0 and < 3:
                    switch (j)
                    {
                        case >= 0 and < 3:
                            return new []{0,3,0,3};
                        case > 2 and < 6:
                            return new []{0,3,3,6};
                        case > 5 and < 9:
                            return new []{0,3,6,9};
                    }
                    break;
                case > 2 and < 6:
                    switch (j)
                    {
                        case >= 0 and < 3:
                            return new []{3,6,0,3};
                        case > 2 and < 6:
                            return new []{3,6,3,6};
                        case > 5 and < 9:
                            return new []{3,6,6,9};
                    }
                    break;
                case > 5 and < 9:
                    switch (j)
                    {
                        case >= 0 and < 3:
                            return new []{6,9,0,3};
                        case > 2 and < 6:
                            return new []{6,9,3,6};
                        case > 5 and < 9:
                            return new []{6,9,6,9};
                    }
                    break;
            }
        
            throw new Exception("you're an idiot");
        }
    }
}