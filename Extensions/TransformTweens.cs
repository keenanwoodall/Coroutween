using UnityEngine;

namespace Beans.Unity.Tweening
{
	public static class TransformTweens
	{
		public static Coroutine PositionTo (this Transform transform, Vector3 to, float duration, EaseType ease)
		{
			return Coroutween.To (transform.position, to, duration, ease, x => transform.position = x);
		}
		public static Coroutine PositionTo (this Transform transform, Vector3 to, float duration, EaseMethod ease)
		{
			return Coroutween.To (transform.position, to, duration, ease, x => transform.position = x);
		}

		public static Coroutine LocalPositionTo (this Transform transform, Vector3 to, float duration, EaseType ease)
		{
			return Coroutween.To (transform.localPosition, to, duration, ease, x => transform.localPosition = x);
		}
		public static Coroutine LocalPositionTo (this Transform transform, Vector3 to, float duration, EaseMethod ease)
		{
			return Coroutween.To (transform.localPosition, to, duration, ease, x => transform.localPosition = x);
		}

		public static Coroutine RotationTo (this Transform transform, Quaternion to, float duration, EaseType ease)
		{
			return Coroutween.To (transform.rotation, to, duration, ease, x => transform.rotation = x);
		}
		public static Coroutine RotationTo (this Transform transform, Quaternion to, float duration, EaseMethod ease)
		{
			return Coroutween.To (transform.rotation, to, duration, ease, x => transform.rotation = x);
		}

		public static Coroutine LocalRotationTo (this Transform transform, Quaternion to, float duration, EaseType ease)
		{
			return Coroutween.To (transform.localRotation, to, duration, ease, x => transform.localRotation = x);
		}
		public static Coroutine LocalRotationTo (this Transform transform, Quaternion to, float duration, EaseMethod ease)
		{
			return Coroutween.To (transform.localRotation, to, duration, ease, x => transform.localRotation = x);
		}

		public static Coroutine LookTo (this Transform transform, Vector3 to, Vector3 up, float duration, EaseType ease)
		{
			return Coroutween.To (transform.rotation, Quaternion.LookRotation (to - transform.position, up), duration, ease, x => transform.localRotation = x);
		}
		public static Coroutine LookTo (this Transform transform, Vector3 to, Vector3 up, float duration, EaseMethod ease)
		{
			return Coroutween.To (transform.rotation, Quaternion.LookRotation (to - transform.position, up), duration, ease, x => transform.localRotation = x);
		}

		public static Coroutine ScaleTo (this Transform transform, Vector3 to, float duration, EaseType ease)
		{
			return Coroutween.To (transform.localScale, to, duration, ease, x => transform.localScale = x);
		}
		public static Coroutine ScaleTo (this Transform transform, Vector3 to, float duration, EaseMethod ease)
		{
			return Coroutween.To (transform.localScale, to, duration, ease, x => transform.localScale = x);
		}
	}
}