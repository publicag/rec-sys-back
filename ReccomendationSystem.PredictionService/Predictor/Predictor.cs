using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace RecommendationSystem.ML.Predictor
{
    public class Predictor
    {
        public static string Predict(List<double> args)
        {
            var start = new ProcessStartInfo();
            var scriptPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(),
                @"Predictor\predict.py"));

            var arguments = new StringBuilder(scriptPath);
            foreach (var arg in args)
            {
                arguments.Append(' ');
                arguments.Append(arg.ToString(CultureInfo.InvariantCulture));
            }
            start.FileName = @"D:\Anaconda\envs\tf2.4\python.exe";
            start.Arguments = arguments.ToString();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

            using (var process = Process.Start(start))
            {
                using (var reader = process.StandardOutput)
                {
                    var result = reader.ReadToEnd();
                    return result;
                }
            }
        }
    }
}
