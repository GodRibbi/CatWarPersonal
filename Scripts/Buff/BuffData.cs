using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffData 
{
    public BuffBase buffBase;
    public Action<Characters> mainAction;
    public BuffData()
    {
        this.buffBase = new BuffBase();
        this.mainAction = null;
    }
    public BuffData(BuffBase buffBase, Action<Characters> mainAction = null)
    {
        this.buffBase = buffBase;
        this.mainAction = mainAction;
    }
}
