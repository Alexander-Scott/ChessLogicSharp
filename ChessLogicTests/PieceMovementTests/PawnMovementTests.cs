using ChessLogicSharp;
using ChessLogicSharp.ChessPlayers;
using ChessLogicSharp.DataStructures;
using NUnit.Framework;

namespace ChessLogicTests.PieceMovementTests
{
    [TestFixture]
    public class PawnMovementTests
    {
        /// <summary>
        /// Test the pawn can move 1 space from the start position
        /// </summary>
        [Test]
        public void PawnMoveUpSinglePieceTest()
        {
            Board board = BoardFactory.CreateBoard();
            BasicPlayer player1 = new BasicPlayer(board, Player.PlayerOne);
            board.AddPlayer(player1);
            BasicPlayer player2 = new BasicPlayer(board, Player.PlayerTwo);
            board.AddPlayer(player2);

            Vector2I pawnPos = new Vector2I(3, 1);
            Assert.IsTrue(board.BoardPieces[pawnPos.X, pawnPos.Y].PieceType == PieceType.Pawn);

            Vector2I pawnDest = new Vector2I(3, 2);
            Assert.IsTrue(board.BoardPieces[pawnDest.X, pawnDest.Y].PieceType == PieceType.None);

            BoardPieceMove move = new BoardPieceMove(pawnPos, pawnDest);
            player1.ApplyMove(move);

            Assert.IsTrue(board.BoardPieces[pawnPos.X, pawnPos.Y].PieceType == PieceType.None);
            Assert.IsTrue(board.BoardPieces[pawnDest.X, pawnDest.Y].PieceType == PieceType.Pawn);
        }

        /// <summary>
        /// Test the pawn can move 2 spaces from the start position
        /// </summary>
        [Test]
        public void PawnMoveUpDoublePieceTest()
        {
            Board board = BoardFactory.CreateBoard();
            BasicPlayer player1 = new BasicPlayer(board, Player.PlayerOne);
            board.AddPlayer(player1);
            BasicPlayer player2 = new BasicPlayer(board, Player.PlayerTwo);
            board.AddPlayer(player2);

            Vector2I pawnPos = new Vector2I(3, 1);
            Assert.IsTrue(board.BoardPieces[pawnPos.X, pawnPos.Y].PieceType == PieceType.Pawn);

            Vector2I pawnDest = new Vector2I(3, 3);
            Assert.IsTrue(board.BoardPieces[pawnDest.X, pawnDest.Y].PieceType == PieceType.None);

            BoardPieceMove move = new BoardPieceMove(pawnPos, pawnDest);
            player1.ApplyMove(move);

            Assert.IsTrue(board.BoardPieces[pawnPos.X, pawnPos.Y].PieceType == PieceType.None);
            Assert.IsTrue(board.BoardPieces[pawnDest.X, pawnDest.Y].PieceType == PieceType.Pawn);
        }

        /// <summary>
        /// Test the pawn can't move 3 spaces 
        /// </summary>
        [Test]
        public void PawnInvalidMovePieceTest()
        {
            Board board = BoardFactory.CreateBoard();
            BasicPlayer player1 = new BasicPlayer(board, Player.PlayerOne);
            board.AddPlayer(player1);
            BasicPlayer player2 = new BasicPlayer(board, Player.PlayerTwo);
            board.AddPlayer(player2);

            Vector2I pawnPos = new Vector2I(3, 1);
            Assert.IsTrue(board.BoardPieces[pawnPos.X, pawnPos.Y].PieceType == PieceType.Pawn);

            Vector2I pawnDest = new Vector2I(3, 4);
            Assert.IsTrue(board.BoardPieces[pawnDest.X, pawnDest.Y].PieceType == PieceType.None);

            BoardPieceMove move = new BoardPieceMove(pawnPos, pawnDest);
            Assert.IsFalse(player1.ApplyMove(move));

            Assert.IsTrue(board.BoardPieces[pawnPos.X, pawnPos.Y].PieceType == PieceType.Pawn);
            Assert.IsTrue(board.BoardPieces[pawnDest.X, pawnDest.Y].PieceType == PieceType.None);
        }

