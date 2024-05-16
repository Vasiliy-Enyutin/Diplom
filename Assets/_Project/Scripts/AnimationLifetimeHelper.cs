using System;
using UnityEngine;

namespace _Project.Scripts
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
