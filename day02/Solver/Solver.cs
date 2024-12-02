
namespace AoC;

public class Solver(List<string> lines)
{
    public int SolvePart1()
    {
        List<Report> reports = ParseReports(lines);
        return reports.Count(r => r.IsSafe);
    }

    public int SolvePart2()
    {
        List<Report> reports = ParseReports(lines);
        return reports.Count(r => r.IsSafeWithProblemDampener);
    }

    public static List<Report> ParseReports(List<string> lines)
    {
        return lines.Select(l => Report.Parse(l))
                    .ToList();
    }
}
