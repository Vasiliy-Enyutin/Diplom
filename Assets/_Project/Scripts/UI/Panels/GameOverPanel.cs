using System;
using UnityEngine;

namespace _Project.Scripts.UI.Panels
{
    public class GameOverPanel : Panel
    {
        public event Action OnRestartKeyDown;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) 
            {
                OnRestartKeyDown?.Invoke();
            }
        }
    }
}
