using UnityEngine;

public abstract class RifleAbstract : MonoBehaviour
{
    public abstract  bool Hit(Collider hitCol, out GameObject hitObj, out int damage);
}
