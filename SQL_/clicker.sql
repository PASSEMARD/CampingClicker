CREATE DATABASE campingclicker;

USE campingclicker;

CREATE TABLE save_table (
    id_save VARCHAR(6) PRIMARY KEY NOT NULL,
    score INT NOT NULL,
    upgrade_click INT NOT NULL,
    upgrade_gatherer INT NOT NULL,
    tree_map VARCHAR(56)
);
