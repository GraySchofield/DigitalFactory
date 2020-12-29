using System;

namespace DGFactory{

    public enum ErrorState
    {
        NORMAL = 0,
        WARNING = 1,
        ERROR = 2
    }

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
    public ErrorState State
        {
        get;
        set;
    }
    
    public WatchItem(string key, string name)
    {
        ItemKey = key;
        ItemName = name;
        State = ErrorState.NORMAL;
    }
}

}