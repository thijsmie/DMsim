using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    class Action
    {
        // Class designed to set up a chain of 'ActionSteps'
        public Action()
        {

        }

    }
    class ActionStep
    {
        // Created in such a way that the player can design powers as flexible as possible

        // Sets the caster and the target. Can be the same.
        Entity Target;
        Entity Self;

        // Constructor: sets the Self and Target entity instances
        public ActionStep(Entity self, Entity target)
        {
            Target = target;
            Self = self;

            AbilityCheckSuccess = false;
            AttackRollSuccess = false;
            AttackRollCritical = false;
        }


        // ActionStep: Rolls a d20 and compares it to the selectes defence stat of the enemy
        #region AttackRoll

        // These bools are read in the DamageRoll ActionStep, they are set by the AttackRoll method
        public bool AttackRollSuccess;
        bool AttackRollCritical;

        public void AttackRoll(string defence, string ability, int bonus)
        {
            Dice DiceRoll = new Dice(0, 0, 0, 0, 0, 1); // AttackRolls are always 20
            int sMod = Self.Abilities[ability];
            if (sMod % 2 == 1) { sMod--; }
            sMod = (sMod - 10) / 2;
            int tDefence = Target.Defences[defence];
            int roll = DiceRoll.RollDice();
            int result = sMod + roll + bonus;
            if (result > tDefence)
            {
                AttackRollSuccess = true;
                if (roll == 20)
                {
                    AttackRollCritical = true;
                }
            }
            else
            {
                AttackRollSuccess = false;
            }

        }
        #endregion

        // ActionStep: Rolls a chosen amount of dices and compares it to an input value
        public bool AbilityCheckSuccess;
        public int AbilityCheck(int CheckValue, string ability, int bonus, Dice DiceRoll)
        {
            int sMod = Self.Abilities[ability];
            if (sMod % 2 == 1) { sMod--; }
            sMod = (sMod - 10) / 2;
            int roll = DiceRoll.RollDice(); // Which dices are thrown are usermade
            int result = sMod + roll + bonus;
            if (result > CheckValue)
            {
                AbilityCheckSuccess = true;
            }
            else
            {
                AbilityCheckSuccess = false;
            }
            return result;
        }

        // ActionStep: Reads the AttackRoll.. vars and acts accordingly
        // If the crit var returns true, apply max damage, else apply randomly calculated damage
        public void DamageRoll(string element, string ability, int bonus, Dice DiceRoll, bool critical)
        {
            int sMod = Self.Abilities[ability];
            if (sMod % 2 == 1) { sMod--; }
            sMod = (sMod - 10) / 2;
            if (Target.Resistances.Contains(element))
            {
                bonus -= 5;
            }

            if (Target.Immunities.Contains(element))
            {
                // Do nothing
            }
            else if (critical)
            {
                Target.Health -= DiceRoll.CritValue() + sMod + bonus;
            }
            else
            {
                Target.Health -= DiceRoll.RollDice() + sMod + bonus;
            }
        }

        // ActionStep: Somewhat the same as
        public void HealRoll(string ability, int bonus, Dice DiceRoll, bool critical)
        {
            int sMod = Self.Abilities[ability];
            if (sMod % 2 == 1) { sMod--; }
            sMod = (sMod - 10) / 2;
            if (critical)
            {
                Target.Health += DiceRoll.CritValue() + sMod + bonus;
            }
            else
            {
                Target.Health += DiceRoll.RollDice() + sMod + bonus;
            }
        }

        public void Relocate(bool self, int DestinationX, int DestinationY)
        {
            Target.PositionX = DestinationX;
            Target.PositionY = DestinationY;
        }

        public void SwitchPosition()
        {
            int oCasterPosX = Self.PositionX;
            int oCasterPosY = Self.PositionY;
            int oTargetPosX = Target.PositionX;
            int oTargetPosY = Target.PositionY;

            Self.PositionX = oTargetPosX;
            Self.PositionY = oTargetPosY;
            Target.PositionX = oCasterPosX;
            Target.PositionY = oCasterPosY;
        }
    }
}
