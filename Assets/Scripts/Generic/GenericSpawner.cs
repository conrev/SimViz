using Unity.VisualScripting;
using UnityEngine;

public class GenericSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject objToSpawn;

    [SerializeField]
    private Transform positionToSpawn;

    [SerializeField]
    private GameObject holder;

    public void Spawn()
    {
        var obj = Instantiate(objToSpawn);
        obj.transform.position = positionToSpawn.position;
        obj.transform.SetParent(holder.transform);
    }
}
