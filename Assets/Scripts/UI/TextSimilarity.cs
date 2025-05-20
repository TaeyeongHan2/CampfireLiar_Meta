using System;
using UnityEngine;

public class TextSimilarity
{

    public static float CalculateSimilarity(string s1, string s2)
    {
        int distance = LevenshteinDistance(s1, s2);
        int maxLength = Mathf.Max(s1.Length, s2.Length);
        if (maxLength == 0) return 1f;
        return 1f - (float)distance / maxLength;
    }

    // 레벤슈타인 거리 계산
    public static int LevenshteinDistance(string s, string t)
    {
        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        if (n == 0) return m;
        if (m == 0) return n;

        for (int i = 0; i <= n; i++) d[i, 0] = i;
        for (int j = 0; j <= m; j++) d[0, j] = j;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                d[i, j] = Mathf.Min(
                    Mathf.Min(d[i - 1, j] + 1,     // 삭제
                        d[i, j - 1] + 1),    // 삽입
                    d[i - 1, j - 1] + cost); // 대체
            }
        }

        return d[n, m];
    }
}
