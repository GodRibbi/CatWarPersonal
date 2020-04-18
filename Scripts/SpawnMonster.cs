using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public static SpawnMonster instance;
    public GameObject metee;
    public GameObject remote;
    public Transform enemys;
    public Dictionary<string, List<GameObject>> monsterPool;
    public List<GameObject> monsterList;
    [Header("决定了内空心的半径")]
    public float insideRadius;
    [Header("决定了生成的空心圆的范围")]
    public float outsideRadius;
    private void Awake()
    {
        instance = this;
        monsterPool = new Dictionary<string, List<GameObject>>();
        monsterList = new List<GameObject>();
    }
    public void SpawnMetee()
    {
        Vector2 p = Random.insideUnitCircle * outsideRadius;
        Vector2 pos = p.normalized * (insideRadius + p.magnitude);
        Vector3 pos2 = new Vector3(pos.x, pos.y, 0);
        GameObject go = ObjectPool.GetInstance().GetObj(metee, pos2, Quaternion.identity, enemys);
        PushMonsterPool(go);
    }
    public void SpawnRemote()
    {
        Vector2 p = Random.insideUnitCircle * outsideRadius;
        Vector2 pos = p.normalized * (insideRadius + p.magnitude);
        Vector3 pos2 = new Vector3(pos.x, pos.y, 0);
        GameObject go = ObjectPool.GetInstance().GetObj(remote, pos2, Quaternion.identity, enemys);
        PushMonsterPool(go);
    }
    public void PushMonsterPool(GameObject prefab)
    {
        string objName = prefab.name;
        if (monsterPool.ContainsKey(objName))
        {
            monsterPool[objName].Add(prefab);
        }
        else
        {
            monsterPool.Add(objName, new List<GameObject>() { prefab });
        }
        monsterList.Add(prefab);
        Debug.Log(monsterPool[objName].Count);
    }
    public void PopMonsterPool(GameObject prefab)
    {
        monsterPool[prefab.name].Remove(prefab);
        monsterList.Remove(prefab);
        Debug.Log(monsterPool[prefab.name].Count);
    }
}
