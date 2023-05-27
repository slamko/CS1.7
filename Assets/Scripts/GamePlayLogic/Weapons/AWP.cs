using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AWP : RifleDamageInfo
{
    private Image aimImage;

    [SerializeField] private Camera playerCam, aimCam;

    private PlayerMovementController player;
    private Animator myAnimator;

    public const float AIM_MOUSE_SENSETIVITY = 0.5F;
    private const float SHOTS_PER_SECOND = 30F;
    private float lastShotTime;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        aimImage = GameObject.Find("AimImage").GetComponent<Image>();
        player = transform.root.gameObject.GetComponent<PlayerMovementController>();

        myAnimator.SetBool("Animate", player.IsRunning);
        player.MovementStateChanged += SwitchAnimation;
    }

    private void SwitchAnimation()
    {
        myAnimator.SetBool("Animate", player.IsRunning);
    }

    private void OnDisable()
    {
        player.MovementStateChanged -= SwitchAnimation;
    }

    public override bool Hit(Collider hitCol, out GameObject hitObj, out int damage)
    {
        if(AimStateActive)
        {
            if(Time.time >= lastShotTime)
            { 
                Debug.LogError("shot");
                lastShotTime = Time.time + 1f / SHOTS_PER_SECOND;

                if(hitCol != null)
                {
                    hitObj = hitCol.transform.gameObject;

                    IBodyPart bodyPart = hitObj.GetComponent<IBodyPart>();
                    if(bodyPart != null)
                    {
                        damage = bodyPart.GetDamage(this);
                        return true;
                    }
                }
            } 
        }
        
        hitObj = null;
        damage = 0;
        
        return false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            playerCam.enabled = !playerCam.enabled;
            aimCam.enabled = !aimCam.enabled;
            aimImage.enabled = !aimImage.enabled;

            AimStateActive = aimCam.enabled;
        }
    }
}
