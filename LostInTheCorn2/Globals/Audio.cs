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
using LostInTheCorn2.Globals;
using LostInTheCorn2.Scenes;
using LostInTheCorn2.Audio;

#endregion

namespace LostInTheCorn2.Globals
{
    public class Audio
    {
        public static SoundManager SoundManager { get; private set; }
        public static SongManager SongManager { get; private set; }

        public static void SetSoundManager(ContentManager content)
        {
            SoundManager = new SoundManager(content);
        }
        public static void SetSongManager(ContentManager content)
        {
            SongManager = new SongManager(content);
        }

    }
}
