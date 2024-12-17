using AoC;

namespace SolverTests;

public class ComputerTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        List<string> input = """
        Register A: 729
        Register B: 123
        Register C: 456

        Program: 0,1,5,4,3,0
        """.Split('\n').ToList();

        // Act
        Computer sut = Computer.Parse(input);

        // Assert
        Assert.That(sut.IP, Is.EqualTo(0));
        Assert.That(sut.A, Is.EqualTo(729));
        Assert.That(sut.B, Is.EqualTo(123));
        Assert.That(sut.C, Is.EqualTo(456));
        List<int> expectedProgram = [0, 1, 5, 4, 3, 0];
        Assert.That(sut.Program, Is.EqualTo(expectedProgram));
    }

    [Test]
    public void TestExecuteProgram()
    {
        // Arrange
        List<string> input = """
        Register A: 729
        Register B: 0
        Register C: 0

        Program: 0,1,5,4,3,0
        """.Split('\n').ToList();
        Computer sut = Computer.Parse(input);

        // Act
        sut.ExecuteProgram();

        // Assert
        List<int> expectedOutput = [4, 6, 3, 5, 6, 3, 5, 2, 1, 0];
        Assert.That(sut.Output, Is.EqualTo(expectedOutput));
    }
}
