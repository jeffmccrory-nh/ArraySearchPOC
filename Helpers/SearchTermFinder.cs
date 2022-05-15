using ArraySearchPOC.Models;

namespace ArraySearchPOC.Helpers
{
    public class SearchTermFinder
    {
        private List<string> _documentWords = new();
        private string[]? _strTermArr;

        public void Init(List<string> documentWords)
        {
            _documentWords = documentWords;
        }

        public KeyValuePair<string, List<Mention>> FindSearchTerm(KeyValuePair<string, List<Mention>> searchTerm)
        {
            _strTermArr = searchTerm.Key.Split(" ");
            
            for (var i = 0; i < _documentWords.Count-1; i++)
            {
                //Word match
                if (_strTermArr[0] == _documentWords[i])
                {
                    //For single word term - add box
                    if (_strTermArr.Length == 1)
                    {
                        searchTerm.Value.Add(new Mention { Boxes = new() { new Box() } });
                    }
                    else
                    {
                        //For multi word term - seek match for rest of the term
                        if (FindNext(1, i + 1))
                        {
                            // Hit! Create mention. In reality box coords will be copied from _documentWords object
                            var newMention = new Mention { Boxes = new() };
                            for (int j = 0; j < _strTermArr.Length - 1; j++) newMention.Boxes.Add(new Box());
                            searchTerm.Value.Add(newMention);
                        }
                    }
                }
            }
            return searchTerm;
        }

        private bool FindNext(int termPos, int docWordsPos)
        {
            while (termPos < _strTermArr.Length && docWordsPos < _documentWords.Count)
            {
                if (_strTermArr[termPos] == _documentWords[docWordsPos])
                {
                    termPos += 1;
                    docWordsPos += 1;
                }
                else return false;
            }
            return true;
        }
    }
}
