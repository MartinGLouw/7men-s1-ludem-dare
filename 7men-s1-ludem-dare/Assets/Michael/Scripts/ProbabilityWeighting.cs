using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


namespace Managers
{
    public class ProbabilityWeighting
    {
        #region Basic Probability check

        //this is the basic Probability function that will take the item chance is 30% and then check if you'll get or not
        public bool ProbabilityCheck(int itemProbability)
        {
            float rnd = Random.Range(1, 101);
            if (rnd <= itemProbability)
                return true;
            else
                return false;
        }
        #endregion

        #region Normal items Probability from 100

        List<float> cumulativeProbability;
        
        //This function is called with the Item probability array and it'll return the index of the item
        // for example the list can look like [10,25,30] so the first item has 10% of showing and next one has 25% and so on
        public int GetItemByProbability(List<float> probability) //[50,10,20,20]
        {
            //if your game will use this a lot of time it is best to build the arry just one time
            //and remove this function from here.
            if(!MakeCumulativeProbability(probability))
                return -1; //when it return false then the list excceded 100 in the last index

            float rnd = Random.Range(1, 101); //Get a random number between 0 and 100

            for (int i = 0; i < probability.Count; i++)
            {
                if (rnd <= cumulativeProbability[i]) //if the probility reach the correct sum
                {
                    return i; //return the item index that has been chosen 
                }
            }
            return -1; //return -1 if some error happens
        }
        
        //this function creates the cumulative list
        bool MakeCumulativeProbability(List<float> probability)
        {
            float probabilitiesSum = 0;

            cumulativeProbability = new List<float>(); //reset the Array

            for (int i = 0; i < probability.Count; i++)
            {
                probabilitiesSum += probability[i]; //add the probability to the sum
                cumulativeProbability.Add(probabilitiesSum); //add the new sum to the list
                
                 //All Probabilities need to be under 100% or it'll throw an exception
                if (probabilitiesSum > 100f)
                    {
                        Debug.LogError("Probabilities exceed 100%");
                        return false;
                    }
            }
            return true;
        }
        #endregion
    }
}