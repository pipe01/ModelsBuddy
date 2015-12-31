using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsBuddy
{
    class CommandManager
    {
        private Dictionary<string, Func<string[], bool>> Commands = new Dictionary<string, Func<string[], bool>>();

        public void RegisterCommand(string cmd, Func<string[], bool> action)
        {
            Commands.Add(cmd, action);
        }

        public bool ExecuteCommand(string cmd, string[] args)
        {
            if (!Commands.ContainsKey(cmd)) return false;

            return Commands[cmd].Invoke(args);
        }
    }
}
