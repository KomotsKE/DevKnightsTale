using System.Diagnostics;
using System.Text;

namespace KnightsTale.Sprites.UserInterface;

public class DialogBox
{
    public SpriteFont Font;
    public string Text { get; set; }

    /// <summary>
    /// Bool that determines active state of this dialog box
    /// </summary>
    public bool Active { get; private set; }

    /// <summary>
    /// X,Y coordinates of this dialog box
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Width and Height of this dialog box
    /// </summary>
    public Vector2 Size { get; set; }

    /// <summary>
    /// Color used to fill dialog box background
    /// </summary>
    public Color FillColor { get; set; }

    /// <summary>
    /// Color used for border around dialog box
    /// </summary>
    public Color BorderColor { get; set; }

    /// <summary>
    /// Color used for text in dialog box
    /// </summary>
    public Color DialogColor { get; set; }

    /// <summary>
    /// Thickness of border
    /// </summary>
    public int BorderWidth { get; set; }

    /// <summary>
    /// Background fill texture (built from FillColor)
    /// </summary>
    private readonly Texture2D _fillTexture;

    /// <summary>
    /// Border fill texture (built from BorderColor)
    /// </summary>
    private readonly Texture2D _borderTexture;

    /// <summary>
    /// Collection of pages contained in this dialog box
    /// </summary>
    private List<string> _pages;

    /// <summary>
    /// Margin surrounding the text inside the dialog box
    /// </summary>
    private const float DialogBoxMargin = 24f;

    /// <summary>
    /// Size (in pixels) of a wide alphabet letter (W is the widest letter in almost every font) 
    /// </summary>
    private Vector2 _characterSize = Globals.Content.Load<SpriteFont>("Fonts/8-bit").MeasureString(new StringBuilder("W", 1));

    /// <summary>
    /// The amount of characters allowed on a given line
    /// NOTE: If you want to use a font that is not monospaced, this will need to be reevaluated
    /// </summary>
    private int MaxCharsPerLine => (int)Math.Floor((Size.X - DialogBoxMargin) / _characterSize.X * 1.4);

    /// <summary>
    /// Determine the maximum amount of lines allowed per page
    /// NOTE: This will change automatically with font size
    /// </summary>
    private int MaxLines => (int)Math.Floor((Size.Y - DialogBoxMargin) / _characterSize.Y) - 1;

    /// <summary>
    /// The index of the current page
    /// </summary>
    private int _currentPage;

    private int _interval;

    private Rectangle TextRectangle => new(Position.ToPoint(), Size.ToPoint());

    /// <summary>
    /// The position and size of the bordering sides on the edges of the dialog box
    /// </summary>
    private List<Rectangle> BorderRectangles => new()
    {
        // Top (contains top-left & top-right corners)
        new Rectangle(TextRectangle.X - BorderWidth, TextRectangle.Y - BorderWidth,
            TextRectangle.Width + BorderWidth*2, BorderWidth),

        // Right
        new Rectangle(TextRectangle.X + TextRectangle.Size.X, TextRectangle.Y, BorderWidth, TextRectangle.Height),

        // Bottom (contains bottom-left & bottom-right corners)
        new Rectangle(TextRectangle.X - BorderWidth, TextRectangle.Y + TextRectangle.Size.Y,
            TextRectangle.Width + BorderWidth*2, BorderWidth),

        // Left
        new Rectangle(TextRectangle.X - BorderWidth, TextRectangle.Y, BorderWidth, TextRectangle.Height)
    };

    private Vector2 StartTextPosition => new(Position.X + DialogBoxMargin / 2, Position.Y + DialogBoxMargin / 2);

    private Stopwatch _stopwatch;

