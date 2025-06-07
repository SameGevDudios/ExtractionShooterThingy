using System;
using UnityEngine;

public static class RaycastHitSorter
{
    private static readonly Comparison<RaycastHit> DistanceComparer =
        (a, b) => a.distance.CompareTo(b.distance);

    public static void SortByDistance(RaycastHit[] hits)
    {
        Array.Sort(hits, DistanceComparer);
    }
}