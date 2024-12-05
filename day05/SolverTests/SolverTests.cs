using AoC;

namespace SolverTests;

public class Tests
{
    private readonly List<string> input = [.. """
        47|53
        97|13
        97|61
        97|47
        75|29
        61|13
        75|53
        29|13
        97|29
        53|29
        61|53
        97|53
        61|29
        47|13
        75|47
        97|75
        47|61
        75|61
        47|29
        75|13
        53|13

        75,47,61,53,29
        97,61,53,29,13
        75,29,13
        75,97,47,61,53
        61,13,29
        97,13,75,29,47
        """.Split('\n')];

    [Test]
    public void TestParse()
    {
        // Arrange
        List<string> input = [.. """
        47|53

        75,47
        """.Split('\n')];
        Solver sut = new(input);
        List<Rule> expectedRules = [new Rule(47, 53)];
        List<List<int>> expectedUpdates = [[75, 47]];

        // Act
        (List<Rule> actualRules, List<List<int>> actualUpdates) = sut.Parse();

        // Assert
        Assert.That(actualRules, Is.EquivalentTo(expectedRules));
        Assert.That(actualUpdates, Is.EquivalentTo(expectedUpdates));
    }

    [TestCase("75,47,61,53,29", true)]
    [TestCase("75,97,47,61,53", false)]
    public void TestIsInCorrectOrder(string updateText, bool expectedResult)
    {
        // Arrange
        List<Rule> rules = Solver.ParseRules(input[0..21]);
        List<int> update = Solver.ParseUpdate(updateText);

        // Act
        bool actual = Solver.IsInCorrectOrder(update, rules);

        // Assert
        Assert.That(actual, Is.EqualTo(expectedResult));
    }

    [TestCase("75,47,61,53,29", 61)]
    public void TestGetMiddlePageNr(string updateText, int expectedPageNr)
    {
        // Arrange
        List<int> update = Solver.ParseUpdate(updateText);

        // Act
        int actual = Solver.GetMiddlePageNr(update);

        // Assert
        Assert.That(actual, Is.EqualTo(expectedPageNr));
    }

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(143));
    }

    [Test]
    public void TestOrderUpdate()
    {
        // Arrange
        List<Rule> rules = Solver.ParseRules(input[0..21]);
        List<int> update = [75, 97, 47, 61, 53];
        List<int> expectedOrderedUpdate = [97, 75, 47, 61, 53];

        // Act
        List<int> actual = Solver.OrderUpdate(update, rules);

        // Assert
        Assert.That(actual, Is.EqualTo(expectedOrderedUpdate));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(123));
    }
}
