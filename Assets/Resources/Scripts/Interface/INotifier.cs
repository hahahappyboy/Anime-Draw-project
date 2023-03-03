
using System.Collections;
using System.Collections.Generic;

namespace Interface {
    /// <summary>
    /// 消息通知者
    /// </summary>
    public interface INotifier {
        public abstract void AddObserver(IObserver observer);
        public abstract void RemoveObserver(IObserver observer);
        public abstract void NotifyObserver(Dictionary<string,object> message);
    }
}