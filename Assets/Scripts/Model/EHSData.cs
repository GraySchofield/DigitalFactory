using System;

namespace DGFactory{
    /// <summary>
    /// EHS 传感器数据
    /// </summary>
    public class EHSData
    {
        /// <summary>
        /// 光电感应
        /// </summary>
        public float LightData{
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
        public float DoorData{
            get;
            set;
        }

        public EHSData()
        {
            LightData = 0f;
            ButtonLeft = true;
            ButtonRight = true;
            ButtonEmergency = false;
            DoorData = 0f;

        }
    }
}