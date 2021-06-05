namespace day01_ex01.Events
{
    // The record name is awful, but the subject requires it.
    public record CreatedEvent : Event
    {
        public CreatedEvent()
        {
            State = TaskState.New;
        }
    }
}