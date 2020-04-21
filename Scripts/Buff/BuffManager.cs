using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    #region 封装
    private static BuffManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    [SerializeField]
    public List<BuffBase> allBuffs;
    /// <summary>
    /// id=1，伤害，中毒
    /// id=2，减速
    /// </summary>
    /// <param name="buff"></param>
    /// <returns></returns>
    public BuffData AddBuff(BuffData buff, Characters ch)
    {
        BuffData data = new BuffData();
        if (buff.buffBase.ID == -1)
        {
            Debug.LogError("AddBuff error type==null");
            return null;
        }
        switch (buff.buffBase.ID)
        {
            case (1):
                data = new BuffData();
                break;
        }
        return buff;
    }
    public BuffBase GetBuff(int id)
    {
        return allBuffs[id];
    }
}

