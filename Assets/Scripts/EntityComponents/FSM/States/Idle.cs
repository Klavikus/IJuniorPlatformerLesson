using EntityComponents.Audio;

namespace EntityComponents.FSM.States
{
    public class Idle : IState
    {
        private readonly AudioPlayer _audioPlayer;

        public Idle(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public void Tick()
        {
        }

        public void Enter()
        {
            _audioPlayer.Stop(isMainSource: true);
        }

        public void Exit()
        {
        }
    }
}