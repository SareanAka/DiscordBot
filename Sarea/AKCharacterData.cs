using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sarea
{
    public class Character
    {
        public string key { get; set; }
        public string id { get; set; }
        public string displayNumber { get; set; }
        public int rarity { get; set; }

        [JsonPropertyName("class")]
        public string _class { get; set; }
        public string classBranch { get; set; }
        public string position { get; set; }
        public string[] tagList { get; set; }
        public string nationId { get; set; }
        public string groupId { get; set; }
        public object teamId { get; set; }
        public bool canUseGeneralPotentialItem { get; set; }
        public object potentialItem { get; set; }
        public object tokenSummon { get; set; }
        public bool isNotObtainable { get; set; }
        public Phase[] phases { get; set; }
        public Trustkeyframe[] trustKeyFrames { get; set; }
        public Potential[] potentials { get; set; }
        public Talent[] talents { get; set; }
        public Skill[] skills { get; set; }
        public Traitcandidate[] traitCandidates { get; set; }
        public object modules { get; set; }
    }

    public class Phase
    {
        public int elite { get; set; }
        public int maxLevel { get; set; }
        public Outfit outfit { get; set; }
        public Attributekeyframe[] attributeKeyFrames { get; set; }
        public Range range { get; set; }
    }

    public class Outfit
    {
        public string id { get; set; }
        public object dynIllustId { get; set; }
        public string avatarId { get; set; }
        public string portraitId { get; set; }
        public object voiceId { get; set; }
        public string voiceType { get; set; }
    }

    public class Range
    {
        public string id { get; set; }
        public Grid[] grids { get; set; }
    }

    public class Grid
    {
        public int row { get; set; }
        public int col { get; set; }
    }

    public class Attributekeyframe
    {
        public int level { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public int maxHp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int magicResistance { get; set; }
        public int cost { get; set; }
        public int blockCnt { get; set; }
        public int moveSpeed { get; set; }
        public int attackSpeed { get; set; }
        public float baseAttackTime { get; set; }
        public int respawnTime { get; set; }
        public int hpRecoveryPerSec { get; set; }
        public int spRecoveryPerSec { get; set; }
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

    public class Trustkeyframe
    {
        public int level { get; set; }
        public Data1 data { get; set; }
    }

    public class Data1
    {
        public int maxHp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int magicResistance { get; set; }
        public int cost { get; set; }
        public int blockCnt { get; set; }
        public int moveSpeed { get; set; }
        public int attackSpeed { get; set; }
        public int baseAttackTime { get; set; }
        public int respawnTime { get; set; }
        public int hpRecoveryPerSec { get; set; }
        public int spRecoveryPerSec { get; set; }
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

    public class Potential
    {
        public int potentialNumber { get; set; }
        public Attribute attribute { get; set; }
    }

    public class Attribute
    {
        public string key { get; set; }
        public int value { get; set; }
    }

    public class Talent
    {
        public int talentNumber { get; set; }
        public Candidate[] candidates { get; set; }
    }

    public class Candidate
    {
        public string key { get; set; }
        public Unlockconditions unlockConditions { get; set; }
        public Variable[] variables { get; set; }
    }

    public class Unlockconditions
    {
        public int elite { get; set; }
        public int level { get; set; }
        public int potential { get; set; }
    }

    public class Variable
    {
        public string key { get; set; }
        public float value { get; set; }
    }

    public class Skill
    {
        public string id { get; set; }
        public object iconId { get; set; }
        public Unlockconditions1 unlockConditions { get; set; }
        public Level[] levels { get; set; }
    }

    public class Unlockconditions1
    {
        public int elite { get; set; }
        public int level { get; set; }
    }

    public class Level
    {
        public int level { get; set; }
        public Variable1[] variables { get; set; }
        public string skillType { get; set; }
        public int duration { get; set; }
        public Spdata spData { get; set; }
    }

    public class Spdata
    {
        public string spType { get; set; }
        public int spCost { get; set; }
        public int initSp { get; set; }
    }

    public class Variable1
    {
        public string key { get; set; }
        public float value { get; set; }
    }

    public class Traitcandidate
    {
        public Unlockconditions2 unlockConditions { get; set; }
        public object[] variables { get; set; }
    }

    public class Unlockconditions2
    {
        public int elite { get; set; }
        public int level { get; set; }
    }
}
