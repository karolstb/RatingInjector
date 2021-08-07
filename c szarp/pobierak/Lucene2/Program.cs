using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Store;
using Lucene.Net.Analysis;
using Lucene.Net.Util;
//using Nest;
using Lucene.Net.Index;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net.Documents;

namespace Lucene2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //I
                // Ensures index backward compatibility
                const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;

                // Construct a machine-independent path for the index
                var basePath = Environment.GetFolderPath(
                    Environment.SpecialFolder.CommonApplicationData);
                var indexPath = Path.Combine(basePath, "index");

                using (var dir = FSDirectory.Open(indexPath))
                {
                    // Lucene.Net.Analysis.
                    // Create an analyzer to process the text
                    var analyzer = new StandardAnalyzer(AppLuceneVersion);

                    // Create an index writer
                    var indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer);
                    using (var writer = new IndexWriter(dir, indexConfig))
                    {
                        //II
                        // Search with a phrase
                        var phrase = new MultiPhraseQuery
                    {
                        new Term("favoritePhrase", "brown"),
                        new Term("favoritePhrase", "fox"),
                        new Term("favoritePhrase", "jumps")
                    };

                        var phrase2 = new MultiPhraseQuery
                    {
                        new Term("test", "fuck")
                    };

                        //III
                        var source = new
                        {
                            Name = "Kermit the Frog",
                            FavoritePhrase = "The quick brown fox jumps over the lazy dog",
                            Test = "what da fuck"
                        };

                        var doc = new Document
                    {
                        // StringField indexes but doesn't tokenize
                        new StringField("name",
                            source.Name,
                            Field.Store.YES),
                        new TextField("favoritePhrase",
                            source.FavoritePhrase,
                            Field.Store.YES),
                        new StringField("test",
                            source.Test,
                            Field.Store.YES)
                    };

                        writer.AddDocument(doc);
                        writer.Flush(triggerMerge: false, applyAllDeletes: false);

                        //IV
                        // Re-use the writer to get real-time updates
                        using (var reader = writer.GetReader(applyAllDeletes: true))
                        {
                            var searcher = new IndexSearcher(reader);
                            var hits = searcher.Search(phrase, 20 /* top 20 */).ScoreDocs;

                            // Display the output in a table
                            Console.WriteLine($"{"Score",20}" +
                                $" {"Name",-15}" +
                                $" {"Favorite Phrase",-40}" +
                                $" {"Test",-40}");
                            foreach (var hit in hits)
                            {
                                var foundDoc = searcher.Doc(hit.Doc);
                                Console.WriteLine($"{hit.Score:f8}" +
                                    $" {foundDoc.Get("name"),-15}" +
                                    $" {foundDoc.Get("favoritePhrase"),-40}" +
                                    $" {foundDoc.Get("test"),-40}");
                            }
                        }
                    }
                }

                Console.WriteLine("siemka");
                Console.ReadKey();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
