using System;
using _Project.Scripts.Descriptors;
using UnityEngine;

namespace _Project.Scripts.Services
{
	public class LightingManager : MonoBehaviour
	{
		 [SerializeField] private Light DirectionalLight;
		 [SerializeField] private LightingPreset Preset;
		
		 //Variables
		 [SerializeField, Range(0, 24)] private float TimeOfDay;
		 [SerializeField] private float dayDurationInSeconds = 600f; // Продолжительность дня в секундах

		 private bool _isNightFallsInvoked = false;
		 private bool _isMorningComesInvoked = false;
		 
		 public event Action OnNightFalls; 
		 public event Action OnMorningComes; 
  
		 private void Update()
		 {
			 if (Preset == null)
				 return;

			 if (Application.isPlaying)
			 {
				 // Увеличиваем время с учетом продолжительности дня
				 TimeOfDay += (Time.deltaTime / dayDurationInSeconds) * 24;
				 TimeOfDay %= 24; // Modulus to ensure always between 0-24

				 // Проверяем, наступило ли время 20:00
				 if ((TimeOfDay >= 20f || TimeOfDay < 8f) && !_isNightFallsInvoked)
				 {
					 OnNightFalls?.Invoke();
					 _isNightFallsInvoked = true;
					 _isMorningComesInvoked = false;
				 }

				 // Проверяем, наступило ли время 8:00
				 if (TimeOfDay >= 8f && TimeOfDay < 20f && !_isMorningComesInvoked)
				 {
					 OnMorningComes?.Invoke();
					 _isMorningComesInvoked = true;
					 _isNightFallsInvoked = false;
				 }

				 UpdateLighting(TimeOfDay / 24f);
			 }
			 else
			 {
				 UpdateLighting(TimeOfDay / 24f);
			 }
		 }
  
		 private void UpdateLighting(float timePercent)
		 {
		 	//Set ambient and fog
		 	RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
		 	RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
  
		 	//If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
		 	if (DirectionalLight != null)
		 	{
		 		DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
		 		DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
		 	}
		 }
		
		 //Try to find a directional light to use if we haven't set one
        private void OnValidate()
        {
            if (DirectionalLight != null)
                return;
       
            //Search for lighting tab sun
            if (RenderSettings.sun != null)
            {
                DirectionalLight = RenderSettings.sun;
            }
            //Search scene for light that fits criteria (directional)
            else
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        DirectionalLight = light;
                        return;
                    }
                }
            }
        }
	}
}