/// <summary>
/// buff类型
/// </summary>
public enum BuffType
{
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
    buff,
    debuff
}

/// <summary>
/// 执行类型
/// </summary>
public enum BuffCalculateType
{
    /// <summary>
    /// 一次
    /// </summary>
    Once,
    /// <summary>
    /// 每次
    /// </summary>
    Loop,
}



public class BuffBase
{
    public int ID;
    public BuffType type;
    public BuffShutDownType shutDownType;
    public BuffEffect effect;
    public BuffCalculateType calculateType;
    /// <summary>
    /// 如果是堆叠层数，表示最大层数，如果是时间，表示最大时间
    /// </summary>
    public int MaxLimit = 0;
    /// <summary>
    /// 执行时间
    /// </summary>
    public float Time = 0;
    /// <summary>
    /// 间隔时间
    /// </summary>
    public float CallFrequency = 1;
    /// <summary>
    /// 执行数值 比如加血就是每次加多少
    /// </summary>
    public float Num;
}
