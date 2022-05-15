namespace ArraySearchPOC.Helpers
{
    public class WordGenerator
    {
        private readonly string[] consonant = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
        private readonly string[] vowel = { "a", "e", "i", "o", "u" };
        public string GenerateWord()
        {
            var length = GenerateWordLength();
            Random rand = new Random();
            if (length < 1) // do not allow words of zero length
                throw new ArgumentException("Length must be greater than 0");

            string word = string.Empty;

            if (rand.Next() % 2 == 0) // randomly choose a vowel or consonant to start the word
                word += consonant[rand.Next(0, 20)];
            else
                word += vowel[rand.Next(0, 4)];

            for (int i = 1; i < length; i += 2) // the counter starts at 1 to account for the initial letter
            { // and increments by two since we append two characters per pass
                string c = consonant[rand.Next(0, 20)];
                string v = vowel[rand.Next(0, 4)];

                if (c == "q") // append qu if the random consonant is a q
                    word += "qu";
                else // otherwise just append a random consant and vowel
                    word += c + v;
            }

            // the word may be short a letter because of the way the for loop above is constructed
            if (word.Length < length) // we'll just append a random consonant if that's the case
                word += consonant[rand.Next(0, 20)];

            return word;
        }

        private int GenerateWordLength()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            return rnd.Next(3, 35);
        }
    }
}
