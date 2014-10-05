using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    class Conditions
    {
        public enum cType
        {
            Blindness,
            Dazed,
            Deafened,
            Dominated,
            Dying,
            Helpless,
            Immobilized,
            Marked,
            Petrified,
            Prone,
            Restrained,
            Slowed,
            Stunned,
            Surprised,
            Unconscious,
            Weakened
        }
        public cType Type;
        public int TurnsRemaining;
        Entity Target;
        Entity Caster;

        public Conditions(Entity target, Entity caster, cType type)
        {
            Target = target;
            Caster = caster;
            Type = type;
            Target.conditions.Add(this);
        }

        ///<summary>
        /// Call these every turn in a foreach loop for the statuseffects dictionary. 
        /// The entity class has a dictionary which holds a statuseffect and the turns remaining.
        /// This method handles the rolls, applies damage, slows speed and so on, it returns the turns remaining.
        ///</summary>
        public int Update()
        {
            switch(Type)
            {
                default:
                    return 0; // Make this return an int method which holds the update data for this type
            }
        }

        public void Remove()
        {
            switch(Type)
            {
                default:
                    break;
            }
            Target.conditions.Remove(this);
        }


        // Constructors, Destructors and updaters of the conditions. It is important to call the updates after the con/destructors
        // This is because removing certain conditions can make a character stop granting combat advantage while another condition still
        // calls for it. The updater will then correct it.
        #region cConstructors
        private void addBlindness()
        {
            Target.CanFlank = false;
            Target.GrantsCombatAdvantage = true;
            Target.Vision = Entity.Sights.Blind;
            Target.Perception -= 10;
        }

        private void addDazed()
        {
            Target.GrantsCombatAdvantage = true;
            Target.ActionsPerTurn = 1;
            Target.CanFlank = false;
        }

        private void addDeafened()
        {
            Target.Perception -= 10;
        }

        private void addDominated()
        {
            Target.Controller = Caster.Name;
            addDazed();
        }

        private void addDying()
        {
            // addUnconscious();
        }

        private void addHelpless()
        {
            Target.GrantsCombatAdvantage = true;
            // Coup de grace check should check if Helpless is in conditions List
        }

        private void AddImmobilized()
        {
            Target.Speed = 0;
        }
        #endregion

        #region cDestructors
        private void removeBlindness()
        {
            Target.CanFlank = true;
            Target.GrantsCombatAdvantage = false;
            Target.Vision = Target.UsualVision;
            Target.Perception += 10;
        }

        private void removeDazed()
        {
            Target.GrantsCombatAdvantage = false;
            Target.ActionsPerTurn = 3;
            Target.CanFlank = true;
        }

        private void removeDeafened()
        {
            Target.Perception += 10;
        }

        private void removeDominated()
        {
            Target.Controller = Target.Creator;
        }

        private void removeDying()
        {

        }

        private void removeHelpless()
        {
            Target.GrantsCombatAdvantage = false;
        }

        private void removeAll()
        {
            foreach (Conditions cond in Target.conditions)
            {
                if (cond != this)
                {
                    cond.Remove();
                }
            }
            this.Remove();
        }

        private void removeImmobilized()
        {
            Target.Speed = Target.UsualSpeed;
        }
        #endregion

        #region cUpdaters
        private void updateBlindness()
        {
            Target.CanFlank = false;
            Target.GrantsCombatAdvantage = true;
            Target.Vision = Entity.Sights.Blind;
        }

        private void updateDazed()
        {
            Target.GrantsCombatAdvantage = true;
            Target.ActionsPerTurn = 1;
            Target.CanFlank = false;
        }
        private void updateDeafened()
        {

        }

        private void updateDominated()
        {
            Target.Controller = Caster.Name;
        }

        private void updateDying()
        {
            Dice DiceRoll = new Dice(0, 0, 0, 0, 0, 1);
            int temp = (DiceRoll.RollDice() + Target.BonusToSavingThrows);
            if(temp >= 20)
            {
                removeDying();
            }
            else if(temp < 10)
            {
                Target.SavingThrows--;
            }

            if(Target.SavingThrows == 0)
            {
                Target.Dead = true;
                removeAll();
            }
        }

        private void updateImmobilized()
        {

        }
        #endregion
    }
}
