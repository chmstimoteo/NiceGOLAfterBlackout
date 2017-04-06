using NiceGOLAfterBlackout.BIW_SFM_AUE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceGOLAfterBlackout.Models
{
    interface IBIWLine
    {
        void CheckLineStatus<T>(T biwLineWorker) where T : IBIW_SFM_GUI;
        void StartProcessLine<T>(T biwLineWorker) where T : IBIW_SFM_GUI;
        void StopProcessLine<T>(T biwLineWorker) where T : IBIW_SFM_GUI;
    }
}
