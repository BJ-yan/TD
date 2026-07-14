namespace Match3.Core.Script.Match3.Core.Env
{
    /// <summary>
    /// 位域写入工具, 生成Element的身份号信息，在指定范围内写成指定内容
    /// </summary>
    public static class LevelUtil
    {
        /// <summary>
        /// 将 value 中从 start 起 length 位替换为 newvalue。
        /// </summary>
        public static long Modify(long value, int start, int length, int newvalue)
        {
            // mask：要改的那一段位
            var mask = (1L << length) - 1 << start;
            // 清零再写入
            value &= ~mask;
            value |= (long)newvalue << start & mask;
            return value;
        }
    }
}