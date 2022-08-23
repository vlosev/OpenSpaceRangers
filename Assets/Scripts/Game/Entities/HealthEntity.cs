using Common;
using Common.Unity;
using UnityEngine;

namespace Game.Entities
{
    public class HealthEntity : NotifiableMonoBehaviour
    {
        [Header("..default parameters")]
        [SerializeField] private int hp = 100;
        [SerializeField] private int maxHp = 500;

        private readonly ReactiveProperty<float> health = new ReactiveProperty<float>();
        public IReadonlyReactiveProperty<float> Health => health;

        private readonly ReactiveProperty<float> maxHealth = new ReactiveProperty<float>();
        public IReadonlyReactiveProperty<float> MaxHealth => maxHealth;

        protected override void SafeAwake()
        {
            base.SafeAwake();

            health.Value = hp;
            maxHealth.Value = maxHp;
        }

        public void Add(int amount)
        {
            if (amount != 0)
            {
                health.Value = Mathf.Clamp(health.Value + amount, 0, maxHealth.Value);
            }
        }

        public void Kill()
        {
            Add(-maxHp);
        }
    }
}