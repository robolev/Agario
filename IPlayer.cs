using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Agario
{
    public interface IPlayer : IDrawable, IUpdatable
    {
        CircleShape circle { get; }
    }
}
