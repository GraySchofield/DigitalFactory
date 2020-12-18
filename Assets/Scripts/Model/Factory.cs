using System.Collections;
using System.Collections.Generic;
namespace DGFactory { 
    public class Factory
    {

        /// <summary>
        /// 工厂所有的生产线， 用产品名字来做Index
        /// </summary>
        public Dictionary<string, ProductLine> CurrentLines
        {
            get;
            private set;
        }

        public Factory()
        {
            CurrentLines = new Dictionary<string, ProductLine>();
        }

        /// <summary>
        /// 添加生产线
        /// </summary>
        /// <param name="line"></param>
        public void addProductLine(ProductLine line)
        {
            CurrentLines.Add(line.CurrentProduct.ProdutName, line);
        }
    }
}