using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace DGFactory
{

    /// <summary>
    /// 负责获取和服务器沟通来获取数据
    /// </summary>
    public class DataManager : BaseManager<DataManager>
    {
        private Factory _currentFactory;

        protected override void Awake()
        {
            //TestCode
             StartCoroutine(generateTestData());

            //True Code, 5 秒拉取一次数据
            //InvokeRepeating("DataFetchLoop", 0, 5); 
        }

        // Start is called before the first frame update
        void Start()
        {

        }


        private void DataFetchLoop()
        {
            StartCoroutine(getData());
        }

        /// <summary>
        /// 拉取数据
        /// </summary>
        /// <returns></returns>
        private IEnumerator getData()
        {
            UnityWebRequest www = UnityWebRequest.Get(AppConst.RequestUrl);

            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                //HandleError
                Debug.Log(www.error);
            }
            else
            {
                //TODO: parse the data from server
                Debug.Log("data from server : " + www.downloadHandler.text);
            }

        }


        //TODO: 需要从服务器获取工厂真实数据
        /// <summary>
        /// 测试数据，需要把数据
        /// </summary>
        private IEnumerator generateTestData()
        {
            if (_currentFactory == null)
                _currentFactory = new Factory();

            Product product = new Product("门铰链");

            ProductLine line = new ProductLine(product, "一汽大众");

            for (int i = 0; i < 5; i++)
            {
                Machine m = new Machine("TestMachine" + i);
                Worker worker = new Worker(i);
                m.WorkerIn(worker);
                if (i == 0)
                {
                    worker.State = WorkingState.OFF;
                    m.CurrentEHS.DoorData.Add(true);
                    m.CurrentEHS.DoorData.Add(true);
                    m.CurrentEHS.DoorData.Add(false);
                    m.CurrentEHS.DoorData.Add(true);


                }

                if (i == 0)
                {
                    m.CurrentWatchItems.Add(new WatchItem("YaRuLuoSi", "压入螺丝监测"));
                    m.CurrentWatchItems.Add(new WatchItem("DuiKuaiLouZhuang", "推块漏装监测"));
                    WatchItem item3 = new WatchItem("TanPianFanZhuang", "弹片反装监测");
                    item3.IsNormal = false;
                    m.CurrentWatchItems.Add(item3);
                }

                if (i == 4)
                {
                    m.CurrentWatchItems.Add(new WatchItem("QiuTouXiaoLou", "球头销漏监测"));
                    m.CurrentWatchItems.Add(new WatchItem("TiaoJieLuoDing", "调节螺钉装监测"));
                    m.CurrentEHS.DoorData.Add(true);
                    m.CurrentEHS.DoorData.Add(true);
                }

                line.Machines.Add(m);
            }

            _currentFactory.addProductLine(line);

            yield return new WaitForSeconds(2f);

            GameManager.Instance.Refresh(_currentFactory);

        }
    }
}
