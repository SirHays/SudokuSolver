using System;
using System.Diagnostics;

namespace SudokuSolver
{
    class Program
    {
        
        public static void PrintSudoku(string sudoku)
        {
            //add checks
            int length = (int) Math.Sqrt(sudoku.Length);
            int boxLength = (int) Math.Sqrt(length);
            Console.Write(" | ");
            InsertLine(0,sudoku,false);
            Console.Write(" | ");
            for (int i = 0; i < sudoku.Length; i++)
            {
                Console.Write(' ');
                Console.Write(sudoku[i] != '0' ? sudoku[i] : ' ');
                
                
                if((i+1) % boxLength == 0)
                    Console.Write(" | ");
                if ((i + 1) % length == 0)
                {
                    Console.WriteLine();
                    Console.Write(" | ");
                }

                if((i+1) % (boxLength * length) == 0)
                    InsertLine(i ,sudoku);
            }
        }

        private static void InsertLine(int j, string s, bool f = true)
        {
            //add checks
            double length = Math.Sqrt(s.Length);
            for (int i = 0; i < length; i++)
            {
                Console.Write(" - ");
            }
            Console.Write(" | ");
            Console.WriteLine();
            //dont add another line at the beginning.
             if(j != s.Length -1 && f)
             {
                 Console.Write(" | ");
             }
                
                
        }
        
        static void Main(string[] args)
        {
            string sudoku = "530070000600195000098000060800060003400803001700020006060000280000419005000080079";
            string sudoku2 = "090060100030201004200080000500103900000006000009000008300509800040000070000020000";
            string sudoku25x25 = "020003>0800000000=4H07100" +
                                 "0:A0006B00F@0<00001000=C0" +
                                 "";
            string s =
                "020003<0800000000=4H07100"+
                "0:A0006B00F@0<00001000=C0"+
                "0?H=700040:003<0B0000F260"+
                "001E00?0F00C=00080000@BD0"+
                "0500D7IC000EAB2:<F9?;0000"+
                ";000F80H715000=@AIG24060C"+
                "@9<0A0CF0000BE00D6=0700G;"+
                "0060E9@0300FDC0000?8I0000"+
                "00G50200;A8000@<900E03:00"+
                "00000600<091I030;00700E00"+
                "00900G05A4@0;0FB20E=00700"+
                "46005002000BEH00C30<G00A0"+
                "000<;0730HAD?=C1058069000"+
                "0F00<C06@00897000H003001B"+
                "00E00I=0D8<0<0:9@?0600400"+
                "00I00H00B0403:50100<00000"+
                "00530A00G7=000BCE00F0;<00"+
                "0000B:80000IG20050@;90300"+
                "AD0020F@6007<0000930B0GHI"+
                "6040@1;<I3C000EAG80B2000<"+
                "00004<H;CGEA@8000129=0050"+
                "01<G00009000C500H0<008A00"+
                "0@;80000106400G0?000><9:0"+
                "0E3000A0000?0ID004:000@;0"+
                "00D20@580000000060CI00030";
                    
            PrintSudoku(s);
            Graph g = new Graph(s);
            var timer = new Stopwatch();
            timer.Start();
            g.ColorGraph(g);
            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);
        }
    }
}