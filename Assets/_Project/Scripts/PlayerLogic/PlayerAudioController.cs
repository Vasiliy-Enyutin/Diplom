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
        private PlayerInputService _playerInputService = null!;
        
        private AudioSource _audioSource;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            UpdateFootstepSound(_playerInputService.MoveDirection);
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

        public void ConstructTest(Vector3 moveDirection, AudioSource audioSource = null, AudioClip[] audioClip = null)
        {
            _audioSource = audioSource;
            _footstepsAudioClips = audioClip;
            
            UpdateFootstepSound(moveDirection);
        }
    }
}