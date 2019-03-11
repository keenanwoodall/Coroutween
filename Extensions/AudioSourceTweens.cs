using UnityEngine;

namespace Coroutween
{
	public static class AudioSourceTweens
	{
		public static Coroutine VolumeTo (this AudioSource source, float to, float duration, EaseType ease)
		{
			return Coroutween.To (source.volume, to, duration, ease, x => source.volume = x);
		}
		public static Coroutine VolumeTo (this AudioSource source, float to, float duration, EaseMethod ease)
		{
			return Coroutween.To (source.volume, to, duration, ease, x => source.volume = x);
		}

		public static Coroutine PitchTo (this AudioSource source, float to, float duration, EaseType ease)
		{
			return Coroutween.To (source.pitch, to, duration, ease, x => source.pitch = x);
		}
		public static Coroutine PitchTo (this AudioSource source, float to, float duration, EaseMethod ease)
		{
			return Coroutween.To (source.pitch, to, duration, ease, x => source.pitch = x);
		}
	}
}