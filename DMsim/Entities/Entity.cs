using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    class Entity
    {
        #region DefineStats
        public string Name, Description, Tactics, Notes;
        public int MaxHealth, Level, UsualSpeed, XP, Initiative, Perception;
        public int Texture;
        public int PositionX, PositionY;
        public Dictionary<string, int> Defences = new Dictionary<string, int>();
        public Dictionary<string, int> Abilities = new Dictionary<string, int>();
        public List<string> Languages = new List<string>();
        public List<string> Resistances = new List<string>();
        public List<string> Immunities = new List<string>();
        public bool Minion;
        public string Creator;
        public int ActionPoints;
        public enum Sights
        {               // Code to implement:
            Normal,     // Can see normal in bright light. Dim light offers sight but creatures have concealment. Blind in darkness.
            LowLight,   // Can see normal in bright and dim light. Blind in darkness.
            DarkVision,  // Can see normal in bright, dim and dark environments
            Blind       // Can't see anything
        }
        public Sights UsualVision;
        #endregion

        #region EncounterSpecificStats
        public int Health;
        public bool Bleeding;
        public int SavingThrows;
        public int BonusToSavingThrows;
        public bool Dead;
        public int Speed;

        // The next block of vars is for conditions
        public List<Conditions> conditions = new List<Conditions>(); // statuseffect = statuseffect (duh) int = turns remaining
        public bool GrantsCombatAdvantage;      // returns whether the character grants any combat advantage to enemies
        public bool CanFlank;                   // returns whether the entity is capable flanking and enemy
        public bool CanOpportunityAttack;       // returns whether the entity is capable of performing opportunity attacks
        public Sights Vision;                   // holds the sight of the entity, if it is changed through a condition, edit this, not UsualVision
        public int ActionsPerTurn;               // Usually 3, standard, minor and move. Impaired by dazed;
        public string Controller;
        #endregion

        #region SetStats
        public void SetDefences(int aAC, int aFortitude, int aReflex, int aWill)
        {
            Defences["AC"] = aAC;
            Defences["Fortitude"] = aFortitude;
            Defences["Reflex"] = aReflex;
            Defences["Will"] = aWill;
        }

        public void SetAbilities(int aStrength, int aConstitution, int aDexterity, int aIntelligence, int aWisdom, int aCharisma)
        {
            Abilities["Strength"] = aStrength;
            Abilities["Constitution"] = aConstitution;
            Abilities["Dexterity"] = aDexterity;
            Abilities["Intelligence"] = aIntelligence;
            Abilities["Wisdom"] = aWisdom;
            Abilities["Charisma"] = aCharisma;
        }

        public void SetPosition(int PosX, int PosY)
        {
            // Because Vectors apparently are XNA only..
            PositionX = PosX;
            PositionY = PosY;
        }

        public void AddLanguage(string language)
        {
            if(!Languages.Contains(language))
            {
                Languages.Add(language);
            }
        }

        public void RemoveLanguage(string language)
        {
            // In case things go haywire and there are multiple instances of 'language' inside Languages, remove all.
            // Also, this is apllied in two other similar Remove(...) methods
            while(Languages.Contains(language))
            {
                Languages.Remove(language);
            }
        }

        public void AddResistance(string resistance)
        {
            if (!Resistances.Contains(resistance))
            {
                Resistances.Add(resistance);
            }
        }

        public void RemoveResistance(string resistance)
        {
            while (Resistances.Contains(resistance))
            {
                Resistances.Remove(resistance);
            }
        }
        
        public void AddImmunity(string immunity)
        {
            if (!Immunities.Contains(immunity))
            {
                Immunities.Add(immunity);
            }
        }

        public void RemoveImmunity(string immunity)
        {
            while (Resistances.Contains(immunity))
            {
                Resistances.Remove(immunity);
            }
        }
#endregion

        public Entity(string[] Players)
        {

        }
        public void AddStatusEffect()
        {

        }

        public void AddMark()
        {

        }

        public void DoAction()
        {

        }
    }
}
