namespace EntityComponents.Control
{
    public struct InputData
    {
        public readonly float Horizontal;
        public readonly bool IsDashing;
        public readonly bool IsJumping;
        public readonly bool IsMainAttack;
        public readonly bool IsSecondaryAttack;

        public InputData(float horizontal, bool isDashing, bool isJumping, bool isMainAttack, bool isSecondaryAttack)
        {
            Horizontal = horizontal;
            IsDashing = isDashing;
            IsJumping = isJumping;
            IsMainAttack = isMainAttack;
            IsSecondaryAttack = isSecondaryAttack;
        }
    }
}