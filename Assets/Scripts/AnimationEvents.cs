using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private void OnLogoAnimationCompleted() => SplashScreenController.instance.EnableTag();

    private void OnTagAnimationCompleted() => SplashScreenController.instance.OpenMainScene();

    private void playSound(AudioClip clip) => SoundManager.instance.playSound(clip, 0.5f);
}
