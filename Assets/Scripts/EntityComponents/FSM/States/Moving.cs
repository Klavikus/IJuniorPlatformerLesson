using EntityComponents.Audio;
using EntityComponents.Movement;
using UnityEngine;
using AudioType = EntityComponents.Audio.AudioType;

namespace EntityComponents.FSM.States
{
    public class Moving : IState
    {
        private static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");

        private readonly Animator _animator;
        private readonly MoveController2D _moveController;
        private readonly AudioPlayer _audioPlayer;

        public Moving(Animator animator, MoveController2D moveController, AudioPlayer audioPlayer)
        {
            _animator = animator;
            _moveController = moveController;
            _audioPlayer = audioPlayer;
        }

        public void Tick()
        {
            _animator.SetFloat(MoveSpeedHash, _moveController.CurrentMoveSpeed);
        }

        public void Enter()
        {
            _audioPlayer.Play(AudioType.Move, isMainSource: true, isLooping: true);
        }

        public void Exit()
        {
            _animator.SetFloat(MoveSpeedHash, 0);
        }
    }
}