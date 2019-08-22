using MasterMemory;
using MessagePack;

public enum Gender {
    Male,
    Female,
    Unknown
}

// table definition marked by MemoryTableAttribute.
// database-table must be serializable by MessagePack-CSsharp
[MemoryTable ("person"), MessagePackObject (true)]
public class Person {
    // index definition by attributes.
    [PrimaryKey]
    public int PersonId { get; set; }

    // secondary index can add multiple(discriminated by index-number).
    [SecondaryKey (0), NonUnique]
    [SecondaryKey (1, keyOrder : 1), NonUnique]
    public int Age { get; set; }

    [SecondaryKey (2), NonUnique]
    [SecondaryKey (1, keyOrder : 0), NonUnique]
    public Gender Gender { get; set; }

    public string Name { get; set; }
}
