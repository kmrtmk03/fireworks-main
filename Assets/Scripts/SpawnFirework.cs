using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFirework : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab = default;

    [SerializeField]
    private GameObject parent = default;

    private GameObject instance;

    public void Spawn(string _data)
    {
        Debug.Log(_data);

        instance = Instantiate(prefab,  parent.transform.position, Quaternion.identity);

        instance.transform.parent = parent.transform;

        StartCoroutine(this.DestroyPrefab(instance));
    }

    private IEnumerator DestroyPrefab(GameObject _object)
    {
        yield return new WaitForSeconds(20.0f);
        Destroy(_object);
    }

}
