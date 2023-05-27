using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraHandler : MonoBehaviour
{
    private float upRotation = 0f;

    private float ROT_SPEED = -0.35F;

    private Quaternion targetRot;
    private Vector3 playerOffset;
    private Vector3 smoothVelocity;
    private PlayerMovementController player;
    private RifleInfo rifle;

    private float lastRotatedAngle;

    private float smoothTime = 1f;

    private bool IsMyPlayer => player.PhotonView.IsMine;

    private void Start()
    {
        player = transform.parent.gameObject.GetComponent<PlayerMovementController>();
        rifle = player.MyRifle;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if(IsMyPlayer)
        {
            float mouseY = Input.GetAxis("Mouse Y");

            float verticalRot = !rifle.AimStateActive ?
                    mouseY * ROT_SPEED * SettingsManager.DefaultMouseSensetivity * Time.deltaTime :
                        mouseY * ROT_SPEED * rifle.AimMouseSensetivity * Time.deltaTime;

            upRotation += verticalRot * 100f;

            transform.RotateAround(transform.right, verticalRot);
        }
    }

    private IEnumerator DeathMove()
    {
        while(true)
        {
            transform.LookAt(transform.parent);
            transform.Translate(Vector3.up * Time.deltaTime * 2f);
        }
    }
}
