namespace algoResearch
{
    public class Word
    {
        private int occurences;
        private string text;

        public int Occurences { get => occurences;}
        public string Text { get => text;}

        public Word(string pText)
        {
            this.text = pText;
        }

        public void NewOccurence(int nbrOccurenceToAdd)
        {
            this.occurences += nbrOccurenceToAdd;
        }
    }
}