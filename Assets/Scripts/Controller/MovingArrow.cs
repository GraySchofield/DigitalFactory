using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGFactory
{
    public class MovingArrow : MonoBehaviour
    {
        public float Speed = 2f;
        private Vector3 _forward = new Vector3(1, 0, 0);

        // Update is called once per frame
        void Update()
        {
            transform.position += _forward * Speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
             ObjectPoolManager.Instance.Release("LineArrow", gameObject);
        }
    }

}