using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimpleAnimationNamespace;

namespace Assignment_01;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _Beans;
    private Texture2D _kyurem;
    private Texture2D _latias;
    private string _message = "Kyurem Gaming";
    private Color BGColor;
    private KeyboardState _kbPreviousState;

    Vector2 _PlayersInput;

    private SpriteFont _arial;
    private string _output = "Kyurem can move";

    private SimpleAnimation _walkingAnimation;
    private SimpleAnimation _walkingPartTwo;

    // Moving
    private Vector2 _position;
    private Vector2 _dimensions;

    private float _speed;
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

        // Moving
        _position = new Vector2(60f, 80f);
        _dimensions = new Vector2(100f, 100f);

        _speed = 1f;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _Beans = Content.Load<Texture2D>("Giant_Chasm_Cave");
        _kyurem = Content.Load<Texture2D>("Kyurem");
        _latias = Content.Load<Texture2D>("latias");

        _arial = Content.Load<SpriteFont>("SystemArialFont");

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

        // Moving
        //_spriteBatch = new SpriteBatch(GraphicsDevice);

        //_pixel = new Texture2D(GraphicsDevice, 1, 1);
        //_pixel.SetData(new[] { Color.White });

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
        KyuremLocation+= _PlayersInput*10;
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

        Move(gameTime);
        base.Update(gameTime);
    }
    Vector2 KyuremLocation =  new Vector2(300, 140);
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        Rectangle screenRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        _spriteBatch.Draw(_Beans, screenRectangle, Color.White);
        // static sprite
        _spriteBatch.Draw(_kyurem,KyuremLocation, Color.White);

        //_spriteBatch.Draw(_latias, new Vector2(150, 150), Color.White);

        // text
        _spriteBatch.DrawString(_arial, _output, new Vector2(230, 250), Color.DarkRed);

        // animation
        _walkingAnimation.Draw(_spriteBatch, new Vector2(100, 200), SpriteEffects.None);

        _walkingPartTwo.Draw(_spriteBatch, new Vector2(385, 100), SpriteEffects.None);


        // Moving

        Rectangle rect = new Rectangle(
            (int)_position.X,
            (int)_position.Y,
            (int)_dimensions.Y,
            (int)_dimensions.X
        );

        _spriteBatch.Draw(_latias, rect, Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);

    }

    private void Move(GameTime gameTime)
    {
        float seconds = (float)gameTime.TotalGameTime.TotalSeconds;
        _position.X += _speed * seconds;

        base.Update(gameTime);

    }
}
