using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    class Dice
    {
        // Class for one dice roll
        // Creates an object which holds the dices that are going to be rolled or the result
        // the player rolls in real life.

        // If the player allows the PC to roll the dice a queue of dices to be 'thrown' is generated
        public enum Dices
        {
            d4,
            d6,
            d8,
            d10,
            d12,
            d20
        }
        public List<Dices> DiceQueue = new List<Dices>();
        
        // If the player decides to throw the dices himself, these vars will be set as such
        bool RollIRL = false;
        int ResultIRL = 0;

        // Constructor; sets IRL vars to digital dicing and creates a queue.
        public Dice(int AmountD4, int AmountD6, int AmountD8, int AmountD10, int AmountD12, int AmountD20)
        {
            RollIRL = false;
            ResultIRL = 0;
            for (int i = 0; i < AmountD4; i++)
            {
                DiceQueue.Add(Dices.d4);
            }
            for (int i = 0; i < AmountD6; i++)
            {
                DiceQueue.Add(Dices.d6);
            }
            for (int i = 0; i < AmountD8; i++)
            {
                DiceQueue.Add(Dices.d8);
            }
            for (int i = 0; i < AmountD10; i++)
            {
                DiceQueue.Add(Dices.d10);
            }
            for (int i = 0; i < AmountD12; i++)
            {
                DiceQueue.Add(Dices.d12);
            }
            for (int i = 0; i < AmountD20; i++)
            {
                DiceQueue.Add(Dices.d20);
            }
        }

        // Constructor; sets IRL vars to IRL dicing and saves the result/input, still creates queue for crit calculation
        public Dice(int AmountD4, int AmountD6, int AmountD8, int AmountD10, int AmountD12, int AmountD20, int result)
        {
            RollIRL = true;
            ResultIRL = result;
            for (int i = 0; i < AmountD4; i++)
            {
                DiceQueue.Add(Dices.d4);
            }
            for (int i = 0; i < AmountD6; i++)
            {
                DiceQueue.Add(Dices.d6);
            }
            for (int i = 0; i < AmountD8; i++)
            {
                DiceQueue.Add(Dices.d8);
            }
            for (int i = 0; i < AmountD10; i++)
            {
                DiceQueue.Add(Dices.d10);
            }
            for (int i = 0; i < AmountD12; i++)
            {
                DiceQueue.Add(Dices.d12);
            }
            for (int i = 0; i < AmountD20; i++)
            {
                DiceQueue.Add(Dices.d20);
            }
        }


        // Returns the max possible outcome with the queued dices. Used in result/crit comparison.
        public int CritValue()
        {
            int result = 0;
            Random rand = new Random();
            foreach (Dices dice in DiceQueue)
            {
                switch (dice)
                {
                    case Dices.d4:
                        result += 4;
                        break;
                    case Dices.d6:
                        result += 6;
                        break;
                    case Dices.d8:
                        result += 8;
                        break;
                    case Dices.d10:
                        result += 10;
                        break;
                    case Dices.d12:
                        result += 12;
                        break;
                    case Dices.d20:
                        result += 20;
                        break;
                }
            }
            return result;
        }

        // Returns either the user input / the actual randomized result of the dice roll
        public int RollDice()
        {
            if (!RollIRL)
            {
                int result = 0;
                Random rand = new Random();
                foreach (Dices dice in DiceQueue)
                {
                    switch (dice)
                    {
                        case Dices.d4:
                            result += rand.Next(1, 4);
                            break;
                        case Dices.d6:
                            result += rand.Next(1, 6);
                            break;
                        case Dices.d8:
                            result += rand.Next(1, 8);
                            break;
                        case Dices.d10:
                            result += rand.Next(1, 10);
                            break;
                        case Dices.d12:
                            result += rand.Next(1, 12);
                            break;
                        case Dices.d20:
                            result += rand.Next(1, 20);
                            break;
                    }
                }
                return result;
            }
            else
            {
                return ResultIRL;
            }
        }
    }
}
