using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace examedu.Helper.RandomGenerator
{
    public static class ChooseRandomFromList
    {   
        /// <summary>
        /// Choose n element (elementAmount) from aray
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="elementAmount"></param>
        /// <typeparam name="T">type of array</typeparam>
        /// <returns></returns>
        public static List<T> ChooseRandom<T>(List<T> inputList, int elementAmount)
        {
            if(inputList.Count < elementAmount)
            {
                return null;
            }
            Random rng = new Random();
            
            var resultList = inputList.OrderBy(a => rng.Next()).Take(elementAmount).ToList();
            return resultList;
        }
    }
}