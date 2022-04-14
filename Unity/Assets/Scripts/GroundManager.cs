using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public const int X_LENGTH = 7;
    public const int Y_LENGTH = 8;

    List<List<char>> ground;

    // Start is called before the first frame update
    void Start()
    {
        for(int x=0; x < X_LENGTH; x++)
        {
            ground.Add(new List<char>());
            for(int z=0; z < Y_LENGTH; z++)
            {
                ground[x].Add('0');
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
