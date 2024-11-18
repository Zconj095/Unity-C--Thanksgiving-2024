using System;
using System.Collections.Generic;
using System.Linq;

public class DataBin
{
    public double[,] ExpectationValues { get; set; }
    public double[,] StandardDeviations { get; set; }
    public int RowCount { get; set; }
    public int ColumnCount { get; set; }

    public DataBin(double[,] expectationValues, double[,] stdDevs, int rowCount, int colCount)
    {
        ExpectationValues = expectationValues;
        StandardDeviations = stdDevs;
        RowCount = rowCount;
        ColumnCount = colCount;
    }
}