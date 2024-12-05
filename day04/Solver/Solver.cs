


namespace AoC;

public class Solver
{
    private readonly Board _board;

    public Solver(List<string> lines)
    {
        _board = new Board(lines);
    }

    public int SolvePart1()
    {
        return FindAll4LetterWords()
            .Where(w => w == "XMAS")
            .Count();
    }

    public List<string> FindAll4LetterWords()
    {
        List<string> fourLetterWords = [];
        for (int y = 0; y < _board.Height; y++)
        {
            for (int x = 0; x < _board.Width; x++)
            {
                fourLetterWords.AddRange(FindAll4LetterWords(x, y));
            }
        }
        return fourLetterWords;
    }

    private IEnumerable<string> FindAll4LetterWords(int x, int y)
    {
        foreach ((Direction dir, Point deltaXY) in DirectionHelpers.Movements)
        {
            string word = Find4LetterWord(x, y, deltaXY);
            if (!string.IsNullOrEmpty(word))
            {
                yield return word;
            }
        }
    }

    private string Find4LetterWord(int x, int y, Point deltaXY)
    {
        string word = "";
        for (int i = 0; i < 4; i++)
        {
            if (!_board.Contains(new Point(x, y)))
            {
                return string.Empty;
            }

            word += _board[x, y];
            x += deltaXY.X;
            y += deltaXY.Y;
        }
        return word;
    }

    public int SolvePart2()
    {
        int count = 0;
        for (int y = 1; y < _board.Height - 1; y++)
        {
            for (int x = 1; x < _board.Width - 1; x++)
            {
                if (_board[x, y] == 'A' &&
                    ((_board[x - 1, y - 1] == 'S' && _board[x + 1, y + 1] == 'M') || (_board[x - 1, y - 1] == 'M' && _board[x + 1, y + 1] == 'S')) &&
                    ((_board[x + 1, y - 1] == 'S' && _board[x - 1, y + 1] == 'M') || (_board[x + 1, y - 1] == 'M' && _board[x - 1, y + 1] == 'S')))
                {
                    count++;
                }
            }
        }
        return count;
    }
}
