global using Point2dL = AoC.Point2d<long>;


using AoC;

namespace SolverTests;

public class MachineTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        List<string> input = """
        Button A: X+94, Y+34
        Button B: X+22, Y+67
        Prize: X=8400, Y=5400
        """.Split('\n').ToList();
        Machine expected = new(new Point2dL(94, 34), new Point2dL(22, 67), new Point2dL(8400, 5400));

        // Act
        Machine actual = Machine.Parse(input);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestParseMultiple()
    {
        // Arrange
        List<string> input = """
        Button A: X+94, Y+34
        Button B: X+22, Y+67
        Prize: X=8400, Y=5400
        
        Button A: X+26, Y+66
        Button B: X+67, Y+21
        Prize: X=12748, Y=12176
        """.Split('\n').ToList();
        List<Machine> expected = [
            new(new Point2dL(94, 34), new Point2dL(22, 67), new Point2dL(8400, 5400)),
            new(new Point2dL(26, 66), new Point2dL(67, 21), new Point2dL(12748, 12176))
        ];

        // Act
        List<Machine> actual = Machine.ParseMultiple(input);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(94, 34, 22, 67, 8400, 5400, 280)]
    [TestCase(26, 66, 67, 21, 12748, 12176, 0)]
    [TestCase(17, 86, 84, 37, 7870, 6450, 200)]
    public void TestPlay(int buttonAX, int buttonAY, int buttonBX, int buttonBY, int prizeX, int prizeY, long expected)
    {
        // Arrange
        Machine sut = new(new Point2dL(buttonAX, buttonAY), new Point2dL(buttonBX, buttonBY), new Point2dL(prizeX, prizeY));

        // Act
        long actual = sut.Play();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
