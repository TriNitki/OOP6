using System.Collections;

namespace OOP6;

public unsafe class Matrix : IEnumerable<Matrix>, ICloneable
{
    private int _rows { get; set; }
    private int _cols { get; set; }
    private double[] _data { get; set; }
    private int _id { get; init; }

    private class Row
    {
        private double* _data { get; set; }
        private int _size { get; init; }

        public Row(int size, double* values)
        {
            _size = size;
            _data = values;
        }

        public double this[int col]
        {
            get
            {
                return _data[col];
            }

            set
            {
                _data[col] = value;
            }
        }
    }

    public Matrix(int otherSize, double[] values) 
        : this(otherSize, otherSize, values)
    { }

    public Matrix(Matrix other)
        : this(other._rows, other._cols, other._data)
    { }

    public Matrix(int rows = 0, int cols = 0, double[]? values = null)
    {
        if (rows < 0)
            throw new ArgumentOutOfRangeException(nameof(rows), "Number of rows cannot be less than 0");

        if (cols < 0)
            throw new ArgumentOutOfRangeException(nameof(cols), "Number of columns cannot be less than 0");

        _rows = rows;
        _cols = cols;

        _data = new double[rows * cols];

        if (values != null)
        {
            for (int i = 0; i < rows * cols; i++)
            {
                _data[i] = values[i];
            }
        }

        _id = Global.Counter;
        Console.WriteLine($"Constructor for matrix {_id}");
    }

    ~Matrix() 
    {
        Console.WriteLine($"Distructor for matrix {_id}");
    }

    public object Clone()
    {
        throw new NotImplementedException();
    }

    public IEnumerator<Matrix> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public int getRows()
    {
        return _rows;
    }

    public int getCols()
    {
        return _cols;
    }

    public double max()
    {
        if (_rows <= 0 || _cols <= 0 || _data == null)
            throw new InvalidDataException("Can not get max value from empty matrix");

        var maxVal = _data[0];

        for (int i = 1; i < _rows * _cols; i++)
        {
            if (maxVal < _data[i])
            {
                maxVal = _data[i];
            }
        }

        return maxVal;
    }

    public double min()
    {
        if (_rows <= 0 || _cols <= 0 || _data == null)
            throw new InvalidDataException("Can not get min value from empty matrix");

        var minVal = _data[0];

        for (int i = 1; i < _rows * _cols; i++)
        {
            if (minVal > _data[i])
            {
                minVal = _data[i];
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

    public double this[int row]
    {
        get
        {
            return Row(_cols, &_data[row * _cols]);
        }

        //set
        //{
        //    _data[row] = value;
        //}
    }

    public override string ToString()
    {
        string output = string.Empty; 

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                output += string.Format("{0, 5}", _data[(i * _cols) + j]);
            }
            output += "\n";
        }

        return output;
    }
}
