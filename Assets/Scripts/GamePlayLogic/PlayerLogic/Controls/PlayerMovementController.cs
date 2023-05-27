using UnityEngine;

public class PlayerMovementController : PlayerController, IPlayerControl, ISpecifiedControl
{
    [SerializeField] private GameObject playerCam, aimCam;

    public RifleInfo MyRifle;

    private CharacterController controller;
    private Quaternion targetRot;

    private const float TURN_SPEED = 20f;

    private float lastRotatedAngle;
    private float gravity =  12f;
    private float velocityY = 0f;
    private float mouseX = 0f;

    public float HeadPosition => RunState ? 1.8f : 0.5f; 

    public void Initialize()
    {
        controller = GetComponent<CharacterController>();

        lastRotatedAngle = transform.rotation.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Execute()
    {
        MoveCharacter();
        RotateCharacter();
    }

    private void RotateCharacter()
    {
        mouseX = Input.GetAxis("Mouse X");

        if(!Mathf.Approximately(mouseX, 0))
        {
            var rotY = !MyRifle.AimStateActive ?
                mouseX * TURN_SPEED * SettingsManager.DefaultMouseSensetivity * Time.deltaTime :
                    mouseX * TURN_SPEED * MyRifle.AimMouseSensetivity * Time.deltaTime;

            lastRotatedAngle += rotY;
            targetRot = Quaternion.Euler(transform.rotation.x, lastRotatedAngle, transform.rotation.z);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 0.7f);
    }

    private void MoveCharacter()
    {
        if (!controller.isGrounded)
            velocityY -= gravity * Time.deltaTime;

        controller.Move(new Vector3(0f, velocityY, 0f) * Time.deltaTime);
    }

    private void UpdateCamPos()
    {
        Vector3 camPos = playerCam.transform.position;
        camPos = new Vector3(camPos.x, transform.position.y + HeadPosition, camPos.z);
        playerCam.transform.position = camPos;
        print(HeadPosition);
    }

    public void JumpAction() => velocityY = IsRunning ? 6f: 5f;

    #region Run
    public void EnterRunState()
    {
        UpdateCamPos();
    }

    public void RunStateAction()
    {
        Move(MyRifle.WalkingSpeed);
    }

    public void ExitRunState() { }
    #endregion


    #region Crouch
    public void EnterCrouchState()
    {
        UpdateCamPos();
    }

    public void CrouchStateAction()
    {
        Move(MyRifle.CrouchSpeed);
    }

    public void ExitCrouchState(){ }
    #endregion

    private void Move(float speed)
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            controller.Move(transform.forward * velZ * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            controller.Move(transform.right * velX * speed * Time.deltaTime);
    }
}
