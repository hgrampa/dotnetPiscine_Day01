using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using day01_ex01.Events;

namespace day01_ex01
{
    public class Task
    {
        private readonly List<Event> _eventsHistory;
        
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskType Type { get; set; }
        public TaskPriority Priority { get; set; }

        public TaskState State => _eventsHistory.Last().State;

        public Task()
        {
            _eventsHistory = new List<Event> {new CreatedEvent()};
            Priority = TaskPriority.Normal;
            DueDate = null;
        }
        
        public void Complete()
        {
            ApplyEvent(new TaskDoneEvent());
        }

        public void Discard()
        {
            ApplyEvent(new TaskWontDoEvent());
        }

        public void ApplyEvent(Event @event)
        {
            if (StateTransactionAllowed(@event.State))
            {
                _eventsHistory.Add(@event);
            }
        }

        // TODO find optimal way to wright enums
        public override string ToString()
        {
            var str = Summary.Length > 0 ? 
                $"{Title}\n[{Type.ToString()}][{State.ToString()}]\nPriority: {Priority.ToString()}, Due till: {DueDate.ToString()}" : 
                $"{Title}\n[{Type.ToString()}][{State.ToString()}]\nPriority: {Priority.ToString()}, Due till: {DueDate.ToString()}\n{Summary}";
            return str;
        }

        private bool StateTransactionAllowed(TaskState eventState)
        {
            var currentState = State;
            
            switch (currentState)
            {
                case TaskState.New:
                    if (eventState == TaskState.New)
                        return false;
                    break;
                
                case TaskState.Completed:
                    return false;
                
                case TaskState.OutOfDate:
                    return false;
                
                default:
                    return false;
            }

            return true;
        }
        
    }
}