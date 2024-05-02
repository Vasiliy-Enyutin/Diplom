using System;
using UnityEngine;

namespace _Project.Scripts.UI.Panels
{
    public class LevelWinPanel : Panel
    {
        public event Action OnContinueKeyDown;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                OnContinueKeyDown?.Invoke();
            }
        }
    }
}
