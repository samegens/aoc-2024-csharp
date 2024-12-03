using AoC;

namespace SolverTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        List<string> input = [.. """
        xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(161));
    }

    [Test]
    public void TestFindMuls()
    {
        // Arrange
        string input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        List<Mul> expected = [new Mul(2, 4), new Mul(5, 5), new Mul(11, 8), new Mul(8, 5)];

        // Act
        List<Mul> result = Solver.FindMuls(input);

        // Assert
        Assert.That(result, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        List<string> input = [.. """
        xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(48));
    }

    [Test]
    public void TestSolvePart2Multiline()
    {
        // Arrange
        List<string> input = [.. """
        xmul(2,4)&mul[3,7]!^don't()_mul(5,5)
        +mul(32,64](mul(11,8)undo()?mul(8,5))
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(48));
    }

    [Test]
    public void TestFindEnabledMuls()
    {
        // Arrange
        string input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
        List<Mul> expected = [new Mul(2, 4), new Mul(8, 5)];

        // Act
        List<Mul> result = Solver.FindEnabledMuls(input);

        // Assert
        Assert.That(result, Is.EquivalentTo(expected));
    }
}
