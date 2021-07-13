using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GW2021
{
    /// <summary>
    /// Script which defines a bin used to put products inside of.
    /// </summary>
    public class ProductBin : MonoBehaviour
    {
        /// <summary>
        /// The score board reference used for displaying key info and notifications to the player.
        /// </summary>
        [SerializeField] private ProductScoreboard _scoreboardRef;

        /// <summary>
        /// Determines if the bin is used for either defective products or normal products.
        /// </summary>
        [SerializeField] private bool _isDefectBin;

        /// <summary>
        /// When a product is detected, call its delegate to trigger the events that happen when
        /// it gets "destroyed".
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Product"))
            {
                Product currentProduct = other.GetComponent<Product>();
                currentProduct.TriggerDisableEvents();
                _scoreboardRef.JudgeProduct(currentProduct, _isDefectBin);
            }
        }
    }
}

