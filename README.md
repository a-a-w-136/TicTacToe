1. TicTacToe directory contains the assembly to run a game of TicTacToe. 
2. For usage see GameEngine directory.
3. The TicTacToe assembly contains an AICumulativeDrawAndWin class to determine which move to make when implementing a Human vs Cpu game.
4. The AICumulativeDrawAndWin class implements the AI abstract.
5. Developers can inherit from the AI class so that the decision making on how moves are made can be extended without the need to modify the source code.
6. This project purpose is to demonstrate the ability to develop programs written in C#, but it is not complete. In particular, with respects to testing and refactoring.
7. One test has been supplied in TicTacToeTestSuite directory. It tests that the 362,880 ways a TicTacToe board can be filled(Progressions) are unique. The TicTacToe assembly builds these progressions. Check the source code for more details.
8. There are several 'TODO' comments made where refactoring may be neccessary.
