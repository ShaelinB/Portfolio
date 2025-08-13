using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


//ar 
//3-8-24
//making button class for main menu buttons and level select 

namespace JohnnyRocket
{
    internal class Button
    {

        private Rectangle buttonRect;
        private Texture2D buttonTexture;
        private string text;
        private MouseState mouse;
        private MouseState prevMouseState;

        public Button(Rectangle rect, Texture2D texture, string text)
        {
            buttonRect = rect;
            buttonTexture = texture;
            this.text = text;
            mouse = new MouseState();
        }

        /// <summary>
        /// gets the button rectangle
        /// </summary>
        public Rectangle ButtonRect { get { return buttonRect; } }

        /// <summary>
        /// gets the buttons textu
        /// </summary>
        public Texture2D ButtonTexture { get { return buttonTexture; } }

        /// <summary>
        /// gets button "text" so we know what button it is 
        /// </summary>
         public string Text
        {
            get => text;
        }

        public bool Click()
        {

            mouse = Mouse.GetState();


            if (buttonRect.Contains(mouse.Position) &&
               (mouse.LeftButton == ButtonState.Pressed) &&
               (prevMouseState.LeftButton == ButtonState.Released))
            {
               return true;
            }

            prevMouseState = mouse;
            
            //if none of the above, false
            return false;
                
        }


    }
}
