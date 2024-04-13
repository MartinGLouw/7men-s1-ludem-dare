using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(menuName = "Mediator/ UI", fileName = "New UI Mediator")]
    public class UIMediator : ScriptableObject
    {
        public float TimeTaken;
        public int Health;
        public int BossHealth;
    }
}