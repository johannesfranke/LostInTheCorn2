using LostInTheCorn;
using LostInTheCorn2.Globals;
using LostInTheCorn2.ModelFunction;
using LostInTheCorn2.MovableObjects;
using LostInTheCorn2.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LostInTheCorn2.map;

public class MapDrawer
{
    private Camera Cam;

    private Grid Grid;
    private Dictionary<int, Model> ModelsWithEnumInfo { get; set; } = new Dictionary<int, Model> { };

    //wegen des Hut bugs
    private Matrix? alternateHatPosition = null;  
    private bool itemPickedOnce = false;
    private MovementAroundPlayerManager _movementManager;

    public MapDrawer(Camera cam, Vector3 startMap, float sizeCube, MovementAroundPlayerManager movementManager)
    {
        //Setzen des Grids und der Position in Grid-Klasse
        Grid = Grid.SetGrid();
        Grid.SetPositions(startMap, sizeCube);
        _movementManager = movementManager;

        this.Cam = cam;
    }


    public void SetModelWithEnum(int key, Model value)
    {
        ModelsWithEnumInfo.TryAdd(key, value);
    }

    public void DrawWorld(bool keyPicked)
    {

        foreach (var pos in Grid.Positions)
        {
            switch (pos.Info)
            {
                case WhatToDraw.PlaneFloor:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                case WhatToDraw.Wall:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(1), pos.Position, Cam);
                    break;
                case WhatToDraw.Hat:
                    if (!Functional.goalReached)
                    {
                        if (Functional.itemPicked == true)
                        {
                            itemPickedOnce = true; 
                            alternateHatPosition = null; 
                        }
                        else if (itemPickedOnce && Functional.itemPicked == false && alternateHatPosition == null)
                        {
                            // Change the hat's position when itemPicked goes from 1 back to 0
                            Vector3 newHatPositionVector = _movementManager.Player.PlayerWorld.Translation - new Vector3(0,2,0);
                            alternateHatPosition = Matrix.CreateWorld(newHatPositionVector, Vector3.Forward, Vector3.Up);
                        }

                        if(Functional.itemPicked == false)
                        {
                            Matrix drawPosition = alternateHatPosition ?? pos.Position;
                            Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(2), drawPosition, Cam);
                        }
                        Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);

                    }
                    break;
                case WhatToDraw.ScareCrow:
                    if (!Functional.goalReached)
                    {
                        Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(4), pos.Position, Cam);
                    }
                    else
                    {
                        Matrix position = pos.Position;
                        Matrix newPos = pos.Position;
                        newPos.M42 = position.M42 + 7.2f;
                        PositionInfo newPosition = new PositionInfo(newPos, 2);
                        Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(3), newPosition.Position, Cam);
                    };
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                case WhatToDraw.Key:
                    if (!keyPicked)
                    {
                        var posKey = pos.PositionVector + new Vector3(0, 3, 0);
                        var matrixKey = Matrix.CreateWorld(posKey, Vector3.Forward, Vector3.Up);
                        Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(5), matrixKey, Cam);
                    }
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                case WhatToDraw.Door:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(6), pos.Position, Cam);
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                case WhatToDraw.CheckpointScarecrow:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break; //warum gibt es das ?
                case WhatToDraw.Map:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(8), pos.Position, Cam);
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                case WhatToDraw.Butterfly:
                    Vector3 newPo = pos.PositionVector + new Vector3(0, 2, 0);
                    Matrix matrix = Matrix.CreateWorld(newPo, Vector3.Forward, Vector3.Up);
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(9), matrix, Cam);
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break; //Platzhalter 
                case WhatToDraw.Finish:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break; //Platzhalter
                case WhatToDraw.DisapearableWall:
                    if (!Functional.goalReached)
                    {
                        Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(1), pos.Position, Cam);
                    }
                    else
                    {
                        Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    }
                    break;
                default:
                    break;
            }
        }

    }


}