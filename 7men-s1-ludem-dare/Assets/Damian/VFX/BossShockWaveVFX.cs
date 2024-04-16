using Events;
using UnityEngine;

public class BossShockWaveVFX : MonoBehaviour
{
    [SerializeField] private GameObject VFXPrefab;

    //listen for the event
    private void OnEnable()
    {
       //EnemyEvents.OnBossShockWave += TriggerVFX;
    }

    private void OnDisable()
    {
        //EnemyEvents.OnBossShockWave -= TriggerVFX;
    }

    public void TriggerVFX()
    {
        Vector3 feetPosition = new Vector3(transform.position.x, GetComponent<Collider>().bounds.min.y, transform.position.z);

        GameObject vfx = Instantiate(VFXPrefab, feetPosition, Quaternion.identity);

        vfx.transform.localScale *= 5;
    }
}