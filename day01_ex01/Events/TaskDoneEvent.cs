namespace day01_ex01.Events
{
    // The record name is awful, but the subject requires it.
    public record TaskDoneEvent : Event
    {
        public TaskDoneEvent()
        {
            State = TaskState.Completed;
        }
    }
}