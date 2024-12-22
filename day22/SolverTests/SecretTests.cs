using AoC;

namespace SolverTests;

public class SecretTests
{
    [TestCase(1, 15887950)]
    [TestCase(10, 5908254)]
    public void TestGenerateNext(int nrIterations, long expected)
    {
        // Arrange
        Secret sut = new(123);

        // Act
        for (int i = 0; i < nrIterations; i++)
        {
            sut.GenerateNext();
        }

        // Assert
        Assert.That(sut.Current, Is.EqualTo(expected));
    }

    [TestCase(1, 8685429)]
    [TestCase(10, 4700978)]
    [TestCase(100, 15273692)]
    [TestCase(2024, 8667524)]
    public void TestGenerateNext2000Times(long seed, long expected)
    {
        // Arrange
        Secret sut = new(seed);

        // Act
        for (int i = 0; i < 2000; i++)
        {
            sut.GenerateNext();
        }

        // Assert
        Assert.That(sut.Current, Is.EqualTo(expected));
    }

}
