using UnityEngine;
using System.Collections;

namespace DGFactory{

    /// <summary>
    /// 当前界面的视图状态
    /// </summary>
    public enum ViewState
    {
        FACTORY_OVERVIEW = 1, //工厂全景
        MACHINE_DETAIL = 3    //机器详情
    }

    /// <summary>
    /// 主要逻辑和数据的Controller
    /// </summary>
    public class GameManager : BaseManager<GameManager>
    {

        #region UIControls
        public UIMachineDetail UIMachineDetail;
        public UIProductionLine UIProductionLine;
        public UILoading UILoading;
        #endregion

        public Factory CurrentFactory;
        public MachineController[] MachineControllers;
        private ViewState _currentViewState;
        private TransitionAndLookAt _cameraTransitor;

        protected override void Awake()
        {
            base.Awake();
            _cameraTransitor = Camera.main.GetComponent<TransitionAndLookAt>();
            _currentViewState = ViewState.FACTORY_OVERVIEW;
            UILoading.Show();
        }


        /// <summary>
        /// 设定全部机器外框
        /// </summary>
        /// <param name="isVisible"></param>
        private void setAllMachineOutline(bool isVisible)
        {
            foreach(MachineController mc in MachineControllers)
            {
                mc.SetOutline(isVisible);
            }
        }


        /// <summary>
        /// 传入数据
        /// </summary>
        /// <param name="factory"></param>
        public void Refresh(Factory factory)
        {
            CurrentFactory = factory;
            InitMachineControllers();
            UIProductionLine.Refresh(CurrentFactory.CurrentLines[AppConst.ProductName]);
            //刷新数据后隐藏Loading
            UILoading.Hide();
        }

        private void InitMachineControllers()
        {
            if(MachineControllers.Length > 0)
            {
                for( int i = 0; i < MachineControllers.Length; i++)
                {
                    MachineControllers[i].Refresh(CurrentFactory.CurrentLines[AppConst.ProductName].Machines[i], (mControler, machine) => {
                        Debug.Log("Cicked Machine : " + machine.Name);
                        setAllMachineOutline(false);
                        showMachineDetail(mControler);
                    });
                }
            }
        }

        private void showMachineDetail(MachineController mControler)
        {
            if (this._currentViewState == ViewState.FACTORY_OVERVIEW)
            {
                //如果是工厂全景，这里要放大到工厂视图
                _cameraTransitor.transitionTo(mControler.DetailAnchor.position, mControler.DetailAnchor.rotation);
                _currentViewState = ViewState.MACHINE_DETAIL;
                mControler.SetOutline(true);
                UIMachineDetail.Show(mControler.CurrentMachine, ()=> {
                    hideMachineDetail();
                });
                
            }
        }

        private void hideMachineDetail()
        {
            if (this._currentViewState == ViewState.MACHINE_DETAIL)
            {
                //如果是工厂视图要退出到工厂全景
                //TODO:
                _cameraTransitor.transitionBack();
                this._currentViewState = ViewState.FACTORY_OVERVIEW;
                setAllMachineOutline(false);

            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                UIProductionLine.Show();
            }
        }

        private MachineController getSelectedMachine()
        {
            foreach(MachineController mc in MachineControllers)
            {
                if (mc.IsSelected)
                {
                    return mc;
                }
            }

            return null;
        }
        
    }

}