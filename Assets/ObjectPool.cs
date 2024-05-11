using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectPool : MonoBehaviour
{
    private Dictionary<GameObject, List<GameObject>> pool = new Dictionary<GameObject, List<GameObject>>();

    public static ObjectPool instance;
   
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
  
    public void CreatePool(GameObject obj, int numberOfCopy)
    {
        if (!pool.ContainsKey(obj))
            pool.Add(obj, new List<GameObject>());

        List<GameObject> list = pool[obj];
        for (int i = 0; i < numberOfCopy; i++)
        {
            GameObject go = Instantiate(obj);
            go.name = obj.name;
            go.transform.parent = transform;
            go.SetActive(false);
            list.Add(go);
        }

    }
    public GameObject GetPooledObject(GameObject obj)
    {
        if (pool.ContainsKey(obj))
        {
            foreach (GameObject go in pool[obj])
                if (!go.activeInHierarchy)
                    return go;

        }
        CreatePool(obj, pool.ContainsKey(obj) == true ? 1 + (pool[obj].Count / 10) : 10);
        return GetPooledObject(obj);
    }


}