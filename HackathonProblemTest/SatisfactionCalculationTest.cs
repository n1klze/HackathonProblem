using HackathonProblem.SatisfactionCalculationMethod;

namespace HackathonProblemTest;

[TestClass]
public sealed class SatisfactionCalculationTest()
{
    private readonly double precision = 1e-9;
    private readonly HarmonicMeanCalculator _calculator = new();

    [TestMethod]
    [DataRow(3, 3, 3, 3)]
    [DataRow(3, 2, 6)]
    [DataRow(1.92, 1, 2, 3, 4)]
    public void CorrectAnswerTest(double expected, params int[] values) =>
        Assert.AreEqual(
            _calculator.calculate(values),
            expected,
            precision,
            "Harmonic mean calculator gives the wrong answer."
        );
}
