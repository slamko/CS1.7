using UnityEngine;

public abstract class RifleDamageInfo : RifleInfo
{
    [SerializeField] protected int headDamage;
    public int HeadDamage { get => headDamage; }

    [SerializeField] protected int chestDamage;
    public int ChestDamage { get => chestDamage; }

    [SerializeField] protected int hands_LegsDamage;
    public int Hands_LegsDamage { get => hands_LegsDamage; }
}

