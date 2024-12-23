using AoC;

namespace SolverTests;

public class SolverTests
{
    private static readonly List<string> input = """
    kh-tc
    qp-kh
    de-cg
    ka-co
    yn-aq
    qp-ub
    cg-tb
    vc-aq
    tb-ka
    wh-tc
    yn-cg
    kh-ub
    ta-co
    de-co
    tc-td
    tb-wq
    wh-td
    ta-ka
    td-qp
    aq-cg
    wq-ub
    ub-vc
    de-ta
    wq-aq
    wq-vc
    wh-yn
    ka-de
    kh-ta
    co-tc
    wh-qp
    tb-vc
    td-yn
    """.Split('\n').ToList();

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        Solver sut = new(input);

        // Act
        int result = sut.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(7));
    }

    [Test]
    [Ignore("WIP")]
    public void TestSolvePart2()
    {
        // Arrange
        Solver sut = new(input);

        // Act
        string result = sut.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo("co,de,ka,ta"));
    }
}
