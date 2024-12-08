namespace AoC;

public class Solver(List<string> lines)
{
    public int SolvePart1()
    {
        Board board = new(lines);
        HashSet<Point> antinodes = [];
        foreach (List<Point> positions in board.GenerateFrequencyLists().Values)
        {
            for (int i = 0; i < positions.Count - 1; i++)
            {
                for (int j = i + 1; j < positions.Count; j++)
                {
                    Point diff = positions[j] - positions[i];
                    Point antinode = positions[i] - diff;
                    if (board.Contains(antinode))
                    {
                        antinodes.Add(antinode);
                    }
                    antinode = positions[j] + diff;
                    if (board.Contains(antinode))
                    {
                        antinodes.Add(antinode);
                    }
                }
            }
        }
        return antinodes.Count;
    }

    public int SolvePart2()
    {
        Board board = new(lines);
        HashSet<Point> antinodes = [];
        foreach (List<Point> positions in board.GenerateFrequencyLists().Values)
        {
            for (int i = 0; i < positions.Count - 1; i++)
            {
                for (int j = i + 1; j < positions.Count; j++)
                {
                    antinodes.Add(positions[i]);
                    antinodes.Add(positions[j]);
                    Point diff = positions[j] - positions[i];
                    Point possibleAntinodePosition = positions[i] - diff;
                    while (board.Contains(possibleAntinodePosition))
                    {
                        antinodes.Add(possibleAntinodePosition);
                        possibleAntinodePosition -= diff;
                    }
                    possibleAntinodePosition = positions[j] + diff;
                    while (board.Contains(possibleAntinodePosition))
                    {
                        antinodes.Add(possibleAntinodePosition);
                        possibleAntinodePosition += diff;
                    }
                }
            }
        }
        return antinodes.Count;
    }
}
