using System;

namespace DGFactory
{
    public enum WorkingState
    {
        ON,
        OFF
    }

    /// <summary>
    /// 工人
    /// </summary>
    public class Worker
    {

        public int Id
        {
            get;
            private set;
        }

        public WorkingState State
        {
            get;
            set;
        }

        public Worker(int id)
        {
            this.Id = id;
            this.State = WorkingState.OFF;
        }
    }

}