using EntityComponents.Attack;

namespace StateMachine
{
    public class Attack : IState
    {
        private readonly AttackController _attackController;

        public Attack(AttackController attackController)
        {
            _attackController = attackController;
        }

        public void Enter(int payload)
        {
            switch (payload)
            {
                case 0:
                    _attackController.TryAttack();
                    break;
                case 1:
                    _attackController.TrySecondaryAttack();
                    break;
            }
            
            Exit();
        }

        public void Exit()
        {
        }
    }
}