using System.Collections;
using System.Linq;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class PursuitMusicManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _pursuitAudioClip;
        [SerializeField]
        private float _pursuitAudioClipDuration;
        [SerializeField]
        private float _clipsFadeDuration;

        private ObjectsLocatorService _objectsLocatorService;

        private AudioSource _audioSource;
        private float _startTime = 0f;
        private float _startVolume;

        public bool IsPlaying { get; private set; } = false;

        [Inject]
        private void Construct(ObjectsLocatorService objectsLocatorService)
        {
	        _objectsLocatorService = objectsLocatorService;
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = true;
            _audioSource.playOnAwake = false;
            _startVolume = _audioSource.volume;
            _audioSource.clip = _pursuitAudioClip;
        }

        private void Update()
        {
            if (CheckEnemyPursuitState())
            {
                if (!IsPlaying)
                {
                    PlayPursuitMusic();
                }
                else
                {
                    _startTime = Time.time;
                }
            }
            else if (!CheckEnemyPursuitState() && IsPlaying)
            {
                if (Time.time - _startTime >= _pursuitAudioClipDuration)
                {
                    StartCoroutine(FadeOutAudio());
                }
            }
        }

        private bool CheckEnemyPursuitState()
        {
	        return _objectsLocatorService.Enemies != null && _objectsLocatorService.Enemies.Any(enemy => enemy.IsPursuingPlayer);
        }

        private void PlayPursuitMusic()
        {
            IsPlaying = true;
            _audioSource.Play();
            _startTime = Time.time;
        }

        private IEnumerator FadeOutAudio()
        {
            float fadeDuration = _clipsFadeDuration;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                _audioSource.volume = Mathf.Lerp(_startVolume, 0f, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _audioSource.Stop();
            _audioSource.volume = _startVolume;
            IsPlaying = false;
        }
    }
}
