using AoC;

namespace SolverTests;

public class NetworkTests
{
    private static readonly List<string> exampleInput = """
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
    public void TestParse()
    {
        // Arrange
        List<string> input = """
        aa-bb
        bb-cc
        cc-aa
        """.Split('\n').ToList();

        // Act
        Network sut = Network.Parse(input);

        // Assert
        Assert.That(sut.VertexCount, Is.EqualTo(3));
    }

    [Test]
    public void TestFindClustersWithSimpleGraph()
    {
        // Arrange
        List<string> input = """
        aa-bb
        bb-cc
        cc-aa
        cc-dd
        dd-ee
        ee-cc
        """.Split('\n').ToList();
        Network sut = Network.Parse(input);
        HashSet<HashSet<string>> expected = [
            ["aa", "bb", "cc"],
            ["cc", "dd", "ee"]
        ];

        // Act
        HashSet<HashSet<string>> actual = sut.FindClusters();

        // Assert
        Assert.That(actual.Count, Is.EqualTo(expected.Count));
        Assert.That(actual.First(), Is.EquivalentTo(expected.First()));
        Assert.That(actual.Last(), Is.EquivalentTo(expected.Last()));
    }

    [Test]
    public void TestFindClustersWithExample()
    {
        // Arrange
        Network sut = Network.Parse(exampleInput);

        // Act
        HashSet<HashSet<string>> actual = sut.FindClusters();

        // Assert
        Assert.That(actual.Count, Is.EqualTo(12));
    }

    [Test]
    public void TestSetEquals()
    {
        // Arrange
        HashSet<string> s1 = ["aa", "bb", "cc"];
        HashSet<string> s2 = ["bb", "cc", "aa"];

        // Act
        bool result = s1.SetEquals(s2);

        // Assert
        Assert.That(result, Is.True);
    }

}
