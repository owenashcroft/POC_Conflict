using System.Collections.Generic;
using System.Linq;

namespace TestFTP
{
    public abstract class BaseCommand
    {
        private readonly IEnumerable<string> _command;

        protected BaseCommand(IEnumerable<string> commands)
        {
            _command = commands;
        }
        protected BaseCommand(string command)
            : this(new[] { command })
        {
            
        }

        public bool Handled(string command)
        {
            return _command.Any(x => command.ToLower().StartsWith(x.ToLower()));
        }
    }
}