using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostInTheCorn2.Audio
{
    public class SoundManager
    {
        private static ContentManager _content;
        private static Dictionary<string, SoundEffect> _soundEffects = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, SoundEffectInstance> _soundInstances = new Dictionary<string, SoundEffectInstance>();
        private static float _volume = 1f; // Default volume (100%)

        public SoundManager(ContentManager content) 
        {
            _content = content;
        }

        public static float Volume
        {
            get => _volume;
            set
            {
                _volume = Math.Clamp(value, 0f, 1f);
                foreach (var instance in _soundInstances.Values)
                {
                    instance.Volume = _volume;
                }
            }
        }

        public static void LoadSound(string soundName)
        {
            if (!_soundEffects.ContainsKey(soundName))
            {
                SoundEffect soundEffect = Functional.ContentManager.Load<SoundEffect>(soundName);
                _soundEffects[soundName] = soundEffect;
            }
        }

        public static void PlaySound(string soundName, bool loop = false)
        {
            if (_soundEffects.ContainsKey(soundName))
            {
                if (!_soundInstances.ContainsKey(soundName) || _soundInstances[soundName].State == SoundState.Stopped)
                {
                    SoundEffectInstance instance = _soundEffects[soundName].CreateInstance();
                    instance.Volume = _volume;
                    instance.IsLooped = loop;
                    instance.Play();
                    _soundInstances[soundName] = instance;
                }
                else
                {
                    if (!_soundInstances[soundName].IsLooped)
                    {
                        _soundInstances[soundName].Stop();
                        _soundInstances[soundName].Play();
                    }
                }
            }
            else
            {
                throw new Exception($"Sound {soundName} not found. Did you forget to load it?");
            }
        }

        public static void StopSound(string soundName)
        {
            if (_soundInstances.ContainsKey(soundName))
            {
                _soundInstances[soundName].Stop();
            }
        }

        public static void StopAllSounds()
        {
            foreach (var instance in _soundInstances.Values)
            {
                instance.Stop();
            }
        }

        public static void UnloadAllSounds()
        {
            _soundEffects.Clear();
            _soundInstances.Clear();
        }
    }
}
