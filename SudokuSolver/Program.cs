using System;

namespace SudokuSolver
{
    class Program
    {
        
        public static void PrintSudoku(string sudoku)
        {
            Console.Write(" | ");
            InsertLine(0,sudoku,false);
            Console.Write(" | ");
            for (int i = 0; i < sudoku.Length; i++)
            {
                Console.Write(' ');
                Console.Write(sudoku[i] != '0' ? sudoku[i] : ' ');
                
                
                if((i+1) % 3 == 0)
                    Console.Write(" | ");
                if ((i + 1) % 9 == 0)
                {
                    Console.WriteLine();
                    Console.Write(" | ");
                }

                if((i+1) % 27 == 0)
                    InsertLine(i ,sudoku);
            }
        }
        public static void InsertLine(int j, string s, bool f = true)
        {
            for (int i = 0; i < 8; i++)
            {
                Console.Write(" - ");
            }
            Console.Write(" | ");
            Console.WriteLine();
             if(j != s.Length -1 && f)
             {
                 Console.Write(" | ");
             }
                
                
        }
        
        static void Main(string[] args)
        {
            string sudoku = "530070000600195000098000060800060003400803001700020006060000280000419005000080079";
            PrintSudoku(sudoku);
            Graph g = new Graph(sudoku);
            
            g.ColorGraph(g,9);
        }
    }
}