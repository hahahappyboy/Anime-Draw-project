
using System.Collections.Generic;
using UnityEngine;

namespace Frame {
    /// <summary>
    /// 对象池框架s
    /// </summary>
    public class ObjectPool : BaseSingleTon<ObjectPool> {
        //对象池
        private Dictionary<string, List<GameObject>> gameObjcetPool;
        
        protected ObjectPool() {
            gameObjcetPool = new Dictionary<string, List<GameObject>>();
        }
        /// <summary>
        /// 生成游戏对象
        /// </summary>
        /// <param name="objectName">对象名字</param>
        /// <param name="objectPrefabPath">对象prefab路径</param>
        /// <returns></returns>
        public GameObject CreateGameObject(string gameObjectName, string gameObjectPrefabPath) {
            GameObject gameObject = null;
            //判断池子里有没有该对象
            if (gameObjcetPool.ContainsKey(gameObjectName)&&
                gameObjcetPool[gameObjectName].Count>0) {
                gameObject = gameObjcetPool[gameObjectName][0];
                gameObject.SetActive(true);
                gameObjcetPool[gameObjectName].RemoveAt(0);
            } else {
                GameObject prefab = Resources.Load<GameObject>(gameObjectPrefabPath);
                gameObject = Object.Instantiate(prefab);
                gameObject.name = gameObjectName;
            }
            return gameObject;
        }
        /// <summary>
        /// 回收游戏对象
        /// </summary>
        /// <param name="gameObject"></param>
        public void RecycleGameObject(GameObject gameObject) {
            gameObject.SetActive(false);
            if (gameObjcetPool.ContainsKey(gameObject.name)) {
                gameObjcetPool[gameObject.name].Add(gameObject);
            } else {
                gameObjcetPool.Add(gameObject.name,new List<GameObject>(){gameObject});
            }
        }
        
    }
}