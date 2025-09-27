namespace VoChiThanh_Module2;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class UTLab6
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
            string s1 = parts.Length > 0 ? parts[0] : string.Empty;
            string s2 = parts.Length > 1 ? parts[1] : string.Empty;
            string s3 = parts.Length > 2 ? parts[2] : string.Empty;
            string expected = parts.Length > 3 ? parts[3] : string.Empty;

            string actual = ThayThe(s1, s2, s3);
            Assert.AreEqual(expected, actual,
                $"Fail tại dòng {i + 1}: s1='{s1}', s2='{s2}', s3='{s3}'");
        }
    }

    // Hàm cần kiểm thử
    private string ThayThe(string s1, string s2, string s3)
    {
        if (string.IsNullOrEmpty(s1)) return s1;
        if (string.IsNullOrEmpty(s2)) return s1;
        return s1.Replace(s2, s3);
    }
}