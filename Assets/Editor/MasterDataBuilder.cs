using System.Collections.Generic;
using System.IO;
using MasterData;
using MessagePack.Resolvers;
using UnityEditor;
using UnityEngine;

public static class MasterDataBuilder {

    [MenuItem ("MasterMemory/Build")]
    private static void BuildMasterData () {
        try {
            CompositeResolver.RegisterAndSetAsDefault (new [] {
                MasterMemoryResolver.Instance,
                    GeneratedResolver.Instance,
                    StandardResolver.Instance
            });
        } catch { }

        var builder = new DatabaseBuilder ();
        builder = BuildParson (builder);
        builder = BuildSkill (builder);
        builder = BuildSkillParameter (builder);

        byte[] data = builder.Build ();

        var resourcesDir = $"{Application.dataPath}/Resources";
        Directory.CreateDirectory (resourcesDir);
        var filename = "/master-data.bytes";

        using (var fs = new FileStream (resourcesDir + filename, FileMode.Create)) {
            fs.Write (data, 0, data.Length);
        }

        Debug.Log ($"Write byte[] to: {resourcesDir + filename}");

        AssetDatabase.Refresh ();
    }

    private static DatabaseBuilder BuildParson (DatabaseBuilder builder) {

        builder.Append (new Person[] {
            new Person { PersonId = 0, Age = 13, Gender = Gender.Male, Name = "Dana Terry" },
            new Person { PersonId = 1, Age = 17, Gender = Gender.Male, Name = "Kirk Obrien" },
            new Person { PersonId = 2, Age = 31, Gender = Gender.Male, Name = "Wm Banks" },
            new Person { PersonId = 3, Age = 44, Gender = Gender.Male, Name = "Karl Benson" },
            new Person { PersonId = 4, Age = 23, Gender = Gender.Male, Name = "Jared Holland" },
            new Person { PersonId = 5, Age = 27, Gender = Gender.Female, Name = "Jeanne Phelps" },
            new Person { PersonId = 6, Age = 25, Gender = Gender.Female, Name = "Willie Rose" },
            new Person { PersonId = 7, Age = 11, Gender = Gender.Female, Name = "Shari Gutierrez" },
            new Person { PersonId = 8, Age = 63, Gender = Gender.Female, Name = "Lori Wilson" },
            new Person { PersonId = 9, Age = 34, Gender = Gender.Female, Name = "Lena Ramsey" },
        });
        return builder;
    }

    private static DatabaseBuilder BuildSkill (DatabaseBuilder builder) {
        builder.Append (new Skill[] {
            new Skill { SkillID = 0, SkillName = "スキル0" },
            new Skill { SkillID = 1, SkillName = "スキル1" },
            new Skill { SkillID = 2, SkillName = "スキル2" },
            new Skill { SkillID = 3, SkillName = "スキル3" },
        });
        return builder;
    }

    private static DatabaseBuilder BuildSkillParameter (DatabaseBuilder builder) {
        var skillParameters = new List<SkillParameter> ();
        for (int i = 0; i < 4; i++) {
            for (int lv = 1; lv < 10; lv++) {
                skillParameters.Add (new SkillParameter {
                    SkillID = i,
                        SkillLv = lv,
                        Damage = lv * 100
                });
            }
        }
        builder.Append (skillParameters);
        return builder;
    }
}
