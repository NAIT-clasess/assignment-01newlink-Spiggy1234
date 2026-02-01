using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimpleAnimationNamespace;

namespace Assignment_01;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _spaceStation;
    private Texture2D _ship;
    private string _message = "Hi. It's unseasonably warm these days.";
    private Color BGColor;
    private KeyboardState _kbPreviousState;

    Vector2 _PlayersInput;

    //private SpriteFont _arial;
    //private string _output = "This is the string I want to output";

    private SimpleAnimation _walkingAnimation;
    private SimpleAnimation _walkingPartTwo;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.PreferredBackBufferHeight = 320;
        _graphics.ApplyChanges();
        // Changes the size of the game window/Running project window 

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _spaceStation = Content.Load<Texture2D>("Beans");
        _ship = Content.Load<Texture2D>("Kyurem");

        //_arial = Content.Load<SpriteFont>("SystemArialFont");

        _walkingAnimation = new SimpleAnimation(
            Content.Load<Texture2D>("Walkingnew"),
            81,
            100,
            4,
            6
        );

        _walkingPartTwo = new SimpleAnimation(
            Content.Load<Texture2D>("Bomb"),
            38,
            144,
            5,
            10
        );
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            Debug.WriteLine("Yes");
        }
        // TODO: Add your update logic here
        _walkingAnimation.Update(gameTime);
        _walkingPartTwo.Update(gameTime);

        // Inputs

        KeyboardState kbCurrentState = Keyboard.GetState();
        _message = "";
        _PlayersInput = Vector2.Zero;
        #region arrow keys
        if (kbCurrentState.IsKeyDown(Keys.Down))
        {
            _PlayersInput += new Vector2(0,1);
            _message += "Down ";
        }
        if (kbCurrentState.IsKeyDown(Keys.Up))
        {
            _PlayersInput += new Vector2(0,-1);

            _message += "Up ";
        }
        if (kbCurrentState.IsKeyDown(Keys.Left))
        {
            _PlayersInput += new Vector2(-1,0);

            _message += "Left ";
        }
        if (kbCurrentState.IsKeyDown(Keys.Right))
        {
            _PlayersInput += new Vector2(1,0);

            _message += "Right ";
        }
        #endregion
        ShipLocation+= _PlayersInput*10;
        #region space states
        if (_kbPreviousState.IsKeyUp(Keys.Space) && kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "\n";
            _message += "Space pressed\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
        }
        else if (kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "\n";
            _message += "Space held";
        }
        else if (_kbPreviousState.IsKeyDown(Keys.Space))
        {
            _message += "\n";
            _message += "Space released\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
        }
        #endregion*/

        _kbPreviousState = kbCurrentState;

        BGColor= Color.White;

         if (kbCurrentState.IsKeyDown(Keys.B))
        {
        BGColor= Color.Blue;
        }

        if (kbCurrentState.IsKeyDown(Keys.R))
        {
        BGColor= Color.Red;
        }
        if (kbCurrentState.IsKeyDown(Keys.G))
        {
        BGColor= Color.Green;
        }

        base.Update(gameTime);
    }
    Vector2 ShipLocation =  new Vector2(300, 140);
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(_spaceStation, Vector2.Zero, Color.White);
        // static sprite
        _spriteBatch.Draw(_ship,ShipLocation, Color.White);

        // text
        //_spriteBatch.DrawString(_arial, _output, new Vector2(20, 20), Color.White);

        // animation
        _walkingAnimation.Draw(_spriteBatch, new Vector2(100, 200), SpriteEffects.None);

        _walkingPartTwo.Draw(_spriteBatch, new Vector2(385, 100), SpriteEffects.None);

        _spriteBatch.End();

        base.Draw(gameTime);

    }
}
