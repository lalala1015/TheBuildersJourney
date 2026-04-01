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

            // TODO: 鍦ㄨ繖閲屾帴鍔ㄧ敾銆侀煶鏁堛€佺鎾炰綋寮€鍏?
            EnterCooldown();
        }

        private void EnterCooldown()
        {
            state = MechanismState.Cooldown;
            timer = cooldownSeconds;
        }
    }
}
