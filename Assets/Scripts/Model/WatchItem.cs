using System;

namespace DGFactory{

/// <summary>
/// 代表机器上面的检测选项
/// </summary>
public class WatchItem
{
    public string ItemKey{
        get;
        private set;
    }

    public string ItemName{
        get;
        private set;
    }


    //是否正常运行
    public bool IsNormal{
        get;
        set;
    }
    
    public WatchItem(string key, string name)
    {
        ItemKey = key;
        ItemName = name;
        IsNormal = true;
    }
}

}