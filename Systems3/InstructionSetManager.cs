public class InstructionSetManager
{
    private InstructionSet defaultInstructionSet;

    public void SetDefaultInstructionSet(InstructionSet instructionSet)
    {
        defaultInstructionSet = instructionSet;
    }

    public InstructionSet GetDefaultInstructionSet()
    {
        return defaultInstructionSet;
    }
}
