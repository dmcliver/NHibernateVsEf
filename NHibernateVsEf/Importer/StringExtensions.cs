namespace NHibernateVsEf.Importer
{
    public static class StringExtensions
    {
        public static string Capitalize(this string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;

            string manipulatedWord = word.Remove(1);
            string capitalizedWord = manipulatedWord.ToUpper() + word.Substring(1);
            return capitalizedWord;
        }
    }
}