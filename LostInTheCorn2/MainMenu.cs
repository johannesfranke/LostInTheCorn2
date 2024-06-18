using LostInTheCorn;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LostInTheCorn2
{
    public static class MainMenu
    {
        /*

        public static GraphicsDevice graphics;
        public static bool mainMenuActive = true;
        public static List<UIObject> menuObjects = new List<UIObject>();
        public static List<UIButton> menuButtons = new List<UIButton>();
        public static string mainMenuCenterText;
        public static bool exit = false;
        public static SpriteFont fontUsed;
        public static ContentManager content;

        private static GameTime currentGameTime;

        public static void init(GraphicsDevice _graphics, ContentManager _content)
        {
            graphics = _graphics;
            content = _content;
            openMainMenu();
            mainMenuCenterText = "LostInTheCorn";
        }
        public static void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            float zoomFactor = 1f;
            spriteBatch.Draw(content.Load<Texture2D>("GraphicEffects/lightblue_bloom5"), new Vector2(UIManager.mainMenu_button_newGame.pos.X + UIManager.mainMenu_button_newGame.texture.Width / 2 - content.Load<Texture2D>("GraphicEffects/lightblue_bloom5").Width * zoomFactor / 2, UIManager.mainMenu_button_newGame.pos.Y + UIManager.mainMenu_button_newGame.texture.Height / 2 - content.Load<Texture2D>("GraphicEffects/lightblue_bloom5").Height * zoomFactor / 2), null, Color.White, 0f, Vector2.Zero, zoomFactor, SpriteEffects.None, 0f);
            spriteBatch.Draw(content.Load<Texture2D>("GraphicEffects/lightblue_bloom5"), new Vector2(UIManager.mainMenu_button_credits.pos.X + UIManager.mainMenu_button_credits.texture.Width / 2 - content.Load<Texture2D>("GraphicEffects/lightblue_bloom5").Width * zoomFactor / 2, UIManager.mainMenu_button_credits.pos.Y + UIManager.mainMenu_button_credits.texture.Height / 2 - content.Load<Texture2D>("GraphicEffects/lightblue_bloom5").Height * zoomFactor / 2), null, Color.White, 0f, Vector2.Zero, zoomFactor, SpriteEffects.None, 0f);
            spriteBatch.Draw(content.Load<Texture2D>("GraphicEffects/lightblue_bloom5"), new Vector2(UIManager.mainMenu_button_quit.pos.X + UIManager.mainMenu_button_quit.texture.Width / 2 - content.Load<Texture2D>("GraphicEffects/lightblue_bloom5").Width * zoomFactor / 2, UIManager.mainMenu_button_quit.pos.Y + UIManager.mainMenu_button_quit.texture.Height / 2 - content.Load<Texture2D>("GraphicEffects/lightblue_bloom5").Height * zoomFactor / 2), null, Color.White, 0f, Vector2.Zero, zoomFactor, SpriteEffects.None, 0f);

            //Drawing UI-Objects
            foreach (UIObject uIObject in menuObjects)
            {
                if (uIObject.active)
                {
                    spriteBatch.Draw(uIObject.texture, uIObject.pos, uIObject.currentColor);
                }

            }

            if (UIManager.mainMenu_creditsMenu.active)
            {
                fontUsed = UIManager.spaceFont_normal;
            }
            else
            {
                fontUsed = UIManager.spaceFont_big;
            }


            spriteBatch.DrawString(fontUsed, mainMenuCenterText, new Vector2(UIManager.mainMenu_creditsMenu.pos.X + UIManager.mainMenu_creditsMenu.texture.Width / 2 - fontUsed.MeasureString(mainMenuCenterText).X / 2, UIManager.mainMenu_creditsMenu.pos.Y + UIManager.mainMenu_creditsMenu.texture.Height / 2 - fontUsed.MeasureString(mainMenuCenterText).Y / 2), Color.LightBlue);

            spriteBatch.End();
        }

        public static void update(MouseState mouseState, GameTime gameTime)
        {
            currentGameTime = gameTime;

            for (int i = 0; i < menuButtons.Count; i++)
            {
                menuButtons[i].updateButton(mouseState);
            }

            UIManager.lastMouseState = mouseState;
        }


        public static void openMainMenu()
        {

            mainMenuActive = true;
            foreach (UIObject obj in menuObjects)
            {
                obj.active = true;
            }

            if (UIManager.mainMenu_creditsMenu != null)
            {
                UIManager.mainMenu_creditsMenu.active = false;
            }

        }

        public static void closeMainMenu()
        {
            mainMenuActive = false;
            foreach (UIObject obj in menuObjects)
            {
                obj.active = false;
            }
        }

        public static void clickOnCredits()
        {
            UIManager.mainMenu_creditsMenu.active = !UIManager.mainMenu_creditsMenu.active;

            if (UIManager.mainMenu_creditsMenu.active)
            {
                mainMenuCenterText = "- Kamerabewegung mit WASD \n oder rechter Maustaste. \n\n - Leertaste: Sprung zum Startplaneten \n\n - Planeten selektieren mit Linksklick, \nPlaneten verbinden mit Drag and Drop. \n\n - Verbesserungen bauen mit Linksklick auf \n freien Bauplatz im Planetenmenü. \n\n - 'Ausgehend'-Boni sind jene Boni, welche \n andere Planeten, die mit diesem \n Planeten verbunden sind, geben. \n\n - Sobald du 100, 1000 oder 10.000 von einer \n Ressource auf einem Planeten produzierst, \n bekommt dieser Planet eine Monopoledge. \n Verbinde zwei Monopolplaneten mit einer \n Monopoledge, um starke Boni zu erhalten \n\n Planode ist entstanden in Zusammenarbeit mit \n Acagamics";
            }
            else
            {
                mainMenuCenterText = "Steuerung";
            }
        }

        public static void clickOnQuit()
        {
            exit = true;        //wird dann in Game1 geschlossen -> wird sowieso mit Polling gemacht, finde die Implementierung von Monogame da aber doof, ist aber so 
        }

        public static void clickOnNewGame()
        {
            closeMainMenu();
            initGame();

        }

        public static void initGame()
        {
            Game1.mainCamera = new Camera(MapManager.getPlanets(), graphics, content);
            GameManager.init();
        }


        public static void hoverOffMainManuButtons()
        {
            if (UIManager.mainMenu_creditsMenu.active)
            {
                UIManager.mainMenu_creditsMenu.active = false;

            }

            mainMenuCenterText = "Planode";
        }

        public static void hoverOnNewGame()
        {
            mainMenuCenterText = "Neues Spiel";
        }

        public static void hoverOnQuit()
        {
            mainMenuCenterText = "Spiel verlassen";
        }

        public static void hoverOnCredits()
        {
            mainMenuCenterText = "Steuerung";
        }
        #

        */
    }
}
