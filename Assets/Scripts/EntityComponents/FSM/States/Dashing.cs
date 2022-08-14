using EntityComponents.Audio;
using UnityEngine;
using AudioType = EntityComponents.Audio.AudioType;

namespace EntityComponents.FSM.States
{
    public class Dashing : IState
    {
        private static readonly int IsDashingHash = Animator.StringToHash("IsDashing");

        private readonly Animator _animator;
        private readonly AudioPlayer _audioPlayer;

        public Dashing(Animator animator, AudioPlayer audioPlayer)
        {
            _animator = animator;
            _audioPlayer = audioPlayer;
        }

        public void Tick()
        {
        }

        public void Enter()
        {
            _animator.SetBool(IsDashingHash, true);
            _audioPlayer.Play(AudioType.Dashing, isMainSource: true);
        }

        public void Exit()
        {
            _animator.SetBool(IsDashingHash, false);
        }
    }
}