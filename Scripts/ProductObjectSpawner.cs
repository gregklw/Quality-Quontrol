using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GW2021
{
    [RequireComponent(typeof (ProductObjectPool))]
    public class ProductObjectSpawner : MonoBehaviour
    {
        /// <summary>
        /// A reference to the object pool used for the spawner.
        /// </summary>
        [SerializeField] private ProductObjectPool _poolRef;

        /// <summary>
        /// The spawn point used for the instantiated products.
        /// </summary>
        [SerializeField] private Transform _spawnPoint;

        /// <summary>
        /// The materials to identify which product is a defect and which one is not.
        /// </summary>
        [SerializeField] private Material _correctMat, _defectMat;

        /// <summary>
        /// Determines the amount of normal and defective products this spawner will create.
        /// </summary>
        [SerializeField] [Range(0, 100)] private int _correctAmnt, _defectAmnt;

        /// <summary>
        /// Determines the amount of money the player receives for placing this product
        /// in the correct place.
        /// </summary>
        [SerializeField] [Range(1, 100)] private int _moneyValue;

        /// <summary>
        /// Position and rotation range values used to randomize the location of the spawn.
        /// </summary>
        private readonly float _xPosRange = 2.5f, _yPosRange = 0.5f, _zPosRange = 2.5f, _rotateRange = 20;

        /// <summary>
        /// Spawn the products at the start of the program.
        /// </summary>
        private void Start()
        {
            SpawnNormals();
            SpawnDefects();
        }

        /// <summary>
        /// Spawn normal products void of defects.
        /// </summary>
        private void SpawnNormals() 
        { 
            SpawnProducts(_correctAmnt, false);
        }

        /// <summary>
        /// Spawn defective products.
        /// </summary>
        private void SpawnDefects()
        {
            SpawnProducts(_defectAmnt, true);
        }

        /// <summary>
        /// Helper method that creates a random spawn point and converts the product's
        /// properties depending on if it's defective or not.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="isDefect"></param>
        private void SpawnProducts(int amount, bool isDefect)
        {
            for (int i = 0; i < amount; i++)
            {
                //---calculates random spawn point values---//
                var randomPos = new Vector3(Random.Range(-_xPosRange, _xPosRange), 
                    Random.Range(-_yPosRange, _yPosRange),
                    Random.Range(-_zPosRange, _zPosRange)
                    );

                var randomRot = new Vector3(Random.Range(-_rotateRange, _rotateRange),
                    Random.Range(-_rotateRange, _rotateRange),
                    Random.Range(-_rotateRange, _rotateRange)
                    );

                var pooledObj = _poolRef.GetFromPool();
                //---calculates random spawn point values---//

                //---initialize the product's spawn point and properties depending on whether it's a defect or not---//
                pooledObj.IsDefect = isDefect;
                pooledObj.MoneyValue = _moneyValue;

                if (isDefect) pooledObj.ProductRenderer.material = _defectMat;
                else pooledObj.ProductRenderer.material = _correctMat;

                pooledObj.gameObject.SetActive(true);
                pooledObj.transform.position = _spawnPoint.position + randomPos;
                pooledObj.transform.eulerAngles += randomRot;
                //---initialize the product's spawn point and properties depending on whether it's a defect or not---//
            }
        }


    }
}