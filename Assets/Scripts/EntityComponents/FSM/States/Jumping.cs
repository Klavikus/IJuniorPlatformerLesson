using EntityComponents.Audio;
using UnityEngine;
using AudioType = EntityComponents.Audio.AudioType;

namespace EntityComponents.FSM.States
{
    public class Jumping : IState
    {
        private static readonly int IsJumpingHash = Animator.StringToHash("IsJumping");

        private readonly Animator _animator;
        private readonly AudioPlayer _audioPlayer;

        public Jumping(Animator animator, AudioPlayer audioPlayer)
        {
            _animator = animator;
            _audioPlayer = audioPlayer;
        }

        public void Tick()
        {
        }

        public void Enter()
        {
            _animator.SetBool(IsJumpingHash, true);
            _audioPlayer.Play(AudioType.Jump, isMainSource: true);
        }

        public void Exit()
        {
            _animator.SetBool(IsJumpingHash, false);
            _audioPlayer.Play(AudioType.Landing, isMainSource: true);
        }
    }
}