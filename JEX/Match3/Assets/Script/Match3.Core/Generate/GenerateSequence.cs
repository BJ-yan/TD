using System;
using System.Collections.Generic;
using Match3.Core.Elements;

namespace Match3.Core.Generate
{
    public class GenerateSequence
    {
        public List<GenerateOption> options;
        
        /// <summary>
        /// 对齐 Jex：totalWeight → RandRange → 递减权重命中。
        /// allowed：本格允许的颜色（无三连候选）；null = 不限制。
        /// 返回 None 表示抽失败。
        /// </summary>
       public ElementType RequestOnce(Random rng, HashSet<ElementType> allowed = null)
        {
            // TODO 按你自己理解实现，对照伪码：
            // total = 0
            // foreach opt in Options:
            //   if allowed != null && !allowed.Contains(opt.Type) continue
            //   if opt.Weight <= 0 continue
            //   total += opt.Weight
            // if total == 0 return None
            // r = rng.Next(1, total + 1)   // 与 Jex RandRange(1, total+1) 同语义
            // foreach opt (同样过滤):
            //   r -= opt.Weight
            //   if r <= 0 return opt.Type
            // return None
            throw new NotImplementedException();
        }
    }
}