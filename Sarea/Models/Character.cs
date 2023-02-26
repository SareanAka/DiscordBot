using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sarea.Models
{
    public class Character
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool canUseGeneralPotentialItem { get; set; }
        public string potentialItemId { get; set; }
        public string nationId { get; set; }
        public object groupId { get; set; }
        public object teamId { get; set; }
        public string displayNumber { get; set; }
        public object tokenKey { get; set; }
        public string appellation { get; set; }
        public string position { get; set; }
        public string[] tagList { get; set; }
        public string itemUsage { get; set; }
        public string itemDesc { get; set; }
        public string itemObtainApproach { get; set; }
        public bool isNotObtainable { get; set; }
        public bool isSpChar { get; set; }
        public int maxPotentialLevel { get; set; }
        public int rarity { get; set; }
        public string profession { get; set; }
        public string subProfessionId { get; set; }
        public object trait { get; set; }
        public Phase[] phases { get; set; }
        public object[] skills { get; set; }
        public Talent[] talents { get; set; }
        public Potentialrank[] potentialRanks { get; set; }
        public Favorkeyframe[] favorKeyFrames { get; set; }
        public object[] allSkillLvlup { get; set; }
    }

    public class Phase
    {
        public string characterPrefabKey { get; set; }
        public string rangeId { get; set; }
        public int maxLevel { get; set; }
        public Attributeskeyframe[] attributesKeyFrames { get; set; }
        public object evolveCost { get; set; }
    }

    public class Attributeskeyframe
    {
        public int level { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public int maxHp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public float magicResistance { get; set; }
        public int cost { get; set; }
        public int blockCnt { get; set; }
        public float moveSpeed { get; set; }
        public float attackSpeed { get; set; }
        public float baseAttackTime { get; set; }
        public int respawnTime { get; set; }
        public float hpRecoveryPerSec { get; set; }
        public float spRecoveryPerSec { get; set; }
        public int maxDeployCount { get; set; }
        public int maxDeckStackCnt { get; set; }
        public int tauntLevel { get; set; }
        public int massLevel { get; set; }
        public int baseForceLevel { get; set; }
        public bool stunImmune { get; set; }
        public bool silenceImmune { get; set; }
        public bool sleepImmune { get; set; }
        public bool frozenImmune { get; set; }
        public bool levitateImmune { get; set; }
    }

    public class Talent
    {
        public Candidate[] candidates { get; set; }
    }

    public class Candidate
    {
        public Unlockcondition unlockCondition { get; set; }
        public int requiredPotentialRank { get; set; }
        public string prefabKey { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object rangeId { get; set; }
        public Blackboard[] blackboard { get; set; }
    }

    public class Unlockcondition
    {
        public int phase { get; set; }
        public int level { get; set; }
    }

    public class Blackboard
    {
        public string key { get; set; }
        public float value { get; set; }
    }

    public class Potentialrank
    {
        public int type { get; set; }
        public string description { get; set; }
        public object buff { get; set; }
        public object equivalentCost { get; set; }
    }

    public class Favorkeyframe
    {
        public int level { get; set; }
        public Data1 data { get; set; }
    }

    public class Data1
    {
        public int maxHp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public float magicResistance { get; set; }
        public int cost { get; set; }
        public int blockCnt { get; set; }
        public float moveSpeed { get; set; }
        public float attackSpeed { get; set; }
        public float baseAttackTime { get; set; }
        public int respawnTime { get; set; }
        public float hpRecoveryPerSec { get; set; }
        public float spRecoveryPerSec { get; set; }
        public int maxDeployCount { get; set; }
        public int maxDeckStackCnt { get; set; }
        public int tauntLevel { get; set; }
        public int massLevel { get; set; }
        public int baseForceLevel { get; set; }
        public bool stunImmune { get; set; }
        public bool silenceImmune { get; set; }
        public bool sleepImmune { get; set; }
        public bool frozenImmune { get; set; }
        public bool levitateImmune { get; set; }
    }
}
