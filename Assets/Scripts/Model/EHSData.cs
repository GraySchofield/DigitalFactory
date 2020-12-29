using System;
using System.Collections.Generic;

namespace DGFactory{
    /// <summary>
    /// EHS 传感器数据
    /// </summary>
    public class EHSData
    {
        /// <summary>
        /// 光电感应
        /// </summary>
        public bool LightData{
            get;
            set;
        }

        /// <summary>
        /// 左手按钮
        /// </summary>
        public bool ButtonLeft{
            get;
            set;
        }

        /// <summary>
        /// 右手按钮
        /// </summary>
        public bool ButtonRight{
            get;
            set;
        }

        /// <summary>
        /// 紧急按钮
        /// </summary>
        public bool ButtonEmergency{
            get;
            set;
        }

        /// <summary>
        /// 开关门检测
        /// </summary>
        public List<bool> DoorData
        {
            get;
            private set;
        }

        public EHSData()
        {
            LightData = true;
            ButtonLeft = true;
            ButtonRight = true;
            ButtonEmergency = false;
            DoorData = new List<bool>();
        }
    }
}