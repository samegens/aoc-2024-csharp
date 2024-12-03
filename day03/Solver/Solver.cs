
using System.Text.RegularExpressions;

namespace AoC;

public class Solver(List<string> lines)
{
    public static List<Mul> FindMuls(string input)
    {
        List<Mul> muls = [];
        Regex r = new(@"mul\((\d*),(\d*)\)");
        foreach (Match match in r.Matches(input))
        {
            int op1 = int.Parse(match.Groups[1].ToString());
            int op2 = int.Parse(match.Groups[2].ToString());
            muls.Add(new Mul(op1, op2));
        }
        return muls;
    }

    public static List<Mul> FindEnabledMuls(string input)
    {
        List<Mul> muls = [];
        Regex r = new(@"(mul\((\d*),(\d*)\))|((do\(\))|(don't\(\)))");
        bool isMulEnabled = true;
        int lastIndex = -1;
        foreach (Match match in r.Matches(input))
        {
            if (match.Index < lastIndex)
            {
                throw new Exception("volgorde is stuk!");
            }
            lastIndex = match.Index;
            string operationMatch = match.Groups[0].ToString();
            if (operationMatch.StartsWith("mul"))
            {
                if (isMulEnabled)
                {
                    int op1 = int.Parse(match.Groups[2].ToString());
                    int op2 = int.Parse(match.Groups[3].ToString());
                    muls.Add(new Mul(op1, op2));
                }
            }
            else if (operationMatch == "don't()")
            {
                isMulEnabled = false;
            }
            else if (operationMatch == "do()")
            {
                isMulEnabled = true;
            }
            else
            {
                throw new Exception("This should not happen, bug!");
            }
        }
        return muls;
    }

    public int SolvePart1()
    {
        return lines.Select(
                        l => FindMuls(l).Sum(m => m.Execute()))
                    .Sum();
    }

    public int SolvePart2()
    {
        return FindEnabledMuls(string.Join("", lines)).Sum(m => m.Execute());
    }
}
