using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day01
{
    internal class Calculateur
    {
        #region Part 1

        public static int CalculerPart1()
        {
            List<string> listNombre = [];

            // Pour chaque ligne
            foreach (string line in InputHandler.GetInputLines())
            {
                char first;
                char last;
                string nombre;

                // Nombre de nombres dans la ligne
                int nbDigit = line.Count(Char.IsDigit);

                // Un seul nombre
                if (nbDigit == 1)
                {
                    first = line.First(char.IsDigit);

                    nombre = char.ToString(first) + char.ToString(first);
                }
                else
                {
                    // Plusieurs nombres

                    first = line.First(char.IsDigit);
                    last = line.Last(char.IsDigit);

                    nombre = char.ToString(first) + char.ToString(last);
                }

                // Ajout du nombre combiné à la liste.
                listNombre.Add(nombre);
            }

            // Passage en int.
            List<int> listeInt = listNombre.Select(int.Parse).ToList();

            return listeInt.Sum();
        }

        #endregion Part 1

        #region Part 2

        private static readonly List<string> ListeNombreEnLettre = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

        private static int ToNumber(string nombre)
        {
            switch (nombre)
            {
                case "one": return 1;
                case "two": return 2;
                case "three": return 3;
                case "four": return 4;
                case "five": return 5;
                case "six": return 6;
                case "seven": return 7;
                case "eight": return 8;
                case "nine": return 9;
            }

            throw new Exception();
        }

        public static int CalculerPart2()
        {
            List<string> inputLines = InputHandler.GetInputLines();
            List<string> inputLinesSansTexte = [];


            foreach (string line in inputLines)
            {
                Dictionary<int, string> keyValues = new Dictionary<int, string>();

                foreach (string nombreEnLettre in ListeNombreEnLettre)
                {
                    int pos = line.IndexOf(nombreEnLettre);

                    if (pos != -1)
                    {
                        keyValues.Add(pos, nombreEnLettre);
                    }
                }

                string res = line;

                for (int i = 0; i < keyValues.Count; i++)
                {
                    res = res.Insert(keyValues.ElementAt(i).Key + i, ToNumber(keyValues.ElementAt(i).Value).ToString());
                }

                inputLinesSansTexte.Add(res);
            }

            List<string> listNombre = [];

            // Pour chaque ligne
            foreach (string line in inputLinesSansTexte)
            {
                char first;
                char last;
                string nombre;

                // Nombre de nombres dans la ligne
                int nbDigit = line.Count(Char.IsDigit);

                // Un seul nombre
                if (nbDigit == 1)
                {
                    first = line.First(char.IsDigit);

                    nombre = char.ToString(first) + char.ToString(first);
                }
                else
                {
                    // Plusieurs nombres

                    first = line.First(char.IsDigit);
                    last = line.Last(char.IsDigit);

                    nombre = char.ToString(first) + char.ToString(last);
                }

                // Ajout du nombre combiné à la liste.
                listNombre.Add(nombre);
            }

            // Passage en int.
            List<int> listeInt = listNombre.Select(int.Parse).ToList();

            return listeInt.Sum();
        }

        #endregion Part 2
    }
}