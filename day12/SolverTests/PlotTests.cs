using AoC;

namespace SolverTests;

public class PlotTests
{
    [Test]
    public void TestArea()
    {
        // Arrange
        Plot sut = new([new Point2dI(0, 0), new Point2dI(0, 1), new Point2dI(1, 1), new Point2dI(2, 1)]);

        // Act
        int actual = sut.Area;

        // Assert
        Assert.That(actual, Is.EqualTo(4));
    }

    [Test]
    public void TestPerimeterWithHoleInside()
    {
        // Arrange
        Plot sut = new([new Point2dI(0, 0), new Point2dI(0, 1), new Point2dI(0, 2),
                        new Point2dI(1, 0),                     new Point2dI(1, 2),
                        new Point2dI(2, 0), new Point2dI(2, 1), new Point2dI(2, 2)]);

        // Act
        int actual = sut.Perimeter;

        // Assert
        Assert.That(actual, Is.EqualTo(12 + 4));
    }

    [Test]
    public void TestPerimeterWithHoleInEdge()
    {
        // Arrange
        Plot sut = new([new Point2dI(0, 0),                     new Point2dI(0, 2),
                        new Point2dI(1, 0), new Point2dI(1, 1), new Point2dI(1, 2),
                        new Point2dI(2, 0), new Point2dI(2, 1), new Point2dI(2, 2)]);

        // Act
        int actual = sut.Perimeter;

        // Assert
        Assert.That(actual, Is.EqualTo(12 + 2));
    }

    [Test]
    public void TestPerimeterWithComplexPlot()
    {
        // Arrange
        // (6,0);(7,0);(7,1);(8,1);(6,1);(6,2);(5,2);(5,3);(4,3);(4,4);(4,5);(5,5);(5,6);(3,3)
        // ......XX.
        // ......XXX
        // .....XX..
        // ...XXX
        // ....X
        // ....XX
        // .....X
        Plot sut = new([new Point2dI(6, 0), new Point2dI(7, 0), new Point2dI(7, 1), new Point2dI(8, 1),
                        new Point2dI(6, 1), new Point2dI(6, 2), new Point2dI(5, 2), new Point2dI(5, 3),
                        new Point2dI(4, 3), new Point2dI(4, 4), new Point2dI(4, 5),
                        new Point2dI(5, 5), new Point2dI(5, 6), new Point2dI(3, 3)]);

        // Act
        int actual = sut.Perimeter;

        // Assert
        Assert.That(actual, Is.EqualTo(28));
    }

    [Test]
    public void TestPerimeterWithHoleAtCorner()
    {
        // Arrange
        Plot sut = new([                    new Point2dI(0, 1), new Point2dI(0, 2),
                        new Point2dI(1, 0), new Point2dI(1, 1), new Point2dI(1, 2),
                        new Point2dI(2, 0), new Point2dI(2, 1), new Point2dI(2, 2)]);

        // Act
        int actual = sut.Perimeter;

        // Assert
        Assert.That(actual, Is.EqualTo(12));
    }

    [Test]
    public void TestNrSidesRectangleWithHoleInside()
    {
        // Arrange
        Plot sut = new([new Point2dI(0, 0), new Point2dI(0, 1), new Point2dI(0, 2),
                        new Point2dI(1, 0),                     new Point2dI(1, 2),
                        new Point2dI(2, 0), new Point2dI(2, 1), new Point2dI(2, 2)]);

        // Act
        int actual = sut.NrSides;

        // Assert
        Assert.That(actual, Is.EqualTo(8));
    }
}