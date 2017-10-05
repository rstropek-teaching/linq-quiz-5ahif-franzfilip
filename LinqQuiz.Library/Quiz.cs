using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqQuiz.Library
{
    public static class Quiz
    {
        /// <summary>
        /// Returns all even numbers between 1 and the specified upper limit.
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// </exception>
        public static int[] GetEvenNumbers(int exclusiveUpperLimit)
        {
            // Tip: Never leave "dead code" in your code. Remove unnecessary code pieces instead
            // of commenting them out. The poor guy who has to maintain your code will ask himself
            // why you kept that throw-statement as a comment ;-)
            //throw new NotImplementedException();
            return Enumerable.Range(1, exclusiveUpperLimit-1).Where(n => n % 2 == 0).ToArray();
        }

        /// <summary>
        /// Returns the squares of the numbers between 1 and the specified upper limit 
        /// that can be divided by 7 without a remainder (see also remarks).
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="OverflowException">
        ///     Thrown if the calculating the square results in an overflow for type <see cref="System.Int32"/>.
        /// </exception>
        /// <remarks>
        /// The result is an empty array if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// The result is in descending order.
        /// </remarks>
        public static int[] GetSquares(int exclusiveUpperLimit)
        {
            //can a random value be generated
            if(exclusiveUpperLimit < 1) {
                return new int[0];
            }
            // Your solution performs Math.Pow more times than necessary. Prefer embedding the `checked` keyword
            // in your LINQ query so that you do not have to calculate the square twice.
            var arr = Enumerable.Range(1, exclusiveUpperLimit - 1).Where(n => Math.Pow(n,2) % 7 == 0).ToArray();
            //sort Array backwards
            Array.Reverse(arr);
            //Array quadrieren
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = checked((int)Math.Pow(arr[i], 2));
            }

            return arr;
        }

        /// <summary>
        /// Returns a statistic about families.
        /// </summary>
        /// <param name="families">Families to analyze</param>
        /// <returns>
        /// Returns one statistic entry per family in <paramref name="families"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="families"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// <see cref="FamilySummary.AverageAge"/> is set to 0 if <see cref="IFamily.Persons"/>
        /// in <paramref name="families"/> is empty.
        /// </remarks>
        public static FamilySummary[] GetFamilyStatistic(IReadOnlyCollection<IFamily> families)
        {
            if(families == null) {
                throw new ArgumentNullException();
            }
            List<FamilySummary> summary = new List<FamilySummary>();
            foreach(var fam in families) {
                //new Family
                FamilySummary placeholder = new FamilySummary();
                placeholder.FamilyID = fam.ID;
                placeholder.NumberOfFamilyMembers = fam.Persons.Count;
                //default age = 0 --> cause of the case if 0 persons are in the family
                placeholder.AverageAge = 0;

                //if there are 0 persons are in the family the average age is 0
                if (fam.Persons.Count > 0) {
                    foreach (var person in fam.Persons) {
                        placeholder.AverageAge += person.Age;
                    }
                    placeholder.AverageAge = placeholder.AverageAge / fam.Persons.Count; 
                }
                //add the placeholder to the summary
                summary.Add(placeholder);
            }

            return summary.ToArray();
        }

        /// <summary>
        /// Returns a statistic about the number of occurrences of letters in a text.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>
        /// Collection containing the number of occurrences of each letter (see also remarks).
        /// </returns>
        /// <remarks>
        /// Casing is ignored (e.g. 'a' is treated as 'A'). Only letters between A and Z are counted;
        /// special characters, numbers, whitespaces, etc. are ignored. The result only contains
        /// letters that are contained in <paramref name="text"/> (i.e. there must not be a collection element
        /// with number of occurrences equal to zero.
        /// </remarks>
        public static (char letter, int numberOfOccurrences)[] GetLetterStatistic(string text) {
            if (text.Length < 1) {
                return new(char letter, int number)[0];
            }
            //max possible letters (26)
            (char letter, int number)[] arr = new(char letter, int number)[26];

            //every character is in the anscii table
            foreach(char a in text.ToUpper()) {
                if (text.Contains(a) && (int)a >=65 && (int)a <= 90) {
                    var characters = text.Where(n => n.Equals(a));
                    arr[(int)a - 65] = (a, characters.Count());
                }
            }
            return arr.Where(n => n.number > 0).ToArray();
        }
    }
}
