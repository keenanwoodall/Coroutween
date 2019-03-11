using UnityEngine;
using UnityEngine.Audio;

namespace Coroutween
{
	public static class AudioMixerTweens
	{
		public static Coroutine FloatTo (this AudioMixer audioMixer, string name, float to, float duration, EaseType ease)
		{
			return FloatTo (audioMixer, name, to, duration, Ease.GetEaseMethod (ease));
		}
		public static Coroutine FloatTo (this AudioMixer audioMixer, string name, float to, float duration, EaseMethod ease)
		{
			var from = 0f;
			if (!audioMixer.GetFloat (name, out from))
				throw new System.Exception ($"Audio Mixer doesn't have a float called '{name} or is currently being edited.'");
			return Coroutween.To (from, to, duration, ease, x => audioMixer.SetFloat (name, x));
		}
	}
}