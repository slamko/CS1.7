using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour, IBodyPart
{
    public int GetDamage(RifleDamageInfo rifle)
    {
        return rifle.HeadDamage;
    }
}
