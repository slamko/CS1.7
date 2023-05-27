using UnityEngine;

public class PlayerAnimator : PlayerController, IPlayerControl, ISpecifiedControl
{
    [SerializeField] private GameObject visibleObject;
    [SerializeField] private SkinnedMeshRenderer[] meshes = new SkinnedMeshRenderer[7];
    [SerializeField] private GameObject awpSkin;

    private Animator animator;

    public void Initialize()
    {
        visibleObject.SetActive(true);
        SwitchMeshes();

        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void SwitchMeshes()
    {
        foreach(var mesh in meshes)
        {
            mesh.enabled = false;
        }
        awpSkin.SetActive(false);
    }

    public void Execute()
    {
        animator.SetFloat("Velocity_X", velX);
        animator.SetFloat("Velocity_Z", velZ);

        animator.SetBool("IsRunning", IsRunning);
    }

    public void JumpAction() => animator.SetTrigger("Jump");


    #region Run
    public void EnterRunState() { }

    public void RunStateAction() => animator.SetBool("Run", IsRunning);

    public void ExitRunState() => animator.SetBool("Run", RunState);
    #endregion


    #region Crouch
    public void EnterCrouchState() => animator.SetBool("Crouch", true);

    public void CrouchStateAction() { }

    public void ExitCrouchState()
    {
        animator.SetBool("Crouch", CrouchState);
        animator.SetBool("Run", RunState);
    }
    #endregion
}
