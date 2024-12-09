using AoC;

namespace SolverTests;

public class FileSystemTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        string line = "234";
        List<FileSystem.Block> expected = [
            new(0, 2),
            new(-1, 3),
            new(1, 4)
        ];

        // Act
        FileSystem actual = FileSystem.Parse(line);

        // Assert
        Assert.That(actual.Blocks, Is.EqualTo(expected));
    }

    [Test]
    public void TestDefragmentTrivial()
    {
        // Arrange
        string line = "213";
        FileSystem sut = FileSystem.Parse(line);
        List<FileSystem.Block> expected = [
            new(0, 2),
            new(1, 3)
        ];

        // Act
        sut.Defragment();

        // Assert
        Assert.That(sut.Blocks, Is.EqualTo(expected));
    }

    [Test]
    public void TestDefragmentLastBlockFillsFreeSpace()
    {
        // Arrange
        string line = "21324";
        FileSystem sut = FileSystem.Parse(line);
        List<FileSystem.Block> expected = [
            new(0, 2),
            new(2, 1),
            new(1, 3),
            new(2, 3)
        ];

        // Act
        sut.Defragment();

        // Assert
        Assert.That(sut.Blocks, Is.EqualTo(expected));
    }

    [Test]
    public void TestDefragmentFreeSpaceNeedsMultipleBlocks()
    {
        // Arrange
        string line = "25324";
        FileSystem sut = FileSystem.Parse(line);
        List<FileSystem.Block> expected = [
            new(0, 2),
            new(2, 4),
            new(1, 1),
            new(1, 2)
        ];

        // Act
        sut.Defragment();

        // Assert
        Assert.That(sut.Blocks, Is.EqualTo(expected));
    }

    [Test]
    public void TestDefragmentWithLotsOfSmallFileBlocksToFillLargeFreeSpace()
    {
        // Arrange
        string line = "29111111111111111111";
        FileSystem sut = FileSystem.Parse(line);
        List<FileSystem.Block> expected = [
            new(0, 2),
            new(9, 1),
            new(8, 1),
            new(7, 1),
            new(6, 1),
            new(5, 1),
            new(4, 1),
            new(3, 1),
            new(2, 1),
            new(1, 1)
        ];

        // Act
        sut.Defragment();

        // Assert
        Assert.That(sut.Blocks, Is.EqualTo(expected));
    }

    [TestCase("213", 1 * 2 + 1 * 3 + 1 * 4)]
    [TestCase("2134", 1 * 2 + 1 * 3 + 1 * 4)]
    public void TestComputeChecksum(string input, long expected)
    {
        // Arrange
        FileSystem sut = FileSystem.Parse(input);
        sut.Defragment();

        // Act
        long actual = sut.ComputeChecksum();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestDefragmentWholeBlocks()
    {
        // Arrange
        string line = "2333133121414131402";
        FileSystem sut = FileSystem.Parse(line);
        List<FileSystem.Block> expected = [
            new(0, 2),
            new(9, 2),
            new(2, 1),
            new(1, 3),
            new(7, 3),
            new(-1, 1),
            new(4, 2),
            new(-1, 1),
            new(3, 3),
            new(-1, 4),
            new(5, 4),
            new(-1, 1),
            new(6, 4),
            new(-1, 5),
            new(8, 4),
            new(-1, 2)
        ];

        // Act
        sut.DefragmentWholeBlocks();
        List<FileSystem.Block> actual = sut.Blocks;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
