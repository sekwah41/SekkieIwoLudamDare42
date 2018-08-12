namespace Game.Utils
{
    public class TrackableValue<T>
    {
        public delegate void OnChangedDelegate(T trackableValue);

        protected T m_value;
        protected OnChangedDelegate onChangedHandlers;

        public T Value
        {
            get { return m_value; }
            set
            {
                m_value = value;
                if(onChangedHandlers != null)
                    onChangedHandlers(m_value);
            }
        }

        public void RegisterOnChangedHandler(OnChangedDelegate del)
        {
            onChangedHandlers += del;
        }
    }
}