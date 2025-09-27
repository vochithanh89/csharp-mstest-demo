namespace VoChiThanh_Module2;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class UTLab8
{
    [TestMethod]
    public void TestContext()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "UTLab7.csv");
        Assert.IsTrue(File.Exists(path), $"Không tìm thấy file CSV: {path}");

        var lines = File.ReadAllLines(path);
        for (int i = 1; i < lines.Length; i++) // bỏ header
        {
            var parts = lines[i].Split(',');
            string listRaw = parts[0].Trim();
            string leftRaw = parts[1].Trim();
            string rightRaw = parts[2].Trim();
            string expectedRaw = parts[3].Trim();

            try
            {
                int[] arr = ParseArray(listRaw);
                int left = int.Parse(leftRaw);
                int right = int.Parse(rightRaw);

                QuickSort(arr, left, right);

                int actual = (arr.Length > 0 && right >= 0 && right < arr.Length)
                    ? arr[right] : 0;

                if (expectedRaw == "Exception")
                {
                    Assert.Fail($"Fail dòng {i + 1}: mong Exception nhưng chạy được, arr={listRaw}");
                }
                else
                {
                    int expected = int.Parse(expectedRaw);
                    Assert.AreEqual(expected, actual,
                        $"Sai tại dòng {i + 1}: arr={listRaw}, left={left}, right={right}");
                }
            }
            catch (Exception)
            {
                if (expectedRaw != "Exception")
                {
                    Assert.Fail($"Fail dòng {i + 1}: arr={listRaw}, left={leftRaw}, right={rightRaw}, expected={expectedRaw} nhưng bị Exception");
                }
            }
        }
    }

    // QuickSort
    private void QuickSort(int[] list, int left, int right)
    {
        if (left >= right) return;

        int i = left, j = right;
        int pivot = list[(left + right) / 2];

        while (i <= j)
        {
            while (list[i] < pivot) i++;
            while (list[j] > pivot) j--;
            if (i <= j)
            {
                int tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
                i++; j--;
            }
        }
        if (left < j) QuickSort(list, left, j);
        if (i < right) QuickSort(list, i, right);
    }

    // Parse chuỗi thành mảng int
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
}