using OOP6;

public class Program
{
    public static void Main(string[] args)
    {
        double[][] data = [[1, 2], [3, 4]];

        var matrix = new Matrix(2, 2, data);
        var matrix2 = new Matrix(matrix);
        matrix += matrix2;

        var matrix3 = matrix2.Clone();

        Console.WriteLine(matrix3);
        Console.WriteLine(matrix * matrix);
    }
}