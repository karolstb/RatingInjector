using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Lucene.Net.Analysis.TokenAttributes;
//using Nest;
using System;
using System.IO;

namespace Lucene
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ensures index backward compatibility
            const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;

            // Construct a machine-independent path for the index
            var basePath = Environment.GetFolderPath(
                Environment.SpecialFolder.CommonApplicationData);
            var indexPath = Path.Combine(basePath, "index");

            using var dir = FSDirectory.Open(indexPath);

            // Create an analyzer to process the text
            //var analyzer = new StandardAnalyzer(AppLuceneVersion);
            
            //// Create an index writer
            //var indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer);
            //using var writer = new IndexWriter(dir, indexConfig);

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
