using OOP6;

public class Program
{
    public static void Main(string[] args)
    {
        double[] data = [1, 2, 3, 4];

        var matrix = new Matrix(2, 2, data);
        var matrix2 = new Matrix(matrix);
        Console.WriteLine(Matrix.canMul(matrix, matrix2));
    }
}