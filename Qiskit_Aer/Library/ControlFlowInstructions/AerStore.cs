using System;

public class AerStore : Instruction
{
    /// <summary>
    /// Store instruction for Aer to work around transpilation issues
    /// of qiskit.circuit.Store.
    /// </summary>
    public bool IsDirective { get; private set; } = true;

    /// <summary>
    /// The store operation associated with this instruction.
    /// </summary>
    public StoreOperation Store { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AerStore"/> class.
    /// </summary>
    /// <param name="numQubits">The number of qubits involved in the store operation.</param>
    /// <param name="numClbits">The number of classical bits involved in the store operation.</param>
    /// <param name="store">The store operation containing lvalue and rvalue.</param>
    public AerStore(int numQubits, int numClbits, StoreOperation store)
        : base("aer_store", numQubits, numClbits, new object[] { store.LValue, store.RValue })
    {
        Store = store;
    }
}
