namespace BenchmarkingApp.Benchmarks.Data {
    using System;
    using System.Linq;

    public static class StringGenerator {
        readonly static string Uid = Guid.NewGuid().ToString().Substring(0, 36 - 8);
        static Random random = new Random(10000);
        public static string UID(this int seed) {
            return Uid + seed.ToString("X8");
        }
        public static string String(this Random random, int length) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        const string loremipsum = "Lorem ipsum dolor sit amet, nec populo denique ei. Pri in illud scripta nominati. Et eum offendit adolescens, ei posse vivendo consetetur sed. Quo at illum eloquentiam. In praesent aliquando nam. Omnes tibique reprimique ad cum. Te iudico euripidis rationibus quo. Nam in falli quodsi. Duo no debitis detraxit. Zril interesset ei nam. Usu saperet eleifend disputationi no, possim officiis laboramus vel ex. Id atomorum dissentiunt pri, ne sea feugait mandamus constituto. Adhuc tantas pertinacia eos ad. Mea viris expetenda te, suas omnium indoctum et mei. Est ex etiam aliquid fuisset, euismod nusquam deterruisset ne duo, agam tempor vix ei. Eam illum animal officiis id, usu ei tale malis. Meliore concludaturque an est, porro dicta mea eu, te nonumy audire per. Error aeque efficiantur ei vix, cum ferri officiis facilisis ex, latine qualisque complectitur mei te. Esse dicta euripidis eam ex, eam labitur pertinax ne. Sea id ipsum persecuti. Nemore recusabo ius an, per omnis sadipscing eloquentiam et. Sed ex liber regione fierent, ei pro alienum epicurei adipisci. Mollis detracto sit in, magna errem pertinax te ius. Dicat quaeque appareat vix no, nonumy dissentiunt eam cu. Mutat hendrerit ei per. Te iudico ridens phaedrum has, eos te postea denique, no choro consul ridens pri. Id nam atqui denique, lorem intellegebat pri ex. Vim quaestio assentior signiferumque no.";
        static readonly string[] loremipsumWords = loremipsum.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        public static string LoremIpsum(this Random random) {
            string[] words = new string[random.Next(loremipsum.Length / 2, loremipsum.Length)];
            for(int i = 0; i < words.Length; i++)
                words[i] = loremipsumWords[random.Next(loremipsumWords.Length)];
            return string.Join(" ", words);
        }
    }
}