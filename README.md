# Coroutween
A 💩 tweening library for Unity - only made for the pun, don't actually use it.

### How to use it
Make sure you have `using Beans.Unity.Tweening;` at the top of your script

After that you have a few options for how to tween stuff.
In the following examples I'll show how to change the position of a transform with each approach.

1. The easiest way is to use premade tweens like this:
```cs
Camera.main.FieldOfViewTo (to: 25f, duration, EaseType.ElasticOut);
```
2. The next way lets you tween any supported value (`int`, `float`, `Vector2/3/4`, `Quaternion`, `Color`)
```cs
Coroutween.To (from: camera.fieldOfView, to: 25f, duration, EaseType.ElasticOut, x => camera.fieldOfView = x);
```
3. The final way gives you complete control by simply providing a callback with the tween's eased progress.
```cs
var from = Camera.main.fieldOfView;
var to = 25f;
Coroutween.CreateInterpolater (duration, EaseType.ElasticOut, t => Camera.main.fieldOfView = Mathf.LerpUnclamped (from, to, t));
```

All of these examples produce the same result:

![1](https://i.imgur.com/Gca8XFf.gif)
