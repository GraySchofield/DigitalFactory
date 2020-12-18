using UnityEngine;
using System.Collections;

namespace DGFactory{

    /// <summary>
    /// 当前界面的视图状态
    /// </summary>
    public enum ViewState
    {
        FACTORY_OVERVIEW = 1, //工厂全景
        MACHINE_DETAIL = 2    //机器详情
    }

    /// <summary>
    /// 主要逻辑和数据的Controller
    /// </summary>
    public class GameManager : BaseManager<GameManager>
    {
        public Factory CurrentFactory;
        public MachineController[] MachineControllers;
        private ViewState currentViewState;

        protected void Awake()
        {
            base.Awake();
            currentViewState = ViewState.FACTORY_OVERVIEW;
            CurrentFactory = new Factory();

            //Test
            generateTestData();
        }

        private void Start()
        {
            //initialzize the machine controllers
            InitMachineControllers();
        }

        private void InitMachineControllers()
        {
            if(MachineControllers.Length > 0)
            {
                for( int i = 0; i < MachineControllers.Length; i++)
                {
                    MachineControllers[i].Refresh(CurrentFactory.CurrentLines["门铰链"].Machines[i], (machine) => {
                        Debug.Log("Cicked Machine : " + machine.Name);

                        if(this.currentViewState == ViewState.FACTORY_OVERVIEW)
                        {
                            //如果是工厂全景，这里要放大到工厂视图
                            //TODO:

                        }
                    });
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(this.currentViewState == ViewState.MACHINE_DETAIL)
                {
                    //如果是工厂视图要退出到工厂全景
                    //TODO:
                }
            }
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

                if(i == 0){
                    m.CurrentWatchItems.Add(new WatchItem("YaRuLuoSi", "压入螺丝监测"));
                    m.CurrentWatchItems.Add(new WatchItem("DuiKuaiLouZhuang", "推块漏装监测"));
                    m.CurrentWatchItems.Add(new WatchItem("TanPianFanZhuang", "弹片反装监测"));
                }

                if(i == 5){
                    m.CurrentWatchItems.Add(new WatchItem("QiuTouXiaoLou", "球头销漏监测"));
                    m.CurrentWatchItems.Add(new WatchItem("TiaoJieLuoDing", "调节螺钉装监测"));
                }

                line.Machines.Add(m);
            }

            CurrentFactory.addProductLine(line);
        }
        
    }

}