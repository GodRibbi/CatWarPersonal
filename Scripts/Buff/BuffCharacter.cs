using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCharacter : MonoBehaviour
{
    [SerializeField]
    public List<BuffData> buffs;
    public void AddBuff(BuffData buff)
    {
        buffs.Add(buff);
    }
    public void Remove(BuffData buff)
    {
        buffs.Remove(buff);
    }
}
