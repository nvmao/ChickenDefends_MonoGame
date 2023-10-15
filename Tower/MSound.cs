using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Tower.Objs;
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Tower
{
	public class Msound
	{
		private static Msound instance = null;

		Song song;
		Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();

		private Msound()
		{
			this.song = Utils.get().content.Load<Song>("music");

			soundEffects.Add("coin", Utils.get().content.Load<SoundEffect>("pickCoin"));
			soundEffects.Add("click", Utils.get().content.Load<SoundEffect>("clickBtn"));
			soundEffects.Add("jump", Utils.get().content.Load<SoundEffect>("jump"));
			soundEffects.Add("throw", Utils.get().content.Load<SoundEffect>("throw"));
			soundEffects.Add("nextLevel", Utils.get().content.Load<SoundEffect>("nextLevel"));
			soundEffects.Add("die", Utils.get().content.Load<SoundEffect>("die"));
			soundEffects.Add("hit", Utils.get().content.Load<SoundEffect>("hit"));
			soundEffects.Add("shoot", Utils.get().content.Load<SoundEffect>("shoot"));



		}
		public void playMusic()
        {
			MediaPlayer.Volume = 1.0f;

			MediaPlayer.Play(song);
		}


		public void play(string key)
        {
			soundEffects[key].Play();

		}








		public static Msound get() // singleton class
		{
			if (instance == null)
				instance = new Msound();
			return instance;
		}


	}
}
