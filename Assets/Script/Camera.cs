using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform player;

    public float mixX;
    public float maxX;
    public float mixY;
    public float maxY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 temp = transform.position;
            temp.x = player.position.x;
            temp.y = player.position.y;
            if (temp.x < mixX)
            {
                temp.x = mixX;
            }
            if (temp.x > maxX)
            {
                temp.x = maxX;
            }
            if (temp.y < mixY)
            {
                temp.y = mixY;
            }
            if (temp.y > maxY)
            {
                temp.y = maxY;
            }
            transform.position = temp;
        }
    }
}
