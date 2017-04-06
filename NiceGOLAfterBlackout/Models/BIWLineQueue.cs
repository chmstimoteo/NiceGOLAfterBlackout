using NiceGOLAfterBlackout.BIW_SFM_AUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace NiceGOLAfterBlackout.Models
{
    interface IBIWLineWorker
    {
        void CheckLineStatus<T>(T biwLine) where T : struct, IBIW_SFM_GUI;
        void StartProcessLine<T>(T biwLine) where T : struct, IBIW_SFM_GUI;
        void StopProcessLine<T>(T biwLine) where T : struct, IBIW_SFM_GUI;

    }

    class BIWLineQueue
    {
        abstract class TypedBIWLineQueue
        {
            public abstract void CheckLineStatusNext(IBIWLineWorker biwLineWorker);
            public abstract void StartProcessLineNext(IBIWLineWorker biwLineWorker);
            public abstract void StopProcessLineNext(IBIWLineWorker biwLineWorker);

        }

        class TypedBIWLineQueue<T> : TypedBIWLineQueue where T : struct, IBIW_SFM_GUI
        {
            Queue<T> m_queue = new Queue<T>();

            public void Enqueue(T biwLine)
            {
                m_queue.Enqueue(biwLine);
            }

            public override void CheckLineStatusNext(IBIWLineWorker biwLineWorker)
            {
                biwLineWorker.CheckLineStatus(m_queue.Dequeue());
            }

            public override void StartProcessLineNext(IBIWLineWorker biwLineWorker)
            {
                biwLineWorker.StartProcessLine(m_queue.Dequeue());
            }

            public override void StopProcessLineNext(IBIWLineWorker biwLineWorker)
            {
                biwLineWorker.StopProcessLine(m_queue.Dequeue());
            }
        }

        Queue<Type> m_listSelectorQueue = new Queue<Type>();
        Dictionary<Type, TypedBIWLineQueue> m_lists =
            new Dictionary<Type, TypedBIWLineQueue>();

        public void Enqueue<T>(T biwLine) where T : struct, IBIW_SFM_GUI
        {
            TypedBIWLineQueue<T> queue;
            if (!m_lists.ContainsKey(typeof(T)))
            {
                queue = new TypedBIWLineQueue<T>();
                m_lists[typeof(T)] = queue;
            }
            else
                queue = (TypedBIWLineQueue<T>)m_lists[typeof(T)];

            queue.Enqueue(biwLine);
            m_listSelectorQueue.Enqueue(typeof(T));
        }

        public void CheckLineStatusNext(IBIWLineWorker biwLineWorker)
        {
            var type = m_listSelectorQueue.Dequeue();
            m_lists[type].CheckLineStatusNext(biwLineWorker);
        }

        public void StartProcessLineNext(IBIWLineWorker biwLineWorker)
        {
            var type = m_listSelectorQueue.Dequeue();
            m_lists[type].StartProcessLineNext(biwLineWorker);
        }
        public void StopProcessLineNext(IBIWLineWorker biwLineWorker)
        {
            var type = m_listSelectorQueue.Dequeue();
            m_lists[type].StopProcessLineNext(biwLineWorker);
        }
    }
}
