namespace api.Utility.Validation
{
    public partial class ValidationResult
    {
        public List<string> MessageList { get; set; } = new List<string>();
        public List<object> ObjectList { get; set; } = new List<object>();
        public DateTime DateCreated { get; set; }
        public bool IsValid => MessageList.Count == 0;
    }

    public partial class ValidationResult
    {
        public ValidationResult() {  }
        public ValidationResult(object obj)
        {
            AddObject(obj);
        }

        public ValidationResult(string msg)
        {
            AddMessage(msg);
        }

        public void AddMessage(string msg)
        {
            MessageList.Add(msg);
        }

        public void AddMessageList(List<string> msgList)
        {
            MessageList.AddRange(msgList);
        }

        public void AddObject(object obj)
        {
            ObjectList.Add(obj);
        }

        public void AddObjectList(List<object> objList)
        {
            ObjectList.AddRange(objList);
        }
    }
}