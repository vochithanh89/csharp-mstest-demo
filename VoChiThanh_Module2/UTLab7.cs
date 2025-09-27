namespace VoChiThanh_Module2;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class UTLab7
{
    [TestMethod]
    public void TestContext()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "UTLab6.csv");
        Assert.IsTrue(File.Exists(path), $"Không tìm thấy file CSV: {path}");

        var lines = File.ReadAllLines(path);
        for (int i = 1; i < lines.Length; i++) // bỏ dòng header
        {
            var parts = lines[i].Split(',');
            string aRaw = parts[0].Trim();
            string expectedRaw = parts[1].Trim();
            try
            {
                int[] arr = ParseArray(aRaw);
                int actual = Largest(arr);
                if (expectedRaw.Contains("^"))
                {
                    var expParts = expectedRaw.Split('^');
                    int baseNum = int.Parse(expParts[0]);
                    int pow = int.Parse(expParts[1]);
                    long expVal = (long)Math.Pow(baseNum, pow);
                    Assert.AreEqual(expVal, actual,
                        $"Fail dòng {i + 1}: a={aRaw}, expected={expectedRaw}, actual={actual}");
                }
                else
                {
                    int expVal = int.Parse(expectedRaw);
                    Assert.AreEqual(expVal, actual,
                        $"Fail dòng {i + 1}: a={aRaw}, expected={expectedRaw}, actual={actual}");
                }
            }
            catch (Exception)
            {
                if (expectedRaw != "Exception")
                {
                    Assert.Fail($"Fail dòng {i + 1}: a={aRaw}, expected={expectedRaw}, nhưng gặp Exception");
                }
            }
        }
    }

    private int[] ParseArray(string raw)
    {
        raw = raw.Trim();
        if (raw == "[]") return Array.Empty<int>();
        raw = raw.TrimStart('[').TrimEnd(']');
        if (string.IsNullOrWhiteSpace(raw)) return Array.Empty<int>();
        var tokens = raw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        List<int> result = new List<int>();
        foreach (var t in tokens)
        {
            if (int.TryParse(t.Trim().Trim('"'), out int val))
            {
                result.Add(val);
            }
            else
            {
                throw new Exception("Invalid element in array");
            }
        }
        return result.ToArray();
    }

    private int Largest(int[] a)
    {
        if (a.Length == 0)
            return int.MaxValue;

        return a.Max();
    }
}