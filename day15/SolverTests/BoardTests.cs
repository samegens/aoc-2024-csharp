using AoC;

namespace SolverTests;

using static Direction;

public class BoardTests
{
    private static readonly List<string> standBoardInput = """
    ########
    #..O.O.#
    ##@.O..#
    #...O..#
    #.#.O..#
    #...O..#
    #......#
    ########
    """.Split('\n').ToList();

    [Test]
    public void TestParseBoardAndDirections()
    {
        // Arrange
        List<string> input = """
        ########
        #..O.O.#
        ##@.O..#
        #...O..#
        #.#.O..#
        #...O..#
        #......#
        ########

        <^^>>>vv<v>>v<<
        """.Split('\n').ToList();
        Board expectedBoard = new(standBoardInput);
        List<Direction> expectedDirections = [Left, Up, Up, Right, Right, Right, Down, Down, Left, Down, Right, Right, Down, Left, Left];

        // Act
        (Board board, List<Direction> directions) = Board.ParseBoardAndDirections(input);

        // Assert
        Assert.That(board, Is.EqualTo(expectedBoard));
        Assert.That(directions, Is.EqualTo(expectedDirections));
    }

    [TestCase(Left, 2, 2)]
    [TestCase(Up, 2, 1)]
    public void TestMove(Direction direction, long expectedX, long expectedY)
    {
        // Arrange
        Board sut = new(standBoardInput);

        // Act
        sut.MoveRobotTo(direction);

        // Assert
        Assert.That(sut.RobotPosition, Is.EqualTo(new Point2d(expectedX, expectedY)));
    }

    [TestCase(Left, 2, 2)]
    [TestCase(Up, 2, 2)]
    [TestCase(Right, 3, 2)]
    [TestCase(Down, 2, 3)]
    public void TestMoveWithBoxesInTheWay(Direction direction, long expectedX, long expectedY)
    {
        // Arrange
        List<string> boardInput = """
        ########
        #.OO.O.#
        #O@OOO.#
        #.O.O..#
        #...O..#
        #.O.O..#
        #.O....#
        ########
        """.Split('\n').ToList();
        Board sut = new(boardInput);

        // Act
        sut.MoveRobotTo(direction);

        // Assert
        Assert.That(sut.RobotPosition, Is.EqualTo(new Point2d(expectedX, expectedY)));
    }

    [Test]
    public void TestMoveAllSmallExample()
    {
        // Arrange
        List<string> input = """
        ########
        #..O.O.#
        ##@.O..#
        #...O..#
        #.#.O..#
        #...O..#
        #......#
        ########

        <^^>>>vv<v>>v<<
        """.Split('\n').ToList();
        (Board sut, List<Direction> directions) = Board.ParseBoardAndDirections(input);
        List<string> output = """
        ########
        #....OO#
        ##.....#
        #.....O#
        #.#O@..#
        #...O..#
        #...O..#
        ########
        """.Split('\n').ToList();
        Board expected = new(output);

        // Act
        sut.MoveRobot(directions);

        // Assert
        Assert.That(sut, Is.EqualTo(expected));
    }

    [Test]
    public void TestMoveAllLargeExample()
    {
        // Arrange
        List<string> input = """
        ##########
        #..O..O.O#
        #......O.#
        #.OO..O.O#
        #..O@..O.#
        #O#..O...#
        #O..O..O.#
        #.OO.O.OO#
        #....O...#
        ##########

        <vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
        vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
        ><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
        <<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
        ^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
        ^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
        >^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
        <><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
        ^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
        v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^
        """.Split('\n').ToList();
        (Board sut, List<Direction> directions) = Board.ParseBoardAndDirections(input);
        List<string> output = """
        ##########
        #.O.O.OOO#
        #........#
        #OO......#
        #OO@.....#
        #O#.....O#
        #O.....OO#
        #O.....OO#
        #OO....OO#
        ##########
        """.Split('\n').ToList();
        Board expected = new(output);

        // Act
        sut.MoveRobot(directions);

        // Assert
        Assert.That(sut, Is.EqualTo(expected));
    }

    [Test]
    public void TestGetGPSScoreSmallExample()
    {
        // Arrange
        List<string> input = """
        ########
        #....OO#
        ##.....#
        #.....O#
        #.#O@..#
        #...O..#
        #...O..#
        ########
        """.Split('\n').ToList();
        Board sut = new(input);

        // Act
        long actual = sut.GetGpsScore();

        // Assert
        Assert.That(actual, Is.EqualTo(2028));
    }

    [Test]
    public void TestGetGPSScoreLargeExample()
    {
        // Arrange
        List<string> input = """
        ##########
        #.O.O.OOO#
        #........#
        #OO......#
        #OO@.....#
        #O#.....O#
        #O.....OO#
        #O.....OO#
        #OO....OO#
        ##########
        """.Split('\n').ToList();
        Board sut = new(input);

        // Act
        long actual = sut.GetGpsScore();

        // Assert
        Assert.That(actual, Is.EqualTo(10092));
    }
}
