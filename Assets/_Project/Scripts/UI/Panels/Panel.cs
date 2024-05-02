using UnityEngine;

namespace _Project.Scripts.UI.Panels
{
    public abstract class Panel : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
