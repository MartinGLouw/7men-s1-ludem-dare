using UnityEngine;

namespace Managers.Utils
{
    public class EnemyHealthUtil : MonoBehaviour, IDamagable<DamageData>
    {
        private DamageFlash _damageFlash;

        public float health = 50f;

        private void Awake()
        {
            _damageFlash = GetComponent<DamageFlash>();
        }

        public void TakeDamage(DamageData Data)
        {

            int sfxIndex = Random.Range(17, 20);

            SoundManager.Instance.PlaySFX(sfxIndex, 0.5f);

            _damageFlash.DamageEffect();

            health -= Data.dmgAmount;

            if (health <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}