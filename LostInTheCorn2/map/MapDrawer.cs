using LostInTheCorn;
using LostInTheCorn2.Globals;
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
                case WhatToDraw.CheckpointScarecrow:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                case WhatToDraw.PlaneFloor:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                case WhatToDraw.NoClip:
                    if (!Functional.goalReached)
                    {
                        Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(1), pos.Position, Cam);
                    }
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                case WhatToDraw.Wall:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(1), pos.Position, Cam);
                    break;
                case WhatToDraw.Box:
                    if (boxPosition == null) { boxPosition = new PositionInfo(pos.Position, 2); }
                    if (!Functional.goalReached && !Functional.itemPicked)
                    {
                        Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(7), boxPosition.Position, Cam);
                    }
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                case WhatToDraw.Goal:
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
                    break;
                case WhatToDraw.Door:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(6), pos.Position, Cam);
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
                case WhatToDraw.Map:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(2), pos.Position, Cam);
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                case WhatToDraw.Finish:
                    Drawable.drawWithEffectModel(ModelsWithEnumInfo.GetValueOrDefault(0), pos.Position, Cam);
                    break;
                default:
                    break;
            }
        }

    }


}