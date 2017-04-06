using NiceGOLAfterBlackout.BIW_SFM_FDA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceGOLAfterBlackout.Models
{
    class BIWLine<T,S> //: IBIWLine
    {
        private string _name { get; set; }
        private T _sfm_wcf { get; set; }
        private S _sfm_state { get; set; }

        public BIWLine(string name, T sfm_wcf, S sfm_state)
        {
            _name = name;
            _sfm_wcf = sfm_wcf;
            _sfm_state = sfm_state;
        }

        public void CheckLineStatus<T1>() where T1 : IBIW_SFM_GUI
        {
            throw new NotImplementedException();
        }

        public void StartProcessLine<T1>() where T1 : IBIW_SFM_GUI
        {
            throw new NotImplementedException();
        }

        public void StopProcessLine<T1>() where T1 : IBIW_SFM_GUI
        {
            throw new NotImplementedException();
        }
    }
}
