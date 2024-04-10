using System;


namespace PassMen.Desktop
{
    public class DataEventArgs : EventArgs
    {
        public object Data { get; set; }
        public DataEventArgs(object data) => Data = data;
    }
}
