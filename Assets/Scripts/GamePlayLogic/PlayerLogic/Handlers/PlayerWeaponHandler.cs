using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] protected RifleAbstract rifleAbstract;
    public RifleAbstract RifleAbstract => rifleAbstract; 

    [SerializeField] protected RifleInfo rifleInfo;
    public RifleInfo RifleInfo => rifleInfo; 
}
