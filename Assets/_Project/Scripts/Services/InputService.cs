using System;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class InputService : MonoBehaviour
    {
	    public event Action AttackButtonPressed;

	    public Vector3 MoveDirection
	    {
		    get { return new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")); }
	    }

	    private void Update()
	    {
		    if (Input.GetMouseButtonDown(0))
		    {
			    AttackButtonPressed?.Invoke();
		    }
	    }
    }
}
