using System;
using _Project.Scripts.PlayerLogic.AttackLogic;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class InputService : MonoBehaviour
    {
	    public event Action AttackButtonPressed;
	    public event Action<WeaponType> WeaponChangeRequested;


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

		    if (Input.GetKeyDown(KeyCode.Alpha1))
		    {
			    WeaponChangeRequested?.Invoke(WeaponType.Melee);
		    }
		    else if (Input.GetKeyDown(KeyCode.Alpha2))
		    {
			    WeaponChangeRequested?.Invoke(WeaponType.Ranged);
		    }
	    }
    }
}
