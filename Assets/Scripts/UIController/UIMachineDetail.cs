using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DGFactory
{

    public delegate void OnDismiss();

    public class UIMachineDetail : MonoBehaviour
    {
        public Sprite ImageYes;
        public Sprite ImageNo;
        public Color ColorYes;
        public Color ColorNo;

        private Machine _currentMachine;
        private OnDismiss _onDismiss;

        private Button _btnClose;
        private GameObject _panelErrorProvention;
        private GameObject _sampleErrorState;

        private GameObject _workerState;
            

        //TODO: EHS 数据显示还没有

        void Awake()
        {
            _btnClose = transform.Find("title/ButtonClose").GetComponent<Button>();
            Transform content = transform.Find("ScrollView/Viewport/Content").transform;
            _panelErrorProvention = content.Find("PanelErrorProvention").gameObject;
            _sampleErrorState = content.Find("PanelErrorProvention/ErrorState").gameObject;

            _workerState = content.Find("WorkerState").gameObject;


            _btnClose.onClick.AddListener(() => {
                Hide();
                if (this._onDismiss != null)
                {
                    this._onDismiss();
                }
            });
            
            //初始化隐藏这个UI
            Hide();
        }


        public void Show(Machine machine, OnDismiss onDismiss)
        {
            _currentMachine = machine;
            _onDismiss = onDismiss;
            gameObject.SetActive(true);
            Refresh();
        }
            
        /// <summary>
        /// 根据当前的Machine 数据，刷新UI
        /// </summary>
        private void Refresh()
        {
           if(_currentMachine != null)
            {
               //防错状态
               if(_currentMachine.CurrentWatchItems.Count > 0)
                {
                    _panelErrorProvention.SetActive(true);
                    foreach(WatchItem item in _currentMachine.CurrentWatchItems)
                    {
                        GameObject errorState = Instantiate(_sampleErrorState);
                        Text title = errorState.GetComponent<Text>();
                        title.text = item.ItemName;
                        setErrorStateUI(errorState, item.IsNormal);
                        errorState.transform.SetParent(_panelErrorProvention.transform);
                        errorState.transform.localScale = Vector3.one;
                    }
                    _sampleErrorState.SetActive(false);
                }
                else
                {
                    _panelErrorProvention.SetActive(false);
                }


                //人员工作状态
                setErrorStateUI(_workerState.transform.Find("ErrorState").gameObject, _currentMachine.CurrentWorker.State == WorkingState.ON);
            }
            else
            {
                Debug.LogError("Error: refresh machine detail ui with null machine data!");
            }
        }

        /// <summary>
        /// 设定通用的 正常，异常UI
        /// </summary>
        private void setErrorStateUI(GameObject targetObject, bool isNormal)
        {
            Image iconState = targetObject.transform.Find("iconState").GetComponent<Image>();
            Text textState = targetObject.transform.Find("textState").GetComponent<Text>();
            iconState.sprite = isNormal ? ImageYes : ImageNo;
            iconState.color = isNormal ? ColorYes : ColorNo;
            textState.text = isNormal ? "正常" : "异常";
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}