using System;
using _Project.Scripts.PlayerLogic.AttackLogic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Services
{
    public class InputService : MonoBehaviour
    {
	    private Vector3 _moveDirection = Vector3.zero;
	    
	    public event Action AttackButtonPressed;
	    public event Action<WeaponType> WeaponChangeRequested;


	    public Vector3 MoveDirection
	    {
		    get { return _moveDirection; }
		    set { _moveDirection = value; }
	    }

	    private void Update()
	    {
		    _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		    
		    if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
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
