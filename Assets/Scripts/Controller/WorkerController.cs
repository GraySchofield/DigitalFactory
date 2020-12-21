using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DGFactory
{
    public class WorkerController : MonoBehaviour
    {
        private Worker _currentWorker;
        private Outline _outline;

        private Image _titleBg;
        private Text _title;

        public Color ActiveColor;
        public Color DeactiveColor;
       
        // Start is called before the first frame update
        void Start()
        {
            _outline = GetComponent<Outline>();
            _titleBg = transform.Find("CanvasHumanStates/Panel").GetComponent<Image>();
            _title = transform.Find("CanvasHumanStates/Panel/Text").GetComponent<Text>();
        }

        public void Refresh(Worker worker)
        {
            _currentWorker = worker;
        }

        // Update is called once per frame
        void Update()
        {
            if(_currentWorker != null)
            {
                if(_currentWorker.State == WorkingState.ON)
                {
                    //工作中
                    _outline.OutlineColor = ActiveColor;
                    _titleBg.color = ActiveColor;
                    _title.text = "工作中";
                }
                else
                {
                    //不在工作之中
                    _outline.OutlineColor = DeactiveColor;
                    _titleBg.color = DeactiveColor;
                    _title.text = "休息中";
                }
            }
        }
    }
}
