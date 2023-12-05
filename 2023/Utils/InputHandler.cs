namespace AdventOfCode.Utils
{
    /// <summary>
    /// Classe qui gère les actions sur l'input.
    /// </summary>
    public static class InputHandler
    {
        /// <summary>
        /// Retourne l'input non traitée.
        /// </summary>
        /// <returns>Texte contenu dans l'input.</returns>
        public static string GetInput(string path)
        {
            return File.ReadAllText(path);
        }

        /// <summary>
        /// Retourne une liste des lignes non vides de l'input.
        /// </summary>
        /// <returns>Liste de lignes.</returns>
        public static List<string> GetInputLines(string path)
        {
            return GetInput(path).Split("\n").Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
        }
    }
}