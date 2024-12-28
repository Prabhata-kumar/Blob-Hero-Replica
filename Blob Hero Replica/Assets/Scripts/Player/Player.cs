using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Func<Transform> PlayerPos;

    private void OnEnable()
    {
        PlayerPos += SendPlayerPosition;
    }

    private void OnDisable()
    {
        PlayerPos -= SendPlayerPosition;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void TakeDamage(float takedamage)
    {

    }

    public Transform SendPlayerPosition()
    {
        return transform;
    }

}
