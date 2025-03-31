using HW1.SquareSolver;
using Xunit;

namespace UnitTests.HW1;

public class SquareEquationSolverTests
{
    private readonly SquareEquationSolver _solver = new();

    public static IEnumerable<object[]> NoRootsSet => new List<object[]>
    {
        new object[] {1, 0, 1, Array.Empty<double>() },
        new object[] {1, 0, 7, Array.Empty<double>() },
        new object[] {1, 0, 100.34, Array.Empty<double>() },
        new object[] {1, 0, 1000, Array.Empty<double>() },
    };

    public static IEnumerable<object[]> OneRootMultiplicityOfTwoSet => new List<object[]>
    {
        new object[] {1, 2, 1, double.Epsilon, new[] { -1d, -1d } },
        new object[] {1, -2, 1, double.Epsilon, new[] { 1d, 1d } },
        new object[] {4, 4, 1, double.Epsilon, new[] { -1/2d, -1/2d } },
        new object[] {4, -4, 1, double.Epsilon, new[] { 1/2d, 1/2d } },
        new object[] {1/4d, 1, 99999999/100000000d, 2/10000d, new[] { -2d, -2d } },
    };
    
    public static IEnumerable<object[]> TwoRootsSet => new List<object[]>
    {
        new object[] {1, 0, -1, double.Epsilon, new[] { 1d, -1d } },
        new object[] {1, -3, 1, double.Epsilon, new[] { (3-Math.Sqrt(5))/2d, (3+Math.Sqrt(5))/2d } },
        new object[] {2, -3, 1, double.Epsilon, new[] { 1/2d, 1 } },
        new object[] {2, 7, 4, double.Epsilon, new[] { (-7-Math.Sqrt(17))/4d, (-7+Math.Sqrt(17))/4d } },
    };

    public static IEnumerable<object[]> IncorrectCoefficientsSet => new List<object[]>
    {
        new object[] {double.NaN, 1, -1, double.Epsilon },
        new object[] {1, double.NaN, -1, double.Epsilon },
        new object[] {1, 4, double.NaN, double.Epsilon },
        new object[] {1, -4, -1, double.NaN },
        new object[] {double.NegativeInfinity, 1, -1, double.Epsilon },
        new object[] {1, double.NegativeInfinity, -1, double.Epsilon },
        new object[] {1, 4, double.NegativeInfinity, double.Epsilon },
        new object[] {1, -4, -1, double.NegativeInfinity },
        new object[] {double.PositiveInfinity, 1, -1, double.Epsilon },
        new object[] {1, double.PositiveInfinity, -1, double.Epsilon },
        new object[] {1, 4, double.PositiveInfinity, double.Epsilon },
        new object[] {1, -4, -1, double.PositiveInfinity },
    };

    [Theory]
    [MemberData(nameof(NoRootsSet))]
    public void SquareEquationSolver_NoRoots(double a, double b, double c, double[] expected)
    {
        // Arrange & Act
        var actual = _solver.Solve(a, b, c);
        
        // Assert
        Assert.Equal(actual, expected);
    }

    [Theory]
    [MemberData(nameof(OneRootMultiplicityOfTwoSet))]
    public void SquareEquationSolver_OneRootMultiplicityOfTwo(double a, double b, double c, double epsilon, double[] expected)
    {
        // Arrange & Act
        var actual = _solver.Solve(a, b, c, epsilon);
        
        // Assert
        Assert.Equal(expected.Order(), actual.Order());
    }

    [Theory]
    [MemberData(nameof(TwoRootsSet))]
    public void SquareEquationSolver_TwoRoots(double a, double b, double c, double epsilon, double[] expected)
    {
        // Arrange
        var comparer = new DoubleDigitsEqualityComparer();
        
        // Act
        var actual = _solver.Solve(a, b, c, epsilon);
        
        //Assert
        Assert.True(actual.Order().SequenceEqual(expected.Order(), comparer));
    }

    [Fact]
    public void SquareEquationSolver_ThrowAnArgumentExceptionIfACoefficientIsZero()
    {
        // Arrange
        var a = 0d;
        var b = 1d;
        var c = 2d;
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _solver.Solve(a, b, c));
    }

    [Theory]
    [MemberData(nameof(IncorrectCoefficientsSet))]
    public void SquareEquationSolver_ThrowAnArgumentExceptionIfAnyCoefficientsAreIncorrect(double a, double b, double c, double epsilon)
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => _solver.Solve(a, b, c, epsilon));
    }
}

public class DoubleDigitsEqualityComparer(double epsilon = double.Epsilon) : IEqualityComparer<double>
{
    public bool Equals(double x, double y)
    {
        return Math.Abs(x - y) <= epsilon;
    }

    public int GetHashCode(double obj)
    {
        return obj.GetHashCode();
    }
}