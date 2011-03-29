using System;

namespace developwithpassion.specifications
{
    public class SetupTearDownPair
    {
        Action context;
        Action tear_down;

        public SetupTearDownPair(Action context, Action tear_down)
        {
            this.context = context;
            this.tear_down = tear_down;
        }

        public void finish()
        {
            this.tear_down();
        }

        public void start()
        {
            this.context();
        }
    }
}