
namespace SmartSpiderCore.In
{
    public class ForEnumeratorRule : EnumeratorRule
    {
        private int _start;
        private int _end;
        private int _step;
        private bool _direction = true;
        private bool _isFirst = true;

        public string Feature { get; set; }

        public int Start { get; set; }

        public int End { get; set; }

        public int Step { get; set; }

        public bool Direction { get; set; }

        public override bool MoveNext()
        {
            if (Direction)
            {
                if (_isFirst)
                {
                    _isFirst = false;
                    return Start <= End;
                }
                else
                {
                    Start += Step;
                }

                return Start <= End;
            }

            if (_isFirst)
            {
                _isFirst = false;
                return Start >= End;
            }
            Start -= Step;
            return Start >= End;
        }

        public override void Reset()
        {
            Start = _start;
            End = _end;
            Step = _step;
            Direction = _direction;
        }

        public override string Current
        {
            get
            {
                return Start.ToString();
            }
        }

        public void Init()
        {
            _start = Start;
            _end = End;
            _step = Step;
            _direction = Direction;
        }

        public override Content Exec(Content content)
        {
            content.ContentText = content.ContentText.Replace(Feature, Start.ToString());

            return content;
        }
    }
}
