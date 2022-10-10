using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour, Iitem
{
    public int ammo = 30;

    public void Use(GameObject target)
    {
        PlayerShoot playerShooter = target.GetComponent<PlayerShoot>();
        if(playerShooter != null && playerShooter.gun != null)
        {
            if(tag == "Player")
            {
                playerShooter.gun.ammoRemain += ammo;
            }
        }

        Destroy(gameObject);

    }
    
}
