#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;
using LostInTheCorn2;
using LostInTheCorn2.Scenes;
using LostInTheCorn2.Globals;
#endregion

namespace LostInTheCorn2.UIClasses
{
    public class ButtonActions
    {
        public ButtonActions() { }
        
        public void startGame()
        {
            Visuals.SceneManager.AddScene(new GameScene());

        }
    }
}
