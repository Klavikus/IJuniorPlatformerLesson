using EntityComponents.Audio;
using UnityEngine;
using AudioType = EntityComponents.Audio.AudioType;

namespace EntityComponents.FSM.States
{
    public class Attacking : IState
    {
        private readonly Animator _animator;
        private readonly AudioPlayer _audioPlayer;

        private static readonly int MainAttackHash = Animator.StringToHash("MainAttack");
    
        public Attacking(Animator animator, AudioPlayer audioPlayer)
        {
            _animator = animator;
            _audioPlayer = audioPlayer;
        }

        public void Tick()
        {
        }

        public void Enter()
        {
            _animator.SetTrigger(MainAttackHash);
            _audioPlayer.Play(AudioType.MainAttack);
        }

        public void Exit()
        {
        }
    }
}