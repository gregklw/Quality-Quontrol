using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GW2021
{
    public class Product : MonoBehaviour
    {
        /// <summary>
        /// Delegate definition used for triggering events when a product is
        /// disabled.
        /// </summary>
        /// <param name="product"></param>
        public delegate void ProductDisableEvents(Product product);

        /// <summary>
        /// Reference variable of delegate intended as a callback that
        /// triggers whenever it's disabled.
        /// </summary>
        public ProductDisableEvents eventsAtDisable;

        /// <summary>
        /// Boolean used to change the appearance of the product.
        /// </summary>
        public bool IsDefect { get; set; }

        /// <summary>
        /// The value of this product when it's placed in the correct bin.
        /// </summary>
        public int MoneyValue { get; set; }

        /// <summary>
        /// Renderer is used to retrieve and change the material of the product.
        /// </summary>
        public Renderer ProductRenderer { get; set; }

        /// <summary>
        /// Set up the renderer.
        /// </summary>
        private void Awake()
        {
            ProductRenderer = GetComponent<Renderer>();
        }

        /// <summary>
        /// Method used to call the 'ProductDisableEvents' delegate whenever appropriate.
        /// </summary>
        public void TriggerDisableEvents() 
        {
            eventsAtDisable?.Invoke(this);
        }

    }
}

