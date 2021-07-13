using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Specifies the events that should happen when headset interacts with this interactable in some way.
/// </summary>

namespace GW2021
{
    [RequireComponent(typeof(XRBaseInteractable))]
    public class ProductInteractEvents : MonoBehaviour
    {
        [SerializeField] private Material _interactMat;
        private Material _originalMat;
        private MeshRenderer _meshRenderer;

        void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            if (_meshRenderer == null) Debug.LogError("No MeshRenderer component detected!");
            _originalMat = _meshRenderer.material;
        }

        /// <summary>
        /// Highlights the product when it is hovered over.
        /// </summary>
        public void HighlightProduct()
        {
            _meshRenderer.material = _interactMat;
        }

        /// <summary>
        /// Turns off highlights if the product exits
        /// </summary>
        public void RemoveHighlight()
        {
            _meshRenderer.material = _originalMat;
        }
    }
}