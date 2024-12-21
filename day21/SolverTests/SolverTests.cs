﻿using AoC;

namespace SolverTests;

public class SolverTests
{
    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        List<string> input = [.. """
        029A
        980A
        179A
        456A
        379A
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(126384));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        List<string> input = [.. """
        hello world
        """.Split('\n')];
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(0));
    }
}
