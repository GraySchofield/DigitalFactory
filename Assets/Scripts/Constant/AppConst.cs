using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用来存储系统常亮
/// </summary>
public static class AppConst
{
    public static string RequestUrl = "http://106.14.205.236:12345/";
    public static string RouteEhs = "state/ehs";  //EHS data
    public static string RouteWorker = "state/prl"; //worker state data
    public static string RoutePy = "state/py"; //error detect data
    public static string RouteOperation = "state/on"; //Operation data

    public static string ProductName = "doorHinge"; //目前暂时hardcode，产品名字

    public static string StateWork = "WORK";
    public static string StateWarning = "WARNING";
    public static string StateError = "ERROR";
}




//TODO: 配置数据改成从ScriptableObject 获取，或者JsonConfig 中获取