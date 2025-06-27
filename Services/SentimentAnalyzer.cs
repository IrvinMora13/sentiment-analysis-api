namespace SentimentApi.Services
{
    public interface InterfaceSentimentAnalyzer
    {
        string AnalyzeSentiment(string text);
    }

    public class SentimentAnalyzer : InterfaceSentimentAnalyzer
    {
        private static readonly string[] PositiveWords = { "excelente", "genial", "espectacular", "bueno", "increible", "agradable", "comodo" };
        private static readonly string[] NegativeWords = { "malo", "terrible", "problematico", "defectuoso", "horrible", "feo", "pesimo" };

        public string AnalyzeSentiment(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "neutral";

            var lower = text.ToLower();
            if (PositiveWords.Any(w => lower.Contains(w))) return "positivo";
            if (NegativeWords.Any(w => lower.Contains(w))) return "negativo";
            return "neutral";
        }
    }
}