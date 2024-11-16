using System;
using System.Collections.Generic;
using UnityEngine;

public class ConsolidateBlocks : MonoBehaviour
{
    private bool forceConsolidate;
    private HashSet<string> basisGates;
    private TwoQubitBasisDecomposer decomposer; // Assuming this class exists to handle decomposition
    private Target target;

    private static readonly Dictionary<string, Gate> KAK_GATE_NAMES = new Dictionary<string, Gate>
    {
        { "cx", new CXGate() },
        { "cz", new CZGate() },
        { "iswap", new iSwapGate() },
        { "ecr", new ECRGate() }
    };

    public ConsolidateBlocks(
        Gate kakBasisGate = null,
        bool forceConsolidate = false,
        List<string> basisGates = null,
        float approximationDegree = 1.0f,
        Target target = null
    )
    {
        this.basisGates = new HashSet<string>(basisGates ?? new List<string>());
        this.forceConsolidate = forceConsolidate;
        this.target = target;

        if (kakBasisGate != null)
        {
            this.decomposer = new TwoQubitBasisDecomposer(kakBasisGate);
        }
        else if (basisGates != null)
        {
            var kakGates = new List<string>(KAK_GATE_NAMES.Keys);
            kakGates.IntersectWith(basisGates);
            if (kakGates.Count > 0)
            {
                this.decomposer = new TwoQubitBasisDecomposer(KAK_GATE_NAMES[kakGates[0]], approximationDegree);
            }
            else
            {
                this.decomposer = new TwoQubitBasisDecomposer(new CXGate());
            }
        }
        else
        {
            this.decomposer = new TwoQubitBasisDecomposer(new CXGate());
        }
    }
}
