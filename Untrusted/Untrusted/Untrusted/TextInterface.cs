using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Untrusted
{
    public class TextInterface
    {
        SpriteFont spriteFont;
        Texture2D texture;
        Rectangle rectangle;
        Vector2 position;
 
        String[] text;
        private int textLine;
        private int textLength;
        public float maxLineSize;
        private float size;
        private Color color;
        public int counter = 0;

        public bool Active;
        public bool Pressed;
        public KeyboardState last;
        public KeyboardState keyboard = Keyboard.GetState();

        public TextInterface()
        {
            last = Keyboard.GetState();
        }
        public void Initialize(Texture2D texture, Vector2 position, Vector2 bounds, SpriteFont spriteFont, String text, float maxLineSize, float size, Color color)
        {
            this.texture = texture;
            this.position = position;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, (int)bounds.X, (int)bounds.Y);  

            this.spriteFont = spriteFont;
            this.color = color;

            this.textLine = 0;
            this.textLength = 0;
            this.maxLineSize = maxLineSize;

            this.text = SplitText(text);            
            this.size = size;

            
            
            this.Active = true;
           
        }

        private string[] SplitText(String text)
        {
            //Settings//
            //float maxLineSize = 1450f;
   
            //Settings//

            String[] words = text.Split(' ');
            StringBuilder stringbuilder = new StringBuilder();
            float linewidth = 0f;
            float spaceWidth = spriteFont.MeasureString(" ").X;

            int numberLines = 0;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (linewidth + size.X < maxLineSize)
                {
                    stringbuilder.Append(word + " ");
                    linewidth += size.X + spaceWidth;
                }
                else
                {
                    stringbuilder.Append("\n" + word + " ");
                    linewidth = size.X + spaceWidth;
                    numberLines++;
                }

            }
            textLength = numberLines;
            return stringbuilder.ToString().Split('\n');
        }

        public void Update(String text)
        {
            this.textLine = 0;
            this.textLength = 0;
            this.text = SplitText(text);
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            Rectangle mouserec = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (!Pressed && mouserec.Intersects(rectangle))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Pressed = true;
                    this.textLine++;
                    textLine = (int)MathHelper.Clamp(textLine, 0, textLength);
                }
            }
            if (Pressed)
            {
                if (mouse.LeftButton == ButtonState.Released)
                {
                    Pressed = false;
                }
            }

            KeyboardState keyboard = Keyboard.GetState();
            

            if (keyboard.IsKeyDown(Keys.Up))
            {
                this.textLine--;
                textLine =(int)MathHelper.Clamp(textLine, 0, textLength);
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                this.textLine++;
                textLine =(int)MathHelper.Clamp(textLine, 0, textLength);
            }

            last = keyboard;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(texture, position, rectangle, Color.Black,
                             0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
                Vector2 FontMeasurement = spriteFont.MeasureString("a");
                

                //Vector2 FontOrigin = spriteFont.MeasureString(text) /8;
                spriteBatch.DrawString(spriteFont, DrawText(), new Vector2(position.X+10, position.Y), color,
        0, Vector2.Zero, size, SpriteEffects.None, 0.5f);
            }
        }

        private String DrawText()
        {
            int maxLines = 10;  //Number of lines display

            string dtext = "";
            //Console.Write("textLine:" textLine + " textlength.length" + text.Length);
            for (int i = textLine; (i < textLine + maxLines)&& (i < text.Length); i++)
            {
                dtext += text[i] + "\n";
            }
            return dtext;
        }
    }
}
