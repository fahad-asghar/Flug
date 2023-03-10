using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private void OnLogoAnimationCompleted()
    {
        SplashScreenController.instance.EnableTag();
    }

    private void OnTagAnimationCompleted()
    {
        SplashScreenController.instance.OpenMainScene();
    }

    private void PlaySwooshSound()
    {
        SoundManager.instance.playSound(SoundManager.instance.swoosh, 0.5f);
    }
}
