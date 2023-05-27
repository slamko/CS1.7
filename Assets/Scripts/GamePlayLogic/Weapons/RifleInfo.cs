using UnityEngine;

public abstract class RifleInfo : RifleAbstract
{
    [SerializeField] protected bool hasSecondState = false;
    [SerializeField] protected float aimMouseSensetivity = 1f;
    public float AimMouseSensetivity { get => hasSecondState ? aimMouseSensetivity : 1f; }

    public bool AimStateActive { get; protected set; }

    [SerializeField] protected float walkingSpeed;
    public float WalkingSpeed { get => walkingSpeed; }

    [SerializeField] protected float crouchSpeed;
    public float CrouchSpeed { get => crouchSpeed; }
}

