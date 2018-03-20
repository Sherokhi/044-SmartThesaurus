using System.Collections.Generic;

namespace algoResearch
{
    public class WebPage
    {
        private string selfURL;
        private int totalWordsOccurence;
        private List<Word> words;

        public WebPage(string pURL)
        {
            this.selfURL = pURL;
            this.words = new List<Word>();
            totalWordsOccurence = 0;
        }

        public void AddWordOccurence(string pWordToUp, int nbrOccurence)
        {
            bool found = false;
            foreach(Word word in words)
            {
                if(word.Text == pWordToUp)
                {
                    word.NewOccurence(nbrOccurence);
                    totalWordsOccurence += nbrOccurence;
                    found = true;
                }
            }

            if (!found)
            {
                Word newWord = new Word(pWordToUp);
                newWord.NewOccurence(nbrOccurence);
                words.Add(newWord);
            }
        }
    }
}
