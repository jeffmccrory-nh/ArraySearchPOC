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
                        var newMention = new Mention { Annotations = new() };
                        var newAnnotation = new Annotation { PageId = 1, Box = Array.Empty<int>() };
                        newMention.Annotations.Add(newAnnotation);
                        searchTerm.Value.Add(newMention);

                    }
                    else
                    {
                        //For multi word term - seek match for rest of the term
                        if (FindNext(1, i + 1))
                        {
                            // Hit! Create mention. 
                            var newMention = new Mention { Annotations = new() };
                            // Add annotation to mention for each word found. In reality page_id and box coords will be copied from _documentWords object
                            for (int j = 0; j < _strTermArr.Length - 1; j++) newMention.Annotations.Add(new Annotation{PageId=1, Box = Array.Empty<int>()});
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
