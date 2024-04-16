using UnityEngine;

namespace Managers.Utils
{
    public class EnemyHealthUtil : MonoBehaviour, IDamagable<DamageData>
    {
        public float health = 50f;
        
        public void TakeDamage(DamageData Data)
        {
            health -= Data.dmgAmount;

            if (health <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}