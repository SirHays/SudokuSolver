﻿namespace SudokuSolver
{
    public class Vertex
    {
        private int id;
        private int color;
        public Vertex[] Connections;
        public Vertex(int id, int color)
        {
            this.id = id;
            this.color = color;
            Connections = new Vertex[20];
        }
        
    }
}