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

        public const char EMPTY_CHAR = '-';

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
                    ground[x].Add(EMPTY_CHAR);
                }
            }
        }

        /// <summary>
        /// Make some tiny adjustment to be correct with the world system coordinate
        /// </summary>
        /// <param name="x">X logical coordinate in the range [0, X_LENGTH)</param>
        /// <param name="y">Y logical coordinate in the range [0, Y_LENGTH)</param>
        /// <returns></returns>
        Vector3 LogicalCoordinatesToWorldCoordonate(int x, int y)
        {
            return new Vector3(-x + 0.5f, 0.5f, -(y + 1));
        }

        /// <summary>
        /// Make a tree grow from type and logical coordonate
        /// </summary>
        /// <param name="logicalX">X Logical value</param>
        /// <param name="logicalY">Y Logical value</param>
        /// <param name="type">Type of the tree</param>
        private void SpawnTree(int logicalX, int logicalY, int type)
        {
            // Get coordinate
            Vector3 coord = LogicalCoordinatesToWorldCoordonate(logicalX, logicalY);

            // Instanciate the tree
            GameObject tree = Instantiate(treeToSpawn[type], coord, Quaternion.identity);

            // Add it inside an empty object with all the tree 
            tree.transform.parent = treeParent.transform;

            // Keep update the information about the forest 
            treeNumber++;
            ground[logicalX][logicalY] = type.ToString()[0]; // /!\ Can broke if more than 10 type are added to the game
        }

        public void MakeATreeGrow(int type)
        {
            if(TreeCanGrow)
            {
                int x = 0, y = 0;
                (x, y) = GetRandomCoordinateForTree();
                SpawnTree(x, y, type);
            }
        }

        private (int, int) GetRandomCoordinateForTree()
        {
            int x, y;
            do
            {
                x = Random.Range(0, X_LENGTH);
                y = Random.Range(0, Y_LENGTH);

                // Check for random coordinate untile a empty one has been found
            } while (ground[x][y] != EMPTY_CHAR);
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

        /// <summary>
        /// Remove all tree and reset logical view (no refund will be made D:)
        /// </summary>
        private void ClearTree()
        {
            // Destroy all elements
            foreach(Transform child in treeParent.transform)
            {
                Destroy(child.gameObject);
            }

            // Reset tree number
            treeNumber = 0;

            // Reset logical view
            for(int x=0; x<X_LENGTH; x++)
            {
                for(int y=0; y<Y_LENGTH; y++)
                {
                    ground[x][y] = EMPTY_CHAR;
                }
            }
        }

        public void LoadTree(string newTree)
        {
            // Remove the forest :( 
            ClearTree();

            // Plant new tree :D
            for(int i = 0; i < (X_LENGTH*Y_LENGTH); i++)
            {
                char tree = newTree[i];
                if(tree != EMPTY_CHAR)
                {
                    // Get logical coordinate from indice value
                    int x = i / Y_LENGTH;
                    int y = i % Y_LENGTH;

                    /* IF you think this is black magic, i'm must say it's magic, yes, but pretty white : 
                     * Every charactere are coded in there Ascii/UTF-8/UTF-16 number but they are always one after the other and in order
                     * so make char(digit) - '0' will always give the good digit as int. /!\ this technic works only for [0,9] values
                     * so changes has to be done in the futur, but a simple int.parse could do the trick */
                    int treeValue = tree - '0';

                    // Make the tree Spawn
                    SpawnTree(x, y, treeValue);
                }
            }
        }
    }

}
