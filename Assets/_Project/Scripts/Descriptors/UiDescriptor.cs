using _Project.Scripts.UI.Panels;
using UnityEngine;

namespace _Project.Scripts.Descriptors
{
    [CreateAssetMenu(fileName = "UiDescriptor", menuName = "Descriptors/UiDescriptor", order = 0)]
    public class UiDescriptor : ScriptableObject
    {
        public MainMenuPanel MainMenuPanelPrefab;
        public GameOverPanel GameOverPanelPrefab;
    }
}