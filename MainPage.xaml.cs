using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Syn.WordNet;

using System.Diagnostics;

namespace InkWords
{
    public sealed partial class MainPage : Page
    {
        Definitions definitions = new Definitions();
        WordNetEngine wordNet;
        string wordNetDir = @"Assets\wn3.1.dict\dict\";
        string noEntries = "No Entries Found";

        public MainPage()
        {
            this.InitializeComponent();
            LoadLibrary();
        }

        public void LoadLibrary()
        {
            wordNet = new WordNetEngine();

            LoadSource("adj",  PartOfSpeech.Adjective);
            LoadSource("adv",  PartOfSpeech.Adverb);
            LoadSource("noun", PartOfSpeech.Noun);
            LoadSource("verb", PartOfSpeech.Verb);

            wordNet.Load();
        }

        private void LoadSource(string type, PartOfSpeech partOfSpeech)
        {
            string dataSource  = wordNetDir + $"data.{type}";
            string indexSource = wordNetDir + $"index.{type}";

            wordNet.AddDataSource(
                new StreamReader(
                    new FileStream(dataSource, FileMode.Open, FileAccess.Read)),
                partOfSpeech);

            wordNet.AddIndexSource(
                new StreamReader(
                    new FileStream(indexSource, FileMode.Open, FileAccess.Read)),
                partOfSpeech);
        }

        private void Define()
        {
            definitions.Clear();

            var synSetList = wordNet.GetSynSets(SearchText.Text.Trim());

            if (synSetList.Count == 0)
                definitions.Add(new Definition("", noEntries, ""));
            else
                foreach (var synSet in synSetList)
                {
                    definitions.Add(new Definition(
                        string.Join(", ", synSet.Words),
                        $"{synSet.PartOfSpeech}",
                        synSet.Gloss));
                }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Define();
        }

        private void SearchTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                Define();
        }
    }
}
