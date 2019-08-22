using MasterMemory;
using MessagePack;
using System.Collections.Generic;
using System;
using MasterData.Tables;

namespace MasterData
{
   public sealed class DatabaseBuilder : DatabaseBuilderBase
   {
        public DatabaseBuilder() : this(null) { }
        public DatabaseBuilder(MessagePack.IFormatterResolver resolver) : base(resolver) { }

        public DatabaseBuilder Append(System.Collections.Generic.IEnumerable<Person> dataSource)
        {
            AppendCore(dataSource, x => x.PersonId, System.Collections.Generic.Comparer<int>.Default);
            return this;
        }

        public DatabaseBuilder Append(System.Collections.Generic.IEnumerable<Skill> dataSource)
        {
            AppendCore(dataSource, x => x.SkillID, System.Collections.Generic.Comparer<int>.Default);
            return this;
        }

        public DatabaseBuilder Append(System.Collections.Generic.IEnumerable<SkillParameter> dataSource)
        {
            AppendCore(dataSource, x => x.SkillID, System.Collections.Generic.Comparer<int>.Default);
            return this;
        }

    }
}