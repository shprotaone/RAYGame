using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private List<ObjectInfo> objectsInfo;

    private Dictionary<ObjectType, Pool> pools;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        InitPool();
    }

    private void InitPool()
    {
        pools = new Dictionary<ObjectType, Pool>();

        var emptyGO = new GameObject();

        foreach (var item in objectsInfo)
        {
            var container = Instantiate(emptyGO, transform, false);
            container.name = item.type.ToString();

            pools[item.type] = new Pool(container.transform);

            for (int i = 0; i < item.startCount; i++)
            {
                var go = InstantiateObject(item.type, container.transform);
                pools[item.type].Objects.Enqueue(go);
            }
        }

        Destroy(emptyGO);
    }

    private GameObject InstantiateObject(ObjectType type, Transform parent)
    {
        var go = Instantiate(objectsInfo.Find(x => x.type == type).prefab, parent);
        go.SetActive(false);
        return go;
    }

    public GameObject GetObject(ObjectType type)
    {
        var obj = pools[type].Objects.Count > 0 ?
            pools[type].Objects.Dequeue() : InstantiateObject(type, pools[type].Container);

        obj.SetActive(true);

        return obj;
    }

    public void DestroyObject(GameObject obj)
    {
        ObjectType type = obj.GetComponent<IPooledObject>().Type;
        pools[type].Objects.Enqueue(obj);
        obj.transform.SetParent(pools[type].Container);
        obj.transform.position = pools[type].Container.transform.position;
        obj.SetActive(false);
    }
}

