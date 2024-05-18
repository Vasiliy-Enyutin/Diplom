using System.Collections;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public class RangedWeapon : WeaponBase
	{
		[SerializeField] private Transform _shootingPoint;
		[SerializeField] private float _tracerWidth;
		[SerializeField] private float _tracerDuration;
		[SerializeField] private Color _tracerColor;

		protected override void PerformAttack()
		{
			RaycastHit hit;
			Vector3 tracerEndPoint;

			if (Physics.Raycast(_shootingPoint.position, _shootingPoint.up, out hit, _weaponData.Range))
			{
				if (hit.collider.TryGetComponent(out IDamageable damageable))
				{
					damageable.TakeDamage(_weaponData.Damage);
				}
				tracerEndPoint = hit.point;
			}
			else
			{
				tracerEndPoint = _shootingPoint.position + _shootingPoint.up * _weaponData.Range;
			}

			StartCoroutine(DrawTracer(_shootingPoint.position, tracerEndPoint));
		}

		private IEnumerator DrawTracer(Vector3 startPoint, Vector3 endPoint)
		{
			GameObject tracerObject = new("Tracer");
			LineRenderer lineRenderer = tracerObject.AddComponent<LineRenderer>();
			lineRenderer.startWidth = _tracerWidth;
			lineRenderer.endWidth = _tracerWidth;
			lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
			lineRenderer.material.color = _tracerColor;
			lineRenderer.SetPosition(0, startPoint);
			lineRenderer.SetPosition(1, endPoint);

			yield return new WaitForSeconds(_tracerDuration);

			Destroy(tracerObject);
		}
	}

}