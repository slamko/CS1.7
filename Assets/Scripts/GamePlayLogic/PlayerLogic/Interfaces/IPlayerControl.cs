
interface IPlayerControl : IPlayerInitialize
{
    void JumpAction();

    void EnterRunState();

    void RunStateAction();

    void ExitRunState();

    void EnterCrouchState();

    void CrouchStateAction();

    void ExitCrouchState();
}
