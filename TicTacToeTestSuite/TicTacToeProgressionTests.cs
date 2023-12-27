using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe;
using System;
using System.Collections;

namespace TicTacToeTestSuite
{
    [TestClass]
    public class TicTacToeProgressionTests
    {
        [TestMethod]
        public void TestProgressionUniqueness()
        {
            /* This test is to show that each of the TicTacToe progressions are unique.
             * 
             * Instead of taking each progression and testing that it is different to every other progression, taking at a minimum of (362,880(362,880 - 1)) / 2 == 65,840,765,760 operations.
             * 
             * BASE INFORMATION
             * 1. Number of ways the fill a TicTacToe board = 9! = 362,880 = number of progressions
             * 2. 9 possible options for the first move
             * 3. Number of progressions with the first move to square 0, sqaure 1, square 2, square 3, etc is 9!/9 = 40,320. (To prove)
             * 4. Foreach of the 40,320 that have a unique first move, there should be 9!/9/8 5040 progressions that move to each unoccupied square. (To prove)
             * 5. Foreach of the 5040 that have a unique first move and second move there should be 9!/9/8/7 720 progressions that move to each unoccupied square. (To prove)
             * 6. The pattern continues...(To prove)
             * 
             * 
             * IT WORKS BY KNOWING THAT THE PROGRESSIONS ARE IN A PARTICULAR ORDER. 
             * 1. Each of the functions called to prove the required conditions returns either null or an integer.
             * 2. If null is returned the block is unique, but if an inter is returned it does not mean that the 
             *    progressions are not unique.
             * 3. The functions must be called in order to prove uniqueness.
             * 4. All the testing function are tested for a failure when the progressions are not in the expected order.
             * 
             * Works, but it could be clearer. I think that the idea here can be used as a basis for refactoring.
             * This is more an example of how to test uniquness in a more efficient way than a minimum 65,840,765,760. Instead it is closer to 362,880 * 9 or 2,903,040 operations.
             */

            TicTacToeProgressions ticTacProgressions = new TicTacToeProgressions();

            ArrayList progressions = new ArrayList();

            int index = 0;
            Progression p = ticTacProgressions.GetProgression(index);
            while (p != null)
            {
                progressions.Add(p);
                index++;
                p = ticTacProgressions.GetProgression(index);
            }

            // !Null doesn't mean there are not unique, but null does mean they are unique. 
            /* Example: Run the test and it will pass if the progressions are in the right order,
            *           then swap progressions[0] with progressions[40320]. The progressions will
            *           still be unique but this test suite will fail becuase the progressions
            *           are not in the right order. Same goes for every function in the 
            *           ProgressionsUniqueness class.
            */          
            Assert.IsNull(ProgressionUniqueness.Prove40320BlocksAreUnique(progressions));

            int firstMoveBlockSize = 40320;
            for (int i = 0; i < progressions.Count; i += firstMoveBlockSize)
            {
                Assert.IsNull(ProgressionUniqueness.Prove5040BlocksAreUnique(progressions, i));
            }

            int secondMoveBlockSize = 5040;
            for (int i = 0; i < progressions.Count; i += secondMoveBlockSize)
            {
                Assert.IsNull(ProgressionUniqueness.Prove720BlocksAreUnique(progressions, i));
            }

            int thirdMoveBlockSize = 720;
            for (int i = 0; i < progressions.Count; i += thirdMoveBlockSize)
            {
                Assert.IsNull(ProgressionUniqueness.Prove120BlocksAreUnique(progressions, i));
            }

            int fourthMoveBlockSize = 120;
            for (int i = 0; i < progressions.Count; i += fourthMoveBlockSize)
            {
                Assert.IsNull(ProgressionUniqueness.Prove24BlocksAreUnique(progressions, i)); 
            }

            int fifthMoveBlockSize = 24; // 6 2 1
            for (int i = 0; i < progressions.Count; i += fifthMoveBlockSize)
            {
                Assert.IsNull(ProgressionUniqueness.Prove6BlocksAreUnique(progressions, i));
            }

            int sixthMoveBlockSize = 6; // 6 2 1
            for (int i = 0; i < progressions.Count; i += sixthMoveBlockSize)
            {
                Assert.IsNull(ProgressionUniqueness.Prove2BlocksAreUnique(progressions, i));
            }

            int seventhMoveBlockSize = 2; // 6 2 1
            for (int i = 0; i < progressions.Count; i += seventhMoveBlockSize)
            {
                Assert.IsNull(ProgressionUniqueness.Prove1BlocksAreUnique(progressions, i));
            }


            /* The next set of tests are to show that ProveXBlocksAreUnique() functions return !null when the
             * progressions are not ina particular order.
             */

            // Force a different organisation of the progressions - level 1
            // Proves that the 40320 - 80639 move 1 is not to position 0.
            Progression progression40320 = (Progression)progressions[40320];
            progressions[40320] = progressions[0];
            Assert.IsNotNull(ProgressionUniqueness.Prove40320BlocksAreUnique(progressions));
            progressions[40320] = progression40320;

            // Force a different organisation of the progressions - level 2
            Progression progression5039 = (Progression)progressions[5039];
            progressions[5039] = progressions[5040];
            Assert.IsNotNull(ProgressionUniqueness.Prove5040BlocksAreUnique(progressions, 0));
            progressions[5039] = progression5039;

            // Force a different organisation of the progressions - level 3
            Progression progression719 = (Progression)progressions[719];
            progressions[719] = progressions[720];
            Assert.IsNotNull(ProgressionUniqueness.Prove720BlocksAreUnique(progressions, 0));
            progressions[719] = progression719;

            // Force a different organisation of the progressions - level 4
            Progression progression119 = (Progression)progressions[119];
            progressions[119] = progressions[120];
            Assert.IsNotNull(ProgressionUniqueness.Prove120BlocksAreUnique(progressions, 0));
            progressions[119] = progression119;

            // Force a different organisation of the progressions - level 5
            Progression progression23 = (Progression)progressions[23];
            progressions[23] = progressions[24];
            Assert.IsNotNull(ProgressionUniqueness.Prove24BlocksAreUnique(progressions, 0));
            progressions[23] = progression23;

            // Force a different organisation of the progressions - level 6
            Progression progression5 = (Progression)progressions[5];
            progressions[5] = progressions[6];
            Assert.IsNotNull(ProgressionUniqueness.Prove6BlocksAreUnique(progressions, 0));
            progressions[5] = progression5;

            // Force a different organisation of the progressions - level 7
            Progression progression1 = (Progression)progressions[1];
            progressions[1] = progressions[2];
            Assert.IsNotNull(ProgressionUniqueness.Prove2BlocksAreUnique(progressions, 0));
            progressions[1] = progression1;

            // Force a different organisation of the progressions - level 8
            Progression progression0 = (Progression)progressions[0];
            progressions[0] = progressions[1];
            Assert.IsNotNull(ProgressionUniqueness.Prove1BlocksAreUnique(progressions, 0));
            progressions[0] = progression0;
        }
    }
    public static class ProgressionUniqueness
    {
        public static int? Prove40320BlocksAreUnique(ArrayList progressions) 
        {
            // 40320
            int blockSize = progressions.Count / 9;
            var squares = Enum.GetValues(typeof(Square));
            int startIndex = 0;
            int endIndex = blockSize;
            foreach (Square square in squares) // 362,880 min ops
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (((Progression)progressions[i]).GetMove(MoveNumber.one).Square != square)
                    {
                        // There is a progression that is in an unexpected position in the collection, becuase Move one has a square that is unexpected. Doesn't men the progressions are not unique.
                        // But if this return never happens, then we know the Number of progressions with the first move to square 0, sqaure 1, square 2, square 3, etc is 9!/9 = 40,320.
                        return i; 
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
        public static int? Prove5040BlocksAreUnique(ArrayList progressions, int startUnique40320) 
        {
            // Every block of 5040 second move should be to the same square. But every 5040 block second move should be different to every other 5040 block in this unique 40320.
            int blockSize = progressions.Count / 9 / 8;
            Square occupiedSquare = ((Progression)progressions[startUnique40320]).GetMove(MoveNumber.one).Square;
            var squares = Enum.GetValues(typeof(Square));
            int startIndex = startUnique40320;
            int endIndex = startIndex + blockSize;
            foreach (Square square in squares) // eight unoccpied // 40320 min ops
            {
                if (square != occupiedSquare)
                {
                    // Iterate 5040 times for every unoccupied square
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (((Progression)progressions[i]).GetMove(MoveNumber.two).Square != square)
                        {
                            return i; // returns only because there is a move to a square that is unexpected. 
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
        public static int? Prove720BlocksAreUnique(ArrayList progressions, int startUnique5040)
        {
            int blockSize = progressions.Count / 9 / 8 / 7;
            Square[] occupiedSquares = new Square[] { ((Progression)progressions[startUnique5040]).GetMove(MoveNumber.one).Square, ((Progression)progressions[startUnique5040]).GetMove(MoveNumber.two).Square };
            var squares = Enum.GetValues(typeof(Square));
            int startIndex = startUnique5040;
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
        public static int? Prove120BlocksAreUnique(ArrayList progressions, int startUnique720)
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
        public static int? Prove24BlocksAreUnique(ArrayList progressions, int startUnique120)
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
        public static int? Prove6BlocksAreUnique(ArrayList progressions, int startUnique24)
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
        public static int? Prove2BlocksAreUnique(ArrayList progressions, int startUnique6)
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
        public static int? Prove1BlocksAreUnique(ArrayList progressions, int startUnique2)
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
