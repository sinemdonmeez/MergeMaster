using UnityEngine;

public class Grid : MonoBehaviour
{
    /*-------------------------------------*/
    [SerializeField]
    public float SizeX = 2f;
    public float SizeZ;

    /*-------------------------------------*/

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / SizeX);
        int yCount = Mathf.RoundToInt(position.y / SizeX);
        int zCount = Mathf.RoundToInt(position.z / SizeZ);

        Vector3 result = new Vector3(
            (float)xCount * SizeX,
            (float)yCount * SizeX,
            (float)zCount * SizeZ);

        result += transform.position;
        return result;
    }

    /*-------------------------------------*/
}