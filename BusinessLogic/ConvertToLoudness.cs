using System.Text.RegularExpressions;

namespace OpenRemoteAPI.BusinessLogic

{
    public static class ProcessingFrequencySpread
    {

        public static float CalculateLoudness(Dictionary<string, int> frequencyReadings)
        {

            //Regex to extract just the numver for the string array which has values as "300Hz" => 300
            int[] frequecies = frequencyReadings.Keys.Select(value =>
                int.Parse(Regex.Match(value, @"\d+").Value)).ToArray();

            //Extract values for each frequency
            int[] frequencyValues = frequencyReadings.Values.ToArray();


            int[] adjustedvalues = AdjustedForHearing(frequecies, frequencyValues);
            float loudness = ConvertToLoudness(adjustedvalues);
            return loudness;
        }

        //Scales values based on human hearing
        //Graph representing how human hearing sensitivity affects percieved sound level for a given frequency: https://www.researchgate.net/publication/309449793/figure/fig2/AS:421511308812289@1477507535171/Spectral-sensitivity-of-human-ear-This-chart-shows-the-spectral-sensitivity-of-the-human.png
        private static int[] AdjustedForHearing(int[] frequencies, int[] frequencyValues)
        {
            int[] balancedValues = new int[frequencies.Length];

            //int frequency = 0;

            for (int i = 0; i < frequencies.Length; i++)
            {

                int frequency = frequencies[i];
                int frequencyValue = frequencyValues[i];


                float newValue = frequency switch
                {
                    _ when frequency <= 20 => 0,
                    _ when frequency > 20 && frequency <= 100 => ((-0.005f * frequency) + 1.9f) * frequencyValue,
                    _ when frequency > 100 && frequency <= 200 => ((-0.002f * frequency) + 1.6f) * frequencyValue,
                    _ when frequency > 200 && frequency <= 500 => ((-0.0003f * frequency) + 1.3f) * frequencyValue,
                    _ when frequency > 500 && frequency <= 1000 => ((-0.0002f * frequency) + 1.2f) * frequencyValue,
                    _ when frequency > 1000 && frequency <= 2000 => 1.0f * frequencyValue,
                    _ when frequency > 2000 && frequency <= 4000 => 0.95f * frequencyValue,
                    _ when frequency > 4000 && frequency <= 5000 => 1.0f * frequencyValue,
                    _ when frequency > 5000 && frequency <= 15000 => ((0.00004f * frequency) + 0.8f) * frequencyValue,
                    _ => 0f
                };
                balancedValues[i] = (int)Math.Round(newValue);
            }

            return balancedValues;
        }

        //Converts scales values into a loudness reading
        private static float ConvertToLoudness(int[] values)
        {
            //return values.Sum() / 600000.0f;
            return (float)(1 / (1 + Math.Exp(-0.000005 * (values.Sum() - 500000))));
        }
    }
}