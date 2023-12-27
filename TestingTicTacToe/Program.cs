using System;
using System.Collections;
using TicTacToe;

namespace TestingTicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            TicTacToeProgressions ticTacProgressions = new TicTacToeProgressions();

            ArrayList progressions = new ArrayList();

            int index = 0;
            Progression p = ticTacProgressions.GetProgression(index);
            while(p != null)
            {
                progressions.Add(p);
                index++;
                p = ticTacProgressions.GetProgression(index);
            }

            // 362880
            if (Prove40320BlocksAreUnique(progressions) != null)
            {
                Console.WriteLine("Error");
            }
            Console.WriteLine("1");


            int firstMoveBlockSize = 40320;
            for(int i = 0; i < progressions.Count; i+= firstMoveBlockSize)
            {
                if(Prove5040BlocksAreUnique(progressions, i, i + firstMoveBlockSize) != null)
                {
                    Console.WriteLine("Error");
                    break;
                }
            }
            Console.WriteLine("2");

            int secondMoveBlockSize = 5040;
            for (int i = 0; i < progressions.Count; i += secondMoveBlockSize)
            {
                if (Prove720BlocksAreUnique(progressions, i, i + secondMoveBlockSize) != null)
                {
                    Console.WriteLine("Error");
                    break;
                }
            }
            Console.WriteLine("3");

            int thirdMoveBlockSize = 720;
            for (int i = 0; i < progressions.Count; i += thirdMoveBlockSize)
            {
                if (Prove120BlocksAreUnique(progressions, i, i + thirdMoveBlockSize) != null)
                {
                    Console.WriteLine("Error");
                    break;
                }
            }
            Console.WriteLine("4");


            int fourthMoveBlockSize = 120;
            for (int i = 0; i < progressions.Count; i += fourthMoveBlockSize)
            {
                if (Prove24BlocksAreUnique(progressions, i, i + fourthMoveBlockSize) != null)
                {
                    Console.WriteLine("Error");
                    break;
                }
            }
            Console.WriteLine("5");


            int fifthMoveBlockSize = 24; // 6 2 1
            for (int i = 0; i < progressions.Count; i += fifthMoveBlockSize)
            {
                if (Prove6BlocksAreUnique(progressions, i, i + fifthMoveBlockSize) != null)
                {
                    Console.WriteLine("Error");
                    break;
                }
            }
            Console.WriteLine("6");

            int sixthMoveBlockSize = 6; // 6 2 1
            for (int i = 0; i < progressions.Count; i += sixthMoveBlockSize)
            {
                if (Prove2BlocksAreUnique(progressions, i, i + sixthMoveBlockSize) != null)
                {
                    Console.WriteLine("Error");
                    break;
                }
            }
            Console.WriteLine("7");

            int seventhMoveBlockSize = 2; // 6 2 1
            for (int i = 0; i < progressions.Count; i += seventhMoveBlockSize)
            {
                if (Prove1BlocksAreUnique(progressions, i, i + seventhMoveBlockSize) != null)
                {
                    Console.WriteLine("Error");
                    break;
                }
            }
            Console.WriteLine("8");
            Console.ReadLine();
        }
        static int? Prove40320BlocksAreUnique(ArrayList progressions)
        {
            // 40320
            int blockSize = progressions.Count / 9;
            var squares = Enum.GetValues(typeof(Square));
            int startIndex = 0;
            int endIndex = blockSize;
            foreach (Square square in squares)
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (((Progression)progressions[i]).GetMove(MoveNumber.one).Square != square)
                    {
                        return i; // return the index of the 
                    }
                }
                startIndex = endIndex;
                endIndex = startIndex + blockSize;
            }
            return null;// return null

            // 0 - 40319 : Move 1 is to sqaure 0.
            // 40320 - 80639: Move 1 is to square 1.
            // 80640 - 12959: Move 1 is to square 2.
            // Group 1 cannot contain a progression that is the same as group 2.
            // etc..
        }
        static int? Prove5040BlocksAreUnique(ArrayList progressions, int startUnique40320, int endUnique40320)
        {
            // Every block of 5040 second move should be to the same square. But every 5040 block second move should be different to every other 5040 block in this unique 40320.
            int blockSize = progressions.Count / 9 / 8;
            Square occupiedSquare = ((Progression)progressions[startUnique40320]).GetMove(MoveNumber.one).Square;
            var squares = Enum.GetValues(typeof(Square));
            int startIndex = startUnique40320;
            int endIndex = startIndex + blockSize;
            foreach (Square square in squares)
            {
                if(square != occupiedSquare)
                {
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (((Progression)progressions[i]).GetMove(MoveNumber.two).Square != square)
                        {
                            return i;
                        }
                    }
                    startIndex = endIndex;
                    endIndex = startIndex + blockSize;
                }
            }
            return null;
            // The first 5040 block, Move 2 is to A
            // The second 5040 block, Move 2 is to B
            // The third 5040 block, Move 2 is to C
            // etc...
            // In this unique 40320 block, Move 2 is different for each 5040 block.
        }
        static int? Prove720BlocksAreUnique(ArrayList progressions, int startUnique5040, int endUnique5040)
        {
            int blockSize = progressions.Count / 9 / 8 / 7;
            Square[] occupiedSquares = new Square[] { ((Progression)progressions[startUnique5040]).GetMove(MoveNumber.one).Square, ((Progression)progressions[startUnique5040]).GetMove(MoveNumber.two).Square };
            var squares = Enum.GetValues(typeof(Square));
            int startIndex = startUnique5040;
            int endIndex = startIndex + blockSize;
            foreach (Square square in squares)
            {
                bool occupied = false;
                foreach(Square occupiedSquare in occupiedSquares)
                {
                    if(occupiedSquare == square)
                    {
                        occupied = true;
                    }
                }
                if (!occupied)
                {
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (((Progression)progressions[i]).GetMove(MoveNumber.three).Square != square)
                        {
                            return i;
                        }
                    }
                    startIndex = endIndex;
                    endIndex = startIndex + blockSize;
                }
            }
            return null;
            // The first 720 block, Move 3 is to A
            // The second 5040 block, Move 3 is to B
            // The third 5040 block, Move 3 is to C
            // etc...
            // In this unique 5040 block, Move 3 is different for each 720 block.
        }
        static int? Prove120BlocksAreUnique(ArrayList progressions, int startUnique720, int endUnique720)
        {
            int blockSize = progressions.Count / 9 / 8 / 7 / 6;
            Square[] occupiedSquares = new Square[]
            {
                ((Progression)progressions[startUnique720]).GetMove(MoveNumber.one).Square,
                ((Progression)progressions[startUnique720]).GetMove(MoveNumber.two).Square,
                ((Progression)progressions[startUnique720]).GetMove(MoveNumber.three).Square
            };
            var squares = Enum.GetValues(typeof(Square));
            int startIndex = startUnique720;
            int endIndex = startIndex + blockSize;
            foreach (Square square in squares)
            {
                bool occupied = false;
                foreach (Square occupiedSquare in occupiedSquares)
                {
                    if (occupiedSquare == square)
                    {
                        occupied = true;
                    }
                }
                if (!occupied)
                {
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        // Possibly check 

                        if (((Progression)progressions[i]).GetMove(MoveNumber.four).Square != square)
                        {
                            return i;
                        }
                    }
                    startIndex = endIndex;
                    endIndex = startIndex + blockSize;
                }
            }
            return null;
            // The first 120 block, Move 4 is to A
            // The second 120 block, Move 4 is to B
            // The third 120 block, Move 4 is to C
            // etc... 
            // In this unique 720 block, Move 4 is different for each 120 block.
        }
        static int? Prove24BlocksAreUnique(ArrayList progressions, int startUnique120, int endUnique120)
        {
            int blockSize = progressions.Count / 9 / 8 / 7 / 6 / 5;
            Square[] occupiedSquares = new Square[]
            {
                ((Progression)progressions[startUnique120]).GetMove(MoveNumber.one).Square,
                ((Progression)progressions[startUnique120]).GetMove(MoveNumber.two).Square,
                ((Progression)progressions[startUnique120]).GetMove(MoveNumber.three).Square,
                ((Progression)progressions[startUnique120]).GetMove(MoveNumber.four).Square,
            };
            var squares = Enum.GetValues(typeof(Square));
            int startIndex = startUnique120;
            int endIndex = startIndex + blockSize;
            foreach (Square square in squares)
            {
                bool occupied = false;
                foreach (Square occupiedSquare in occupiedSquares)
                {
                    if (occupiedSquare == square)
                    {
                        occupied = true;
                    }
                }
                if (!occupied)
                {
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (((Progression)progressions[i]).GetMove(MoveNumber.five).Square != square)
                        {
                            return i;
                        }
                    }
                    startIndex = endIndex;
                    endIndex = startIndex + blockSize;
                }
            }
            return null;
            // The first 24 block, Move 5 is to A
            // The second 24 block, Move 5 is to B
            // The third 24 block, Move 5 is to C
            // etc... 
            // In this unique 120 block, Move 5 is different for each 24 block.
        }
        static int? Prove6BlocksAreUnique(ArrayList progressions, int startUnique24, int endUnique24)
        {
            int blockSize = progressions.Count / 9 / 8 / 7 / 6 / 5 / 4;
            Square[] occupiedSquares = new Square[]
            {
                ((Progression)progressions[startUnique24]).GetMove(MoveNumber.one).Square,
                ((Progression)progressions[startUnique24]).GetMove(MoveNumber.two).Square,
                ((Progression)progressions[startUnique24]).GetMove(MoveNumber.three).Square,
                ((Progression)progressions[startUnique24]).GetMove(MoveNumber.four).Square,
                ((Progression)progressions[startUnique24]).GetMove(MoveNumber.five).Square
            };
            var squares = Enum.GetValues(typeof(Square));
            int startIndex = startUnique24;
            int endIndex = startIndex + blockSize;
            foreach (Square square in squares)
            {
                bool occupied = false;
                foreach (Square occupiedSquare in occupiedSquares)
                {
                    if (occupiedSquare == square)
                    {
                        occupied = true;
                    }
                }
                if (!occupied)
                {
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (((Progression)progressions[i]).GetMove(MoveNumber.six).Square != square)
                        {
                            return i;
                        }
                    }
                    startIndex = endIndex;
                    endIndex = startIndex + blockSize;
                }
            }
            return null;
            // The first 6 block, Move 6 is to A
            // The second 6 block, Move 6 is to B
            // The third 6 block, Move 6 is to C
            // etc... 
            // In this unique 24 block, Move 6 is different for each 6 block.
        }
        static int? Prove2BlocksAreUnique(ArrayList progressions, int startUnique6, int endUnique6)
        {
            int blockSize = progressions.Count / 9 / 8 / 7 / 6 / 5 / 4 / 3;
            Square[] occupiedSquares = new Square[]
            {
                ((Progression)progressions[startUnique6]).GetMove(MoveNumber.one).Square,
                ((Progression)progressions[startUnique6]).GetMove(MoveNumber.two).Square,
                ((Progression)progressions[startUnique6]).GetMove(MoveNumber.three).Square,
                ((Progression)progressions[startUnique6]).GetMove(MoveNumber.four).Square,
                ((Progression)progressions[startUnique6]).GetMove(MoveNumber.five).Square,
                ((Progression)progressions[startUnique6]).GetMove(MoveNumber.six).Square
            };
            var squares = Enum.GetValues(typeof(Square));
            int startIndex = startUnique6;
            int endIndex = startUnique6 + blockSize;
            foreach (Square square in squares)
            {
                bool occupied = false;
                foreach (Square occupiedSquare in occupiedSquares)
                {
                    if (occupiedSquare == square)
                    {
                        occupied = true;
                    }
                }
                if (!occupied)
                {
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (((Progression)progressions[i]).GetMove(MoveNumber.seven).Square != square)
                        {
                            return i;
                        }
                    }
                    startIndex = endIndex;
                    endIndex = startIndex + blockSize;
                }
            }
            return null;
            // The first 2 block, Move 7 is to A
            // The second 2 block, Move 7 is to B
            // The third 2 block, Move 7 is to C
            // etc... 
            // In this unique 6 block, Move 7 is different for each 2 block.
        }
        static int? Prove1BlocksAreUnique(ArrayList progressions, int startUnique2, int endUnique2)
        {
            int blockSize = progressions.Count / 9 / 8 / 7 / 6 / 5 / 4 / 3 / 2;
            Square[] occupiedSquares = new Square[]
            {
                ((Progression)progressions[startUnique2]).GetMove(MoveNumber.one).Square,
                ((Progression)progressions[startUnique2]).GetMove(MoveNumber.two).Square,
                ((Progression)progressions[startUnique2]).GetMove(MoveNumber.three).Square,
                ((Progression)progressions[startUnique2]).GetMove(MoveNumber.four).Square,
                ((Progression)progressions[startUnique2]).GetMove(MoveNumber.five).Square,
                ((Progression)progressions[startUnique2]).GetMove(MoveNumber.six).Square,
                ((Progression)progressions[startUnique2]).GetMove(MoveNumber.seven).Square
            };
            var squares = Enum.GetValues(typeof(Square));
            int startIndex = startUnique2;
            int endIndex = startUnique2 + blockSize;
            foreach (Square square in squares)
            {
                bool occupied = false;
                foreach (Square occupiedSquare in occupiedSquares)
                {
                    if (occupiedSquare == square)
                    {
                        occupied = true;
                    }
                }
                if (!occupied)
                {
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (((Progression)progressions[i]).GetMove(MoveNumber.eight).Square != square)
                        {
                            return i;
                        }
                    }
                    startIndex = endIndex;
                    endIndex = startIndex + blockSize;
                }
            }
            return null;
            // The first 1 block Move 8 is to A
            // The second 1 block Move 8 is to B
            // etc... 
            // In this unique 2 block, Move 8 is different for each 1 block.
        }
    }
}
