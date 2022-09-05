// ReSharper disable All
using UnityEngine;

public class Translator : MonoBehaviour
{
    public Vector3 velocity;

    void Update()
    {
        transform.position += velocity * Time.deltaTime;    
    }
}
