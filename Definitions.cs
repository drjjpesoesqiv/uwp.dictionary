using System;
using System.Collections.ObjectModel;

namespace InkWords
{
    public class Definition
    {
        public String Words { get; set; }
        public String PartOfSpeech { get; set; }
        public String Gloss { get; set; }

        public Definition(String words, String partOfSpeed, String gloss)
        {
            this.Words = words;
            this.PartOfSpeech = partOfSpeed;
            this.Gloss = gloss;
        }
    }

    public class Definitions : ObservableCollection<Definition>
    {
        public Definitions()
        {
        }
    }
}