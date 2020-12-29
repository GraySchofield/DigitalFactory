using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

namespace DGFactory
{

    public delegate void DataResponseHandler(JSONNode data);

    /// <summary>
    /// 负责获取和服务器沟通来获取数据
    /// </summary>
    public class DataManager : BaseManager<DataManager>
    {
        private Factory _currentFactory;

        protected override void Awake()
        {
            //先获取初始运营数据，用来构建Factory Model
            StartCoroutine(getData(AppConst.RouteOperation, handOperationData));

        }

        // Start is called before the first frame update
        void Start()
        {

        }


        private void DataFetchLoop()
        {
            StartCoroutine(getData(AppConst.RouteOperation, handOperationData));
            StartCoroutine(getData(AppConst.RouteEhs, handleEhsData));
            StartCoroutine(getData(AppConst.RouteWorker, handleWorkerData));
            StartCoroutine(getData(AppConst.RoutePy, handlePYData));
        }


        private void handOperationData(JSONNode data)
        {
            string productName = data["product"].Value;
            string customerName = data["customer"].Value;
            int count = data["quantity"].AsInt;
            int pace = data["cycle"].AsInt;
            int badCount = data["ng"].AsInt;
            AppConst.ProductName = productName;

            if (_currentFactory == null)
            {
                //第一次获取到数据的时候生成工厂数据
                generateInitialData(customerName);
                InvokeRepeating("DataFetchLoop", 0, AppConst.DataRefreshRate);
            }

            ProductLine line = _currentFactory.CurrentLines[productName];
            line.Pace = pace;
            line.ProductionCount = count;
            line.BadProductionCount = badCount;
            
        }

        private void handleEhsData(JSONNode data)
        {
            JSONArray array = data.AsArray;
            ProductLine line = _currentFactory.CurrentLines[AppConst.ProductName];

            for (int i = 0; i < array.Count; i++)
            {
                JSONNode node = array[i];
                int id = node["num"].AsInt;
                if (id > line.Machines.Count) continue;
                Machine m = line.Machines[id - 1];
                if (id <= line.Machines.Count)  //正常应该只有5 个机器
                {
                    if (node["buttons"].Value.Equals("WORK"))
                    {
                        m.CurrentEHS.ButtonLeft = true;
                        m.CurrentEHS.ButtonRight = true;
                    }
                    else
                    {
                        m.CurrentEHS.ButtonLeft = false;
                        m.CurrentEHS.ButtonRight = false;
                    }

                    if (node["bright"].Value.Equals("WORK"))
                    {
                        m.CurrentEHS.LightData = true;
                    }
                    else
                    {
                        m.CurrentEHS.LightData = false;
                    }

                    m.CurrentEHS.DoorData.Clear();

                    JSONArray doorArray = node["doors"].AsArray;
                    for(int j = 0;  j < doorArray.Count; j++)
                    {
                        JSONNode dNode = doorArray[j];
                        m.CurrentEHS.DoorData.Add(dNode["state"].Value.Equals("WORK"));
                    }
                }
            }
            
        }


        /// <summary>
        /// 处理工作人员数据
        /// </summary>
        /// <param name="data"></param>
        private void handleWorkerData(JSONNode data)
        {
            JSONArray array = data.AsArray;
            Debug.Log("Worker data : " + array.ToString());
            ProductLine line = _currentFactory.CurrentLines[AppConst.ProductName];
       
            for (int i = 0; i < array.Count; i++)
            {
                JSONNode node = array[i];

                Worker worker = line.Machines[node["num"].AsInt - 1].CurrentWorker;

                string state = node["state"].Value;
                Debug.Log("Work state is : " + state);
                if (state.Equals(AppConst.StateWork))
                {
                    worker.State = WorkingState.ON;
                }
                else {
                    worker.State = WorkingState.OFF;
                };

            }
        }


        /// <summary>
        /// 处理防错状态数据
        /// </summary>
        /// <param name="data"></param>
        private void handlePYData(JSONNode data)
        {
            JSONArray array = data.AsArray;
            ProductLine line = _currentFactory.CurrentLines[AppConst.ProductName];
            foreach (Machine m in line.Machines)
            {
                m.CurrentWatchItems.Clear();
            };
            for (int i = 0; i < array.Count; i++)
            {
                JSONNode node = array[i];
                int id = node["num"].AsInt;
                if (id > line.Machines.Count) continue;

                WatchItem item = new WatchItem(node["title"].Value, node["label"].Value);
                string state = node["state"].Value;
                if (state.Equals("WORK"))
                {
                    item.State = ErrorState.NORMAL;
                }else if (state.Equals("WARNING"))
                {
                    item.State = ErrorState.WARNING;
                }
                else
                {
                    item.State = ErrorState.ERROR;
                }
               
                line.Machines[id - 1].CurrentWatchItems.Add(item);
            }

        }

        /// <summary>
        /// 拉取数据
        /// </summary>
        /// <returns></returns>
        private IEnumerator getData(string endPoint, DataResponseHandler hanlder)
        {
            UnityWebRequest www = UnityWebRequest.Get(AppConst.RequestUrl + endPoint);

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
                JSONNode node = JSONNode.Parse(www.downloadHandler.text);
                int code = node["stat"].AsInt;
                if(code == 0)
                {
                    //no error
                    hanlder(node["data"]);

                }
            }

        }


        //TODO: 需要从服务器获取工厂真实数据
        /// <summary>
        /// 测试数据，需要把数据
        /// </summary>
        private void generateInitialData(string customer)
        {
            if (_currentFactory == null)
                _currentFactory = new Factory();

            Product product = new Product(AppConst.ProductName);

            ProductLine line = new ProductLine(product, customer);

            for (int i = 0; i < 5; i++)
            {
                Machine m = new Machine("TestMachine" + i, i + 1);
                Worker worker = new Worker(i);
                m.WorkerIn(worker);
                line.Machines.Add(m);
            }

            _currentFactory.addProductLine(line);


            GameManager.Instance.Refresh(_currentFactory);

        }
    }
}
