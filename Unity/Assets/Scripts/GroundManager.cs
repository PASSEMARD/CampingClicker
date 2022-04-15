using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace clicker
{

    public class GroundManager : MonoBehaviour
    {
        public const int COST_TREE = 20;

        public const int X_LENGTH = 7;
        public const int Y_LENGTH = 8;

        List<List<char>> ground;

        // Keep the count of how many tree has been grow in order to not buy more than ground tile
        private int treeNumber; 
        public bool TreeCanGrow { 
            get
            {
                return treeNumber < (X_LENGTH * Y_LENGTH);
            }
        }

        [SerializeField] private List<GameObject> treeToSpawn;
        [SerializeField] private GameObject treeParent;

        void Start()
        {
            treeNumber = 0;

            // Init logical view of the forest
            ground = new List<List<char>>();
            for(int x=0; x < X_LENGTH; x++)
            {
                ground.Add(new List<char>());
                for(int y=0; y < Y_LENGTH; y++)
                {
                    ground[x].Add('0');
                }
            }
        }


        public void MakeATreeGrow(int type)
        {
            if(TreeCanGrow)
            {
                int x = 0, y = 0;
                (x, y) = GetRandomCoordonateForTree();

                // Instanciate the tree
                Vector3 coord = new Vector3(-x +0.5f, 0.5f, -(y+1)); // Make some tiny adjustment to be correct with the world system
                Debug.Log("Generating tree in coordinate : " + coord.ToString());
                GameObject tree = Instantiate(treeToSpawn[type], coord, Quaternion.identity);

                // Add it inside an empty object with all the tree 
                tree.transform.parent = treeParent.transform; 

                // Keep update the information about the forest 
                treeNumber++;
                ground[x][y] = type.ToString()[0]; // /!\ Can broke if more than 10 type are added to the game
            }
        }

        public (int, int) GetRandomCoordonateForTree()
        {
            int x, y;
            do
            {
                x = Random.Range(0, X_LENGTH);
                y = Random.Range(0, Y_LENGTH);

                // Check for random coordonate untile a empty one has been found
            } while (ground[x][y] != '0');
            return (x, y);
        }

        /// <summary>
        /// Copy the logical data inside a string and return it
        /// </summary>
        /// <returns>Return a (X_LENGTH x Y_LENGTH) string containning all logical data</returns>
        public string GetLogicalTreeHasString()
        {
            string res = "";

            for (int x = 0; x < X_LENGTH; x++)
                for (int y = 0; y < Y_LENGTH; y++)
                    res += ground[x][y];

            return res;
        }
    }

}
