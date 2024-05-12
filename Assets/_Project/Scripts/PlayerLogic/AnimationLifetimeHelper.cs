using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
	public class AnimationLifetimeHelper : MonoBehaviour
	{
		public event Action OnAttackAnimationEnd;

		// Call from animator
		public void InvokeAttackAnimationEnd()
		{
			OnAttackAnimationEnd?.Invoke();
		}
	}
}
