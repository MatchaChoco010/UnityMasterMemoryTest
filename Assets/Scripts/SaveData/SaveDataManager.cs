using System.Collections.Generic;
using System.IO;
using MessagePack;
using UnityEngine;

public class SaveDataManager {
    private static SaveDataManager _instance = new SaveDataManager ();
    public static SaveDataManager Instance => _instance;

    private SaveData _saveData;

    private void CreateNewSavedata () {
        _saveData = new SaveData (new List<int> { 1, 1, 1, 2 });
    }

    public void Load (string saveFilename) {
        var saveDirectory = $"{Application.dataPath}/SaveData";
        var filePath = $"{saveDirectory}/{saveFilename}";

        try {
            using (var fs = new FileStream (filePath, FileMode.Open, FileAccess.Read)) {
                byte[] bytes = new byte[fs.Length];
                fs.Read (bytes, 0, bytes.Length);
                _saveData = MessagePackSerializer
                    .Deserialize<SaveData> (bytes);
            }
        } catch {
            CreateNewSavedata ();
        }
    }

    public void SaveSkillLevels (string saveFilename, List<int> skillLevels) {
        _saveData.SkillLevels = skillLevels;

        var bytes = MessagePackSerializer.Serialize (_saveData);

        var saveDirectory = $"{Application.dataPath}/SaveData";
        Directory.CreateDirectory (saveDirectory);

        var filePath = $"{saveDirectory}/{saveFilename}";

        using (var fs = new FileStream (filePath, FileMode.Create)) {
            fs.Write (bytes, 0, bytes.Length);
        }
    }

    public IReadOnlyList<int> SkillLevels => _saveData.SkillLevels;
}
