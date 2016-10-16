// 19 - Indexers and Enumerators\Indexing with Multiple Parameters
// copyright 2000 Eric Gunnerson
using System;

public class Player
{
    string name;
    
    public Player(string name)
    {
        this.name = name;
    }
    
    public override string ToString()
    {
        return(name);
    }
}

public class Board
{
    Player[,] board = new Player[8, 8];
    
    int RowToIndex(string row)
    {
        string temp = row.ToUpper();
        return((int) temp[0] - (int) 'A');
    }
    
    int PositionToColumn(string pos)
    {
        return(pos[1] - '0' - 1);
    }
    
    public Player this[string row, int column]
    {
        get
        {
            return(board[RowToIndex(row), column - 1]);
        }
        set
        {
            board[RowToIndex(row), column - 1] = value;
        }
    }    
    
    public Player this[string position]
    {
        get
        {
            return(board[RowToIndex(position),
            PositionToColumn(position)]);
        }
        set
        {
            board[RowToIndex(position),
            PositionToColumn(position)] = value;
        }
    }    
}
class Test
{
    public static void Main()
    {
        Board board = new Board();
        
        board["A", 4] = new Player("White King");
        board["H", 4] = new Player("Black King");
        
        Console.WriteLine("A4 = {0}", board["A", 4]);
        Console.WriteLine("H4 = {0}", board["H4"]);
    }
}