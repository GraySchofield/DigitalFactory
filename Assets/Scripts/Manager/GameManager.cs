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
        #endregion

        public Factory CurrentFactory;
        public MachineController[] MachineControllers;
        private ViewState _currentViewState;
        private TransitionAndLookAt _cameraTransitor;

        protected void Awake()
        {
            base.Awake();
            _cameraTransitor = Camera.main.GetComponent<TransitionAndLookAt>();
            _currentViewState = ViewState.FACTORY_OVERVIEW;
            CurrentFactory = new Factory();

            //Test
            generateTestData();

            //Test Commit
        }

        private void Start()
        {
            //initialzize the machine controllers
            InitMachineControllers();
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

        private void InitMachineControllers()
        {
            if(MachineControllers.Length > 0)
            {
                for( int i = 0; i < MachineControllers.Length; i++)
                {
                    MachineControllers[i].Refresh(CurrentFactory.CurrentLines["门铰链"].Machines[i], (mControler, machine) => {
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

        //TODO: 需要从服务器获取工厂真实数据
        /// <summary>
        /// 测试数据，需要把数据
        /// </summary>
        private void generateTestData(){

            Product product = new Product("门铰链");

            ProductLine line = new ProductLine(product, "一汽大众");

            for(int i = 0 ; i < 5 ; i ++){
                Machine m = new Machine("TestMachine" + i);
                Worker worker = new Worker(i);
                m.WorkerIn(worker);
                if(i == 0)
                {
                    worker.State = WorkingState.OFF;
                }

                if(i == 0){
                    m.CurrentWatchItems.Add(new WatchItem("YaRuLuoSi", "压入螺丝监测"));
                    m.CurrentWatchItems.Add(new WatchItem("DuiKuaiLouZhuang", "推块漏装监测"));
                    WatchItem item3 = new WatchItem("TanPianFanZhuang", "弹片反装监测");
                    item3.IsNormal = false;
                    m.CurrentWatchItems.Add(item3);
                }

                if(i == 5){
                    m.CurrentWatchItems.Add(new WatchItem("QiuTouXiaoLou", "球头销漏监测"));
                    m.CurrentWatchItems.Add(new WatchItem("TiaoJieLuoDing", "调节螺钉装监测"));
                }

                line.Machines.Add(m);
            }

            CurrentFactory.addProductLine(line);

            UIProductionLine.Refresh(line);

        }
        
    }

}