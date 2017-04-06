using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceGOLAfterBlackout.Models
{
    public sealed class SgMThBgWorker
    {
        private static volatile BackgroundWorker instance;
        private static object syncRoot = new Object();

        private SgMThBgWorker() { }

        public static BackgroundWorker getInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new BackgroundWorker();
                    }
                }

                return instance;
            }
        }
    }
}
