using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGFactory
{
    public delegate void OnTabMachine(MachineController self, Machine machine);

    public class MachineController : MonoBehaviour
    {
        /// <summary>
        /// 这个Controller 所显示的数据
        /// </summary>
        private Machine _currentMachine;
        private OnTabMachine _onTap;
        private Outline _outline;

        private Transform _detailAnchor;

        public bool IsSelected{
            get
            {
                return _outline.enabled;
            }
        }

        public Machine CurrentMachine
        {
            get
            {
                return _currentMachine;
            }
        }


        /// <summary>
        /// 详情相机对准位置
        /// </summary>
        public Transform DetailAnchor
        {
            get
            {
                return _detailAnchor;
            }
        }

        private void Awake()
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;

            _detailAnchor = transform.Find("DetailAnchor");
        }

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
                        _onTap(this, _currentMachine);
                    }
                }
            }
        }

        /// <summary>
        /// 是否显示外框
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetOutline(bool isVisible)
        {
            _outline.enabled = isVisible;
        }
    }

}