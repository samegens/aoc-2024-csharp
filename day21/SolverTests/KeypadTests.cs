using AoC;

namespace SolverTests;

public class KeypadTests
{
    [Test]
    public void TestLockSubstitutions()
    {
        // Act
        Dictionary<string, string> result = Keypad.LockSubstitutions;

        // Assert
        Assert.That(result["A0"], Is.EqualTo("<A"));
        Assert.That(result["02"], Is.EqualTo("^A"));
        Assert.That(result["29"], Is.EqualTo(">^^A"));
        Assert.That(result["9A"], Is.EqualTo("vvvA"));
    }

    [TestCase("029A", "<A^A>^^AvvvA")]
    [TestCase("0029A", "<AA^A>^^AvvvA")]
    public void TestGetLockCommands(string input, string expected)
    {
        // Act
        string result = Keypad.GetLockCommands(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("^^", "<AA")]
    [TestCase("<A^A>^^AvvvA", "v<<A>>^A<A>AvA^<AA>Av<AAA>^A")]
    [TestCase("v<<A>>^A<A>AvA<^AA>A<vAAA>^A", "v<A<AA>>^AvAA^<A>Av<<A>>^AvA^Av<A>^Av<<A>^A>AAvA^Av<<A>A>^AAAvA^<A>A")]
    public void TestGetDirectionKeypadCommands(string input, string expected)
    {
        // Act
        string result = Keypad.GetDirectionKeypadCommands(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("029A", 68)]
    [TestCase("980A", 60)]
    [TestCase("179A", 68)]
    [TestCase("456A", 64)]
    [TestCase("379A", 64)]
    public void TestGetHumanSequenceLength(string input, int expected)
    {
        // Act
        int actual = Keypad.GetHumanSequenceLength(input, 3);

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
