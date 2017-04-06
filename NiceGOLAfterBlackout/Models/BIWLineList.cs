using NiceGOLAfterBlackout.BIW_SFM_COF;
using System.Collections.Generic;

namespace NiceGOLAfterBlackout.Models
{
    
    abstract class TypedBIWLineList
    {
        public abstract void CheckLineStatusNext(IBIWLineWorker biwLineWorker);
        public abstract void StartProcessLineNext(IBIWLineWorker biwLineWorker);
        public abstract void StopProcessLineNext(IBIWLineWorker biwLineWorker);

    }

    class BIWLineList<U> where U : IBIWLine
    {
        private List<U> biwlinelist;

        public BIWLineList()
        {
            biwlinelist = new List<U>();
        }

        public void Add(U u) 
        {
            biwlinelist.Add(u);
        }

        public void CheckLineStatusNext(IBIWLineWorker biwLineWorker)
        {
           // biwLineWorker.CheckLineStatus(biwLineWorker);
        }

        public void StartProcessLineNext(IBIWLineWorker biwLineWorker)
        {
           // biwLineWorker.StartProcessLine(biwlinelist.Dequeue());
        }

        public void StopProcessLineNext(IBIWLineWorker biwLineWorker)
        {
           // biwLineWorker.StopProcessLine(biwlinelist.Dequeue());
        }

    }
}
