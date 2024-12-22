using AoC;

namespace SolverTests;

public class KeypadTests
{
    [TestCase("0A", 1, 4)]
    [TestCase("0A", 2, 12)]
    [TestCase("0A", 3, 28)]
    [TestCase("029A", 3, 68)]
    [TestCase("980A", 3, 60)]
    [TestCase("179A", 3, 68)]
    [TestCase("456A", 3, 64)]
    [TestCase("379A", 3, 64)]
    public void TestGetHumanSequenceLength(string input, int nrRobots, long expected)
    {
        // Arrange
        Keypad sut = new(nrRobots);

        // Act
        long actual = sut.GetHumanSequenceLength(input);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestGetCommandSequences()
    {
        // Arrange
        List<string> expected = ["<<^^A", "^^<<A"];

        // Act
        List<string> commandSequences = Keypad.GetCommandSequences(new Point2d(2, 2), new Point2d(0, 0), 3);

        // Assert
        Assert.That(commandSequences, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestGetHumanSequencesTwoChars()
    {
        // Act
        List<string> sequences = Keypad.GetHumanSequences('A', '0', 3);

        // Assert
        Assert.That(sequences, Contains.Item("<vA<AA>>^AvAA<^A>A"));
    }

    [Test]
    public void TestGetHumanSequences()
    {
        // Act
        List<string> sequences = Keypad.GetHumanSequences("A029A", 0, 3);

        // Assert
        Assert.That(sequences, Contains.Item("<vA<AA>>^AvAA<^A>Av<<A>>^AvA^A<vA>^Av<<A>^A>AAvA^Av<<A>A>^AAAvA<^A>A"));
    }
}
