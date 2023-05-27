using UnityEngine;

interface IShootObserver
{
    void ShootUpdate(GameObject hittedPlayer, int damage);
}

