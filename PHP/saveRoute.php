<?php

    include_once('utils.php');

    function Save()
    {
        // Setup sanytize
        $sanitizeOptions = array(
            'score' => FILTER_SANITIZE_NUMBER_INT,
            'upgradeClick' => FILTER_SANITIZE_NUMBER_INT,
            'upgradeGatherer' => FILTER_SANITIZE_NUMBER_INT
        );

        // Check arguments

        $args = array('score', 'upgradeClick', 'upgradeGatherer');
        for($i=0; $i < count($args); $i++)
        {
            /* If the argument his not found in the request, return false for a 400 bad request result,
             * don't explicite the name of missing arguments for security reason */

            if(!array_key_exists($args[$i],$_POST))
            {
                return false;
            }
        }

        // Sanitize
        $input = filter_input_array(INPUT_POST, $sanitizeOptions);
        
        // Generate a pseudo-random code
        $saveCode = GenerateRandomCode();
        
        // Add inside the BDD the value of the save
        if(!AddSave($saveCode, $input['score'], $input['upgradeClick'], $input['upgradeGatherer']))
        {
            // If a problem have been trigger
            return false;
        }

        // Send the code if the process complete properly
        echo($saveCode);
        return true;
    }

    function GenerateRandomCode()
    {
        $code = '';
        $possibility = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ';
        
        $success = true;

        do {
            $code = ''; // reset the code if the while loop has been triggered 

            for($i = 0; $i < 6; $i++)
            {
                // Add a random caractere between all the possibility show above
                $randomLetter = rand(0, strlen($possibility)-1);
                $code .= $possibility[$randomLetter];
            }

        } while (!TestRandomCode($code)); // Continue until a good code has been finded 

        return $code;
    }

    function TestRandomCode($code)
    {
        // Setup the query to find if a id_save has always been generated with this code characters  
        $query = "SELECT * FROM save_table WHERE id_save='$code';";

        // Connect to mysql
        $mysql = ConnectMySQL();
        
        // Send the request
        $res = $mysql->query($query);
        
        // Return if the result his equal to 0 (no code has already been generated with this code)
        return $res->num_rows == 0;
    }

    function AddSave($code, $score, $upgradeClick, $upgradeGatherer)
    {
        // Setup the query to save
        $query = "INSERT INTO save_table(id_save, score, upgrade_click, upgrade_gatherer) VALUES ('$code', $score, $upgradeClick, $upgradeGatherer);";
        
        // connect to mysql 
        $mysql = ConnectMySQL();
        
        return $mysql->query($query);
    }

?> 