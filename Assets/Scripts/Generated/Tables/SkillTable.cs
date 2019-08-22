using MasterMemory;
using MessagePack;
using System.Collections.Generic;
using System;

namespace MasterData.Tables
{
   public sealed partial class SkillTable : TableBase<Skill>
   {
        readonly Func<Skill, int> primaryIndexSelector;


        public SkillTable(Skill[] sortedData)
            : base(sortedData)
        {
            this.primaryIndexSelector = x => x.SkillID;
        }


        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Skill FindBySkillID(int key)
        {
            var lo = 0;
            var hi = data.Length - 1;
            while (lo <= hi)
            {
                var mid = (int)(((uint)hi + (uint)lo) >> 1);
                var selected = data[mid].SkillID;
                var found = (selected < key) ? -1 : (selected > key) ? 1 : 0;
                if (found == 0) { return data[mid]; }
                if (found < 0) { lo = mid + 1; }
                else { hi = mid - 1; }
            }
            return default;
        }

        public Skill FindClosestBySkillID(int key, bool selectLower = true)
        {
            return FindUniqueClosestCore(data, primaryIndexSelector, System.Collections.Generic.Comparer<int>.Default, key, selectLower);
        }

        public RangeView<Skill> FindRangeBySkillID(int min, int max, bool ascendant = true)
        {
            return FindUniqueRangeCore(data, primaryIndexSelector, System.Collections.Generic.Comparer<int>.Default, min, max, ascendant);
        }

    }
}