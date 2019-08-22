using MasterMemory;
using MessagePack;
using System.Collections.Generic;
using System;
using MasterData.Tables;

namespace MasterData
{
   public sealed class MemoryDatabase : MemoryDatabaseBase
   {
        public PersonTable PersonTable { get; private set; }
        public SkillTable SkillTable { get; private set; }
        public SkillParameterTable SkillParameterTable { get; private set; }

        public MemoryDatabase(
            PersonTable PersonTable,
            SkillTable SkillTable,
            SkillParameterTable SkillParameterTable
        )
        {
            this.PersonTable = PersonTable;
            this.SkillTable = SkillTable;
            this.SkillParameterTable = SkillParameterTable;
        }

        public MemoryDatabase(byte[] databaseBinary, bool internString = true, MessagePack.IFormatterResolver formatterResolver = null)
            : base(databaseBinary, internString, formatterResolver)
        {
        }

        protected override void Init(Dictionary<string, (int offset, int count)> header, int headerOffset, byte[] databaseBinary, MessagePack.IFormatterResolver resolver)
        {
            this.PersonTable = ExtractTableData<Person, PersonTable>(header, headerOffset, databaseBinary, resolver, xs => new PersonTable(xs));
            this.SkillTable = ExtractTableData<Skill, SkillTable>(header, headerOffset, databaseBinary, resolver, xs => new SkillTable(xs));
            this.SkillParameterTable = ExtractTableData<SkillParameter, SkillParameterTable>(header, headerOffset, databaseBinary, resolver, xs => new SkillParameterTable(xs));
        }

        public ImmutableBuilder ToImmutableBuilder()
        {
            return new ImmutableBuilder(this);
        }

        public DatabaseBuilder ToDatabaseBuilder()
        {
            var builder = new DatabaseBuilder();
            builder.Append(this.PersonTable.GetRawDataUnsafe());
            builder.Append(this.SkillTable.GetRawDataUnsafe());
            builder.Append(this.SkillParameterTable.GetRawDataUnsafe());
            return builder;
        }
    }
}