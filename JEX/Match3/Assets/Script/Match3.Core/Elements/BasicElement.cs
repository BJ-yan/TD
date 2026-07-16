using Match3.Core.Board;
    
namespace Match3.Core.Elements
{
    public class ColorElement : ElementBase
    {
        private readonly ElementType _elementType;

        public BasicElement(ElementType elementType)
        {
            _elementType = elementType;
        }
        
        public override ElementType Type => _elementType;
    }
}