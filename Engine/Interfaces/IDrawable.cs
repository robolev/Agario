using SFML.Graphics;

namespace Agario;

   public interface IDrawable
   {
      public void Draw(RenderTarget target);

      public int ZIndex { get; set; }
   }


