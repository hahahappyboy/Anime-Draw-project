using System.Collections.Generic;

namespace Interface {
    public interface IObserver {
        /// <summary>
        /// 通知者告知观察者
        /// </summary>
        /// <param name="message"></param>
        public abstract void Notify(Dictionary<string,object> message);
    }
}