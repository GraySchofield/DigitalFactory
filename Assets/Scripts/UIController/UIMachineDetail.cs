using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DGFactory
{
    public class UIMachineDetail : BaseUI
    {
        public Sprite ImageYes;
        public Sprite ImageNo;
        public Sprite ImageWarning;

        public Color ColorYes;
        public Color ColorNo;
        public Color ColorWarning;

        public Color ColorDoorOpen;
        public Color ColorDoorClose;

        private Machine _currentMachine;

        //防错数据
        private GameObject _panelErrorProvention;
        private GameObject _sampleErrorState;
        private List<GameObject> _errorStates;

        //人员状态
        private GameObject _workerState;

        //EHS数据
        private GameObject _ehsState;
        private GameObject _ehsLightState;
        private GameObject _ehsHandState;
        private GameObject _ehsDoorState;
        private GameObject _doorSample;
        private List<GameObject> _doors;
            

        //TODO: EHS 数据显示还没有

        protected override void Awake()
        {
            base.Awake();
            Transform content = transform.Find("ScrollView/Viewport/Content").transform;
            _panelErrorProvention = content.Find("PanelErrorProvention").gameObject;
            _sampleErrorState = content.Find("PanelErrorProvention/ErrorState").gameObject;

            _workerState = content.Find("WorkerState").gameObject;

            _ehsState = content.Find("EHSState").gameObject;
            _ehsLightState = _ehsState.transform.Find("LightState").gameObject;
            _ehsHandState = _ehsState.transform.Find("HandState").gameObject;
            _ehsDoorState = _ehsState.transform.Find("DoorState").gameObject;
            _doorSample = _ehsDoorState.transform.Find("door").gameObject;
            _doorSample.SetActive(false);
            _errorStates = new List<GameObject>();
            _doors = new List<GameObject>();

            //初始化隐藏这个UI
            Hide();
        }

        private void Start()
        {
            InvokeRepeating("Refresh", 0, AppConst.DataRefreshRate / 3);
        }


        public void Show(Machine machine, OnDismiss onDismiss)
        {
            base.Show();
            _currentMachine = machine;
            _onDismiss = onDismiss;
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

                    clearList(_errorStates);

                    foreach(WatchItem item in _currentMachine.CurrentWatchItems)
                    {
                        GameObject errorState = Instantiate(_sampleErrorState);
                        Text title = errorState.GetComponent<Text>();
                        title.text = item.ItemName;
                        setErrorStateUIForEnum(errorState, item.State);
                        errorState.transform.SetParent(_panelErrorProvention.transform);
                        errorState.transform.localScale = Vector3.one;
                        errorState.SetActive(true);
                        _errorStates.Add(errorState);
                    }
                    _sampleErrorState.SetActive(false);
                }
                else
                {
                    _panelErrorProvention.SetActive(false);
                }


                //人员工作状态
                setErrorStateUI(_workerState.transform.Find("ErrorState").gameObject, _currentMachine.CurrentWorker.State == WorkingState.ON);

                //EHS数据
                setErrorStateUI(_ehsHandState.transform.Find("LeftHandState").gameObject, _currentMachine.CurrentEHS.ButtonLeft);
                setErrorStateUI(_ehsHandState.transform.Find("RightHandState").gameObject, _currentMachine.CurrentEHS.ButtonRight);
                setErrorStateUI(_ehsLightState, _currentMachine.CurrentEHS.LightData);

                if (_currentMachine.CurrentEHS.DoorData.Count > 0)
                {
                    clearList(_doors);

                    _ehsDoorState.SetActive(true);

                    for(int i = 0; i < _currentMachine.CurrentEHS.DoorData.Count; i++)
                    {
                        GameObject door = Instantiate(_doorSample, _ehsDoorState.transform);
                        Image bg = door.GetComponent<Image>();

                        door.transform.localScale = Vector3.one;
                        door.SetActive(true);
                        _doors.Add(door);
                        Text text = door.GetComponentInChildren<Text>();
                        text.text = (i + 1) + "号门" + (_currentMachine.CurrentEHS.DoorData[i] ? "关闭" : "打开" );
                        bg.color = _currentMachine.CurrentEHS.DoorData[i] ? ColorDoorClose : ColorDoorOpen;
                    }

                    
                }
                else
                {
                    _ehsDoorState.SetActive(false);
                }

            }
            else
            {
                Debug.LogError("Error: refresh machine detail ui with null machine data!");
            }
        }


        private void clearList(List<GameObject> list)
        {
            foreach (GameObject gObject in list)
            {
                Destroy(gObject);
            }

            list.Clear();
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

        /// <summary>
        /// 设定通用的，正常，警告，异常 UI
        /// </summary>
        /// <param name="targetObject"></param>
        /// <param name="isNormal"></param>
        private void setErrorStateUIForEnum(GameObject targetObject, ErrorState state)
        {
            Image iconState = targetObject.transform.Find("iconState").GetComponent<Image>();
            Text textState = targetObject.transform.Find("textState").GetComponent<Text>();

            if(state == ErrorState.NORMAL)
            {
                iconState.sprite = ImageYes;
                iconState.color = ColorYes;
                textState.text = "正常";
            }else if(state == ErrorState.WARNING)
            {
                iconState.sprite = ImageWarning;
                iconState.color = ColorWarning;
                textState.text = "警告";
            }
            else
            {
                iconState.sprite = ImageNo;
                iconState.color = ColorNo;
                textState.text = "异常";
            }
          
        }

       
    }

}