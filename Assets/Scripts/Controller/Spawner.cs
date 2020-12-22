using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGFactory
{

    public class Spawner : MonoBehaviour
    {
        public float Interval = 2f;
        public string PoolName;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("spwan", 1, Interval);
        }

        private void spwan()
        {

            if(ObjectPoolManager.Instance.GetPool(PoolName) != null)
            {
                GameObject spwanedObject = ObjectPoolManager.Instance.Get(PoolName);
                spwanedObject.transform.SetParent(transform);
                spwanedObject.SetActive(true);
                spwanedObject.transform.localScale = Vector3.one;
                spwanedObject.transform.localPosition = Vector3.zero;
            }
           
        }

    }

}