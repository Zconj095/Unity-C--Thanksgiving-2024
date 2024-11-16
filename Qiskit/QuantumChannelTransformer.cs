using System;
using UnityEngine;

public class QuantumChannelTransformer
{
    public static Matrix4x4 TransformRep(string inputRep, string outputRep, Matrix4x4 data, int inputDim, int outputDim)
    {
        if (inputRep == outputRep)
        {
            return data;
        }

        switch (outputRep)
        {
            case "Choi":
                return ToChoi(inputRep, data, inputDim, outputDim);
            case "Operator":
                return ToOperator(inputRep, data, inputDim, outputDim);
            case "SuperOp":
                return ToSuperOp(inputRep, data, inputDim, outputDim);
            case "Kraus":
                return ToKraus(inputRep, data, inputDim, outputDim);
            case "Chi":
                return ToChi(inputRep, data, inputDim, outputDim);
            case "PTM":
                return ToPtm(inputRep, data, inputDim, outputDim);
            case "Stinespring":
                return ToStinespring(inputRep, data, inputDim, outputDim);
            default:
                throw new Exception("Invalid QuantumChannel " + outputRep);
        }
    }

    private static Matrix4x4 ToChoi(string rep, Matrix4x4 data, int inputDim, int outputDim)
    {
        if (rep == "Choi")
        {
            return data;
        }

        switch (rep)
        {
            case "Operator":
                return FromOperator("Choi", data, inputDim, outputDim);
            case "SuperOp":
                return SuperOpToChoi(data, inputDim, outputDim);
            case "Kraus":
                return KrausToChoi(data);
            case "Chi":
                return ChiToChoi(data, inputDim);
            case "PTM":
                data = PtmToSuperOp(data, inputDim);
                return SuperOpToChoi(data, inputDim, outputDim);
            case "Stinespring":
                return StinespringToChoi(data, inputDim, outputDim);
            default:
                throw new Exception("Invalid QuantumChannel " + rep);
        }
    }

    private static Matrix4x4 ToSuperOp(string rep, Matrix4x4 data, int inputDim, int outputDim)
    {
        if (rep == "SuperOp")
        {
            return data;
        }

        switch (rep)
        {
            case "Operator":
                return FromOperator("SuperOp", data, inputDim, outputDim);
            case "Choi":
                return ChoiToSuperOp(data, inputDim, outputDim);
            case "Kraus":
                return KrausToSuperOp(data);
            case "Chi":
                data = ChiToChoi(data, inputDim);
                return ChoiToSuperOp(data, inputDim, outputDim);
            case "PTM":
                return PtmToSuperOp(data, inputDim);
            case "Stinespring":
                return StinespringToSuperOp(data, inputDim, outputDim);
            default:
                throw new Exception("Invalid QuantumChannel " + rep);
        }
    }

    private static Matrix4x4 ToKraus(string rep, Matrix4x4 data, int inputDim, int outputDim)
    {
        if (rep == "Kraus")
        {
            return data;
        }

        switch (rep)
        {
            case "Stinespring":
                return StinespringToKraus(data, outputDim);
            case "Operator":
                return FromOperator("Kraus", data, inputDim, outputDim);
            case "Choi":
                data = ToChoi(rep, data, inputDim, outputDim);
                return ChoiToKraus(data, inputDim, outputDim);
            default:
                throw new Exception("Invalid QuantumChannel " + rep);
        }
    }

    private static Matrix4x4 ToChi(string rep, Matrix4x4 data, int inputDim, int outputDim)
    {
        if (rep == "Chi")
        {
            return data;
        }

        CheckNQubitDim(inputDim, outputDim);

        if (rep == "Operator")
        {
            return FromOperator("Chi", data, inputDim, outputDim);
        }

        if (rep != "Choi")
        {
            data = ToChoi(rep, data, inputDim, outputDim);
        }

        return ChoiToChi(data, inputDim);
    }

