using System.Collections;
using System.Text;

namespace OOP6;

public unsafe class Matrix : IFormattable, IEnumerable<double>, ICloneable, IComparable<Matrix>
{
    private static int _idCounter = 0;

    private uint _rows;
    private uint _cols;
    private double[][] _data;
    private int _id;

    public uint Rows { get => _rows; }
    public uint Cols { get => _cols; }

    public Matrix(uint otherSize, double[][] values) 
        : this(otherSize, otherSize, values)
    { }

    public Matrix(Matrix other)
        : this(other._rows, other._cols, other._data)
    { }

    public Matrix(uint rows = 0, uint cols = 0, double[][]? values = null)
    {
        _rows = rows;
        _cols = cols;

        _data = new double[rows][];

        for (int i = 0; i < rows; i++)
        {
            _data[i] = new double[cols];
        }

        if (values != null)
        {
            _data = values;
        }

        _id = _idCounter++;

        Console.WriteLine($"Constructor for matrix {_id}");
    }

    ~Matrix() 
    {
        Console.WriteLine($"Distructor for matrix {_id}");
    }

    public uint getRows()
    {
        return _rows;
    }

    public uint getCols()
    {
        return _cols;
    }

    public void setRows(uint rows)
    {
        _rows = rows;
    }

    public void setCols(uint cols) 
    {
        _cols = cols;
    }

    public void setData(double[][] data)
    {
        _data = data;
    }

    public double getValue(uint index) 
    {
        return _data[index / _cols][index % _cols];
    }

    public double max()
    {
        if (_rows <= 0 || _cols <= 0 || _data == null)
            throw new InvalidDataException("Can not get max value from empty matrix");

        var maxVal = _data[0][0];

        for (uint i = 1; i < _rows * _cols; i++)
        {
            if (maxVal < getValue(i))
            {
                maxVal = getValue(i);
            }
        }

        return maxVal;
    }

    public double min()
    {
        if (_rows <= 0 || _cols <= 0 || _data == null)
            throw new InvalidDataException("Can not get min value from empty matrix");

        var minVal = _data[0][0];

        for (uint i = 1; i < _rows * _cols; i++)
        {
            if (minVal > getValue(i))
            {
                minVal = getValue(i);
            }
        }

        return minVal;
    }

    static public bool canMul(Matrix first, Matrix second)
    {
        return first.getRows() == second.getCols();
    }

    static public bool canSum(Matrix first, Matrix second)
    {
        return first.getRows() == second.getRows() && first.getCols() == second.getCols();
    }

    public static Matrix operator +(Matrix first, Matrix second)
    {
        if (!canSum(first, second))
            throw new ArgumentException("Matrices have different dimensions");

        uint rows = first.getRows();
        uint cols = first.getCols();


        for (uint i = 0; i < rows; i++)
        {
            for (uint j = 0; j < cols; j++)
            {
                first[i][j] += second[i][j];
            }
        }

        return first;
    }

    public static Matrix operator -(Matrix first, Matrix second)
    {
        if (!canSum(first, second))
            throw new ArgumentException("Matrices have different dimensions");

        uint rows = first.getRows();
        uint cols = first.getCols();


        for (uint i = 0; i < rows; i++)
        {
            for (uint j = 0; j < cols; j++)
            {
                first[i][j] -= second[i][j];
            }
        }

        return first;
    }

    public static Matrix operator *(Matrix first, Matrix second)
    {
        if (!canMul(first, second))
            throw new ArgumentException("Columns of the first matrix must be equal to rows of the second matrix");

        uint rows = first.getRows();
        uint cols = second.getCols();

        var sum = 0.0;
        var resultData = new double[rows][];

        for (int i = 0; i < rows; i++)
        {
            resultData[i] = new double[cols];
        }

        for (uint i = 0; i < rows; i++)
        {
            for (uint j = 0; j < cols; j++)
            {
                for (uint k = 0; k < cols; k++)
                    sum += first[i][k] * second[k][j];
                resultData[i][j] = sum;
                sum = 0.0;
            }
        }

        first.setCols(cols);
        first.setRows(rows);
        first.setData(resultData);

        return first;
    }

    public static Matrix operator *(Matrix first, int scalar)
    {
        uint rows = first.getRows();
        uint cols = first.getCols();


        for (uint i = 0; i < rows; i++)
        {
            for (uint j = 0; j < cols; j++)
            {
                first[i][j] *= scalar;
            }
        }

        return first;
    }

    public double[] this[uint index]
    {
        get
        {
            if (index >= _rows)
                throw new IndexOutOfRangeException("Index out of range");

            return _data[index];
        }

        set
        {
            if (index >= _rows)
                throw new IndexOutOfRangeException("Index out of range");

            _data[index] = value;
        }
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public IEnumerator<double> GetEnumerator()
    {
        for (uint i = 0; i < _rows; i++)
        {
            for (uint j = 0; j < _cols; j++)
            {
                yield return _data[i][j];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int CompareTo(Matrix? other)
    {
        if (!(other is Matrix))
            throw new ArgumentException("Incorrect type");

        uint thisSize = getCols() * getRows();
        uint otherSize = other.getCols() * other.getRows();

        return thisSize.CompareTo(otherSize);
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format == null)
            format = "{0,5}";

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Matrix {_id}:");

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                sb.AppendFormat(format, _data[i][j]);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
