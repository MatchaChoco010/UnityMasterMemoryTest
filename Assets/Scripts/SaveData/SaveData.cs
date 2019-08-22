using System.Collections.Generic;
using MessagePack;

[MessagePackObject]
public class SaveData {
    [Key (0)]
    public List<int> SkillLevels { get; set; }

    public SaveData (List<int> skillLevels) {
        SkillLevels = skillLevels;
    }
}
