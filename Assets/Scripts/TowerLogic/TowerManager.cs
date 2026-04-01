using UnityEngine;
using System.Collections.Generic;

namespace TheBuildersJourney.TowerLogic
{
    public class TowerManager : MonoBehaviour
    {
        [Header("Tower Configuration")]
        [Tooltip("Assign all layers in order from bottom (0) to top (n)")]
        [SerializeField] private List<TowerLayer> layers = new List<TowerLayer>();

        [Tooltip("The layer index the player is currently standing on")]
        [SerializeField] private int currentPlayerLayerIndex = 0;

        [Header("Input Settings")]
        [SerializeField] private KeyCode rotateClockwiseKey = KeyCode.E;
        [SerializeField] private KeyCode rotateCounterClockwiseKey = KeyCode.Q;

        // [Header("Events (Optional for later)")]
        // public UnityEvent OnLayerConnected;
        // public UnityEvent OnHiddenAreaRevealed;

        private void Update()
        {
            if (layers.Count == 0) return;

            // Only rotate the layer the player is currently on, or the one they are trying to unlock
            // Let's assume rotating the *next* layer to align it with the current layer
            int targetLayerToRotate = Mathf.Min(currentPlayerLayerIndex + 1, layers.Count - 1);

            if (Input.GetKeyDown(rotateClockwiseKey))
            {
                RotateLayer(targetLayerToRotate, 90f);
            }
            else if (Input.GetKeyDown(rotateCounterClockwiseKey))
            {
                RotateLayer(targetLayerToRotate, -90f);
            }

            // You can also add inputs to switch which layer you are rotating if needed
        }

        private void RotateLayer(int index, float angle)
        {
            if (index < 0 || index >= layers.Count) return;

            var layer = layers[index];
            layer.RotateLayer(angle);

            Debug.Log($"[TowerManager] Rotating Layer {index} by {angle} degrees.");

            // Check if connection is solved
            CheckConnection(index);
            
            // Check for hidden areas (e.g., if rotation reaches exactly 180 degrees)
            CheckHiddenAreas(layer);
        }

        private void CheckConnection(int targetLayerIndex)
        {
            // If the rotating layer is aligned with its required angle
            if (layers[targetLayerIndex].IsAligned())
            {
                Debug.Log($"<color=green>[TowerManager] Layer {targetLayerIndex} is ALIGNED! Path unlocked!</color>");
                
                // If it's the layer above the player, the player can now climb up
                if (targetLayerIndex == currentPlayerLayerIndex + 1)
                {
                    // Trigger player climb animation here later
                    // currentPlayerLayerIndex++; 
                }
            }
            else
            {
                Debug.Log($"[TowerManager] Layer {targetLayerIndex} is not aligned yet.");
            }
        }

        private void CheckHiddenAreas(TowerLayer layer)
        {
            // Normalize current rotation 0-360
            float normalizedCurrent = (layer.currentRotation % 360 + 360) % 360;

            // Simple example: If layer is rotated to exactly 180 degrees, reveal something
            if (Mathf.Abs(normalizedCurrent - 180f) < 1f)
            {
                Debug.Log($"<color=yellow>[TowerManager] Hidden Area revealed on Layer {layer.layerIndex}!</color>");
                // UIManager.ShowHiddenInteractionBtn();
            }
        }

        /// <summary>
        /// Call this when the player successfully moves to the next layer
        /// </summary>
        public void PlayerAscended()
        {
            if (currentPlayerLayerIndex < layers.Count - 1)
            {
                currentPlayerLayerIndex++;
                Debug.Log($"[TowerManager] Player ascended to Layer {currentPlayerLayerIndex}");
            }
        }
    }
}