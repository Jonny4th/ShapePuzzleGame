using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Command
{
    public class CommandManager: MonoBehaviour
    {
        public static CommandManager instance;

        public List<ICommand> commands = new();
    }
}
