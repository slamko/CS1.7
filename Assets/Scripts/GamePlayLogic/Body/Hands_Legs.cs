using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands_Legs : MonoBehaviour, IBodyPart
{
    public int GetDamage(RifleDamageInfo rifle)
    {
        return rifle.Hands_LegsDamage;
    }
}
