using System.Collections.Generic;
/// <summary>
/// buff类型
/// </summary>
public enum BuffType
{
    Null,
    /// <summary>
    /// 减速
    /// </summary>
    decelerate,
    /// <summary>
    /// 伤害
    /// </summary>
    damage
}
/// <summary>
/// 关闭类型
/// </summary>
public enum BuffShutDownType
{
    Null,
    /// <summary>
    /// 关闭所有
    /// </summary>
    All,
    /// <summary>
    /// 单层关闭
    /// </summary>
    Layer,
}
/// <summary>
/// buff效果（增益减益）
/// </summary>
public enum BuffEffect
{
    Null,
    buff,
    debuff
}

/// <summary>
/// 执行类型
/// </summary>
public enum BuffCalculateType
{
    Null,
    /// <summary>
    /// 一次
    /// </summary>
    Once,
    /// <summary>
    /// 每次
    /// </summary>
    Loop,
}

/// <summary>
/// 叠加类型
/// </summary>
public enum BuffOverlap
{
    Null,
    /// <summary>
    /// 增加时间
    /// </summary>
    StackedTime,
    /// <summary>
    /// 堆叠层数
    /// </summary>
    StackedLayer,
    /// <summary>
    /// 重置时间
    /// </summary>
    ResterTime,
}


public class BuffBase
{
    public int ID;
    public BuffType type;
    public BuffShutDownType shutDownType;
    public BuffEffect effect;
    public BuffCalculateType calculateType;
    public BuffOverlap overlap;
    /// <summary>
    /// 如果是堆叠层数，表示最大层数，如果是时间，表示最大时间
    /// </summary>
    public int maxLimit = 0;
    /// <summary>
    /// 执行时间
    /// </summary>
    public float time = 0;
    /// <summary>
    /// 间隔时间
    /// </summary>
    public float callFrequency = 1;
    /// <summary>
    /// 执行数值 比如加血就是每次加多少
    /// </summary>
    public float num;
    public string name;
    public BuffBase()
    {
        ID = -1;
    }
    public BuffBase(BuffType type = 0, int id = -1, BuffShutDownType shutDownType = 0,
        BuffEffect effect = 0, BuffCalculateType calculateType = 0, BuffOverlap overlap = 0,
        int maxLimit = -1, float time = -1, float callFrequency = -1, float num = -1, string name = "Null")
    {
        this.type = type;
        this.ID = id;
        this.shutDownType = shutDownType;
        this.effect = effect;
        this.calculateType = calculateType;
        this.overlap = overlap;
        this.maxLimit = maxLimit;
        this.time = time;
        this.callFrequency = callFrequency;
        this.num = num;
        this.name = name;
    }
}
