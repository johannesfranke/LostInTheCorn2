using LostInTheCorn2.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LostInTheCorn2
{
    public class SliderButton : Animated2d
    {
        public Vector2 position;
        public Vector2 dimensions;
        public float minValue, maxValue, currentValue;
        public bool isDragging;
        public bool isHovered;
        public Texture2D sliderTexture;
        public Texture2D knobTexture;
        public Color sliderColor, knobColor;
        public Action<float> OnValueChanged;
        private Vector2 knobPosition;
        public SpriteFont font;
        public string label;

        public SliderButton(string PATH, Vector2 position, Vector2 dimensions, float minValue, float maxValue, float initialValue, Texture2D sliderTexture, Texture2D knobTexture, string fontPath, string label, Action<float> onValueChanged)
            : base(PATH, position, dimensions, Color.White) // Ensure base initialization
        {
            this.position = position;
            this.dimensions = dimensions;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.currentValue = MathHelper.Clamp(initialValue, minValue, maxValue);
            this.sliderTexture = sliderTexture;
            this.knobTexture = knobTexture;
            this.label = label;
            this.OnValueChanged = onValueChanged;
            this.isDragging = false;
            this.isHovered = false;

            this.sliderColor = Color.White;
            this.knobColor = Color.White;

            if (!string.IsNullOrEmpty(fontPath))
            {
                font = Functional.ContentManager.Load<SpriteFont>(fontPath);
            }

            UpdateKnobPosition();
        }

        public override void Update(Vector2 OFFSET)
        {
            Vector2 mousePosition = Functional.MouseHelper.newMousePos + OFFSET;

            isHovered = Hover(OFFSET);

            if (isHovered && Functional.MouseHelper.LeftClickHold())
            {
                isDragging = true;
            }

            if (isDragging)
            {
                float relativeMouseX = mousePosition.X - (position.X + OFFSET.X);
                float newValue = minValue + (relativeMouseX / dimensions.X) * (maxValue - minValue);
                currentValue = MathHelper.Clamp(newValue, minValue, maxValue);
                UpdateKnobPosition();

                OnValueChanged?.Invoke(currentValue);
            }

            if (Functional.MouseHelper.LeftClickRelease())
            {
                isDragging = false;
            }
        }

        private void UpdateKnobPosition()
        {
            // Correct calculation of knob position relative to the slider range
            float ratio = (currentValue - minValue) / (maxValue - minValue);
            knobPosition = new Vector2(position.X + ratio * (dimensions.X - (dimensions.Y / 2)), position.Y);
        }

        public override void Draw(Vector2 OFFSET)
        {
            Color currentSliderColor = isHovered ? Color.LightGray : sliderColor;
            Color currentKnobColor = isHovered ? Color.LightGray : knobColor;

            // Draw the slider (this is the bar texture)
            Visuals.SpriteBatch.Draw(
                sliderTexture,
                new Rectangle((int)(position.X + OFFSET.X), (int)(position.Y + OFFSET.Y), (int)dimensions.X, (int)dimensions.Y / 2), // Make the slider bar thinner
                null,
                currentSliderColor,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f
            );

            // Draw the knob
            Visuals.SpriteBatch.Draw(
                knobTexture,
                new Rectangle((int)(knobPosition.X + OFFSET.X), (int)(knobPosition.Y + OFFSET.Y), (int)(dimensions.Y / 2), (int)(dimensions.Y / 2)), // Draw the knob as a square
                null,
                currentKnobColor,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f
            );

            // Draw the label and value
            if (font != null && !string.IsNullOrEmpty(label))
            {
                string displayText = $"{label}: {Math.Round(currentValue, 2)}";
                Vector2 textSize = font.MeasureString(displayText);
                Visuals.SpriteBatch.DrawString(font, displayText, position + OFFSET + new Vector2(dimensions.X / 2 - textSize.X / 2, -textSize.Y ), Color.Black);
            }
        }

        public override bool Hover(Vector2 OFFSET)
        {
            Vector2 mousePos = Functional.MouseHelper.newMousePos;

            return (mousePos.X >= (position.X + OFFSET.X) &&
                    mousePos.X <= (position.X + OFFSET.X) + dimensions.X &&
                    mousePos.Y >= (position.Y + OFFSET.Y) &&
                    mousePos.Y <= (position.Y + OFFSET.Y) + dimensions.Y);
        }
    }
}
