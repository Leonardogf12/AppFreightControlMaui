namespace FreightControlMaui.Controls.Animations
{
    public class ClickAnimation
    {
        private const int DefaultScale = 1;
        private const double Scale = 0.95;
        private const uint DurationOfAnimantion = 100;
        private const double Fade = 0.5;
        private const uint Length = 100;
        private const int DefaultOpacit = 1;

        public static async Task SetScaleOnElement(View element, double scale = Scale, uint durationOfAnimation = DurationOfAnimantion)
        {
            await element.ScaleTo(scale, durationOfAnimation, Easing.Linear);
            await element.ScaleTo(DefaultScale, durationOfAnimation, Easing.Linear);
        }

        public static async Task SetFadeOnElement(View element, int delay = 100, double fade = Fade)
        {
            await element.FadeTo(fade, Length, Easing.SpringIn);
            await Task.Delay(delay);
            await element.FadeTo(DefaultOpacit, Length);
        }
    }
}