using MasterMemory;
using MessagePack;
using System.Collections.Generic;
using System;
using MasterData.Tables;

namespace MasterData
{
   public sealed class ImmutableBuilder : ImmutableBuilderBase
   {
        MemoryDatabase memory;

        public ImmutableBuilder(MemoryDatabase memory)
        {
            this.memory = memory;
        }

        public MemoryDatabase Build()
        {
            return memory;
        }

        public void ReplaceAll(System.Collections.Generic.IList<Person> data)
        {
            var newData = CloneAndSortBy(data, x => x.PersonId, System.Collections.Generic.Comparer<int>.Default);
            var table = new PersonTable(newData);
            memory = new MemoryDatabase(
                table,
                memory.SkillTable,
                memory.SkillParameterTable
            
            );
        }

        public void RemovePerson(int[] keys)
        {
            var data = RemoveCore(memory.PersonTable.GetRawDataUnsafe(), keys, x => x.PersonId, System.Collections.Generic.Comparer<int>.Default);
            var newData = CloneAndSortBy(data, x => x.PersonId, System.Collections.Generic.Comparer<int>.Default);
            var table = new PersonTable(newData);
            memory = new MemoryDatabase(
                table,
                memory.SkillTable,
                memory.SkillParameterTable
            
            );
        }

        public void Diff(Person[] addOrReplaceData)
        {
            var data = DiffCore(memory.PersonTable.GetRawDataUnsafe(), addOrReplaceData, x => x.PersonId, System.Collections.Generic.Comparer<int>.Default);
            var newData = CloneAndSortBy(data, x => x.PersonId, System.Collections.Generic.Comparer<int>.Default);
            var table = new PersonTable(newData);
            memory = new MemoryDatabase(
                table,
                memory.SkillTable,
                memory.SkillParameterTable
            
            );
        }

        public void ReplaceAll(System.Collections.Generic.IList<Skill> data)
        {
            var newData = CloneAndSortBy(data, x => x.SkillID, System.Collections.Generic.Comparer<int>.Default);
            var table = new SkillTable(newData);
            memory = new MemoryDatabase(
                memory.PersonTable,
                table,
                memory.SkillParameterTable
            
            );
        }

        public void RemoveSkill(int[] keys)
        {
            var data = RemoveCore(memory.SkillTable.GetRawDataUnsafe(), keys, x => x.SkillID, System.Collections.Generic.Comparer<int>.Default);
            var newData = CloneAndSortBy(data, x => x.SkillID, System.Collections.Generic.Comparer<int>.Default);
            var table = new SkillTable(newData);
            memory = new MemoryDatabase(
                memory.PersonTable,
                table,
                memory.SkillParameterTable
            
            );
        }

        public void Diff(Skill[] addOrReplaceData)
        {
            var data = DiffCore(memory.SkillTable.GetRawDataUnsafe(), addOrReplaceData, x => x.SkillID, System.Collections.Generic.Comparer<int>.Default);
            var newData = CloneAndSortBy(data, x => x.SkillID, System.Collections.Generic.Comparer<int>.Default);
            var table = new SkillTable(newData);
            memory = new MemoryDatabase(
                memory.PersonTable,
                table,
                memory.SkillParameterTable
            
            );
        }

        public void ReplaceAll(System.Collections.Generic.IList<SkillParameter> data)
        {
            var newData = CloneAndSortBy(data, x => x.SkillID, System.Collections.Generic.Comparer<int>.Default);
            var table = new SkillParameterTable(newData);
            memory = new MemoryDatabase(
                memory.PersonTable,
                memory.SkillTable,
                table
            
            );
        }


    }
}