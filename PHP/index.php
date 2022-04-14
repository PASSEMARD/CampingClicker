<?php
    
    include('saveRoute.php');
    include('loadRoute.php');
    include('router.php');
    
    // Return a 404 HTML code = Not found
    function Return404()  
    {
        header('X-PHP-Response-Code: 404', true, 404);
        exit(404);
    }
    
    // Return a 400 HTML code = Bad Request
    function Return400() 
    {
        header('X-PHP-Response-Code: 400', true, 400);
        exit(400);
    }
    

    // Setup Router
    $router = new Router($_GET['url']);

    // Save Route
    $router->post('save', function() 
    { 
        if(!Save()) // If the Save function return false (bad arguments), return a bad request 
            Return400(); 
    });

    $router->get('load', function()
    {
        if(!Load())
            Return400();
    });
    
    // let the root access has 404 
    $router->get('/', function(){ Return404(); });    
    
    try {
        
        // Launch the router 
        $router->run();

    } catch (Exception $th) {
        if($th->getMessage() == '404')
        {
            // If the remaining route his not existant, send a 404
            Return404();
        }
        else throw new Exception("Error no handled : " + $th->getMessage());
    }
?>