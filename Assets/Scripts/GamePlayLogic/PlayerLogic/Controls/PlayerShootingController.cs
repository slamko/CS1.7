using System;
using UnityEngine;
using Photon.Pun;

public class PlayerShootingController : PlayerController, IPlayerInitialize, ISpecifiedControl
{
    [SerializeField] private GameObject aimCam;
    [SerializeField] private GameObject awpObject;

    public RifleAbstract Rifle;
    public bool IsAlive { get; private set; }

    private IShootObserver shootObserver;

    public void Initialize()
    {
        shootObserver = GetComponent<PlayerManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Execute()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(aimCam.transform.position, aimCam.transform.forward);
            bool raycastHit = Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo);

            if(Rifle.Hit(hitInfo.collider, out GameObject hittedObj, out int damage))
            {
                if (hittedObj != null)
                {
                    GameObject hittedPlayer = hittedObj.transform.root.gameObject;

                    shootObserver.ShootUpdate(hittedPlayer, damage);
                }
            }
        }
    }
}

