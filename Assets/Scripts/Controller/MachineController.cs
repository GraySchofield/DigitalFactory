using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGFactory
{
    public delegate void OnTabMachine(Machine machine);
    
    public class MachineController : MonoBehaviour
    {
        /// <summary>
        /// 这个Controller 所显示的数据
        /// </summary>
        private Machine _currentMachine;
        private OnTabMachine _onTap;

        public void Refresh(Machine machine, OnTabMachine onTap)
        {
            _currentMachine = machine;
            _onTap = onTap;
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray ,out hit) && Input.GetMouseButtonDown(0))
            {
                if(hit.transform == transform)
                {
                    if (_onTap != null)
                    {
                        _onTap(_currentMachine);
                    }
                }
            }
        }
    }

}