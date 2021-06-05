namespace day01_ex01.Events
{
    public abstract record Event
    {
        public TaskState State { get; init; }
    }
}