using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Command
{
    public class CommandManager : MonoBehaviour
    {
        private List<ICommand> commandHistory = new();

        private int m_currentIndex;

        void Awake()
        {
            m_currentIndex = -1;
        }

        public void ResetHistory()
        {
            commandHistory.Clear();
        }

        public void Undo(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            if(m_currentIndex == -1 || commandHistory.Count < 0) return;

            commandHistory[m_currentIndex].Undo();
            m_currentIndex--;

            if(m_currentIndex < -1) m_currentIndex = -1;
        }

        public void Redo(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            if(m_currentIndex == commandHistory.Count - 1) return;

            m_currentIndex++;
            commandHistory[m_currentIndex].Execute();
        }

        private void AddCommand(ICommand command)
        {
            command.Execute();

            if(commandHistory.Count == 0 || m_currentIndex == commandHistory.Count - 1)
            {
                m_currentIndex = commandHistory.Count == 0 ? 0 : m_currentIndex + 1;
                commandHistory.Add(command);
                return;
            }
            else
            {
                m_currentIndex++;
                commandHistory.RemoveRange(m_currentIndex, commandHistory.Count - m_currentIndex);
                commandHistory.Add(command);
                return;
            }
        }

        public void OnCommandReceived(Component sender, object data)
        {
            if(data is not ICommand command) return;
            AddCommand(command);
        }
    }
}
