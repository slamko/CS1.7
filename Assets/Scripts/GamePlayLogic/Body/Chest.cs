using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IBodyPart
{
    public int GetDamage(RifleDamageInfo rifle)
    {
        return rifle.ChestDamage;
    }
}