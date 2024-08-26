﻿using LostInTheCorn;
using LostInTheCorn2.ModelFunction;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LostInTheCorn2.map;

public class MapDrawer
{
    private Camera Cam;

    private Grid Grid;
    private Dictionary<int, Model> ModelsWithEnumInfo { get; set; } = new Dictionary<int, Model> { };

    public MapDrawer(Camera cam, Vector3 startMap, float sizeCube)
    {
        //Setzen des Grids und der Position in Grid-Klasse
        Grid = Grid.SetGrid();
        Grid.SetPositions(startMap, sizeCube);

        this.Cam = cam;
    }

    
    public void SetModelWithEnum(int key, Model value)
    {
        ModelsWithEnumInfo.TryAdd(key, value);
    }

    public void DrawWorld(bool keyPicked, PositionInfo boxPosition)
    {
        
        foreach (var pos in Grid.Positions)
        {
            switch (pos.Info)
            {
                case WhatToDraw.PlaneFloor:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                case WhatToDraw.NoClip:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(1), pos.Position, Cam);
                    break;
                case WhatToDraw.Wall:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(1), pos.Position, Cam);
                    break;
                case WhatToDraw.Box:
                    if (boxPosition == null) {boxPosition = new PositionInfo(pos.Position, 2); }
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(2), boxPosition.Position, Cam);
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                case WhatToDraw.Goal:
                    break;
                case WhatToDraw.Door:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(1), pos.Position, Cam);
                    break;
                case WhatToDraw.Key:
                    if (!keyPicked)
                    {
                        Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(2), pos.Position, Cam);
                    }
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                default:
                    break;
            }
        }

    }
    

}