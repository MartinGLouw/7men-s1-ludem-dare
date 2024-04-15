using System.Collections;
using System.Collections.Generic;
using Managers.Lawyer;
using Unity.VisualScripting;
using UnityEngine;

public class DespawnScript : MonoBehaviour
{
    public Projectiles projectileData;
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WorldBorder")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<IDamageable<Projectiles>>(out IDamageable<Projectiles> damageable))
        {
            damageable.TakeDamage(projectileData);
        }
        
        if (other.gameObject.tag == "Player" && gameObject.tag == "EnemyProjectile")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Enemy" && gameObject.tag == "PlayerProjectile")
        {
            Destroy(gameObject);
        }
    }
}
