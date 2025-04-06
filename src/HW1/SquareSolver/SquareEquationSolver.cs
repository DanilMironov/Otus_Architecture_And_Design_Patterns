namespace HW1.SquareSolver;

public class SquareEquationSolver
{
    /// <summary>
    /// Нахождение корней квадратного уравнения
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    public double[] Solve(double a, double b, double c, double epsilon = double.Epsilon)
    {
        if (ArgumentsAreIncorrect(a, b, c, epsilon)) throw new ArgumentException("Arguments are incorrect");
        
        if (Math.Abs(a) < epsilon) throw new ArgumentException("Коэффициент 'a' не может быть равен 0");
        
        var discriminant = b * b - 4 * a * c;
        
        if (discriminant < -epsilon) return [];

        if (Math.Abs(discriminant) <= epsilon)
        {
            return [-b/(2*a), -b/(2*a)];
        }

        return [(-b+Math.Sqrt(discriminant))/(2*a), (-b-Math.Sqrt(discriminant))/(2*a)];
    }

    private static bool ArgumentsAreIncorrect(double a, double b, double c, double epsilon)
    {
        return a is double.NaN || a is double.NegativeInfinity || a is double.PositiveInfinity
            || b is double.NaN || b is double.NegativeInfinity || b is double.PositiveInfinity
            || c is double.NaN || c is double.NegativeInfinity || c is double.PositiveInfinity
            || epsilon is double.NaN || epsilon is double.NegativeInfinity;
    }
}