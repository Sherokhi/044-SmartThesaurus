///ETML
///Auteur : perretje
///Date : 01.05.2018
///Description : Classe utilisée pour les instances de pages web

using System.Collections.Generic;

namespace algoResearch
{
    public class WebPage
    {
        /// <summary>
        /// URL de la page
        /// </summary>
        private string selfURL;

        /// <summary>
        /// Nombres d'occurences totales sur la pages
        /// </summary>
        private int totalWordsOccurences;

        /// <summary>
        /// Listes des mots sur la pages
        /// </summary>
        private List<Word> words;

        /// <summary>
        /// Constructeur de classe
        /// </summary>
        /// <param name="pURL"></param>
        public WebPage(string pURL)
        {
            this.selfURL = pURL;
            this.words = new List<Word>();
            totalWordsOccurences = 0;
        }

        /// <summary>
        /// Ajoute une occurence à un mot de la liste
        /// </summary>
        /// <param name="pWordToUp"></param>
        /// <param name="nbrOccurence"></param>
        public void AddWordOccurence(string pWordToUp, int nbrOccurence)
        {
            bool found = false;
            foreach(Word word in words)
            {
                if(word.Text == pWordToUp)
                {
                    word.NewOccurence(nbrOccurence);
                    totalWordsOccurences += nbrOccurence;
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
