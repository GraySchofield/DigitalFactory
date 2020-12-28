using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 代表一个生产设备
/// </summary>
namespace DGFactory
{
    public class Machine
    {
        public string Name
        {
            get;
            private set;
        }

        public int Id
        {
            get;
            set;
        }

        public Worker CurrentWorker
        {
            get;
            private set;
        }

        //当前机器的放错状态
        public List<WatchItem> CurrentWatchItems
        {
            get;
            private set;
        }

        /// <summary>
        /// EHS 传感器数据
        /// </summary>
        public EHSData CurrentEHS
        {
            get;
            set;
        }

        public Machine(string name , int id)
        {
            Name = name;
            this.Id = id;
            CurrentWatchItems = new List<WatchItem>();
            CurrentEHS = new EHSData();
        }

        /// <summary>
        /// 工人进入开始工作
        /// </summary>
        /// <param name="worker"></param>
        public void WorkerIn(Worker worker)
        {
            CurrentWorker = worker;
            CurrentWorker.State = WorkingState.ON;
            CurrentWatchItems = new List<WatchItem>();
        }

        /// <summary>
        /// 工人离开
        /// </summary>
        public void WokerOut()
        {
            CurrentWorker = null;
        }


    }

}