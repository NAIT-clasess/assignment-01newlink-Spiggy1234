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

    //private SpriteFont _arial;
    //private string _output = "This is the string I want to output";

    private SimpleAnimation _walkingAnimation;

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
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _walkingAnimation.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(_spaceStation, Vector2.Zero, Color.White);
        // static sprite
        _spriteBatch.Draw(_ship, new Vector2(300, 140), Color.White);

        // text
        //_spriteBatch.DrawString(_arial, _output, new Vector2(20, 20), Color.White);

        // animation
        _walkingAnimation.Draw(_spriteBatch, new Vector2(100, 200), SpriteEffects.None);

        _spriteBatch.End();

        base.Draw(gameTime);

    }
}
