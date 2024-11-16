using System;
using System.Collections.Generic;

public class SaveMatrixProductState : SaveSingleData
{
    /// <summary>
    /// Save matrix product state instruction.
    /// </summary>
    public SaveMatrixProductState(int numQubits, string label = "matrix_product_state", bool pershot = false, bool conditional = false)
        : base("save_matrix_product_state", numQubits, label, pershot, conditional)
    {
        // This save instruction must always be performed on the full width of qubits in a circuit.
    }
}


