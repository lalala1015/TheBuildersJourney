using UnityEngine;

namespace TheBuildersJourney.TowerLogic
{
    public class TowerLayer : MonoBehaviour
    {
        [Header("Layer Configuration")]
        [Tooltip("The index of this layer (0 = bottom, 1, 2, 3...)")]
        public int layerIndex;

        [Tooltip("The required world Y-axis rotation needed to connect to the previous layer")]
        public float requiredConnectionAngle = 0f;

        [Tooltip("Current Y-axis rotation of this layer")]
        public float currentRotation = 0f;

        [Tooltip("Is this layer currently visible/active?")]
        public bool isActive = false;

        [Header("Animation Settings")]
        [SerializeField] private float rotationSpeed = 5f;

        private float targetRotation = 0f;
        private bool isRotating = false;

        private void Start()
        {
            targetRotation = currentRotation;
            transform.rotation = Quaternion.Euler(0, currentRotation, 0);
        }

        private void Update()
        {
            if (isRotating)
            {
                // Smooth rotation animation
                currentRotation = Mathf.LerpAngle(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
                transform.rotation = Quaternion.Euler(0, currentRotation, 0);

                if (Mathf.Abs(Mathf.DeltaAngle(currentRotation, targetRotation)) < 0.1f)
                {
                    currentRotation = targetRotation;
                    transform.rotation = Quaternion.Euler(0, currentRotation, 0);
                    isRotating = false;
                }
            }
        }

        /// <summary>
        /// Rotate the layer by a given angle (e.g., 90 or -90)
        /// </summary>
        public void RotateLayer(float angle)
        {
            targetRotation += angle;
            isRotating = true;
        }

        /// <summary>
        /// Check if this layer is aligned correctly to be connected
        /// </summary>
        public bool IsAligned()
        {
            // Normalize angle to 0-360 for comparison
            float normalizedCurrent = (currentRotation % 360 + 360) % 360;
            float normalizedRequired = (requiredConnectionAngle % 360 + 360) % 360;
            
            return Mathf.Abs(Mathf.DeltaAngle(normalizedCurrent, normalizedRequired)) < 1f;
        }
    }
}