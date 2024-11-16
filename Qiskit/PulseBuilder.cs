public class PulseBuilder : IDisposable
{
    public void StartNewSchedule(string name) { }
    public void Call(Instruction instruction) { }
    public void Delay(int duration, int qubit) { }
    public void Play(Pulse pulse, Channel channel) { }
    public Schedule Build() { return new Schedule(); }
    public void Dispose() { }
}