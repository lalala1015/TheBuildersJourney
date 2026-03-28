using UnityEngine;

namespace TheBuildersJourney.MiniGames.Common
{
    public enum MechanismState
    {
        Idle,
        Triggered,
        Cooldown
    }

    public class InteractableMechanism : MonoBehaviour
    {
        [SerializeField] private float cooldownSeconds = 1.5f;

        private MechanismState state = MechanismState.Idle;
        private float timer = 0f;

        private void Update()
        {
            if (state == MechanismState.Cooldown)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    state = MechanismState.Idle;
                }
            }
        }

        public void Trigger()
        {
            if (state != MechanismState.Idle) return;

            state = MechanismState.Triggered;
            Debug.Log($"[Mechanism] {name} triggered.");

            // TODO: 在这里接动画、音效、碰撞体开关
            EnterCooldown();
        }

        private void EnterCooldown()
        {
            state = MechanismState.Cooldown;
            timer = cooldownSeconds;
        }
    }
}
