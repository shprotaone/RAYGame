using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float FindAngle(Vector3 start, Vector3 end)
    {
        Vector3 dir;
        if (end.y - start.y < 0)
        {
            dir = Vector3.left;
        }
        else
        {
            dir = Vector3.right;
        }

        float result = Vector3.Angle(end - start, dir);
        return result;
    }

    public static Vector3[] ConvertToVector3Array(List<Vector2> points)
    {
        Vector3[] result = new Vector3[points.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = points[i];
        }

        return result;
    }
}