    public DialogBox()
    {
        BorderWidth = 5;
        DialogColor = Color.Black;
        Font = Globals.Content.Load<SpriteFont>("Fonts/8-bit");

        FillColor = new Color(231, 201, 134);

        BorderColor = new Color(165, 65, 0);

        _fillTexture = new Texture2D(Globals.SpriteBatch.GraphicsDevice, 1, 1);
        _fillTexture.SetData(new[] { FillColor });

        _borderTexture = new Texture2D(Globals.SpriteBatch.GraphicsDevice, 1, 1);
        _borderTexture.SetData(new[] { BorderColor });

        _pages = new List<string>();
        _currentPage = 0;

        var sizeX = (int)(Globals.ScreenWidth * 0.5);
        var sizeY = (int)(Globals.ScreenHeight * 0.2);

        Size = new Vector2(sizeX, sizeY);

        var posX = Globals.ScreenWidth/2 - (Size.X / 2f);
        var posY = Globals.ScreenHeight - Size.Y - 30;

        Position = new Vector2(posX, posY);
    }

    public void Initialize(string text = null)
    {
        Text = text ?? Text;

        _currentPage = 0;

        Show();
    }

    public void Show()
    {
        Active = true;

        _stopwatch = new Stopwatch();

        _stopwatch.Start();

        _pages = WordWrap(Text);
    }

    public void Hide()
    {
        Active = false;

        _stopwatch.Stop();

        _stopwatch = null;
    }

    public void Update()
    {
        if (Active)
        {
            if (Globals.MyKeyboard.GetSinglePress("Space"))
            {
                if (_currentPage >= _pages.Count - 1)
                {
                    Hide();
                }
                else
                {
                    _currentPage++;
                    _stopwatch.Restart();
                }
            }

            if (Globals.MyKeyboard.GetPress("X"))
            {
                Hide();
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Active)
        {
            foreach (var side in BorderRectangles)
            {
                spriteBatch.Draw(_borderTexture, side,BorderColor);
            }

            spriteBatch.Draw(_fillTexture,TextRectangle, null,FillColor,0f,Vector2.Zero,SpriteEffects.None,1);

            spriteBatch.DrawString(Font, _pages[_currentPage], StartTextPosition, DialogColor, 0f, Vector2.Zero,1f,SpriteEffects.None, 1);

            if (BlinkIndicator() || _currentPage == _pages.Count - 1)
            {
                var indicatorPosition = new Vector2(TextRectangle.X + TextRectangle.Width - (_characterSize.X) - 4,
                    TextRectangle.Y + TextRectangle.Height - (_characterSize.Y));

                spriteBatch.DrawString(Font, ">", indicatorPosition, Color.Red,0f,Vector2.Zero,1f,SpriteEffects.None,1);
            }
        }
    }

    private bool BlinkIndicator()
    {
        _interval = (int)Math.Floor((double)(_stopwatch.ElapsedMilliseconds % 1000));

        return _interval < 500;
    }

    private List<string> WordWrap(string text)
    {
        var pages = new List<string>();

        var capacity = MaxCharsPerLine * MaxLines > text.Length ? text.Length : MaxCharsPerLine * MaxLines;

        var result = new StringBuilder(capacity);
        var resultLines = 0;

        var currentWord = new StringBuilder();
        var currentLine = new StringBuilder();

        for (var i = 0; i < text.Length; i++)
        {
            var currentChar = text[i];
            var isNewLine = text[i] == '\n';
            var isLastChar = i == text.Length - 1;

            currentWord.Append(currentChar);

            if (char.IsWhiteSpace(currentChar) || isLastChar)
            {
                var potentialLength = currentLine.Length + currentWord.Length;

                if (potentialLength > MaxCharsPerLine)
                {
                    result.AppendLine(currentLine.ToString());

                    currentLine.Clear();

                    resultLines++;
                }

                currentLine.Append(currentWord);

                currentWord.Clear();

                if (isLastChar || isNewLine)
                {
                    result.AppendLine(currentLine.ToString());
                }

                if (resultLines > MaxLines || isLastChar || isNewLine)
                {
                    pages.Add(result.ToString());

                    result.Clear();

                    resultLines = 0;

                    if (isNewLine)
                    {
                        currentLine.Clear();
                    }
                }
            }
        }

        return pages;
    }
}