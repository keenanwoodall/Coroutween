using System.Collections;
using UnityEngine;

namespace Beans.Unity.Tweening
{
	public enum EaseType
	{
		Linear,
		InQuad,
		OutQuad,
		InOutQuad,
		InCubic,
		OutCubic,
		InOutCubic,
		InQuart,
		OutQuart,
		InOutQuart,
		InQuint,
		OutQuint,
		InOutQuint
	}

	public class Coroutweener : MonoBehaviour { }

	public static class Coroutween
	{

		public delegate void ProgressChanged<T> (T from, T to, float t) where T : struct;

		private static Coroutweener tweener;
		private static Coroutweener Tweener
		{
			get
			{
				if (tweener == null)
					tweener = new GameObject ("Tweener (Don't Delete)").AddComponent<Coroutweener> ();
				return tweener;
			}
		}

		public static Coroutine To<T> (T from, T to, float duration, ProgressChanged<T> onProgressChanged, EaseType ease = EaseType.InOutCubic) where T : struct
		{
			return Tweener.StartCoroutine (ToRoutine (from, to, duration, onProgressChanged, ease));
		}

		private static IEnumerator ToRoutine<T> (T from, T to, float duration, ProgressChanged<T> onProgressChanged) where T : struct
		{
			if (duration == 0f)
			{
				onProgressChanged (from, to, 1f);
				yield break;
			}

			var time = 0f;

			while (time < duration)
			{
				onProgressChanged (from, to, time / duration);
				time += Time.deltaTime;
				yield return null;
			}

			onProgressChanged (from, to, 1f);
			yield break;
		}

		private static IEnumerator ToRoutine<T> (T from, T to, float duration, ProgressChanged<T> onProgressChanged, EaseType ease) where T : struct
		{
			var easeMethod = GetEaseMethod (ease);
			return ToRoutine (from, to, duration, (a, b, t) => onProgressChanged (a, b, easeMethod (t)));
		}

		private delegate float EaseMethod (float t);
		private static EaseMethod GetEaseMethod (EaseType ease)
		{
			switch (ease)
			{
				default:
					return Linear;
				case EaseType.InQuad:
					return InQuad;
				case EaseType.OutQuad:
					return OutQuad;
				case EaseType.InOutQuad:
					return InOutQuad;
				case EaseType.InCubic:
					return InCubic;
				case EaseType.OutCubic:
					return OutCubic;
				case EaseType.InOutCubic:
					return InOutCubic;
				case EaseType.InQuart:
					return InQuart;
				case EaseType.OutQuart:
					return OutQuart;
				case EaseType.InOutQuart:
					return InOutQuart;
				case EaseType.InQuint:
					return InQuint;
				case EaseType.OutQuint:
					return OutQuint;
				case EaseType.InOutQuint:
					return InOutQuint;
			}
		}
		private static float Linear (float t) => t;
		private static float InQuad (float t) => t * t;
		private static float OutQuad (float t) => t * (2f - t);
		private static float InOutQuad (float t) => t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;
		private static float InCubic (float t) => t * t * t;
		private static float OutCubic (float t) => (t - 1f) * t * t + 1f;
		private static float InOutCubic (float t) => t < 0.5f ? 4f * t * t * t : (t - 1f) * (2f * t - 2f) * (2 * t - 2) + 1f;
		private static float InQuart (float t) => t * t * t * t;
		private static float OutQuart (float t) => 1f - (t - 1f) * t * t * t;
		private static float InOutQuart (float t) => t < 0.5f ? 8f * t * t * t * t : 1f - 8f *(t - 1f) * t * t * t;
		private static float InQuint (float t) => t * t * t * t * t;
		private static float OutQuint (float t) => 1f + (t - 1f) * t * t * t * t;
		private static float InOutQuint (float t) => t < 0.5f ? 16f * t * t * t * t * t : 1f + 16f * (t - 1f) * t * t * t * t;
	}
}