using System;

namespace Cheats.Console
{
    public interface IDebugCammand
    {
        string CommandId { get; }
        string CommandDescrition { get; }
        string CommandFormat { get; }
    }

    public class DebugCommandBase : IDebugCammand
    {
        private string commandId;
        private string commandDescrition;
        private string commandFormat;

        public string CommandId => commandId;
        public string CommandDescrition => commandDescrition;
        public string CommandFormat => commandFormat;

        public DebugCommandBase(string id, string description, string format)
        {
            commandId = id;
            commandDescrition = description;
            commandFormat = format;
        }
    }

    public class DebugCommand : DebugCommandBase
    {
        private Action command;

        public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
        {
            this.command = command;
        }

        public void Invoke()
        {
            command.Invoke();
        }
    }

    public class DebugCommand<T1> : DebugCommandBase
    {
        private Action<T1> command;

        public DebugCommand(string id, string description, string format, Action<T1> command) : base(id, description, format)
        {
            this.command = command;
        }

        public void Invoke(T1 value)
        {
            command.Invoke(value);
        }
    }

}