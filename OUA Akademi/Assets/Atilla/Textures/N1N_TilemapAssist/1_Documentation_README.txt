This document outlines how to use the different features in this package.

In order for these to work, the 2d tilemap extras package must be installed. This package has been in the "preview" state for a while,
so to download it from the package manager you need to enable preview packages in the project settings window, under the package manager section.


    1: Auto Select Brush

        Use: if you have multiple tilemaps in a scene that have different properties (IE a ground tilemap, 
        a background tilemap, and a hazard/spike tilemap) then you can set this brush up so that you don't
        have to manually select which tilemap you are drawing to.

        How to Use: in order to get the auto brush to work properly, you need to write in which tiles map to where.
        To do this, open the c# file for it and go to line 44; This is the start of a list of tile-tilemap pairs.
        For each tile in the tile pallete, find the name of its corresponding asset in the project window.
        Then find the name of the tilemap object in the scene hiarchy that you want it to draw to, and write those as a pair.
        For example, if the tile is named ExampleTile and the tilemap is named Tilemap, then you would write:

            new TilePair("ExampleTile", "Tilemap"),

        To use the brush, select it in the bottom left corner of the tile pallete window.
    
    2: Putting Tilemap Sprite Sheets in the Correct format

        in order to use your tile sheet with these rule tiles, you need to have them in the correct format.
        they should be in the same configuration as the tiles in the "TilesPreset" asset. to easily set it up, copy the preset file
        and draw your tiles over those on the preset tile sheet. On the preset, there are tiles with numbers: these are alternate versions of
        other tiles that the rule tiles will swap in at random to give the tilemap more variety. the blank tiles numbered 1-6 are tiles that
        line the interior of the drawn tiles.

    3: Base Rule Tile

        Use: automatically sets the tiles so that you don't have to draw in every top edge tile and every 
        side tile and every corner tile as individual tiles.

        How to Use: To make a new rule tile, go to the project window and select create > 2d > tiles > override rule tile. This will 
        make a new override rule tile. insect the new tile, and in the "tile" option, drag in the Base Rule Tile asset.
        To give the tile the correct sprite sheet, click the three dots button in the top right of the inspector window, and select populate
        from sprite sheet, then drag in the sprite sheet you want to use. the sprite sheet must have the correct format of tiles.
        the rule tile should be ready at this point. if it gives you trouble then drag the base rule tile into the tile option of the rule tile again,
        I don't know why this fixes it, but it does.

    4: Friendly Tile
    
        Use: the borders of this tile will blend with other types of tiles on the same tilemap as it, so that you can have
        smooth transitions between types of rule tiles.

        How To Use: the setup for this is the same as for the base rule tile, except you drag in the FriendlyTile asset when setting up the
        override rule tile.


Thanks for downloading this, if there are any problems you have with it, let me know. if there are any problems with it that you
figure out how to solve, let me know so I can fix them in the base version.
