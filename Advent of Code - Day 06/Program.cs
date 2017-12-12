using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Advent_of_Code___Day_06
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set up the variables
            string input = File.ReadAllText(@"C:\Users\ben.rendall\Drive Documents\Visual Studio\Projects\Advent of Code 2017\Day 06\input.txt");
            string[] inputSplit = input.Split('\t');
            string sectionOneDuplicateSequence = null;
            Dictionary<int, int> memoryBanks = new Dictionary<int, int>();
            List<string> bankSequences = new List<string>();
            int genericCounter = 0;
            int highestBankValue = 0;
            int highestBankKey = 0;
            int currentBank;
            int cycleInterations = 0;
            int sectionOneIterations = 0;
            int sectionTwoIterations = 0;

            //Populate the dictionary of memory banks - index 0
            foreach(string value in inputSplit)
            {
                memoryBanks.Add(genericCounter, Convert.ToInt32(value));
                genericCounter++;
            }

            //Convert the dictionary values into a string and add that to a list for tracking
            string sequenceString = string.Join(";", memoryBanks.Select(m => m.Value.ToString()).ToArray());
            bankSequences.Add(sequenceString);

            //Join the infinite loop untill broken out with the final answers
            while (true)
            {
                //Loop through the dictionary and find the highest value bank, ensuring the lowest number key is saved
                foreach(var bank in  memoryBanks)
                {
                    if(bank.Value > highestBankValue)
                    {
                        highestBankValue = bank.Value;
                        highestBankKey = bank.Key;
                    }
                    else if(bank.Value == highestBankValue)
                    {
                        if(bank.Key < highestBankKey)
                        {
                            highestBankKey = bank.Key;
                        }
                    }
                }

                //Copy the highest key into another interger for iterating through the list
                currentBank = highestBankKey;
                //Set the highest bank to 0 ready for distributing
                memoryBanks[highestBankKey] = 0;

                //Begin distributing the values through the dictionary in a loop till empty
                while(highestBankValue > 0)
                {
                    //Check to see if the bank value will be outside the dictionary length and act appropriately
                    if (currentBank + 1 == memoryBanks.Count())
                    {
                        currentBank = 0;
                    }
                    else
                    {
                        currentBank = currentBank + 1;
                    }

                    //Increase the currently selected bank and decrase the total to be worked with
                    memoryBanks[currentBank] += 1;
                    highestBankValue -= 1;
                }

                //Recreate sequenceString with the new updated values
                sequenceString = string.Join(";", memoryBanks.Select(m => m.Value.ToString()).ToArray());

                cycleInterations++;

                //Section 1 check
                if (sectionOneIterations == 0)
                {
                    //Check to see if the new sequence has been seen before
                    if (bankSequences.Contains(sequenceString))
                    {
                        //Set the interger for Section 1
                        sectionOneIterations = cycleInterations;
                        sectionOneDuplicateSequence = sequenceString;
                    }
                    else
                    {
                        bankSequences.Add(sequenceString);
                    }
                }
                else
                {
                    //Section 2 check
                    if (sequenceString == sectionOneDuplicateSequence)
                    {
                        sectionTwoIterations++;
                        break;
                    }
                    else
                    {
                        sectionTwoIterations++;
                    }
                }
                
                highestBankValue = 0;
                highestBankKey = 0;
            }

            //Stop at end
            Console.WriteLine("\nEnd");
            Console.ReadKey();
        }
    }
}