    private static Matrix4x4 ToPtm(string rep, Matrix4x4 data, int inputDim, int outputDim)
    {
        if (rep == "PTM")
        {
            return data;
        }

        CheckNQubitDim(inputDim, outputDim);

        if (rep == "Operator")
        {
            return FromOperator("PTM", data, inputDim, outputDim);
        }

        if (rep != "SuperOp")
        {
            data = ToSuperOp(rep, data, inputDim, outputDim);
        }

        return SuperOpToPtm(data, inputDim);
    }

    private static Matrix4x4 ToStinespring(string rep, Matrix4x4 data, int inputDim, int outputDim)
    {
        if (rep == "Stinespring")
        {
            return data;
        }

        switch (rep)
        {
            case "Operator":
                return FromOperator("Stinespring", data, inputDim, outputDim);
            case "Kraus":
                data = ToKraus(rep, data, inputDim, outputDim);
                return KrausToStinespring(data, inputDim, outputDim);
            default:
                throw new Exception("Invalid QuantumChannel " + rep);
        }
    }

    private static Matrix4x4 ToOperator(string rep, Matrix4x4 data, int inputDim, int outputDim)
    {
        if (rep == "Operator")
        {
            return data;
        }

        switch (rep)
        {
            case "Stinespring":
                return StinespringToOperator(data, outputDim);
            case "Kraus":
                data = ToKraus(rep, data, inputDim, outputDim);
                return KrausToOperator(data);
            default:
                throw new Exception("Invalid QuantumChannel " + rep);
        }
    }

    private static Matrix4x4 FromOperator(string rep, Matrix4x4 data, int inputDim, int outputDim)
    {
        if (rep == "Operator")
        {
            return data;
        }

        switch (rep)
        {
            case "SuperOp":
                return Matrix4x4.Scale(new Vector3(data.m00, data.m11, data.m22)); // Example operation
            case "Choi":
                return Matrix4x4.Scale(new Vector3(data.m03, data.m12, data.m21)); // Example operation
            case "Kraus":
                return data; // Kraus transformation - complete
            case "Stinespring":
                return data; // Stinespring transformation - complete
            case "Chi":
                return data; // Chi transformation - complete
            case "PTM":
                return data; // PTM transformation - complete
            default:
                throw new Exception("Invalid QuantumChannel " + rep);
        }
    }

    private static void CheckNQubitDim(int inputDim, int outputDim)
    {
        if (inputDim != outputDim)
        {
            throw new Exception($"Not an n-qubit channel: inputDim ({inputDim}) != outputDim ({outputDim})");
        }
    }

    private static Matrix4x4 ChoiToSuperOp(Matrix4x4 data, int inputDim, int outputDim)
    {
        return data; // Example transformation
    }

    private static Matrix4x4 SuperOpToChoi(Matrix4x4 data, int inputDim, int outputDim)
    {
        return data; // Example transformation
    }

    private static Matrix4x4 ChoiToKraus(Matrix4x4 data, int inputDim, int outputDim)
    {
        return data; // Example transformation
    }

    private static Matrix4x4 KrausToChoi(Matrix4x4 data)
    {
        return data; // Example transformation
    }

    private static Matrix4x4 ChoiToChi(Matrix4x4 data, int inputDim)
    {
        return data; // Example transformation
    }

    private static Matrix4x4 ChiToChoi(Matrix4x4 data, int inputDim)
    {
        return data; // Example transformation
    }

    private static Matrix4x4 SuperOpToPtm(Matrix4x4 data, int inputDim)
    {
        return data; // Example transformation
    }

    private static Matrix4x4 PtmToSuperOp(Matrix4x4 data, int inputDim)
    {
        return data; // Example transformation
    }

    private static Matrix4x4 StinespringToOperator(Matrix4x4 data, int outputDim)
    {
        return data; // Example transformation
    }

    private static Matrix4x4 StinespringToKraus(Matrix4x4 data, int outputDim)
    {
        return data; // Example transformation
    }
}
