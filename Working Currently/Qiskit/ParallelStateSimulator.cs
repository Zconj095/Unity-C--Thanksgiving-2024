using System.Threading.Tasks;

public class ParallelStateSimulator : QuantumSimulator
{
    public ParallelStateSimulator(QuantumCircuit circuit, QuantumVisualizer visualizer)
        : base(circuit, visualizer) { }

    public override void Simulate()
    {
        Parallel.ForEach(Circuit.Gates, gate =>
        {
            state.ApplyGate(gate); // Directly access the protected state field
        });

        UnityEngine.Debug.Log("Parallel simulation complete.");
    }
}
