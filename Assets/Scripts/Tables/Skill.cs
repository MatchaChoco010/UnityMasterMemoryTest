using MasterMemory;
using MessagePack;

[MemoryTable ("skill"), MessagePackObject (true)]
public class Skill {
    [PrimaryKey]
    public int SkillID { get; set; }
    public string SkillName { get; set; }
}
