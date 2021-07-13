using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GW2021
{
    public class ProductObjectPool : BaseObjectPool<Product>
    {
        /// <summary>
        /// Subscribe 'ReturnToPool' method to the product's event delegate
        /// so that the product can return to the pool whenever applicable.
        /// </summary>
        /// <param name="product"></param>
        protected override void OnGetEvents(Product product)
        {
            product.eventsAtDisable += ReturnToPool;
        }

        /// <summary>
        /// Unsubscribe 'ReturnToPool' method from the product's event delegate
        /// to avoid memory leaks when using delegates.
        /// </summary>
        /// <param name="product"></param>
        protected override void OnReturnEvents(Product product)
        {
            product.eventsAtDisable -= ReturnToPool;
        }

        /// <summary>
        /// Unsubscribe 'ReturnToPool' method to the product's event delegate
        /// when this script gets destroyed in order to prevent errors when
        /// the product's delegate gets called.
        /// </summary>
        /// <param name="product"></param>
        protected override void OnDestroyEvents(Product product)
        {
            product.eventsAtDisable -= ReturnToPool;
        }
    }
}