using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGFactory
{
    public class UILoading : MonoBehaviour
    {

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
    }

}