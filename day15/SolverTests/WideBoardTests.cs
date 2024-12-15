using AoC;

namespace SolverTests;

using static Direction;

public class WideBoardTests
{
    [Test]
    public void TestConstructor()
    {
        // Arrange
        List<string> input = """
        #######
        #...#.#
        #.....#
        #..OO@#
        #..O..#
        #.....#
        #######
        """.Split('\n').ToList();
        string expected = """
        ##############
        ##......##..##
        ##..........##
        ##....[][]@.##
        ##....[]....##
        ##..........##
        ##############

        """;

        // Act
        WideBoard sut = new(input);

        // Assert
        Assert.That(sut.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestParse()
    {
        // Arrange
        List<string> input = """
        ##############
        ##......##..##
        ##..........##
        ##....[][]@.##
        ##....[]....##
        ##..........##
        ##############
        """.Split('\n').ToList();
        string expected = """
        ##############
        ##......##..##
        ##..........##
        ##....[][]@.##
        ##....[]....##
        ##..........##
        ##############

        """;

        // Act
        WideBoard actual = WideBoard.Parse(input);

        // Assert
        Assert.That(actual.ToString(), Is.EqualTo(expected));
    }

    [TestCase(Up, 10, 2)]
    [TestCase(Right, 10, 3)]
    [TestCase(Down, 10, 4)]
    [TestCase(Left, 9, 3)]
    public void TestMoveEmptySpaceAndWall(Direction direction, long expectedX, long expectedY)
    {
        // Arrange
        List<string> input = """
        ##############
        ##......##..##
        ##..........##
        ##....[]..@###
        ##....[]....##
        ##..........##
        ##############
        """.Split('\n').ToList();
        WideBoard sut = WideBoard.Parse(input);

        // Act
        sut.MoveRobotTo(direction);

        // Assert
        Assert.That(sut.RobotPosition, Is.EqualTo(new Point2d(expectedX, expectedY)));
    }

    [Test]
    public void TestMoveBoxesLeftCanMove()
    {
        // Arrange
        List<string> input = """
        ##############
        ##......##..##
        ##..........##
        ##.[].[][]@.##
        ##....[]....##
        ##..........##
        ##############
        """.Split('\n').ToList();
        WideBoard sut = WideBoard.Parse(input);
        string expected = """
        ##############
        ##......##..##
        ##..........##
        ##.[][][]@..##
        ##....[]....##
        ##..........##
        ##############

        """;

        // Act
        sut.MoveRobotTo(Left);

        // Assert
        Assert.That(sut.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestMoveBoxesLeftCantMove()
    {
        // Arrange
        List<string> input = """
        ##############
        ##......##..##
        ##..........##
        ######[][]@.##
        ##....[]....##
        ##..........##
        ##############
        """.Split('\n').ToList();
        WideBoard sut = WideBoard.Parse(input);
        string expected = """
        ##############
        ##......##..##
        ##..........##
        ######[][]@.##
        ##....[]....##
        ##..........##
        ##############

        """;

        // Act
        sut.MoveRobotTo(Left);

        // Assert
        Assert.That(sut.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestMoveBoxesRightCanMove()
    {
        // Arrange
        List<string> input = """
        ##############
        ##......##..##
        ##..........##
        ##...@[][]..##
        ##....[]....##
        ##..........##
        ##############
        """.Split('\n').ToList();
        WideBoard sut = WideBoard.Parse(input);
        string expected = """
        ##############
        ##......##..##
        ##..........##
        ##....@[][].##
        ##....[]....##
        ##..........##
        ##############

        """;

        // Act
        sut.MoveRobotTo(Right);

        // Assert
        Assert.That(sut.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestMoveBoxesRightCantMove()
    {
        // Arrange
        List<string> input = """
        ##############
        ##......##..##
        ##..........##
        ##...@[][][]##
        ##....[]....##
        ##..........##
        ##############
        """.Split('\n').ToList();
        WideBoard sut = WideBoard.Parse(input);
        string expected = """
        ##############
        ##......##..##
        ##..........##
        ##...@[][][]##
        ##....[]....##
        ##..........##
        ##############

        """;

        // Act
        sut.MoveRobotTo(Right);

        // Assert
        Assert.That(sut.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestMoveBoxesUpCanMove()
    {
        // Arrange
        List<string> input = """
        ##############
        ##..........##
        ##..[][][]..##
        ##...[][]...##
        ##....[]....##
        ##....@.....##
        ##############
        """.Split('\n').ToList();
        WideBoard sut = WideBoard.Parse(input);
        string expected = """
        ##############
        ##..[][][]..##
        ##...[][]...##
        ##....[]....##
        ##....@.....##
        ##..........##
        ##############

        """;

        // Act
        sut.MoveRobotTo(Up);

        // Assert
        Assert.That(sut.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestMoveRobotLargeExample()
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
        (WideBoard board, List<Direction> directions) = WideBoard.ParseWideBoardAndDirections(input);
        string expected = """
        ####################
        ##[].......[].[][]##
        ##[]...........[].##
        ##[]........[][][]##
        ##[]......[]....[]##
        ##..##......[]....##
        ##..[]............##
        ##..@......[].[][]##
        ##......[][]..[]..##
        ####################

        """;

        // Act
        board.MoveRobot(directions);
        board.Print();

        // Assert
        Assert.That(board.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestMoveRobotSmallExample()
    {
        // Arrange
        List<string> input = """
        #######
        #...#.#
        #.....#
        #..OO@#
        #..O..#
        #.....#
        #######

        <vv<<^^<<^^
        """.Split('\n').ToList();
        (WideBoard board, List<Direction> directions) = WideBoard.ParseWideBoardAndDirections(input);
        string expected = """
        ##############
        ##...[].##..##
        ##...@.[]...##
        ##....[]....##
        ##..........##
        ##..........##
        ##############

        """;

        // Act
        board.MoveRobot(directions);
        board.Print();

        // Assert
        Assert.That(board.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestGetGpsScore()
    {
        // Arrange
        List<string> input = """
        ####################
        ##[].......[].[][]##
        ##[]...........[].##
        ##[]........[][][]##
        ##[]......[]....[]##
        ##..##......[]....##
        ##..[]............##
        ##..@......[].[][]##
        ##......[][]..[]..##
        ####################
        """.Split('\n').ToList();
        WideBoard sut = WideBoard.Parse(input);

        // Act
        long actual = sut.GetGpsScore();

        // Assert
        Assert.That(actual, Is.EqualTo(9021));
    }
}
