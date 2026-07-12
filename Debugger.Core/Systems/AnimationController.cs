namespace Debugger.Core.Systems;

public class AnimationController
{
    private Dictionary<string, Animation> _animations = new();

    public Animation? CurrentAnimation { get; private set; }

    public int CurrentFrameIndex { get; private set; }
    private float _frameTimer;

    private bool _isFinished;
    

    public void AddAnimation(Animation animation)
    {
        _animations[animation.Name] = animation;
    }

    public void Play(string name)
    {
        if (CurrentAnimation?.Name == name) return;

        if (_animations.TryGetValue(name, out var animation))
        {
            Console.WriteLine($"Swithing to animation: {name}");
            CurrentAnimation = animation;
            Restart();
        }
    }

    public void Restart()
    {
        CurrentFrameIndex = CurrentAnimation?.StartFrame ?? 0;
        _frameTimer = 0f;
        _isFinished = false;
    }
    
    public void Stop()
    {
        _isFinished = true;
    }

    public void Update(float dt)
    {
        if (CurrentAnimation == null || _isFinished) return;

        _frameTimer += dt;
        if (_frameTimer >= CurrentAnimation.FrameDuration)
        {
            _frameTimer -= CurrentAnimation.FrameDuration;
            CurrentFrameIndex++;

            int endFrame = CurrentAnimation.StartFrame + CurrentAnimation.FrameCount;
            if (CurrentFrameIndex >= endFrame)
            {
                if (CurrentAnimation.IsLooping)
                {
                    CurrentFrameIndex = CurrentAnimation.StartFrame;
                }
                else
                {
                    CurrentFrameIndex = endFrame - 1;
                    _isFinished = true;
                }
            }
        }

    }
}

public class Animation
{

    public string Name { get; }
    public string TextureKey {get;}
    public int StartFrame { get; }
    public int FrameCount { get; }
    public float FrameDuration { get; }
    public bool IsLooping { get; }

    public bool FlipHorizontal {get;set;}

    public Animation(string name, string textureKey, int startFrame, int frameCount, float frameDuration, bool isLooping = false, bool flipHorizontal = false)
    {
        Name = name;
        TextureKey = textureKey;
        StartFrame = startFrame;
        FrameCount = frameCount;
        FrameDuration = frameDuration;
        
        IsLooping = isLooping;
        FlipHorizontal = flipHorizontal;
    }
}