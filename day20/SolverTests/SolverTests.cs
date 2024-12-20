using AoC;

namespace SolverTests;

public class SolverTests
{
    private static readonly List<string> input = """
        ###############
        #...#...#.....#
        #.#.#.#.#.###.#
        #S#...#.#.#...#
        #######.#.#.###
        #######.#.#...#
        #######.#.###.#
        ###..E#...#...#
        ###.#######.###
        #...###...#...#
        #.#####.#.###.#
        #.#...#.#.#...#
        #.#.#.#.#.#.###
        #...#...#...###
        ###############
        """.Split('\n').ToList();

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart1(2);

        // Assert
        Assert.That(result, Is.EqualTo(44));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart2(50);

        // Assert
        Assert.That(result, Is.EqualTo(32 + 31 + 29 + 39 + 25 + 23 + 20 + 19 + 12 + 14 + 12 + 22 + 4 + 3));
    }
}
