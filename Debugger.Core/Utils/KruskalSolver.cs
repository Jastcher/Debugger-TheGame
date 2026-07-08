// Using System.Collections.Generic;
using System;

// GeeksForGeeks
class KruskalSolver
{
    public static List<int[]> KruskalsMST(int V, List<int[]> edges)
    {
        // Sort all edges based on weight
        edges.Sort((e1,e2)=> e1[2].CompareTo(e2[2]));

        List<int[]> selectedEdges = new List<int[]>();
        // Traverse edges in sorted order
        DSU dsu = new DSU(V);
        int cost = 0, count = 0;

        foreach (var e in edges)
        {
            int x = e[0], y = e[1], w = e[2];

            // Make sure that there is no cycle
            if (dsu.Find(x) != dsu.Find(y))
            {
                dsu.Union(x, y);
                cost += w;
                selectedEdges.Add(e);
                if (++count == V - 1) break;
            }
        }
        return selectedEdges;
    }

}

// Disjoint set data structure
class DSU
{
    private int[] parent, rank;

    public DSU(int n)
    {
        parent = new int[n];
        rank = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
            rank[i] = 1;
        }
    }

    public int Find(int i)
    {
        if (parent[i] != i)
        {
            parent[i] = Find(parent[i]);
        }
        return parent[i];
    }

    public void Union(int x, int y)
    {
        int s1 = Find(x);
        int s2 = Find(y);
        if (s1 != s2)
        {
            if (rank[s1] < rank[s2])
            {
                parent[s1] = s2;
            }
            else if (rank[s1] > rank[s2])
            {
                parent[s2] = s1;
            }
            else
            {
                parent[s2] = s1;
                rank[s1]++;
            }
        }
    }
}