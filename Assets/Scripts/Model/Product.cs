using System;
/// <summary>
/// 代表生成的产品
/// </summary>
namespace DGFactory
{
    public class Product
    {
        public int ProductId
        {
            get;
            set;
        }

        public string ProdutName
        {
            get;
            set;
        }

        //TODO:这里的ID 暂时是随机的
        public Product(String name)
        {
            this.ProdutName = name;
            this.ProductId = (int)Math.Floor((double)UnityEngine.Random.Range(0, 1000000));
        }

    }

}