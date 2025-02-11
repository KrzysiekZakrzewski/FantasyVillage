using System;

namespace Cheats.Console
{
    public class DebugCommandBase
    {
        public string CommandId { private set; get; }
        public string CommandDescrition { private set; get; }
        public string CommandFormat { private set; get; }

        public DebugCommandBase(string id, string description, string format)
        {
            CommandId = id;
            CommandDescrition = description;
            CommandFormat = format;
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