using EntityComponents.Audio;
using UnityEngine;
using AudioType = EntityComponents.Audio.AudioType;

namespace EntityComponents.FSM.States
{
    public class Dying : IState
    {
        private static readonly int DyingHash = Animator.StringToHash("Dying");

        private readonly Animator _animator;
        private readonly AudioPlayer _audioPlayer;

        public Dying(Animator animator, AudioPlayer audioPlayer)
        {
            _animator = animator;
            _audioPlayer = audioPlayer;
        }

        public void Tick()
        {
        }

        public void Enter()
        {
            _animator.SetBool(DyingHash, true);
            _audioPlayer.Play(AudioType.Dying, isMainSource: true);
        }

        public void Exit()
        {
            _animator.SetBool(DyingHash, false);
        }
    }
}