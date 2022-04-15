<?php

    include_once('utils.php');

    function Load()
    {        
        // If the code isn't send 
        if(!array_key_exists('saveCode',$_GET))
        {
            return false; // trigger an error 
        }

        // Sanityze the input
        $code = filter_input(INPUT_GET, 'saveCode' , FILTER_SANITIZE_SPECIAL_CHARS);

        // Research the correct save code
        $query = "SELECT * FROM save_table WHERE id_save='$code';";

        // Connect to mysql
        $mysql = ConnectMySQL();

        // Send the request
        $res = $mysql->query($query);

        if($res->num_rows == 0)
        {
            echo("Code not found");
            return false; // If no row were find, trigger an error
        }
        
        // Send the request
        $row = $res->fetch_assoc();

        // Convert the result in JSON and send them back to the client
        $result = array(
            "score"=>$row['score'],
            "upgradeClick"=>$row['upgrade_click'],
            "upgradeGatherer"=>$row['upgrade_gatherer'],
            "treeMap"=>$row['tree_map']
        );

        $jsonResult = json_encode($result);
        echo($jsonResult);
        return true;
    }

?>