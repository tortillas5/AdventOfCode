namespace Day01
{
    /// <summary>
    /// Classe qui gère les actions sur l'input.
    /// </summary>
    internal class InputHandler
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="InputHandler"/>.
        /// </summary>
        private InputHandler()
        { }

        /// <summary>
        /// Ficher texte lu.
        /// </summary>
        private static string? InputData;

        /// <summary>
        /// Retourne l'input non traitée.
        /// </summary>
        /// <returns>Texte contenu dans l'input.</returns>
        public static string GetInput()
        {
            InputData ??= File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "input.txt"));

            return InputData;
        }

        /// <summary>
        /// Retourne une liste des lignes non vides de l'input.
        /// </summary>
        /// <returns>Liste de lignes.</returns>
        public static List<string> GetInputLines()
        {
            return GetInput().Split("\n").Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
        }
    }
}