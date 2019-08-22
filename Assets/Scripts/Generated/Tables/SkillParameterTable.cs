using MasterMemory;
using MessagePack;
using System.Collections.Generic;
using System;

namespace MasterData.Tables
{
   public sealed partial class SkillParameterTable : TableBase<SkillParameter>
   {
        readonly Func<SkillParameter, int> primaryIndexSelector;

        readonly SkillParameter[] secondaryIndex0;
        readonly Func<SkillParameter, (int SkillID, int SkillLv)> secondaryIndex0Selector;

        public SkillParameterTable(SkillParameter[] sortedData)
            : base(sortedData)
        {
            this.primaryIndexSelector = x => x.SkillID;
            this.secondaryIndex0Selector = x => (x.SkillID, x.SkillLv);
            this.secondaryIndex0 = CloneAndSortBy(this.secondaryIndex0Selector, System.Collections.Generic.Comparer<(int SkillID, int SkillLv)>.Default);
        }

        public RangeView<SkillParameter> SortBySkillIDAndSkillLv => new RangeView<SkillParameter>(secondaryIndex0, 0, secondaryIndex0.Length - 1, true);

        public RangeView<SkillParameter> FindBySkillID(int key)
        {
            return FindManyCore(data, primaryIndexSelector, System.Collections.Generic.Comparer<int>.Default, key);
        }

        public RangeView<SkillParameter> FindClosestBySkillID(int key, bool selectLower = true)
        {
            return FindManyClosestCore(data, primaryIndexSelector, System.Collections.Generic.Comparer<int>.Default, key, selectLower);
        }

        public RangeView<SkillParameter> FindRangeBySkillID(int min, int max, bool ascendant = true)
        {
            return FindManyRangeCore(data, primaryIndexSelector, System.Collections.Generic.Comparer<int>.Default, min, max, ascendant);
        }

        public SkillParameter FindBySkillIDAndSkillLv((int SkillID, int SkillLv) key)
        {
            return FindUniqueCore(secondaryIndex0, secondaryIndex0Selector, System.Collections.Generic.Comparer<(int SkillID, int SkillLv)>.Default, key);
        }

        public SkillParameter FindClosestBySkillIDAndSkillLv((int SkillID, int SkillLv) key, bool selectLower = true)
        {
            return FindUniqueClosestCore(secondaryIndex0, secondaryIndex0Selector, System.Collections.Generic.Comparer<(int SkillID, int SkillLv)>.Default, key, selectLower);
        }

        public RangeView<SkillParameter> FindRangeBySkillIDAndSkillLv((int SkillID, int SkillLv) min, (int SkillID, int SkillLv) max, bool ascendant = true)
        {
            return FindUniqueRangeCore(secondaryIndex0, secondaryIndex0Selector, System.Collections.Generic.Comparer<(int SkillID, int SkillLv)>.Default, min, max, ascendant);
        }

    }
}