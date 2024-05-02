using System;
using UnityEngine;

namespace _Project.Scripts.UI.Panels
{
    public class MainMenuPanel : Panel
    {
        public event Action OnPlayerAnyKeyDown;
        
        private void Update()
        {
            if (Input.anyKeyDown) 
            {
                OnPlayerAnyKeyDown?.Invoke();
            }
        }
    }
}
