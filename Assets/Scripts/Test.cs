using MasterData;
using MasterMemory;
using UnityEngine;

public class Test : MonoBehaviour {
    void Start () {
        var db = MasterDataDB.DB;

        foreach (var skill in db.SkillTable.All) {
            Debug.Log ($"Skill Name: {skill.SkillName}");

            var lv = (int) Random.Range (1, 10);
            Debug.Log ($"Skill Lv: {lv}");

            var parameter = db.SkillParameterTable
                .FindBySkillIDAndSkillLv ((skill.SkillID, lv));
            Debug.Log ($"Skill Damage: {parameter.Damage}");
        }
    }
}
