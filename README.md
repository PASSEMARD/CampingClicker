# CampingClicker

## Note before starting:

* You will find inside the [php](/PHP/) folder the file [.htaccess](/PHP/.htaccess) mainly use to remove all the routing feature to Appach and leaving the hand to php to control everything.
* The loading and saving process are too fast too really see the loading screen, so don't blink if you want to see it.
* For now, the loading screen his the same for saving and loading. 
* The Gatherer logic his place on the tent object (please don't ask, longue night codding i think) and the GroundManager is attached to the Ground object in the scene. 
* Every needed feature has been implemented so far in the project.

## Server side information : 

* Every route not defined by the server will result in a 404 error.
* The routing process in php his mainly taken [from this article](https://grafikart.fr/tutoriels/router-628). It can be find in the [router.php](PHP/router.php) file.
* The file system of the server his pretty simple :
    * [index.php](PHP/index.php) : This file use router.php class to setup the routing protocole and collect every request to the server before redirecting them to the good function.
    * [router.php](PHP/router.php) : File containning all routing class needed by the server.
    * [loadRoute.php](PHP/loadRoute.php) : File containing all loading logic for the game (server side).
    * [saveRoute.php](PHP/saveRoute.php) : File containing all save logic for the game (server side).
    * [utils.php](PHP/utils.php) : File use by saveRoute.php and loadRoute.php in order to centralize SQL login information. If you want to change them, this is the right place to be.
* In the DataBase, the tree are stored in a constant array of char, every char being a tile. For now, tree are written in the range [0,2] and the caractere '-' has been use to suit to a free tile. 

## Unity: 

If we look at the tree part of the Unity code (mainly in [GroundManager.cs](Unity/Assets/Scripts/GroundManager.cs)) we can see that the tree type his define by an int and not by an enum. The reason his simple : The use of an enum in a simple code like that would have only complexify the code. Plus, if we change often the enum type, numerous bug will increase the dev-time drasticly. But if we have a list of object than will not change or not change often, yeah, an enum type will be the right thing to implement.

## Personalization

* Upgrade level are editable one by one in order to ease the development and the balancing of the game. Noneless, a hard coded and perfectly optimize version can be find, commented in the file [PlayerInformation](Unity/Assets/Scripts/PlayerInformation.cs) at line 91.
* The upgrades and tree cost has been split to ease futur feature and balancing.
   * The upgrade cost is a const that can be find in the [UpgradesValues](Unity/Assets/Scripts/Data/UpgradesValues.cs) class.
   * The tree cost is a const that can be find in the [GroundManager](Unity/Assets/Scripts/GroundManager.cs) class.


## Improvement than can be done :

* According to the project needs, if the upgrades lvl will always contains low values (like [0,255]), the use of TINYINT in the data base would reduce the need of space. 
* The use of an [UUID](https://fr.wikipedia.org/wiki/Universally_unique_identifier) could replace the system implemented in [saveRoute.php](PHP/saveRoute.php) in order to get valid and unique identifier.
* If we look at the code in [GroundManager.cs](Unity/Assets/Scripts/GroundManager.cs) we can see than at line 78 and line 162, the type of tree than can be added can't excess 10. For futur developpment, a class grouping a char attribute, a Prefab and an int for the type would be perfect to increase futher more this limit. 
 
