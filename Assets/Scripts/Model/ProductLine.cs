using System;
using System.Collections.Generic;

namespace DGFactory
{
    public enum ProductionState
    {
        PAUSED, //暂停生产
        RUNNING,   //正在生产
        DOWN    //停止生产
    }

    /// <summary>
    /// 代表一条生产线
    /// </summary>
    public class ProductLine
    {

        //客户信息
        public string ClientName
        {
            get;
            set;
        }

        //当前生产的产品
        public Product CurrentProduct
        {
            get;
            private set;
        }

        //节拍
        public int Pace
        {
            get;
            set;
        }

        //当前生产数量
        public int ProductionCount
        {
            get;
            set;
        }

        //不良产品数量
        public int BadProductionCount
        {
            get;
            set;
        }

        //当前的生产线状态
        public ProductionState State
        {
            get;
            set;
        }

        
        public List<Machine> Machines
        {
            get;
            private set;
        }

        public ProductLine(Product product, String client)
        {
            CurrentProduct = product;
            ClientName = client;
            State = ProductionState.RUNNING;
            Machines = new List<Machine>();
        }
    }

}