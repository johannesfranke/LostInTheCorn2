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

namespace LostInTheCorn2.Audio
{
    public class SongManager
    {
        private static ContentManager _content;
        private static Dictionary<string, Song> _songs = new Dictionary<string, Song>();
        private static string _currentSong = string.Empty;

        public SongManager(ContentManager content)
        {
            _content = content;
        }

        public static float Volume
        {
            get => MediaPlayer.Volume;
            set => MediaPlayer.Volume = Math.Clamp(value, 0f, 1f);
        }

        public static bool IsRepeating
        {
            get => MediaPlayer.IsRepeating;
            set => MediaPlayer.IsRepeating = value;
        }

        public static void LoadSong(string songName)
        {
            if (!_songs.ContainsKey(songName))
            {
                Song song = Functional.ContentManager.Load<Song>(songName);
                _songs[songName] = song;
            }
        }

        public static void PlaySong(string songName, bool isRepeating = false)
        {
            if (_songs.ContainsKey(songName))
            {
                if (_currentSong != songName)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.IsRepeating = isRepeating;
                    MediaPlayer.Play(_songs[songName]);
                    _currentSong = songName;
                }
            }
            else
            {
                throw new Exception($"Song '{songName}' not found. Did you forget to load it?");
            }
        }

        public static void PauseSong()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
        }

        public static void ResumeSong()
        {
            if (MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Resume();
            }
        }

        public static void StopSong()
        {
            MediaPlayer.Stop();
            _currentSong = string.Empty;
        }

        public static bool IsSongPlaying()
        {
            return MediaPlayer.State == MediaState.Playing;
        }

        public static void UnloadAllSongs()
        {
            _songs.Clear();
            StopSong();
        }
    }
}
