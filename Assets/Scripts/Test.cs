using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour {
    void Start () {
        var db = MasterDataDB.DB;

        var saveDataManager = SaveDataManager.Instance;
        saveDataManager.Load ("save-file-0");
        var skillLevels = saveDataManager.SkillLevels;

        foreach (var skill in db.SkillTable.All) {
            Debug.Log ($"Skill Name: {skill.SkillName}");

            var lv = skillLevels[skill.SkillID];
            Debug.Log ($"Skill Lv: {lv}");

            var parameter = db.SkillParameterTable
                .FindBySkillIDAndSkillLv ((skill.SkillID, lv));
            Debug.Log ($"Skill Damage: {parameter.Damage}");
        }

        var newLevels = skillLevels.Select (lv => {
            if (lv >= 9) return lv;
            return lv + 1;
        }).ToList ();
        saveDataManager.SaveSkillLevels ("save-file-0", newLevels);
    }
}
