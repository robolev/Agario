using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Agario
{
    public interface IPlayer : IDrawable, IUpdatable
    {
        public Blob blob { get; set; }
    }
}
