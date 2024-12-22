using AoC;

namespace SolverTests;

public class KeypadTests
{
    [TestCase("029A", 68)]
    [TestCase("980A", 60)]
    [TestCase("179A", 68)]
    [TestCase("456A", 64)]
    [TestCase("379A", 64)]
    public void TestGetHumanSequenceLength(string input, long expected)
    {
        // Arrange
        Keypad sut = new(3);

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
