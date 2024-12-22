using AoC;

namespace SolverTests;

public class BuyerTests
{
    [Test]
    public void TestGetSequencePrice()
    {
        // Arrange
        Buyer sut = new(123);
        string sequence = Buyer.GetSequenceFromChanges(-1, -1, 0, 2);

        // Act
        int actual = sut.GetSequencePrice(sequence);

        // Assert
        Assert.That(actual, Is.EqualTo(6));
    }

    [Test]
    public void TestGetBestSequence()
    {
        // Arrange
        List<string> input = [.. """
        1
        2
        3
        2024
        """.Split('\n')];
        List<Buyer> buyers = input
            .Select(l => new Buyer(long.Parse(l)))
            .ToList();
        string expected = Buyer.GetSequenceFromChanges(-2, 1, -1, 3);

        // Act
        string actual = Buyer.GetBestSequence(buyers);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

}
