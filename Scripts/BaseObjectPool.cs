using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GW2021
{
    /// <summary>
    /// Object pool which uses generics for reusability potential and a queue for quick access.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseObjectPool<T> : MonoBehaviour where T : Component
    {
        /// <summary>
        /// The Transform used to store parent object stuff.
        /// </summary>
        [SerializeField] private Transform _objParentT;
        [SerializeField] private GameObject _prefab;

        /// <summary>
        /// Boolean to decide whether or not the collection of objects
        /// should be managed in the situation this script gets destroyed.
        /// </summary>
        protected bool CanManagePoolWhenDestroyed { get; set; }

        /// <summary>
        /// The object pool which uses a queue for quick access of its elements
        /// and also because it will not be used for iteration.
        /// </summary>
        private Queue<T> _objPool;

        /// <summary>
        /// Initializes the object pool queue for use.
        /// </summary>
        private void Awake()
        {
            _objPool = new Queue<T>();
        }

        /// <summary>
        /// Grabs an object from the pool and returns it.
        /// </summary>
        /// <returns></returns>
        public T GetFromPool()
        {
            if (_objPool.Count <= 0)
            {
                AddToPool();
            }
            var t = _objPool.Dequeue();
            OnGetEvents(t);
            return t;
        }

        /// <summary>
        /// Put object back into the pool and disable it for future use.
        /// Can also do something when object returns to the pool.
        /// </summary>
        /// <param name="t"></param>
        public void ReturnToPool(T t)
        {
            t.gameObject.SetActive(false);
            OnReturnEvents(t);
            _objPool.Enqueue(t);
        }

        /// <summary>
        /// Instantiates a new GameObject with component T and adds it to this pool.
        /// Helper to the 'GetFromPool' method.
        /// </summary>
        private void AddToPool()
        {
            var t = Instantiate(_prefab).GetComponent<T>();
            t.transform.SetParent(_objParentT);
            ReturnToPool(t);
        }

        /// <summary>
        /// In the case this script is destroyed, if we are allowed to manage the pool members,
        /// then trigger the 'OnDestroyEvents' for every object in the pool.
        /// </summary>

        private void OnDestroy()
        {
            if (CanManagePoolWhenDestroyed)
            {
                while (_objPool.Count > 0)
                {
                    var pooledObj = GetFromPool();
                    OnDestroyEvents(pooledObj);
                }
            }
        }

        /// <summary>
        /// Customizable function that determine what the object will do when
        /// its retrieved from the pool.
        /// </summary>
        /// <param name="t"></param>
        protected abstract void OnGetEvents(T t);

        /// <summary>
        /// Customizable function that determine what the object will do when
        /// its returned to the pool.
        /// </summary>
        /// <param name="t"></param>
        protected abstract void OnReturnEvents(T t);

        /// <summary>
        /// Customizable function that determine what the object will do when
        /// its returned to the pool.
        /// </summary>
        /// <param name="t"></param>
        protected abstract void OnDestroyEvents(T t);
    }
}