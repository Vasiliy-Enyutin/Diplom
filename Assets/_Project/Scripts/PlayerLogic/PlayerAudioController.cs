using _Project.Scripts.Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.PlayerLogic
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudioController : MonoBehaviour
    {
        [SerializeField]
        public AudioClip[] _footstepsAudioClips;
        
        [Inject]
        private InputService _inputService = null!;
        
        private AudioSource _audioSource;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            UpdateFootstepSound(_inputService.MoveDirection);
        }

        private void UpdateFootstepSound(Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero || _footstepsAudioClips.Length <= 0 || _audioSource.isPlaying)
            {
                return;
            }
            
            int randomIndex = Random.Range(0, _footstepsAudioClips.Length);
            AudioClip audioClip = _footstepsAudioClips[randomIndex];
            _audioSource.PlayOneShot(audioClip);
        }
    }
}