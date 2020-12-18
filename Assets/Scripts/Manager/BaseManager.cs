using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGFactory
{
    public class BaseManager<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        protected virtual void Awake()
        {
            Instance = this as T;
        }
    }
}

