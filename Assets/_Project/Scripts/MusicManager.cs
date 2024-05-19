using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] _idleAudioClips;

        private AudioSource _audioSource;
        private bool _isPaused = false;

        private PursuitMusicController _pursuitMusicController;

        [Inject]
        private void Construct(PursuitMusicController pursuitMusicController)
        {
	        _pursuitMusicController = pursuitMusicController;
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
            _audioSource.clip = null;
            PlayIdleMusic();
        }

        private void Update()
        {
            if (_pursuitMusicController.IsPlaying)
            {
                _isPaused = true;
                _audioSource.Pause();
            }
            else if (_isPaused && !_pursuitMusicController.IsPlaying)
            {
                _isPaused = false;
                _audioSource.Play();
            }
            
            if (!_isPaused && !_audioSource.isPlaying)
            {
                PlayIdleMusic();
            }
        }

        private void PlayIdleMusic()
        {
            _audioSource.clip = GetRandomClip(_idleAudioClips);
            _audioSource.Play();
        }

        private AudioClip GetRandomClip(AudioClip[] clips)
        {
            if (clips.Length == 0)
            {
                return null;
            }

            int randomIndex = Random.Range(0, clips.Length);
            return clips[randomIndex];
        }
    }
}