        /// <summary>
        /// Test the pawn can't move 2 spaces after already moving
        /// </summary>
        [Test]
        public void PawnInvalidMovePieceTest2()
        {
            char[,] boardLayout =
            {
                {'C', 'N', 'B', 'Q', 'K', 'B', 'N', 'C'},
                {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                {'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'},
                {'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'},
                {'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'},
                {'e', 'e', 'e', 'p', 'e', 'e', 'e', 'e'}, // Pawn has already moved
                {'p', 'p', 'p', 'e', 'p', 'p', 'p', 'p'},
                {'c', 'n', 'b', 'q', 'k', 'b', 'n', 'c'}
            };
            boardLayout = boardLayout.RotateArray();

            Board board = BoardFactory.CreateBoard(boardLayout);
            BasicPlayer player1 = new BasicPlayer(board, Player.PlayerOne);
            board.AddPlayer(player1);
            BasicPlayer player2 = new BasicPlayer(board, Player.PlayerTwo);
            board.AddPlayer(player2);
            
            var pawnPos = new Vector2I(3, 2);
            Assert.IsTrue(board.BoardPieces[pawnPos.X, pawnPos.Y].PieceType == PieceType.Pawn);

            var pawnDest = new Vector2I(3, 4);
            Assert.IsTrue(board.BoardPieces[pawnDest.X, pawnDest.Y].PieceType == PieceType.None);

            var move = new BoardPieceMove(pawnPos, pawnDest);
            Assert.IsFalse(player1.ApplyMove(move));

            Assert.IsTrue(board.BoardPieces[pawnPos.X, pawnPos.Y].PieceType == PieceType.Pawn);
            Assert.IsTrue(board.BoardPieces[pawnDest.X, pawnDest.Y].PieceType == PieceType.None);
        }
        
        /// <summary>
        /// Test the pawn can't move backwards
        /// </summary>
        [Test]
        public void PawnInvalidMovePieceTest3()
        {
            char[,] boardLayout =
            {
                {'C', 'N', 'B', 'Q', 'K', 'B', 'N', 'C'},
                {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                {'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'},
                {'e', 'e', 'e', 'p', 'e', 'e', 'e', 'e'},
                {'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'},
                {'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'}, // Pawn has already moved
                {'p', 'p', 'p', 'e', 'p', 'p', 'p', 'p'},
                {'c', 'n', 'b', 'q', 'k', 'b', 'n', 'c'}
            };
            boardLayout = boardLayout.RotateArray();

            Board board = BoardFactory.CreateBoard(boardLayout);
            BasicPlayer player1 = new BasicPlayer(board, Player.PlayerOne);
            board.AddPlayer(player1);
            BasicPlayer player2 = new BasicPlayer(board, Player.PlayerTwo);
            board.AddPlayer(player2);
            
            var pawnPos = new Vector2I(3, 4);
            Assert.IsTrue(board.BoardPieces[pawnPos.X, pawnPos.Y].PieceType == PieceType.Pawn);

            var pawnDest = new Vector2I(3, 3);
            Assert.IsTrue(board.BoardPieces[pawnDest.X, pawnDest.Y].PieceType == PieceType.None);

            var move = new BoardPieceMove(pawnPos, pawnDest);
            Assert.IsFalse(player1.ApplyMove(move));

            Assert.IsTrue(board.BoardPieces[pawnPos.X, pawnPos.Y].PieceType == PieceType.Pawn);
            Assert.IsTrue(board.BoardPieces[pawnDest.X, pawnDest.Y].PieceType == PieceType.None);
        }

        [Test]
        public void PieceTakenEnPassantTest()
        {
            char[,] boardLayout =
            {
                {'C', 'N', 'B', 'Q', 'K', 'B', 'N', 'C'},
                {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                {'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'},
                {'e', 'e', 'e', 'p', 'e', 'e', 'e', 'e'}, // Pawn take pawn
                {'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'}, 
                {'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'},
                {'p', 'p', 'p', 'e', 'p', 'p', 'p', 'p'},
                {'c', 'n', 'b', 'q', 'k', 'b', 'n', 'c'}
            };
            boardLayout = boardLayout.RotateArray();

            Board board = BoardFactory.CreateBoard(boardLayout);
            BasicPlayer player1 = new BasicPlayer(board, Player.PlayerOne);
            board.AddPlayer(player1);
            BasicPlayer player2 = new BasicPlayer(board, Player.PlayerTwo);
            board.AddPlayer(player2);
            
            board.PlayerTurn = Player.PlayerTwo;
            
            var pawnPos = new Vector2I(4, 6); // Move enemy pawn
            var pawnDest = new Vector2I(4, 4);
            var move = new BoardPieceMove(pawnPos, pawnDest);
            player2.ApplyMove(move);
            
            pawnPos = new Vector2I(3, 4); // En passant take
            pawnDest = new Vector2I(4, 5);
            move = new BoardPieceMove(pawnPos, pawnDest);
            player1.ApplyMove(move);

            Assert.IsTrue(board.BoardPieces[pawnPos.X, pawnPos.Y].PieceType == PieceType.None);
            Assert.IsTrue(board.BoardPieces[pawnDest.X, pawnDest.Y].PieceType == PieceType.Pawn && board.BoardPieces[pawnDest.X, pawnDest.Y].PieceOwner == Player.PlayerOne);
            Assert.IsTrue(board.BoardPieces[4, 4].PieceType == PieceType.None);
        }
    }
}