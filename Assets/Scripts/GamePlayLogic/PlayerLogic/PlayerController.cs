using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    private List<IPlayerControl> controls = new List<IPlayerControl>();
    private List<ISpecifiedControl> specifiedControls = new List<ISpecifiedControl>();
    private List<IPlayerInitialize> initializeControls = new List<IPlayerInitialize>();

    public PhotonView PhotonView { get; private set; }

    private const float SPEED = 3f;

    protected float velX, velZ;

    private bool isRunning;
    public bool IsRunning 
    {
        get
        {
            var flag = !(Mathf.Abs(velX) < 0.1f && Mathf.Abs(velZ) < 0.1f);
            if(isRunning != flag)
            {
                isRunning = flag;
                OnMovementStateChanged();
            }
            return isRunning;
        }
    }

    public event Action MovementStateChanged;

    protected bool isJump = false;
    protected bool RunState { get; set; } = true;
    protected bool CrouchState { get; set; }

    private void Start()
    {
        PhotonView = GetComponent<PhotonView>();

        if(PhotonView.IsMine)
        {
            controls.AddRange(GetComponentsInChildren<IPlayerControl>());

            specifiedControls.AddRange(GetComponentsInChildren<ISpecifiedControl>());

            initializeControls.AddRange(GetComponentsInChildren<IPlayerInitialize>());

            foreach (IPlayerInitialize control in initializeControls)
                control.Initialize();
        }

        StartCoroutine(nameof(RunBlenderState));
    }

    private void OnMovementStateChanged()
    {
        MovementStateChanged?.Invoke();
    }

    private void Update()
    {
        if(PhotonView.IsMine)
        {
            velX = Input.GetAxis("Horizontal") * SPEED;
            velZ = Input.GetAxis("Vertical") * SPEED;

            if(specifiedControls.Count > 0)
            {
                foreach(ISpecifiedControl control in specifiedControls)
                    control.Execute();
            }

            JumpCheck();
        }
    }

    private void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            isJump = true;
            Jump();
        }
    }

    private IEnumerator RunBlenderState()
    {
        foreach (IPlayerControl control in controls)
            control.EnterRunState();

        while (RunState)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                RunState = false;
                CrouchState = !RunState;

                foreach (IPlayerControl control in controls)
                    control.ExitRunState();

                StartCoroutine(nameof(CrouchBlenderState));
                yield break;
            }

            foreach(IPlayerControl control in controls)
                control.RunStateAction();

            yield return null;
        }
    }

    private IEnumerator CrouchBlenderState()
    {
        foreach (IPlayerControl control in controls)
            control.EnterCrouchState();

        while (CrouchState)
        {
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                CrouchState = false;
                RunState = !CrouchState;

                foreach (IPlayerControl control in controls)
                    control.ExitCrouchState();

                StartCoroutine(nameof(RunBlenderState));
                yield break;
            }
     
            foreach (IPlayerControl control in controls)
                control.CrouchStateAction();

            yield return null;
        }
    }

    private void Jump()
    {
        foreach (IPlayerControl control in controls)
            control.JumpAction();
    }
}